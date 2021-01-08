//---------------------------------------------------------+
// スコア保存用クラス
// 2021 Yutaro Ono
//---------------------------------------------------------+
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScore : MonoBehaviour
{

    public static int score;
    public int score_debug;

    // Start is called before the first frame update
    void Awake()
    {
        score = 0;
        score_debug = score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// <summary>
    /// スコア加算関数
    /// </summary>
    /// <param name="_addVal">加算するスコアの値</param>
    public void AddScore(int _addVal)
    {
        score += _addVal;
        score_debug += _addVal;
    }
}
