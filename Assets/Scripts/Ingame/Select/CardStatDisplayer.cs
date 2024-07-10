using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardStatDisplayer : MonoBehaviour
{
    private Text m_value;
    private Text m_stat;

    public void SetStat(float pValue, Stats pStat)
    {
        m_value = transform.GetChild(0).GetComponent<Text>();
        m_stat = transform.GetChild(1).GetComponent<Text>();

        bool isPositive = pValue > 0;

        m_value.text = pValue.ToString();

        switch (pStat)
        {
            case Stats.Attack_Speed:
                m_value.text += "s";
                m_stat.text = "공격 속도";
                isPositive = !isPositive;
                break;
            case Stats.Bullet:
                m_stat.text = "총알 개수";
                break;
            case Stats.Reload_Speed:
                m_value.text += "s";
                m_stat.text = "재장전 속도";
                isPositive = !isPositive;
                break;
            case Stats.Damage:
                m_stat.text = "데미지";
                break;
            case Stats.Health:
                m_stat.text = "체력";
                break;
            case Stats.Skill_Cooldown:
                m_value.text += "s";
                m_stat.text = "스킬 쿨타임";
                isPositive = !isPositive;
                break;
            case Stats.Move_Speed:
                m_stat.text = "이동 속도";
                break;
            default:
                break;
        }

        m_value.color = isPositive ? Color.cyan : Color.magenta;
    }
}
