using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDancer : MonoBehaviour
{
    /// <summary>
    /// アニメーター
    /// </summary>
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// 踊る
    /// </summary>
    /// <param name="_phrase">踊らせたいフレーズ</param>
    public void Dance(Phrase _phrase)
    {
        StartCoroutine("WaitForFrame",  _phrase);
    }

  /// <summary>
  /// ステップをステップ間隔分待たせながらすべて再生させる
  /// </summary>
  /// <param name="_phrase">再生させるフレーム</param>
  /// <returns></returns>
    public IEnumerator WaitForFrame(Phrase _phrase)
    {
        //ステップの間隔　全体時間/回数
        float stepInterval = (float)_phrase.phraseTime / (float)_phrase.stepCount;

        //ステップ情報を基にアニメーションさせる
        foreach (var step in _phrase.stepTable)
        {
            //左
            if (step == StepDirection.LeftStep)
            {
                animator.SetTrigger("Left");
                Debug.Log("Left");
            }
            //右
            else if (step == StepDirection.RightStep)
            {
                animator.SetTrigger("Right");
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
            //stepInterval = (float)_phrase.phraseTime / (float)_phrase.stepCount;
        }

    }
}

