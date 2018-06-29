using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneySystem : MonoBehaviour
{
    private static float sPlayerMoney = 0f;
    private const string KEY_PLAYER_MONEY = "key_player_money";
    [Header("Settings")] [SerializeField] bool m_DontDestroyOnLoad = true;
    [Header("Observar")] [SerializeField] float m_CurrentPlayerMoney = 0f;

    void Awake()
    {
        if (m_DontDestroyOnLoad) { DontDestroyOnLoad(this.gameObject); }

        sPlayerMoney = PlayerPrefs.GetFloat(KEY_PLAYER_MONEY, 0f);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        m_CurrentPlayerMoney = sPlayerMoney;
    }

    public float GetPlayerMoney() { return sPlayerMoney; }
    public void SetPlayerMoney(float value)
    {
        sPlayerMoney = value;
        PlayerPrefs.SetFloat(KEY_PLAYER_MONEY, value);
    }
}
