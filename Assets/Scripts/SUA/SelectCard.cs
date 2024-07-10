using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCard : MonoBehaviour
{
    [SerializeField] private List<Button> m_buttons;
    private int m_index = 0;

    private CardEffect m_effect;

    //gameObject.GetComponent<CardEffect>();
    //GameObject.FInd("Select Card").GetComponent<CardEffect>();


    void Start()
    {
        m_effect = GetComponent<CardEffect>();
    }

    //transform.localScale += new Vector3(0.1, 0.1, 0.1);
    //Button source = gameObject.AddComponent<AudioSource>();
    //m_sources.Add(source);

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && m_index != 0)
        {
            m_buttons[m_index].gameObject.GetComponent<CardEffect>().Canceled();

            //CardEffect.Canceled(m_buttons[m_index]);
            m_index++;
            //CardEffect.Selected(m_buttons[m_index]);
            m_buttons[m_index].gameObject.GetComponent<CardEffect>().Selected();


            //transform.localScale += new Vector3(0.1, 0.1, 0.1);
            //transform.localScale += new Vector3(0.1, 0.1, 0.1);
        }

        if (Input.GetKeyDown(KeyCode.D) && m_index != 4)
        {
            //CardEffect.Canceled(m_buttons[m_index]);
            m_index--;
            //CardEffect.Selected(m_buttons[m_index]);

        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            // 여기서 호출
        }
    }


    //Button source = gameObject.AddComponent<AudioSource>();
    //m_sources.Add(source);
}










































