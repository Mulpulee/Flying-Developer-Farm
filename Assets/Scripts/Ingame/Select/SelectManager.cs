using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectManager : MonoBehaviour, IPacketListener<SelectPacket>
{
    private List<Button> m_buttons;

    private GameClient m_client;
    private SelectCard m_cardSelector;

    private Dictionary<int, CardObject> m_cards;

    private InGameState m_state;
    private int m_index;
    private bool m_isMyTurn;
    private int[] m_list;

    private void Start()
    {
        m_cardSelector = FindObjectOfType<SelectCard>();
        m_cards = new Dictionary<int, CardObject>();

        foreach(var c in Resources.LoadAll<CardObject>("ScriptableObject"))
        {
            m_cards.Add(c.ID, c);
        }
        PacketEvent<SelectPacket>.Instance.Assign(this);
    }

    public void Init(GameClient pClient, InGameState pState, bool isMyTurn)
    {
        m_client = pClient;
        StartSelecting(pState, isMyTurn);
    }

    public void StartSelecting(InGameState pState, bool isMyTurn)
    {
        m_index = 0;
        m_state = pState;
        m_isMyTurn = isMyTurn;
        GameObject.Find("notmyturn").SetActive(!isMyTurn);

        if (pState == InGameState.Start || pState == InGameState.Card)
        {
            CardSelect();
        }
        else
        {
            //DecoSelect();
            CardSelect();
        }
    }

    private void CardSelect()
    {
        m_list = new int[5];
        m_cardSelector = FindObjectOfType<SelectCard>();
        m_cardSelector.Init(this);
        if (m_isMyTurn)
        {
            m_buttons = new List<Button>();
            Transform bg = FindObjectOfType<SelectCard>().transform;

            for (int i = 0; i < 5; i++)
            {
                int rank = Random.Range(0, 20);
                if (rank < 12)
                {
                    GetUnique(i, 15);
                    m_list[i] += 70001;
                }
                else if (rank < 19)
                {
                    GetUnique(i, 15);
                    m_list[i] += 70016;

                }
                else
                {
                    GetUnique(i, 5);
                    m_list[i] += 70031;
                }

                m_buttons.Add(bg.GetChild(i).GetComponent<Button>());
                m_buttons[i].gameObject.GetComponent<CardController>().SetCardInfo(m_cards[m_list[i]]);
            }

            m_client.Send(new SelectPacket(SelectPacketTypes.List, 0, m_list));
        }

        m_cardSelector.gameObject.SetActive(true);
        m_cardSelector.isMyTurn = m_isMyTurn;
    }

    private void DecoSelect()
    {

    }

    public void Move(int pIndex)
    {
        m_client.Send(new SelectPacket(SelectPacketTypes.Move, pIndex, m_list));
    }

    public void Selected(int pIndex)
    {
        m_client.Send(new SelectPacket(SelectPacketTypes.Submit, pIndex, m_list));
        m_index = pIndex;
        EndSelecting();
    }

    private void EndSelecting()
    {
        if (GameManagerEx.Instance.IsHost)
        {
            if (m_state == InGameState.Start)
            {
                if (m_isMyTurn) GameManagerEx.Instance.ChangeState(InGameState.Start, false);
                else
                {
                    GameManagerEx.Instance.ChangeState(InGameState.Battle, true);
                }
            }
            else if (m_state == InGameState.Card)
            {
                GameManagerEx.Instance.ChangeState(InGameState.Item, !m_isMyTurn);
            }
            else if (m_state == InGameState.Item)
            {
                GameManagerEx.Instance.ChangeState(InGameState.Battle, true);
            }
        }
    }

    public void OnPacket(SelectPacket pPacket)
    {
        switch (pPacket.Type)
        {
            case SelectPacketTypes.List:
                m_index = 0;
                m_buttons = new List<Button>();
                Transform bg = FindObjectOfType<SelectCard>().transform;
                for (int i = 0; i < 5; i++)
                {
                    m_buttons.Add(bg.GetChild(i).GetComponent<Button>());
                    m_buttons[i].gameObject.GetComponent<CardController>().SetCardInfo(m_cards[pPacket.Array[i]]);
                }
                break;
            case SelectPacketTypes.Move:
                m_index = pPacket.Index;
                m_cardSelector = FindObjectOfType<SelectCard>();
                m_cardSelector.MoveIndex(m_index);
                break;
            case SelectPacketTypes.Submit:
                m_index = pPacket.Index;
                EndSelecting();
                break;
            default:
                break;
        }
    }

    private void GetUnique(int index, int range)
    {
        m_list[index] = Random.Range(0, range);
        for (int j = 0; j < index; j++)
        {
            if (m_list[j] == m_list[index])
            {
                m_list[index] = Random.Range(0, range);
                j = -1;
            }
        }
    }
}
