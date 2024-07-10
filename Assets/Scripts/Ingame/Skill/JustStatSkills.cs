using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustMyStat : JustStatSkill
{
    private int m_ID = 70001;
    public int ID { get { return m_ID; } }

    public Stat Stat { get; }

    public JustMyStat(int pId, Stat pStat)
    {
        m_ID = pId;
        Stat = pStat;
    }

    public void Apply()
    {

    }
}

public class MeAndOpponent : JustStatSkill
{
    private int m_ID = 70018;
    public int ID { get { return m_ID; } }

    public Stat Stat { get; }
    public Stat OppStat { get; }

    public MeAndOpponent(int pID, Stat pStat, Stat pOpp)
    {
        m_ID = pID;
        Stat = pStat;
        OppStat = pOpp;
    }

    public void Apply()
    {

    }
}

public class Conversion : JustStatSkill // 총알 개수 -2, 데미지 x2
{
    private int m_ID = 70011;
    public int ID { get { return m_ID; } }

    private Stat m_stat;
    public Stat Stat { get; }

    public Conversion()
    {
        m_stat = new Stat();
        m_stat.Bullet = -2;
    }

    public void Apply()
    {

    }
}

public class Scatter : JustStatSkill // 공격 속도 0.2초, 총알 개수 100개, 데미지 5 고정
{
    private int m_ID = 70023;
    public int ID { get { return m_ID; } }

    private Stat m_stat;
    public Stat Stat { get; }

    public Scatter()
    {
        m_stat = new Stat();
        m_stat.Attack_Speed = 0.2f;
        m_stat.Bullet = 100;
        m_stat.Damage = 5;
    }

    public void Apply()
    {

    }
}