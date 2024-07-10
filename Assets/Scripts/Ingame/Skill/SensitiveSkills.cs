using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Madness : SensitiveSkill
{
    private int m_ID = 70013;
    public int ID { get { return m_ID; } }

    public Stat Stat { get; }

    public Madness(PlayerStat pPlayer)
    {
        // 총알개수 +1
    }

    public void DoSkill(PlayerStat pPlayer)
    {
        // 5초간 이속 +10
    }
}

public class Deception : SensitiveSkill
{
    private int m_ID = 70014;
    public int ID { get { return m_ID; } }

    public Stat Stat { get; }

    public void DoSkill(PlayerStat pPlayer)
    {
        // 방어상태였다면 적에게 50딜
    }
}

public class Sticky : SensitiveSkill
{
    private int m_ID = 70016;
    public int ID { get { return m_ID; } }

    public Stat Stat { get; }

    public Sticky(PlayerStat pPlayer)
    {
        // 총알 개수 +2
    }

    public void DoSkill(PlayerStat pPlayer)
    {
        // 상대 이속 -4(10초)
    }
}

public class Precision : SensitiveSkill
{
    private int m_ID = 70019;
    public int ID { get { return m_ID; } }

    public Stat Stat { get; }

    public Precision(PlayerStat pPlayer)
    {
        // 총알 개수 +1
    }

    public void DoSkill(PlayerStat pPlayer)
    {
        // 장전 전까지 +100뎀
    }
}

public class Winner : SensitiveSkill
{
    private int m_ID = 70020;
    public int ID { get { return m_ID; } }

    public Stat Stat { get; }

    public Winner(PlayerStat pPlayer)
    {
        // 총알 개수 +1
    }

    public void DoSkill(PlayerStat pPlayer)
    {
        // 즉시 재장전
    }
}

public class Absorb : SensitiveSkill
{
    private int m_ID = 70027;
    public int ID { get { return m_ID; } }

    public Stat Stat { get; }

    public void DoSkill(PlayerStat pPlayer)
    {
        pPlayer.Hit(-100);
    }
}
