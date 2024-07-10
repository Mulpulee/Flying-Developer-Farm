using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meditation : ActiveSkill
{
    private int m_ID = 70012;
    public int ID { get { return m_ID; } }

    public Stat Stat { get; }

    public void DoSkill(PlayerStat pPlayer)
    {
        pPlayer.Hit(-50);
    }
}

public class Trap : ActiveSkill
{
    private int m_ID = 70028;
    public int ID { get { return m_ID; } }

    public Stat Stat { get; }

    public void DoSkill(PlayerStat pPlayer)
    {
        // 플레이어 2배크기 범위에 상대가 있으면 100뎀
    }
}

public class Opportunity : ActiveSkill
{
    private int m_ID = 70029;
    public int ID { get { return m_ID; } }

    public Stat Stat { get; }

    public void DoSkill(PlayerStat pPlayer)
    {
        // 다음 공격 피해량 +75
        // 플레이어 스탯이나 건매니저에 인스턴트데미지 같은거 추가해서 한 번 쓰면 다음딜은 돌아오게!!
    }
}

public class Sentencing : ActiveSkill
{
    private int m_ID = 70035;
    public int ID { get { return m_ID; } }

    public Stat Stat { get; }

    private int m_count = 0;

    public void DoSkill(PlayerStat pPlayer)
    {
        m_count++;
        if (m_count == 5)
        {
            // 상대 데미지 0
        }
    }
}
