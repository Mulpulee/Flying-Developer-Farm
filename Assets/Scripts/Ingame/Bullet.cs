using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float m_power;
    [SerializeField] private float m_damage;

    [SerializeField] private Vector2 m_deadline;

    private void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up.normalized * -1f * m_power, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet")) return;

        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerStat>().Hit(m_damage);
        }
        Destroy(gameObject);
    }

    private void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(0, 0, 0), new Vector3(m_deadline.x, m_deadline.y, 0));
    }
}
