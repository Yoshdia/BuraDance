using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ダンサーたちに指示を送るクラス
/// </summary>
public class InstructionDancer : MonoBehaviour
{
    /// <summary>
    /// ステップの最大数、増加する
    /// </summary>
    private int stepCountMax;

    /// <summary>
    /// フレーズの最長時間、減少する
    /// </summary>
    private int phraseTimeMax;

    /// <summary>
    /// フレーズの最短時間
    /// </summary>
    private const int phraseTimeMin = 300;

    private void Start()
    {
        stepCountMax = 3;
        phraseTimeMax = 500;
    }

    /// <summary>
    /// ダンサーそれぞれに躍らせるフレーズを生成
    /// </summary>
    /// <returns></returns>
    private Phrase CreatePhrase()
    {
        int stepCount = Random.Range(1, stepCountMax+1);
        int phraseTime = Random.Range(phraseTimeMin, phraseTimeMax);

        return new Phrase(stepCount, phraseTime);
    }

    public void Instruction()
    {
        Phrase thisPhrase = CreatePhrase();
    }
}
