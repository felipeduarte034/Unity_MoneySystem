using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissions : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] [Tooltip("Valor recebido por completar a entrega")] float m_RecompensaMissao = 500f;

    [Header("Tags")]
    [SerializeField] string TAG_MONEY_SYSTEM = "MoneySystem";
    [SerializeField] string TAG_ENTREGA_START = "EntregaStart";
    [SerializeField] string TAG_ENTREGA_END = "EntregaEnd";
    [SerializeField] string TAG_SPAWN_ENTREGA_END = "SpawnEntregaEnd";

    [Header("Prefabs")]
    [SerializeField] [Tooltip("Prefab do EntregaEnd")] GameObject m_pfEntregaEnd;
    [SerializeField] [Tooltip("Prefab da Seta")] GameObject m_pfArrow;
    [SerializeField] [Tooltip("Coloque aqui a posição que a Seta devera ficar")] Transform m_SpawnArrow;

    GameObject m_goEntregaBegin;
    GameObject m_goEntregaEnd;
    GameObject m_goArrow;
    GameObject[] m_ListSpawn;

    float m_PlayerMoney = 0f;
    bool entregaStart = false;
    //[SerializeField] bool entregaEnd = false;
    int m_Rand = 0;

    private MoneySystem scriptMS;

    // Use this for initialization

    void Awake()
    {
        m_ListSpawn = GameObject.FindGameObjectsWithTag(TAG_SPAWN_ENTREGA_END);
        if (m_ListSpawn == null) { Debug.Log("m_ListSpawn == NULL - tag não encotrada"); }

        //if (m_ListSpawn.Length > 0) { m_Rand = Random.Range(0, m_ListSpawn.Length - 1); } // gera um indice aleatorio
        //m_Rand = GeraIndiceAleatorio();
        //m_goEntregaEnd = Instantiate(m_pfEntregaEnd, m_ListSpawn[m_Rand].transform.position, Quaternion.identity);
        //if (m_goEntregaEnd == null) { Debug.Log("EntregaEnd == NULL - não encotrado"); }

        m_goArrow = Instantiate(m_pfArrow, m_SpawnArrow.position, Quaternion.identity);
        if (m_goArrow == null) { Debug.Log("m_goArrow == NULL - não encotrado"); }

        scriptMS = GameObject.FindWithTag(TAG_MONEY_SYSTEM).GetComponent<MoneySystem>();
        if (scriptMS == null) { Debug.Log("MONEY SYSTEM == NULL - tag não encotrada"); }

        m_goEntregaBegin = GameObject.FindWithTag(TAG_ENTREGA_START);
        if (m_goEntregaBegin == null) { Debug.Log("EntregaStart == NULL - tag não encotrada"); }

        //m_goEntregaEnd = GameObject.FindWithTag(TAG_ENTREGA_END);

        if (m_goEntregaBegin != null) { m_goEntregaBegin.SetActive(true); }
        if (m_goEntregaEnd != null) { m_goEntregaEnd.SetActive(false); }
        if (m_goArrow != null) { m_goArrow.SetActive(false); }
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (scriptMS != null) { m_PlayerMoney = scriptMS.GetPlayerMoney(); }

        m_goArrow.transform.position = m_SpawnArrow.position; //posiçao da seta
        if (m_goArrow != null && entregaStart) { m_goArrow.transform.LookAt(m_goEntregaEnd.transform); } //direçao da seta
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == TAG_ENTREGA_START)
        {
            //Debug.Log("Entrou: EntregaStart");
            if (!entregaStart)
            {
                entregaStart = true;
                //m_goEntregaBegin.SetActive(false);

                m_Rand = GeraIndiceAleatorio();
                m_goEntregaEnd = Instantiate(m_pfEntregaEnd, m_ListSpawn[m_Rand].transform.position, Quaternion.identity);

                m_goEntregaEnd.SetActive(true);
                m_goArrow.SetActive(true);
            }
        }
        if (other.tag == TAG_ENTREGA_END)
        {
            //Debug.Log("Entrou: EntregaEnd");
            if (entregaStart)
            {
                //entregaEnd = true;
                entregaStart = false;
                m_goEntregaEnd.SetActive(false);
                Destroy(m_goEntregaEnd);
                scriptMS.SetPlayerMoney(scriptMS.GetPlayerMoney() + m_RecompensaMissao);
                m_goArrow.SetActive(false);
            }
        }
    }

    private int GeraIndiceAleatorio()
    {
        int rand = 0;
        if (m_ListSpawn.Length > 0) { rand = Random.Range(0, m_ListSpawn.Length); }
        return rand;
    }

    void OnCollisionEnter(Collision other)
    {

    }
}
