using SocketLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using UnityEngine;
using UnityEngine.Windows;

public class GameManagerEx : MonoBehaviour
{
    private static GameManagerEx instance;
    public static GameManagerEx Instance { get { return instance; } }

    private PacketHandler handler;

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

            Debug.Log("�� ���� ����");
            Debug.Log("������ ��ٸ�����.");

            Socket socket = await listener.AcceptAsync();
            listener.Close();
            listener.Dispose();
            OpenGame(new GameClient(socket, handler), true);
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
            Debug.Log("�� ���� ����.");
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
            Debug.Log("�� ���� ����.");
            client.Socket.Disconnect();
            return;
        }
        Debug.Log("�� ����");
        OpenGame(client, false);
    }

    private void OpenGame(GameClient pClient, Boolean pIshost)
    {
        PacketController.Instance.Init(pClient);
    }
}
