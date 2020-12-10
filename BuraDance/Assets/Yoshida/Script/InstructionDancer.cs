using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ダンサーたちに指示を送るクラス
/// </summary>
public class InstructionDancer : MonoBehaviour
{
    [SerializeField]
    /// <summary>
    /// ステップの最大数、増加する
    /// </summary>
    private int stepCountMax = 3;

    [SerializeField]
    /// <summary>
    /// フレーズの最短時間
    /// </summary>
    private float phraseTimeMax = 8.0f;

    [SerializeField]
    /// <summary>
    /// Phraseの情報に合わせお手本として踊るダンサー達
    /// Inspectorから追加する必要あり
    /// </summary>
    List<AutoDancer> autoDancers = new List<AutoDancer>();

    [SerializeField]
    /// <summary>
    /// ダンスを真似するクラス
    /// </summary>    
    MatchDancer matchDancer = default;

    /// <summary>
    /// AutoDancerの処理が終わったフラグ
    /// </summary>
    bool endAutoDance = false;

    /// <summary>
    /// 1フレーズが終わったときに建つフラグ
    /// プレイヤーの判定が終わった後のエフェクト再生や次のフレーズに移行する処理を書くために必要
    /// </summary>
    bool endPhrase = false;

    [SerializeField]
    /// <summary>
    /// フレーズ間のインターバル
    /// </summary>
    public float PhraseInterval = 250.0f;
    public float phraseInterval = 0.0f;

    private void Update()
    {
        //AutoDancerの処理が終了しMatchDancerとのダンスを照合する
        if (endAutoDance)
        {
            //MatchDancerに踊らせその入力を取得
            int danceResult = matchDancer.MatchingWithModelDance();
            //全て成功
            if (danceResult == 1)
            {
                endAutoDance = false;
                endPhrase = true;
                phraseInterval = PhraseInterval;
                Debug.Log("Dance Clear!");
            }
            //失敗
            else if (danceResult == -1)
            {
                endAutoDance = false;
                endPhrase = true;
                phraseInterval = PhraseInterval;
                Debug.Log("Dance Missed...");
            }
        }
        if (endPhrase)
        {
            if (phraseInterval >= 0)
            {
                phraseInterval--;
            }
            else
            {
                Instruction();
                endPhrase = false;
            }
        }
    }

    /// <summary>
    /// ダンサーそれぞれに躍らせるフレーズを生成
    /// </summary>
    /// <returns>生成した構造体フレーズ</returns>
    private Phrase CreatePhrase()
    {
        //定められた定数の範囲の乱数でこのフレーズのステップ数とステップ時間を決定
        int stepCount = Random.Range(1, stepCountMax + 1);

        return new Phrase(stepCount, phraseTimeMax);
    }

    /// <summary>
    /// ダンサーたちに指示を送る
    /// </summary>
    public void Instruction()
    {
        //フレーズ生成
        Phrase onePhrase = CreatePhrase();

        //踊らせる相手がゼロではないか
        if (autoDancers.Count < 1)
        {
            Debug.Log("Error! no Auto Dancer!");
            return;
        }

        //前回までのダンスを終わらせる
        endAutoDance = false;
        StopCoroutine("UseDancers");

        //踊らせる
        StartCoroutine("UseDancers", onePhrase);
    }

    /// <summary>
    /// １フレーズ分の時間をあけてそれぞれに指示を送る
    /// </summary>
    /// <param name = "_onePhrase" > 踊らせたいフレーズ </ param >
    /// < returns ></ returns >
    private IEnumerator UseDancers(Phrase _onePhrase)
    {
        float phraseTime = (float)_onePhrase.phraseTime;
        float frame = phraseTime;
        //AutoDancerに指示した回数
        int dancerCount = 0;
        //一人目からフレーズを渡し踊らせ、１フレーズ分の時間が経過すると次のダンサーに踊らせる
        //ダンサーの数繰り返す。
        foreach (var dancer in autoDancers)
        {
            dancer.Dance(_onePhrase);
            ////１フレーズ分待たせる    
            while (frame > 0)
            {
                // frameで指定したフレームだけループ
                yield return null;
                frame -= 0.016f;
            }
            frame = phraseTime;
            dancerCount++;
            //最後のAutoDancerに踊らせるとき待機時間を１ステップ分減らす
            if (dancerCount == autoDancers.Count - 1)
            {
                frame -= ((float)_onePhrase.phraseTime / ((float)_onePhrase.stepCount));
            }
        }
        //ダンスを真似するクラスにフレーズを渡す
        matchDancer.RefreshPhrase(_onePhrase);
        //AutoDancerの処理が終了
        endAutoDance = true;
        Debug.Log("DanceEnd");
    }

}
