using SocketLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletPacketTypes
{
    Create = 0,
    Sync = 1,
    Delete = 2
}

public class BulletPacket : IPacket
{
    public int PacketType => (int)PacketTypes.Bullet;

    public BulletPacketTypes Type { get; set; }
    public int ID { get; set; }
    public Vector2 Position { get; set; }
    public float Damage { get; set; }

    public BulletPacket() { }

    public BulletPacket(BulletPacketTypes pType, int pID, Vector2 pPosition, float pDamage)
    {
        Type = pType;
        ID = pID;
        Position = pPosition;
        Damage = pDamage;
    }

    public void Deserialize(IPacketReader pReader)
    {
        Type = (BulletPacketTypes)pReader.ReadInt();
        ID = pReader.ReadInt();

        float posX, posY;
        posX = pReader.ReadSingle();
        posY = pReader.ReadSingle();
        Position = new Vector2(posX, posY);
        Damage = pReader.ReadSingle();
    }

    public void Serialize(IPacketWritter pWritter)
    {
        pWritter.WriteInt(PacketType);
        pWritter.WriteInt((int)Type);
        pWritter.WriteInt(ID);
        pWritter.WriteSingle(Position.x);
        pWritter.WriteSingle(Position.y);
        pWritter.WriteSingle(Damage);
    }
}
