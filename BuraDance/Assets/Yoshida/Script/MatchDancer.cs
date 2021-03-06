﻿using System.Collections;
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

    /// <summary>
    /// 右にステップしたときのエフェクト再生位置
    /// </summary>
    [SerializeField]
    Transform throwRightPosition;

    /// <summary>
    /// 左にステップしたときのエフェクト再生位置
    /// </summary>
    [SerializeField]
    Transform throwLeftPosition=null;

    /// <summary>
    /// 右にステップしたときのエフェクト
    /// </summary>
    [SerializeField]
    GameObject throwRightObject=null;

    /// <summary>
    /// 左にステップしたときのエフェクト
    /// </summary>
    [SerializeField]
    GameObject throwLeftObject = null;

    AudioSource audioSource = null;

    [SerializeField]
    AudioClip stepSoundClip;

    void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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
        StepDirection myStep = InputDance(ref pushed);

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

    public StepDirection InputDance(ref bool _pushed)
    {
        if (Input.GetMouseButtonDown(0))
        {
            //左
            animator.SetTrigger("LeftDance");
            _pushed = true;

            stepNumber++;
            //左方向にステップエフェクトを再生しこのオブジェクトの親を親にする
            GameObject bura = Instantiate(throwLeftObject, throwLeftPosition.position, Quaternion.identity);
            bura.transform.SetParent(this.transform);
            audioSource.PlayOneShot(stepSoundClip);
            return StepDirection.LeftStep;
        }
        else if (Input.GetMouseButtonDown(1))
        {
            //右
            animator.SetTrigger("RightDance");
            _pushed = true;

            stepNumber++;
            //右方向にステップエフェクトを再生しこのオブジェクトの親を親にする
            GameObject bura = Instantiate(throwRightObject, throwRightPosition.position, Quaternion.identity);
            bura.transform.SetParent(this.transform);
            audioSource.PlayOneShot(stepSoundClip);
            return StepDirection.RightStep;
        }
        return StepDirection.NoStep;
    }

    /// <summary>
    /// ダンスの結果によってトリガーをセットする
    /// </summary>
    /// <param name="_result">結果[1=成功,-1=失敗]</param>
    public void ResultDancing(int _result)
    {
        if (_result == 1 || _result == 2)
        {
            animator.SetTrigger("Happy");
        }
        else if (_result == -1)
        {
            animator.SetTrigger("Missed");
        }
    }


    /// <summary>
    /// 待機ダンスを始めさせる
    /// </summary>
    public void StartIdleDance()
    {
        animator.SetBool("StartDance", true);
    }
    
    /// <summary>
    /// 結果を表示した後に待機ダンスを再生させるトリガー
    /// 待機ダンスの再生に差を作らないため必要
    /// </summary>
    public void RestartDance()
    {
        animator.SetTrigger("Restart");
    }

    public void GameOverDance()
    {
        animator.SetTrigger("OverDance");
    }

    /// <summary>
    /// フィーバー中のダンス
    /// </summary>
    /// <param name="_frag"></param>
    public void SetFeaverDance(bool _frag)
    {
        animator.SetBool("FeaverDance", _frag);
    }
}
