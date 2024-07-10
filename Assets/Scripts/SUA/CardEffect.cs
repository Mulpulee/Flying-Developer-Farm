using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardEffect : MonoBehaviour
{
    //[SerializeField] private GameObject m_line;

    private Image m_image;
    private Text[] m_texts;

    void Start()
    {
        m_image = GetComponent<Image>();
        m_texts = transform.GetComponentsInChildren<Text>(true);

        m_image.color = new Color32(50, 50, 50, 255);
        foreach (var text in m_texts) text.color = new Color(text.color.r, text.color.g, text.color.b, 0.05f);
        transform.localScale = new Vector3(0.75f, 0.75f, 1f);
    }

    public void Selected()
    {
        m_image = GetComponent<Image>();
        m_texts = transform.GetComponentsInChildren<Text>(true);
        Color color = new Color32(255, 255, 255, 255);
        //color.a = 1f;

        m_image.color = color;
        foreach (var text in m_texts) text.color = new Color(text.color.r, text.color.g, text.color.b, 1);

        //m_text.transform.localPosition = new Vector3(0, 920, 0);
        //m_text.transform.Translate(new Vector3(0, 10, 0));

        //m_image.transform.localScale += new Vector3(0.05f, 0.05f, 0.0f);
        transform.localScale = new Vector3(0.9f, 0.9f, 1f);

        //transform.GetChild(1).gameObject.SetActive(true);
        //m_line.SetActive(true);
    }

    public void Canceled()
    {
        m_image = GetComponent<Image>();
        m_texts = transform.GetComponentsInChildren<Text>(true);
        Color color = new Color32(50, 50, 50, 255);
        //color.a = 0.4f;

        m_image.color = color;
        foreach (var text in m_texts) text.color = new Color(text.color.r, text.color.g, text.color.b, 0.05f);

        //m_text.transform.localPosition = new Vector3(0, 820, 0);
        //m_text.transform.Translate(new Vector3(0, -12, 0));

        //m_image.transform.localScale += new Vector3(-0.05f, -0.05f, 0.0f);
        transform.localScale = new Vector3(0.75f, 0.75f, 1f);

        //transform.GetChild(1).gameObject.SetActive(false);
        //m_line.SetActive(false);
    }
}
