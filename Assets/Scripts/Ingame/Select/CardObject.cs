using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct Stat
{
    public float Attack_Speed;
    public int Bullet;
    public float Reload_Speed;
    public float Damage;
    public float Health;
    public float Skill_Cooldown;
    public float Move_Speed;
}

public enum CardRank
{
    Rare,
    Epic,
    Legend
}

public enum Stats
{
    Attack_Speed,
    Bullet,
    Reload_Speed,
    Damage,
    Health,
    Skill_Cooldown,
    Move_Speed
}

[CreateAssetMenu(fileName = "NewCardObject", menuName = "Scriptable Object/Card")]
public class CardObject : ScriptableObject
{
    public int ID;
    public string Name;
    public string Description;

    public CardRank Rank;
    public Stat Stat;

    public Sprite Illust;
}
