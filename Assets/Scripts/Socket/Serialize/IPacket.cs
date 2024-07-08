using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketLib
{
    public interface IPacket
    {
        Int32 PacketType { get; }
        void Serialize(IPacketWritter pWritter);
        void Deserialize(IPacketReader pReader);
    }
}
