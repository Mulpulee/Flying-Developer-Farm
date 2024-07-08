using SocketLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketController : MonoBehaviour
{
    private GameClient m_client;
    public string Name { get; set; } = nameof(PacketController);
    public Vector2 Position { get; set; }

    public PacketController(GameClient pClient)
    {
        m_client = pClient;
    }

    private void Update()
    {
        while (m_client.Handler.TryGetPacket(out IPacket packet))
        {
            PacketEventController.Instance.OnPacket(packet);
        }
    }
}
