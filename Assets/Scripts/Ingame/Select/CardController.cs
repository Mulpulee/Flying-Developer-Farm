using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    [SerializeField] private Text m_name;
    [SerializeField] private Text m_description;
    [SerializeField] private Transform m_stat;

    private Image m_illust;

    private CardStatDisplayer[] m_displayer;
    private int m_opened = 0;

    private void Start()
    {
        m_opened = 0;
        m_displayer = new CardStatDisplayer[3];
        for (int i = 0; i < 3; i++)
        {
            m_displayer[i] = m_stat.GetChild(i).GetComponent<CardStatDisplayer>();
            m_displayer[i].gameObject.SetActive(false);
        }

        m_illust = GetComponent<Image>();
    }

    public void SetCardInfo(CardObject pInfo)
    {
        m_opened = 0;
        m_name.text = pInfo.Name; //E5FFAC
        m_description.text = pInfo.Description;
        m_illust.sprite = pInfo.Illust;

        CheckStat(pInfo.Stat.Attack_Speed, Stats.Attack_Speed);
        CheckStat(pInfo.Stat.Bullet, Stats.Bullet);
        CheckStat(pInfo.Stat.Reload_Speed, Stats.Reload_Speed);
        CheckStat(pInfo.Stat.Damage, Stats.Damage);
        CheckStat(pInfo.Stat.Health, Stats.Health);
        CheckStat(pInfo.Stat.Skill_Cooldown, Stats.Skill_Cooldown);
        CheckStat(pInfo.Stat.Move_Speed, Stats.Move_Speed);
    }

    private void CheckStat(float pValue, Stats pStat)
    {
        if (pValue != 0)
        {
            m_displayer[m_opened].gameObject.SetActive(true);
            m_displayer[m_opened].SetStat(pValue, pStat);
            m_opened++;
        }
    }
}
