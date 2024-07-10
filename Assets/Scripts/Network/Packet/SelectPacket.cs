using SocketLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SelectPacketTypes
{
    List = 0,
    Move = 1,
    Submit = 2
}

public class SelectPacket : IPacket
{
    public int PacketType => (int)PacketTypes.Select;

    public SelectPacketTypes Type { get; set; }
    public int Index { get; set; }
    public int[] Array { get; set; }

    public SelectPacket()
    {
        Array = new int[5];
    }

    public SelectPacket(SelectPacketTypes pType, int pID, int[] pArray)
    {
        Type = pType;
        Index = pID;
        Array = pArray;
    }

    public void Deserialize(IPacketReader pReader)
    {
        Type = (SelectPacketTypes)pReader.ReadInt();
        Index = pReader.ReadInt();
        for (int i = 0; i < 5; i++) Array[i] = pReader.ReadInt();
    }

    public void Serialize(IPacketWritter pWritter)
    {
        pWritter.WriteInt(PacketType);
        pWritter.WriteInt((int)Type);
        pWritter.WriteInt(Index);

        for (int i = 0; i < 5; i++)
        {
            pWritter.WriteInt(Array[i]);
        }
    }
}
