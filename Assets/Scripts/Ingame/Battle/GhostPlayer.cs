using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GhostPlayer : MonoBehaviour, IPacketListener<PlayerPacket>
{
    private Transform m_gun;
    private PlayerStat m_stat;

    private void Start()
    {
        m_gun = transform.GetChild(0);
        m_stat = GetComponent<PlayerStat>();
        PacketEvent<PlayerPacket>.Instance.Assign(this);
    }

    private void Update()
    {

    }

    public void OnPacket(PlayerPacket pPacket)
    {
        switch (pPacket.Type)
        {
            case PlayerPacketTypes.Sync:
                transform.position = pPacket.Position;
                m_gun.rotation = pPacket.Gun;
                m_stat.HP = pPacket.Health;
                break;
            case PlayerPacketTypes.Dead:
                Debug.Log("Dead!!");
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}

