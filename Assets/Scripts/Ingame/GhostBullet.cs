using SocketLib;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GhostBullet : MonoBehaviour, IPacketListener<BulletPacket>
{
    [SerializeField] private GameObject m_bullet;

    public string Name { get; set; } = nameof(GhostBullet);
    public Vector2 Position { get; set; }

    private Queue<BulletPacket> m_createBulletQueue;
    private Dictionary<int, GameObject> m_ghostBullets;
    private int m_consumedID;

    public void Init(bool pIsHost)
    {
        m_createBulletQueue = new Queue<BulletPacket>();
        m_ghostBullets = new Dictionary<int, GameObject>();
        m_consumedID = pIsHost ? 10000 : 20000;
    }

    private void Start()
    {
        PacketEvent<BulletPacket>.Instance.Assign(this);
    }

    private void Update()
    {
        while (m_createBulletQueue.Any())
        {
            var packet = m_createBulletQueue.Dequeue();

            var b = Instantiate(m_bullet, packet.Position, Quaternion.identity);
            b.GetComponent<BulletStat>().Damage = packet.Damage;
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
                    b.GetComponent<BulletStat>().Damage = pPacket.Damage;
                }
                break;
            default:
                break;
        }
    }
}
