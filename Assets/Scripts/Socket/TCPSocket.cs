using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketLib
{
    public class TCPSocket
    {
        public static readonly Int32 TCP_MAX_PACKET_SIZE = 1024;

        private Socket m_socket;
        private Buffer m_buffer;

        private Buffer m_storeBuffer;
        private PacketHandler m_handler;
        private Boolean m_isDisconnected;

        private Int32 m_recvOffset;
        private Int32 m_readOffset;

        public Boolean IsDisConnected => m_isDisconnected;

        public TCPSocket(PacketHandler pHandler)
        {
            m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            m_buffer = new Buffer(TCP_MAX_PACKET_SIZE * TCP_MAX_PACKET_SIZE);
            m_storeBuffer = new Buffer(TCP_MAX_PACKET_SIZE * TCP_MAX_PACKET_SIZE);
            m_handler = pHandler;
        }

        public TCPSocket(Socket pSocket, PacketHandler pHandler)
        {
            m_socket = pSocket;
            m_buffer = new Buffer(TCP_MAX_PACKET_SIZE * TCP_MAX_PACKET_SIZE);
            m_storeBuffer = new Buffer(TCP_MAX_PACKET_SIZE * TCP_MAX_PACKET_SIZE);
            m_handler = pHandler;
            ReceiveAsync();
        }


        public Boolean Bind(Int32 pPort)
        {
            try
            {
                m_socket.Bind(new IPEndPoint(IPAddress.Any, pPort));
                return true;
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return false;
            }
        }

        public async Task<Boolean> ConnectAsync(IPEndPoint pPoint)
        {
            try
            {
                await m_socket.ConnectAsync(pPoint);
                ReceiveAsync();
                return true;
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return false;
            }
        }

        public async Task<Boolean> SendAsync(Byte[] pBytes)
        {
            try
            {
                int sendSize = await m_socket.SendAsync(pBytes, SocketFlags.None);
                if (sendSize < 0)
                {
                    HandleError(SocketError.SendBelowZero);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                HandleException(ex);
                HandleError(SocketError.SendFailed);
                return false;
            }
        }



        public async void ReceiveAsync()
        {
            try
            {
                int recvSize = await m_socket.ReceiveAsync(m_buffer.Array, SocketFlags.None);
                if (recvSize < 0)
                {
                    HandleError(SocketError.ReceiveBelowZero);
                    return;
                }

                Array.Copy(m_buffer.Array, 0, m_storeBuffer.Array, m_recvOffset, recvSize);
                m_recvOffset += recvSize;

                if (recvSize < sizeof(Int32) * 2)
                {
                    return;
                }

                while (m_readOffset + sizeof(Int32) < m_recvOffset)
                {
                    Int32 packetSize = BitConverter.ToInt32(m_storeBuffer.Array, m_readOffset);

                    if (m_recvOffset - m_readOffset < packetSize)
                        break;

                    m_readOffset += 4;
                    m_handler.PushPacket(m_storeBuffer, m_readOffset + 4, packetSize);

                    Array.Copy(m_storeBuffer.Array, m_readOffset + packetSize, m_storeBuffer.Array, 0, m_recvOffset - (m_readOffset + packetSize));
                    m_recvOffset = m_recvOffset - (m_readOffset + packetSize);
                    m_readOffset = 0;
                }

                ReceiveAsync();
            }
            catch (Exception ex)
            {
                HandleException(ex);
                HandleError(SocketError.ReceiveFailed);
            }
        }

        public void Disconnect()
        {
            if (m_socket == null)
                return;

            m_socket?.Disconnect(false);
            m_socket?.Close();
            m_socket?.Dispose();
            m_socket = null;
            m_isDisconnected = true;
        }

        private void HandleException(Exception pEx)
        {
            Logger.Error(pEx);
        }

        private void HandleError(SocketError pError)
        {
            Logger.Error(pError);
            Disconnect();
        }
    }
}
