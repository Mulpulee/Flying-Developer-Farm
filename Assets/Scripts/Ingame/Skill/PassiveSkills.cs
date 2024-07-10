using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prudence : PassiveSkill
{
    private int m_ID = 70008;
    public int ID { get { return m_ID; } }

    public Stat Stat { get; }

    public void Check(PlayerStat pPlayer, int pIndex)
    {
        if (pIndex == 1) DoSkill(pPlayer);
    }

    public void DoSkill(PlayerStat pPlayer)
    {
        //인스턴트 대미지 현재의 두배
    }
}

public class Lucky : PassiveSkill
{
    private int m_ID = 70009;
    public int ID { get { return m_ID; } }

    public Stat Stat { get; }

    public void Check(PlayerStat pPlayer, int pIndex)
    {
        if (pIndex == 1) DoSkill(pPlayer);
    }

    public void DoSkill(PlayerStat pPlayer)
    {
        //인스턴트 대미지 현재의 두배
    }
}

public class Firepower : PassiveSkill
{
    private int m_ID = 70024;
    public int ID { get { return m_ID; } }

    public Stat Stat { get; }

    public Firepower(PlayerStat pPlayer)
    {
        // 총알 개수 +1
    }

    public void Check(PlayerStat pPlayer, int pIndex)
    {
        if (pIndex != -1) DoSkill(pPlayer);
    }

    public void DoSkill(PlayerStat pPlayer)
    {
        //인스턴트 대미지 현재의 두배
    }
}

public class Absolute : PassiveSkill // 따로 빼거나 구현 안해야할듯
{
    private int m_ID = 70031;
    public int ID { get { return m_ID; } }

    public Stat Stat { get; }

    public void Check(PlayerStat pPlayer, int pIndex)
    {

    }

    public bool Check(int pIndex)
    {
        return pIndex == 1;
    }

    public void DoSkill(PlayerStat pPlayer)
    {

    }
}
