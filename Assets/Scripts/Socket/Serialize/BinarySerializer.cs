using SocketLib;
using System;
using System.Buffers;
using System.Text;

namespace SocketLib
{
    public class BinarySerializeReader : IPacketReader
    {
        private Byte[] m_buffer;
        private Int32 m_offset;

        public void Deserialize(byte[] pValue, Int32 pSize)
        {
            m_buffer = ArrayPool<Byte>.Shared.Rent(pSize);
            Array.Copy(pValue, m_buffer, pSize);
            m_offset = 0;
        }

        public int ReadInt()
        {
            Int32 value = BitConverter.ToInt32(m_buffer, m_offset);
            m_offset += sizeof(Int32);
            return value;
        }

        public string ReadString()
        {
            Int32 length = ReadInt();
            String value = BitConverter.ToString(m_buffer, m_offset, length);
            m_offset += length;
            return value;
        }

        public float ReadSingle()
        {
            Single value = BitConverter.ToSingle(m_buffer, m_offset);
            m_offset += sizeof(Single);
            return value;
        }

        public void Clean()
        {
            ArrayPool<Byte>.Shared.Return(m_buffer);
        }
    }

    public class BinarySerializeWritter : IPacketWritter
    {
        public static readonly Int32 DEFAULT_BUFFER_LENGTH = 2048;
        private readonly Int32 m_size;
        private Byte[] m_buffer;
        private Int32 m_offset;
        private Boolean m_enabled;


        public BinarySerializeWritter()
        {
            m_size = DEFAULT_BUFFER_LENGTH;
            m_buffer = System.Buffers.ArrayPool<Byte>.Shared.Rent(DEFAULT_BUFFER_LENGTH);
            m_enabled = true;
        }

        public unsafe void WriteInt(int pValue)
        {
            if (!m_enabled)
                throw new ObjectDisposedException("ByteSerializeWritter");

            if (m_size < m_offset + sizeof(Int32))
                throw new IndexOutOfRangeException($"offset is currently {m_offset}");

            fixed (void* array = &m_buffer[m_offset])
            {
                *(Int32*)array = pValue;
            }

            m_offset += sizeof(Int32);
        }

        public void WriteString(string pValue)
        {
            if (!m_enabled)
                throw new ObjectDisposedException("ByteSerializeWritter");

            Byte[] stringArray = Encoding.UTF8.GetBytes(pValue);
            Int32 length = stringArray.Length;

            WriteInt(length);

            if (m_size < m_offset + length)
                throw new IndexOutOfRangeException($"offset is currently {m_offset}");

            Array.Copy(stringArray, 0, m_buffer, m_offset, stringArray.Length);

            m_offset += length;
        }

        public unsafe void WriteSingle(float pValue)
        {
            if (!m_enabled)
                throw new ObjectDisposedException("ByteSerializeWritter");

            if (m_size < m_offset + sizeof(Single))
                throw new IndexOutOfRangeException($"offset is currently {m_offset}");

            fixed (void* array = &m_buffer[m_offset])
            {
                *(float*)array = pValue;
            }

            m_offset += sizeof(float);
        }

        public Byte[] Serialize()
        {
            Byte[] result = new Byte[m_offset];
            Array.Copy(m_buffer, result, m_offset);
            m_enabled = false;
            ArrayPool<Byte>.Shared.Return(m_buffer, true);
            m_buffer = null;

            return result;
        }
    }
}

