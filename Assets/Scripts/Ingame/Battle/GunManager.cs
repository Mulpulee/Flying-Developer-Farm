using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunManager : MonoBehaviour
{
    public static int UsedBulletID { get; private set; }

    private GameClient m_client;

    [SerializeField] private GameObject m_aim;
    [SerializeField] private GameObject m_bullet;
    [SerializeField] private GameObject m_gun;

    private float m_power = 40;
    private float m_damage = 50;

    //[SerializeField] private Text m_powerCount;

    private void Start()
    {

    }

    public void Init(GameClient pClient, bool pIsHost)
    {
        m_client = pClient;
        UsedBulletID = pIsHost ? 10000 : 20000;
    }

    private void Update()
    {
        if (m_client == null) return;

        transform.LookAt(m_aim.transform, Vector3.forward);
        transform.rotation = new Quaternion(0, 0, transform.rotation.z, transform.rotation.w);

        if (Input.GetMouseButtonDown(0))
        {
            GameObject b = Instantiate(m_bullet);
            b.transform.position = m_gun.transform.position;
            b.transform.rotation = transform.rotation;
            b.GetComponent<Bullet>().Init(m_client, ++UsedBulletID, m_power, m_damage);
        }
    }

    //public void SetBulletPower(float value)
    //{
    //    m_power = value;
    //    m_powerCount.text = value.ToString();
    //}
}
