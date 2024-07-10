using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBullet : MonoBehaviour
{
    private int m_ID;
    private float m_damage;

    public int ID { get { return m_ID; } }
    public float Damage { get { return m_damage; } set { m_damage = value; } }
}
