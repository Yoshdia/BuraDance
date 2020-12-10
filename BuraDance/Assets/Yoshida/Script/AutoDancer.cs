using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDancer : MonoBehaviour
{
    /// <summary>
    /// アニメーター
    /// </summary>
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// 踊る
    /// </summary>
    /// <param name="_phrase">踊らせたいフレーズ</param>
    public void Dance(Phrase _phrase)
    {
        StartCoroutine("AutoDance", _phrase);
    }

    /// <summary>
    /// ステップをステップ間隔分待たせながらすべて再生させる
    /// </summary>
    /// <param name="_phrase">再生させるフレーム</param>
    /// <returns></returns>
    public IEnumerator AutoDance(Phrase _phrase)
    {
        //ステップの間隔　全体時間/回数
        float stepInterval = (float)_phrase.phraseTime / ((float)_phrase.stepCount);

        //ステップ情報を基にアニメーションさせる
        foreach (var step in _phrase.stepTable)
        {
            //左
            if (step == StepDirection.LeftStep)
            {
                animator.SetTrigger("LeftDance");
                Debug.Log("Left",this.gameObject);
            }
            //右
            else if (step == StepDirection.RightStep)
            {
                animator.SetTrigger("RightDance");
                Debug.Log("Right");
            }
            //ステップの間隔を待つ
            // ループ
            while (stepInterval > 0)
            {
                // frameで指定したフレームだけループ
                yield return null;
                stepInterval -= 0.016f;
            }
            if (step != StepDirection.NoStep)
            {
                stepInterval = (float)_phrase.phraseTime / ((float)_phrase.stepCount );
            }
        }
    }

    /// <summary>
    /// 待機ダンスを始めさせる
    /// </summary>
    public void StartIdleDance()
    {
        animator.SetBool("StartDance",true);
    }

    /// <summary>
    /// ダンスに成功し喜ぶアニメーションを再生
    /// </summary>
    public void HappyDance()
    {
        animator.SetTrigger("Happy");
    }

}

