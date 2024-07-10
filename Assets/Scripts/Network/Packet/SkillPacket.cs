using SocketLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillPacketTypes
{
    Sync = 0,
    Dead = 1
}

public class SkillPacket : IPacket
{
    public int PacketType => (int)PacketTypes.Skill;

    public SkillPacketTypes Type { get; set; }

    public SkillPacket() { }

    public SkillPacket(SkillPacketTypes pType)
    {
        Type = pType;
    }

    public void Deserialize(IPacketReader pReader)
    {
        Type = (SkillPacketTypes)pReader.ReadInt();
    }

    public void Serialize(IPacketWritter pWritter)
    {
        pWritter.WriteInt(PacketType);
        pWritter.WriteInt((int)Type);
    }
}
