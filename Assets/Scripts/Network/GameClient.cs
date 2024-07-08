using SocketLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

public class GameClient
{
    private PacketHandler m_handler;
    private TCPSocket m_socket;

    public TCPSocket Socket => m_socket;
    public PacketHandler Handler => m_handler;

    public GameClient(PacketHandler pHandler)
    {
        m_handler = pHandler;
        m_socket = new TCPSocket(m_handler);
    }

    public GameClient(Socket pSocket,PacketHandler pHandler)
    {
        m_handler = pHandler;
        m_socket = new TCPSocket(pSocket,m_handler);
    }

    public async void Send(IPacket pPacket)
    {
        Byte[] bytes = PacketUtil.Serialize(pPacket);
        Boolean result = await m_socket.SendAsync(bytes);
    }
}

