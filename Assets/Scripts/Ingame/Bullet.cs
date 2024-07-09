using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int m_ID;
    private float m_power;
    private float m_damage;
    private GameClient m_client;

    [SerializeField] private Vector2 m_deadline;

    public void Init(GameClient pPClient, int pID, float pPower, float pDamage)
    {
        m_ID = pID;
        m_client = pPClient;
        m_power = pPower;
        m_damage = pDamage;

        m_client.Send(new BulletPacket(BulletPacketTypes.Create, pID, transform.position, pDamage));
    }

    private void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up.normalized * -1f * m_power, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet") || collision.CompareTag("Player")) return;

        if (collision.CompareTag("GhostPlayer"))
        {
            collision.GetComponent<PlayerStat>().Hit(m_damage);
        }
        m_client.Send(new BulletPacket(BulletPacketTypes.Delete, m_ID, Vector2.zero, m_damage));
        Destroy(gameObject);
    }

    private void Update()
    {
        if (transform.position.x < -m_deadline.x || m_deadline.x < transform.position.x
            || transform.position.y < -m_deadline.y || m_deadline.y < transform.position.y)
        {
            m_client.Send(new BulletPacket(BulletPacketTypes.Delete, m_ID, Vector2.zero, 0));
            Destroy(gameObject);
            return;
        }

        m_client.Send(new BulletPacket(BulletPacketTypes.Sync, m_ID, transform.position, m_damage));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(0, 0, 0), new Vector3(m_deadline.x * 2, m_deadline.y * 2, 0));
    }
}
