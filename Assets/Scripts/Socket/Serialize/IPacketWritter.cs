using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketLib
{
    public interface IPacketWritter
    {
        void WriteInt(Int32 pValue);
        void WriteString(String pValue);
        void WriteSingle(Single pValue);
        Byte[] Serialize();
    }
}
