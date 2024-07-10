using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[CSharpCallLua]
public interface Card
{
    uint ID { get; }
    string Name { get; }
    string Description { get; }

    uint CardRank { get; }

    float Attack_Speed { get; }
    int Bullet { get; }
    float Reload_Speed { get; }
    float Damage { get; }
    float Health { get; }
    float Skill_Cooldown { get; }
    float Move_Speed { get; }
}

public class LuaRunner : MonoBehaviour
{
    [SerializeField] private TextAsset m_luaScript;
    private LuaEnv m_luaEnv;

    private Dictionary<int, Card> m_cards;
    [SerializeField] private int m_cardCount;

    private void Awake()
    {
        m_luaEnv = new LuaEnv();
        m_cards = new Dictionary<int, Card>();
        m_luaEnv.DoString(m_luaScript.text);

        for (int i = 70001; i < m_cardCount + 70001; i++)
        {
            Card c = m_luaEnv.Global.Get<Card>(i.ToString());
            m_cards.Add(i, c);
        }
    }

    public Card GetCard(int pID)
    {
        return m_cards[pID];
    }
}
