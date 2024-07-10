using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour
{
    [SerializeField] private Slider m_hpBar;

    private Stat m_stat;

    private float m_hp = 200;
    public float HP { get { return m_hp; } set { m_hp = value; } }

    private void Update()
    {
        m_hpBar.value = m_hp / m_stat.Health;
    }

    public void Hit(float pDamage)
    {
        m_hp -= pDamage;
    }

    public void SetStat(Stat pStat)
    {
        m_stat = pStat;
    }

    public void AddStat(Stat pStat)
    {
        m_stat.Attack_Speed += pStat.Attack_Speed;
        m_stat.Bullet += pStat.Bullet;
        m_stat.Reload_Speed += pStat.Reload_Speed;
        m_stat.Damage += pStat.Damage;
        m_stat.Health += pStat.Health;
        m_stat.Skill_Cooldown += pStat.Skill_Cooldown;
        m_stat.Move_Speed += pStat.Move_Speed;
    }
}
