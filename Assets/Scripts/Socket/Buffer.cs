using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketLib
{
    public class Buffer 
    {
        private Byte[] m_byteArray;

        public Byte[] Array => m_byteArray;
        public Int32 Size => m_byteArray.Length;

        public Buffer(Int32 pSize)
        {
            m_byteArray = new byte[pSize]; 
        }
    }
}
