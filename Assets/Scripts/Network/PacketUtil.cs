using SocketLib;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PacketUtil
{
    public static Byte[] Serialize(IPacket pPacket)
    {
        IPacketWritter writter = Dependency<IPacketWritter>.Get();
        pPacket.Serialize(writter);
        return writter.Serialize();
    }
}

public enum PacketTypes
{
    Player = 10001,
    Bullet = 10002,
    Select = 10003
}
