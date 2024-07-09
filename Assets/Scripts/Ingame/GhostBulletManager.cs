using SocketLib;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GhostBulletManager : MonoBehaviour, IPacketListener<BulletPacket>
{
    [SerializeField] private GameObject m_bullet;
    [SerializeField] private PlayerStat m_playerStat;

    private Queue<BulletPacket> m_createBulletQueue;
    private Dictionary<int, GameObject> m_ghostBullets;

    private void Start()
    {
        m_createBulletQueue = new Queue<BulletPacket>();
        m_ghostBullets = new Dictionary<int, GameObject>();
        PacketEvent<BulletPacket>.Instance.Assign(this);
    }

    private void Update()
    {
        while (m_createBulletQueue.Any())
        {
            var packet = m_createBulletQueue.Dequeue();

            var b = Instantiate(m_bullet, packet.Position, Quaternion.identity);
            b.GetComponent<GhostBullet>().Damage = packet.Damage;
            m_ghostBullets.Add(packet.ID, b);
        }
    }

    public void OnPacket(BulletPacket pPacket)
    {
        switch (pPacket.Type)
        {
            case BulletPacketTypes.Create:
                m_createBulletQueue.Enqueue(pPacket);
                break;
            case BulletPacketTypes.Sync:
                if (m_ghostBullets.TryGetValue(pPacket.ID, out var b))
                {
                    b.transform.position = pPacket.Position;
                    b.GetComponent<GhostBullet>().Damage = pPacket.Damage;
                }
                break;
            case BulletPacketTypes.Delete:
                if (m_ghostBullets.TryGetValue(pPacket.ID, out var c))
                {
                    if (pPacket.Damage > 0) m_playerStat.Hit(pPacket.Damage);

                    m_ghostBullets.Remove(pPacket.ID);
                    Destroy(c);
                }
                break;
            default:
                break;
        }
    }
}
