using SocketLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.SceneManagement;

public enum InGameState
{
    Start,
    Battle,
    Card,
    Item,
    End
}

public class GameManagerEx : MonoBehaviour, IPacketListener<StatePacket>
{
    private static GameManagerEx instance;
    public static GameManagerEx Instance { get { return instance; } }

    public bool IsHost { get; private set; }

    private static InGameState m_state;
    private PacketHandler handler;
    private GameClient client;
    private bool isMyTurn;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this) Destroy(gameObject);
        }
    }

    private void Start()
    {
        Dependency<IPacketReader>.Assign(() => new BinarySerializeReader());
        Dependency<IPacketWritter>.Assign(() => new BinarySerializeWritter());

        handler = new PacketHandler();
        handler.AssignPacket<BulletPacket>((int)PacketTypes.Bullet);
        handler.AssignPacket<PlayerPacket>((int)PacketTypes.Player);
        handler.AssignPacket<SelectPacket>((int)PacketTypes.Select);
        handler.AssignPacket<SkillPacket>((int)PacketTypes.Skill);
        handler.AssignPacket<StatePacket>((int)PacketTypes.State);
    }

    public void CreateGame()
    {
        AsyncCreateGame();
    }

    private async void AsyncCreateGame()
    {
        try
        {
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(new IPEndPoint(IPAddress.Any, 5000));
            listener.Listen(10);

            Debug.Log("방 열기 성공");
            Debug.Log("접속을 기다리는중.");

            Socket socket = await listener.AcceptAsync();
            listener.Close();
            listener.Dispose();

            client = new GameClient(socket, handler);
            OpenGame(client, true);
            IsHost = true;
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
            Debug.Log("방 열기 실패.");
            return;
        }
    }

    public void JoinGame(string input)
    {
        AsyncJoinGame(input);
    }

    private async void AsyncJoinGame(string input)
    {
        Debug.Log(input);
        client = new GameClient(handler);
        //Boolean result = await client.Socket.ConnectAsync(IPEndPoint.Parse($"{input}:5000"));
        Boolean result = await client.Socket.ConnectAsync(new IPEndPoint(IPAddress.Parse(input), 5000));

        if (!result)
        {
            Debug.Log("방 접속 실패.");
            client.Socket.Disconnect();
            return;
        }
        Debug.Log("방 접속");
        OpenGame(client, false);
        IsHost = false;
    }

    private void OpenGame(GameClient pClient, Boolean pIshost)
    {
        PacketController.Instance.Init(pClient);
        PacketEvent<StatePacket>.Instance.Assign(this);

        m_state = InGameState.Start;
        isMyTurn = pIshost;
        StartCoroutine(InGame());
    }

    private IEnumerator LoadSceneRoutine(string pScene, Action pCallback)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(pScene);

        yield return new WaitUntil(() => async.isDone);

        pCallback.Invoke();
    }

    private IEnumerator InGame()
    {
        yield return null;

        switch (m_state)
        {
            case InGameState.Start:
                StartCoroutine(LoadSceneRoutine("SelectScene", () =>
                {
                    FindObjectOfType<SelectManager>().Init(client, InGameState.Start, isMyTurn);
                }
                ));
                break;
            case InGameState.Battle:
                StartCoroutine(LoadSceneRoutine("SampleScene", () =>
                {
                    FindObjectOfType<GunManager>().Init(client, IsHost);
                    FindObjectOfType<PlayerController>().Init(client);
                }
                ));
                break;
            case InGameState.Card:
                StartCoroutine(LoadSceneRoutine("SelectScene", () =>
                {
                    FindObjectOfType<SelectManager>().Init(client, InGameState.Card, isMyTurn);
                }
                ));
                break;
            case InGameState.Item:
                StartCoroutine(LoadSceneRoutine("SelectScene", () =>
                {
                    FindObjectOfType<SelectManager>().Init(client, InGameState.Item, isMyTurn);
                }
                ));
                break;
            case InGameState.End:
                break;
            default:
                break;
        }
    }

    public void ChangeState(InGameState pState, bool pIsMyTurn)
    {
        m_state = pState;
        isMyTurn = pIsMyTurn;
        StartCoroutine(InGame());

        client.Send(new StatePacket(pState, isMyTurn ? 0 : 1));
    }

    public void OnPacket(StatePacket pPacket)
    {
        m_state = pPacket.GameState;
        isMyTurn = pPacket.Turn == 1 ? true : false;
        StartCoroutine(InGame());
    }
}
