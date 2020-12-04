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

        StopCoroutine("Dance");
        //踊らせる
        StartCoroutine("Dance", onePhrase);
    }

    /// <summary>
    /// １フレーズ分の時間をあけてそれぞれに指示を送る
    /// </summary>
    /// <param name = "_onePhrase" > 踊らせたいフレーズ </ param >
    /// < returns ></ returns >
    private IEnumerator Dance(Phrase _onePhrase)
    {
        float phraseTime = (float)_onePhrase.phraseTime;
        float frame = phraseTime;
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
        }
        Debug.Log("DanceEnd");
    }
}
