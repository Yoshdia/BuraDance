using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDancer : MonoBehaviour
{
    /// <summary>
    /// 踊る
    /// </summary>
    /// <param name="_phrase">踊らせたいフレーズ</param>
    public void Dance(Phrase _phrase)
    {
        //踊らせたいアニメーター
        Animator animator = GetComponent<Animator>();
        //ステップの間隔　全体時間/回数
        float stepInterval = (float)_phrase.phraseTime / (float)_phrase.stepCount;

        //Debug.Log(_phrase.stepTable.Count);
        //ステップ情報を基にアニメーションさせる
        foreach (var step in _phrase.stepTable)
        {
            if (step == StepDirection.LeftStep)
            {
                animator.SetTrigger("Left");
            }
            else if (step == StepDirection.RightStep)
            {
                animator.SetTrigger("Right");
            }
            //ステップの間隔を待つ
            StartCoroutine("WaitForFrame", stepInterval);
            Debug.Log("待機終わり");
        }
    }

    /// <summary>
    /// _frame分待つ
    /// </summary>
    /// <param name="_frame">待ちたいフレーム数</param>
    /// <returns></returns>
    public IEnumerator WaitForFrame(float _frame)
    {
        // ループ
        while (_frame > 0)
        {
            // frameで指定したフレームだけループ
            yield return null;
            Debug.Log(_frame);
            _frame -= 0.16f;
        }
    }
}

