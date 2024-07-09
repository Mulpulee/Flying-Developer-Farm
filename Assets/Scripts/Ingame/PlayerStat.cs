using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour
{
    [SerializeField] private Slider m_hpBar;

    private float m_maxHp = 200;
    private float m_hp = 200;
    public float HP { get { return m_hp; } set { m_hp = value; } }

    private void Update()
    {
        m_hpBar.value = m_hp / m_maxHp;
    }

    public void Hit(float pDamage)
    {
        m_hp -= pDamage;
    }
}
