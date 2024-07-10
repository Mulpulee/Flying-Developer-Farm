using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCard : MonoBehaviour
{
    private SelectManager m_select;

    [SerializeField] private List<CardEffect> m_cards;
    private int m_index = 0;

    public bool isMyTurn = true;

    public void Init(SelectManager pSelect)
    {
        m_select = pSelect;
        m_cards[0].Selected();

        m_cards = new List<CardEffect>();

        for(int i = 0; i < 5; i++)
        {
            m_cards.Add(transform.GetChild(i).GetComponent<CardEffect>());
        }
    }

    void Update()
    {
        if (m_cards[0] == null)
        {
            m_cards = new List<CardEffect>();

            for (int i = 0; i < 5; i++)
            {
                m_cards.Add(transform.GetChild(i).GetComponent<CardEffect>());
            }
        }

        if (!isMyTurn) return;

        if (Input.GetKeyDown(KeyCode.A) && m_index != 0)
        {
            m_cards[m_index--].Canceled();
            //m_index--;
            m_cards[m_index].Selected();

            m_select.Move(m_index);
        }

        if (Input.GetKeyDown(KeyCode.D) && m_index != 4)
        {
            m_cards[m_index++].Canceled();
            //m_index++;
            m_cards[m_index].Selected();

            m_select.Move(m_index);
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            m_select.Selected(m_index);
            Debug.Log(m_index + "번째 카드 선택했습니당 ㅎ");
        }
    }

    public void MoveIndex(int pIndex)
    {
        if (m_cards[0] == null)
        {
            m_cards = new List<CardEffect>();

            for (int i = 0; i < 5; i++)
            {
                m_cards.Add(transform.GetChild(i).GetComponent<CardEffect>());
            }
        }
        m_cards[m_index].Canceled();
        m_cards[pIndex].Selected();

        m_index = pIndex;
    }
}
