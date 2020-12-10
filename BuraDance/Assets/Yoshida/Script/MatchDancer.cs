using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchDancer : MonoBehaviour
{
    Animator animator;
    /// <summary>
    /// 手本となるフレーズ
    /// </summary>
    Phrase modelPhrase;
    /// <summary>
    /// そのフレーズ内でのステップ数
    /// List内のstepNumber番目にアクセスするため-1で初期化する
    /// </summary>
    int stepNumber = -1;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// フレーズをセットしstepNumberを初期化
    /// </summary>
    /// <param name="_phrase">手本となるフレーズ</param>
    public void RefreshPhrase(Phrase _phrase)
    {
        stepNumber = -1;
        modelPhrase = _phrase;
    }

    /// <summary>
    /// 手本となるフレーズと照合
    /// </summary>
    public int MatchingWithModelDance()
    {
        //ボタンが押されたか。押されたときのみ照合したい
        bool pushed = false;
        //ボタンが押されたときのステップ
        StepDirection myStep = StepDirection.NoStep;

        if (Input.GetMouseButtonDown(0))
        {
            //左
            myStep = StepDirection.LeftStep;
            animator.SetTrigger("LeftDance");
            pushed = true;

            stepNumber++;
        }
        else if (Input.GetMouseButtonDown(1))
        {
            //右
            myStep = StepDirection.RightStep;
            animator.SetTrigger("RightDance");
            pushed = true;

            stepNumber++;
        }

        //照合の必要が無い範囲を除外
        if (stepNumber >= modelPhrase.stepTable.Count ||
            stepNumber < 0)
        {
            return 0;
        }

        //今行ったステップと手本(model)のステップを照合
        if (pushed)
        {
            if (myStep == modelPhrase.stepTable[stepNumber])
            {
                Debug.Log("matchStep");
                if (stepNumber + 1 == modelPhrase.stepTable.Count - 1)
                {
                    return 1;
                }
            }
            else
            {
                return -1;
            }
        }
        return 0;
    }

    /// <summary>
    /// ダンスに成功し喜ぶアニメーションを再生
    /// </summary>
    public void HappyDance()
    {
        animator.SetTrigger("Happy");
    }
}
