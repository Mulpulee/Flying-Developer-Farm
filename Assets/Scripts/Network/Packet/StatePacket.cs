using SocketLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePacket : IPacket
{
    public int PacketType => (int)PacketTypes.State;

    public InGameState GameState { get; set; }
    public int Turn { get; set; }

    public StatePacket() { }

    public StatePacket(InGameState pState, int pTurn)
    {
        GameState = pState;
        Turn = pTurn;
    }

    public void Deserialize(IPacketReader pReader)
    {
        GameState = (InGameState)pReader.ReadInt();
        Turn = pReader.ReadInt();
    }

    public void Serialize(IPacketWritter pWritter)
    {
        pWritter.WriteInt(PacketType);
        pWritter.WriteInt((int)GameState);
        pWritter.WriteInt(Turn);
    }
}
