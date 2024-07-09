using SocketLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.SceneManagement;

public class GameManagerEx : MonoBehaviour
{
    private static GameManagerEx instance;
    public static GameManagerEx Instance { get { return instance; } }

    private PacketHandler handler;
    public bool IsHost { get; private set; }

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
            OpenGame(new GameClient(socket, handler), true);
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
        GameClient client = new GameClient(handler);
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
        StartCoroutine(LoadSceneRoutine("SampleScene", () =>
        {
            FindObjectOfType<GunManager>().Init(pClient, pIshost);
            FindObjectOfType<PlayerController>().Init(pClient);
        }
        ));
    }

    private IEnumerator LoadSceneRoutine(string pScene, Action pCallback)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(pScene);

        yield return new WaitUntil(() => async.isDone);

        pCallback.Invoke();
    }
}
