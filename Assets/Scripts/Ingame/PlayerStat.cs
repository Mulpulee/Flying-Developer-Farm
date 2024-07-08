using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour
{
    [SerializeField] private Slider m_hpBar;

    private float m_maxHp = 100;
    private float m_hp = 100;

    private void Update()
    {
        m_hpBar.value = m_hp / m_maxHp;
    }

    public void Hit(float pDamage)
    {
        m_hp -= pDamage;
    }
}
