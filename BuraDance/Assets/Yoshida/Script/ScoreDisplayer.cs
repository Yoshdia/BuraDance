using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplayer : MonoBehaviour
{
    /// <summary>
    /// スコア合計
    /// </summary>
    //int nowScoreSum;

    /// <summary>
    /// displayerに表示させるスコア
    /// stackScore*100を足すとスコアの合計になる
    /// </summary>
    int shortScore;

    /// <summary>
    /// displayerの表示をオーバーするスコアを記憶する変数
    /// shortScore*100を足すとスコアの合計になる
    /// </summary>
    int stackScore;

    /// <summary>
    /// スコアに応じてサイズが変わるオブジェクト達
    /// 外部からアタッチする必要あり
    /// </summary>
    [SerializeField]
    ToDisplay[] displayers;

    /// <summary>
    /// displayersを拡大する最大サイズ
    /// シーン上に最初に配置されていた数字を記憶する
    /// </summary>
    Vector3 defaultScale;

    /// <summary>
    /// displayersを拡大する最大速度
    /// </summary>
    const float mostSpeedChangeScale = 1.2f;

    /// <summary>
    /// stackScoreを表示するためのText
    /// </summary>
    FontScript texter;

    void Start()
    {
        //nowScoreSum = 0;
        shortScore = 0;
        defaultScale = displayers[0].transform.localScale;
        foreach (ToDisplay displayer in displayers)
        {
            displayer.ResetScale();
        }
        texter = GetComponent<FontScript>();
    }

    /// <summary>
    /// スコア加算
    /// </summary>
    /// <param name="_score"></param>
    public void PlusScore(int _score)
    {
        shortScore += _score;
        if(shortScore>= displayers .Length* 100)
        {
            stackScore += displayers.Length;
            shortScore -= displayers.Length * 100;
            foreach(ToDisplay displayer in displayers)
            {
                displayer.ResetScale();
            }
            //texter.SetSpriteNumber(stackScore);
        }
        UpdateScoreDisplayers();
    }

    /// <summary>
    /// UIを更新する
    /// </summary>
    void UpdateScoreDisplayers()
    {
        ///for文に使用するdisplayer用の引数
        int i = 0;

        //int m = (int)(nowScoreSum / 100);
        //100で割り切れる分をdisplayerに表示する(340だった場合300分を表示するため3つのdisplayerを最大拡張に指示
        for (; i < (int)(shortScore / 100); i++)
        {
            //オーバーロード対策
            if (i >= displayers.Length)
            {
                return;
            }
            //最大サイズに変えさせる
            displayers[i].ScaleChange(defaultScale, mostSpeedChangeScale);
        }
        ///nowScoreSumを100で割った余り
        float surplusScore = shortScore % 100;
        //0以上、余りがあるとき(150のときの50
        if (surplusScore > 0)
        {
            //オーバーロード対策
            if (i >= displayers.Length)
            {
                return;
            }
            displayers[i].ScaleChange(defaultScale * ((surplusScore) / 100), mostSpeedChangeScale);
        }
    }
}
