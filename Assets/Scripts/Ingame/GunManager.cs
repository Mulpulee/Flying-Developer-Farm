using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public static int UsedBulletID { get; private set; }

    [SerializeField] private GameObject m_aim;
    [SerializeField] private GameObject m_bullet;
    [SerializeField] private GameObject m_gun;

    private void Start()
    {
        
    }

    private void Update()
    {
        transform.LookAt(m_aim.transform, Vector3.forward);
        transform.rotation = new Quaternion(0, 0, transform.rotation.z, transform.rotation.w);

        if (Input.GetMouseButtonDown(0))
        {
            GameObject b = Instantiate(m_bullet);
            b.transform.position = m_gun.transform.position;
            b.transform.rotation = transform.rotation;
        }
    }
}
