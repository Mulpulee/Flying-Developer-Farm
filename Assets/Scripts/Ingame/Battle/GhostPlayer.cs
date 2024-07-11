using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GhostPlayer : MonoBehaviour, IPacketListener<PlayerPacket>
{
    private static GameObject m_go;
    public static GameObject GhostGO
    {
        get
        {
            if (m_go == null)
            {
                m_go = FindObjectOfType<GhostPlayer>().gameObject;
                if (m_go == null)
                {
                    m_go = new GameObject();
                    GhostPlayer g = m_go.AddComponent<GhostPlayer>();
                    g.m_gun = m_go.transform.GetChild(0);
                    g.m_stat = m_go.GetComponent<PlayerStat>();
                    PacketEvent<PlayerPacket>.Instance.Assign(m_go.GetComponent<GhostPlayer>());
                }
            }

            return m_go;
        }
    }

    private Transform m_gun;
    private PlayerStat m_stat;

    private void Start()
    {
        m_gun = transform.GetChild(0);
        m_stat = GetComponent<PlayerStat>();
        m_stat.SetStat(new Stat()
        {
            Attack_Speed = 1.5f,
            Bullet = 3,
            Reload_Speed = 2,
            Damage = 50,
            Health = 200,
            Skill_Cooldown = 8,
            Move_Speed = 5
        });
        PacketEvent<PlayerPacket>.Instance.Assign(GhostPlayer.GhostGO.GetComponent<GhostPlayer>());
    }

    private void Update()
    {

    }

    public void Init()
    {
    }

    public void OnPacket(PlayerPacket pPacket)
    {
        switch (pPacket.Type)
        {
            case PlayerPacketTypes.Sync:
                GhostPlayer.GhostGO.transform.position = pPacket.Position;
                GhostPlayer.GhostGO.GetComponent<GhostPlayer>().m_gun.rotation = pPacket.Gun;
                GhostPlayer.GhostGO.GetComponent<GhostPlayer>().m_stat.HP = pPacket.Health;
                break;
            case PlayerPacketTypes.Dead:
                if (GameManagerEx.Instance.IsHost)
                {
                    GameManagerEx.Instance.ChangeState(InGameState.Card, true);
                }
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}

