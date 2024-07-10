using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCard : MonoBehaviour
{
    [SerializeField] private List<Button> m_buttons;
    private int m_index = 0;

    private CardEffect m_effect;

    void Start()
    {
        m_effect = GetComponent<CardEffect>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && m_index != 0)
        {
            m_buttons[m_index].gameObject.GetComponent<CardEffect>().Canceled();
            m_index--;
            m_buttons[m_index].gameObject.GetComponent<CardEffect>().Selected();

        }

        if (Input.GetKeyDown(KeyCode.D) && m_index != 4)
        {
            m_buttons[m_index].gameObject.GetComponent<CardEffect>().Canceled();
            m_index++;
            m_buttons[m_index].gameObject.GetComponent<CardEffect>().Selected();

        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            // 여기서 호출
            Debug.Log(m_index + "번째 카드 선택했습니당 ㅎ");
        }
    }
}
