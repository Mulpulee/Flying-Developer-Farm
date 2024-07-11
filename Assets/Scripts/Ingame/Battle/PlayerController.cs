using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameClient m_client;

    [SerializeField] private float m_speed;
    [SerializeField] private float m_jump;

    private bool m_canJump = true;
    public bool canJump { set { m_canJump = value; } }

    private Rigidbody2D m_rigid;
    private Transform m_gun;
    private PlayerStat m_stat;

    public void Init(GameClient pClient)
    {
        m_client = pClient;
    }

    private void Start()
    {
        m_rigid = GetComponent<Rigidbody2D>();
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
    }

    private void Update()
    {
        if (m_client == null) return;

        if (Input.GetKeyDown(KeyCode.Space) && m_canJump)
        {
            m_rigid.AddForce(Vector2.up * m_jump, ForceMode2D.Impulse);
        }

        if (m_stat.HP <= 0)
        {
            m_client.Send(new PlayerPacket(PlayerPacketTypes.Dead, transform.position, m_gun.rotation, m_stat.HP));
            if (GameManagerEx.Instance.IsHost) GameManagerEx.Instance.ChangeState(InGameState.Card, false);
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (m_client == null) return;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * m_speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * m_speed * Time.deltaTime);
        }

        m_client.Send(new PlayerPacket(PlayerPacketTypes.Sync, transform.position, m_gun.rotation, m_stat.HP));
    }
}
