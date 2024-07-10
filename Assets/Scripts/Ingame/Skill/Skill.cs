using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface Skill
{
    public int ID { get; }

    public Stat Stat { get; }
}

public interface JustStatSkill : Skill // 단순 스탯 변화
{
    public void Apply();
}

public interface ActiveSkill : Skill // 스킬(우클릭) 사용 시 발동
{
    public void DoSkill(PlayerStat pPlayer);
}

public interface PassiveSkill : Skill // 특정 총알 대미지
{
    public void Check(PlayerStat pPlayer, int pIndex);
    public void DoSkill(PlayerStat pPlayer);
}

public interface SensitiveSkill : Skill // 상대가 맞았거나 내가 맞았거나 
{
    public void DoSkill(PlayerStat pPlayer);
}

public interface CardSkill : Skill // 다음카드 전설, 승리 스킬 선택 등 카드선택관련
{
    // 제일 후순위일듯
}
