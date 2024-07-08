using SocketLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketController : MonoBehaviour
{
    private static PacketController instance;
    public static PacketController Instance { get { return instance; } }

    private GameClient m_client;

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

    public void Init(GameClient pClient)
    {
        m_client = pClient;
    }

    private void Update()
    {
        if (m_client == null) return;

        while (m_client.Handler.TryGetPacket(out IPacket packet))
        {
            PacketEventController.Instance.OnPacket(packet);
        }
    }
}
