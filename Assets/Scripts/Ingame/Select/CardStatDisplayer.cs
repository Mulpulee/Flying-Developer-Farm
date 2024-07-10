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
                m_stat.text = "���� �ӵ�";
                isPositive = !isPositive;
                break;
            case Stats.Bullet:
                m_stat.text = "�Ѿ� ����";
                break;
            case Stats.Reload_Speed:
                m_value.text += "s";
                m_stat.text = "������ �ӵ�";
                isPositive = !isPositive;
                break;
            case Stats.Damage:
                m_stat.text = "������";
                break;
            case Stats.Health:
                m_stat.text = "ü��";
                break;
            case Stats.Skill_Cooldown:
                m_value.text += "s";
                m_stat.text = "��ų ��Ÿ��";
                isPositive = !isPositive;
                break;
            case Stats.Move_Speed:
                m_stat.text = "�̵� �ӵ�";
                break;
            default:
                break;
        }

        m_value.color = isPositive ? Color.cyan : Color.magenta;
    }
}
