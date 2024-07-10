using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectManager : MonoBehaviour, IPacketListener<SelectPacket>
{
    [SerializeField] private List<Button> m_buttons;

    private int m_index;

    public void StartSelecting(InGameState pState, bool isMyTurn)
    {

    }

    public void OnPacket(SelectPacket pPacket)
    {
        switch (pPacket.Type)
        {
            case SelectPacketTypes.List:
                m_index = 0;
                break;
            case SelectPacketTypes.Move:
                m_index = pPacket.Index;
                break;
            case SelectPacketTypes.Submit:
                m_index = pPacket.Index;
                break;
            default:
                break;
        }
    }
}
