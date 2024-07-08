using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_speed;
    [SerializeField] private float m_jump;

    private bool m_canJump = true;
    public bool canJump { set { m_canJump = value; } }

    private Rigidbody2D m_rigid;

    private void Start()
    {
        m_rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && m_canJump)
        {
            m_rigid.AddForce(Vector2.up * m_jump, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * m_speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * m_speed * Time.deltaTime);
        }
    }
}
