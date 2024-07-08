using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketLib
{
    public interface IPacketReader 
    {
        Int32 ReadInt();
        Single ReadSingle();
        String ReadString();
        void Deserialize(Byte[] pBytes,Int32 pSize);
        void Clean();
    }
}
