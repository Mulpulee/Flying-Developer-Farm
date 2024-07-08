using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketLib
{
    public class PacketHandler
    {
        private Dictionary<Int32, Func<IPacket>> m_packetCtor;
        private ConcurrentQueue<IPacket> m_packetQueue;

        public PacketHandler()
        {
            m_packetCtor = new Dictionary<int, Func<IPacket>>();
            m_packetQueue = new ConcurrentQueue<IPacket>();
        }

        public void AssignPacket<T>(Int32 pID) where T : IPacket,new()
        {
            m_packetCtor.Add(pID, () => new T());
        }

        public void PushPacket(Buffer pBuffer,Int32 pSize)
        {
            IPacketReader reader = Dependency<IPacketReader>.Get();
            reader.Deserialize(pBuffer.Array,pSize);

            Int32 type = reader.ReadInt();
            if(m_packetCtor.TryGetValue(type,out var ctor))
            {
                IPacket packet = ctor.Invoke();
                packet.Deserialize(reader);
                m_packetQueue.Enqueue(packet);
                reader.Clean();
            }
            else
            {
                Logger.Error($"packet type [{type}] is not assigned");
            }
        }

        public Boolean TryGetPacket(out IPacket packet)
        {
            return m_packetQueue.TryDequeue(out packet);
        }
    }
}
