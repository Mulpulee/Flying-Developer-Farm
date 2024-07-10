using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardEffect : MonoBehaviour
{
    private Image m_image;
    private Text m_text;


    void Start()
    {
        m_image = GetComponent<Image>();
        m_text = transform.GetChild(0).GetComponent<Text>();
    }

    void Update()
    {

    }

    public void Selected()
    {
        Color color = m_image.color;
        color = new Color32(255, 255, 255, 255);
        //color.a = 1f;

        m_image.color = color;
        m_text.color = color;

        m_text.transform.localPosition = new Vector3(0, 920, 0);
        //m_text.transform.Translate(new Vector3(0, 10, 0));

        m_image.transform.localScale += new Vector3(0.05f, 0.05f, 0.0f);

        transform.GetChild(1).gameObject.SetActive(true);
    }

    public void Canceled()
    {
        Color color = m_image.color;
        color = new Color32(50, 50, 50, 255);
        //color.a = 0.4f;

        m_image.color = color;
        m_text.color = color;

        m_text.transform.localPosition = new Vector3(0, 820, 0);
        //m_text.transform.Translate(new Vector3(0, -12, 0));

        m_image.transform.localScale += new Vector3(-0.05f, -0.05f, 0.0f);

        transform.GetChild(1).gameObject.SetActive(false);
    }
}
