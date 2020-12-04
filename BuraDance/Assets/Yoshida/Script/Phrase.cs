using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステップの方向
/// </summary>
public enum StepDirection
{
    /// <summary>
    /// ステップしていない
    /// </summary>
    NoStep,
    /// <summary>
    /// 左ステップ
    /// </summary>
    LeftStep,
    /// <summary>
    /// 右ステップ
    /// </summary>
    RightStep
}

/// <summary>
/// 1フレーズ
/// ステップの回数、それぞれステップの方向、1フレーズの時間を持つ
/// </summary>
public struct Phrase
{
    /// <summary>
    /// このフレーズ内でのステップ数 1以上
    /// </summary>
    public readonly int stepCount;

    /// <summary>
    /// このフレーズの時間 
    /// 1キャラクターに用意されたステップ時間
    /// </summary>
    public readonly float phraseTime;

    /// <summary>
    /// このフレーズ内でのステップ一覧
    /// </summary>
   public readonly IList<StepDirection> stepTable;

    /// <summary>
    /// フレーズ生成
    /// </summary>
    /// <param name="_stepCount">ステップの数</param>
    public Phrase(int _stepCount,float _phraseTime)
    {
        this.stepCount = _stepCount;
        this.phraseTime = _phraseTime;
        stepTable = new List<StepDirection>();

        //左右のステップを乱数で生成する
        for (int i = 1; i <= this.stepCount; i++)
        {
            int randam = Random.Range(0, 2);
            if (randam == 1)
            {
                //左
                stepTable.Add(StepDirection.LeftStep);
            }
            else
            {
               //右
                stepTable.Add(StepDirection.RightStep);
            }
        }
        stepTable.Add(StepDirection.NoStep);
        Debug.Log(stepTable.Count-1);
    }

}
