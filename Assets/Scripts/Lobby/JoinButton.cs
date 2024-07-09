using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinButton : MonoBehaviour
{
    [SerializeField] private InputField m_input;

    public void Submit()
    {
        GameManagerEx.Instance.JoinGame(m_input.text);
    }
}
