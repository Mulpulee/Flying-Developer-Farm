using SocketLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerPacketTypes
{
    Sync = 0,
    Dead = 1
}

public class PlayerPacket : IPacket
{
    public int PacketType => (int)PacketTypes.Player;

    public PlayerPacketTypes Type { get; set; }
    public Vector2 Position { get; set; }
    public Quaternion Gun { get; set; }

    public float Health { get; set; }

    public PlayerPacket() { }

    public PlayerPacket(PlayerPacketTypes pType, Vector2 pPosition, Quaternion pGun, float pHealth)
    {
        Type = pType;
        Position = pPosition;
        Gun = pGun;
        Health = pHealth;
    }

    public void Deserialize(IPacketReader pReader)
    {
        Type = (PlayerPacketTypes)pReader.ReadInt();
        float posX = pReader.ReadSingle();
        float posY = pReader.ReadSingle();
        Position = new Vector2(posX, posY);

        float rotZ = pReader.ReadSingle();
        float rotW = pReader.ReadSingle();
        Gun = new Quaternion(0, 0, rotZ, rotW);

        Health = pReader.ReadSingle();
    }

    public void Serialize(IPacketWritter pWritter)
    {
        pWritter.WriteInt(PacketType);
        pWritter.WriteInt((int)Type);
        pWritter.WriteSingle(Position.x);
        pWritter.WriteSingle(Position.y);
        pWritter.WriteSingle(Gun.z);
        pWritter.WriteSingle(Gun.w);
        pWritter.WriteSingle(Health);
    }
}
