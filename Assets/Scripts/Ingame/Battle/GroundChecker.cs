using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private PlayerController m_pc;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground")) m_pc.canJump = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground")) m_pc.canJump = false;        
    }
}
