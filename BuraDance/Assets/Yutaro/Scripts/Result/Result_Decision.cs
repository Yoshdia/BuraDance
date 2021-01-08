//--------------------------------------------------------------+
// 結果判定. スコアに応じて表示する各画像を切り替える
// 2021 Yutaro Ono.
//--------------------------------------------------------------+
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result_Decision : MonoBehaviour
{

    [SerializeField]
    int[] rankScore;

    // スコアによって変動する画像
    [SerializeField]
    GameObject[] m_bg;
    [SerializeField]
    GameObject[] m_flowers;
    [SerializeField]
    GameObject[] m_flowersS;
    [SerializeField]
    GameObject[] m_ranks;
    [SerializeField]
    GameObject m_audience;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < m_bg.Length; i++)
        {
            m_bg[i].SetActive(false);
        }

        for (int i = 0; i < m_flowers.Length; i++)
        {
            m_flowers[i].SetActive(false);
        }
        for (int i = 0; i < m_flowersS.Length; i++)
        {
            m_flowersS[i].SetActive(false);
        }
        for (int i = 0; i < m_ranks.Length; i++)
        {
            m_ranks[i].SetActive(false);
        }

        m_audience.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(SaveScore.score >= rankScore[0] && SaveScore.score < rankScore[1])
        {
            m_bg[1].SetActive(true);

            m_flowers[0].SetActive(true);
            m_flowers[1].SetActive(true);
            m_ranks[1].SetActive(true);
        }
        else if(SaveScore.score >= rankScore[1])
        {
            m_bg[2].SetActive(true);

            m_flowersS[0].SetActive(true);
            m_flowersS[1].SetActive(true);
            m_flowersS[2].SetActive(true);
            m_ranks[2].SetActive(true);
            m_audience.SetActive(true);
        }
        else
        {
            m_bg[0].SetActive(true);
            m_flowers[0].SetActive(true);
            m_ranks[0].SetActive(true);
        }
    }
}
