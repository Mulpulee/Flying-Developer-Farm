using SocketLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IPacketListener<T> where T : IPacket
{
    void OnPacket(T pPacket);
}

