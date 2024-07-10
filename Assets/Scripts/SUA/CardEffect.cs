using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardEffect : MonoBehaviour
{
    private Image m_image;


    void Start()
    {
        m_image = GetComponent<Image>();
    }

    void Update()
    {

    }

    public void Selected()
    {
        Color color = m_image.color;
        color.a = 1f;
        m_image.color = color;


        transform.GetChild(1).gameObject.SetActive(true);
    }

    public void Canceled()
    {
        Color color = m_image.color;
        color.a = 0.4f;
        m_image.color = color;


        transform.GetChild(1).gameObject.SetActive(false);
    }
}
