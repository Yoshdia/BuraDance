using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDancer : MonoBehaviour
{
    /// <summary>
    /// アニメーター
    /// </summary>
    private Animator animator;

    /// <summary>
    /// 右にステップしたときのエフェクト再生位置
    /// </summary>
    [SerializeField]
    Transform throwRightPosition;

    /// <summary>
    /// 左にステップしたときのエフェクト再生位置
    /// </summary>
    [SerializeField]
    Transform throwLeftPosition;

    /// <summary>
    /// 右にステップしたときのエフェクト
    /// </summary>
    [SerializeField]
    GameObject throwRightObject;

    /// <summary>
    /// 左にステップしたときのエフェクト
    /// </summary>
    [SerializeField]
    GameObject throwLeftObject;

    /// <summary>
    /// オーディオソース
    /// </summary>
    AudioSource audioSource;

    /// <summary>
    /// ステップ用のサウンド
    /// </summary>
    [SerializeField]
    AudioClip stepSoundClip;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// 受け取ったフレーズで踊る
    /// </summary>
    /// <param name="_phrase">フレーズ</param>
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
        //ステップの間隔
        float stepInterval = (float)_phrase.phraseTime;

        //ステップ情報を基にアニメーションさせる
        foreach (var step in _phrase.stepTable)
        {
            //左
            if (step == StepDirection.LeftStep)
            {
                Debug.Log("Left", this.gameObject);

                animator.SetTrigger("LeftDance");
                //左方向にステップエフェクトを再生しこのオブジェクトの親を親にする
                GameObject bura = Instantiate(throwLeftObject, throwLeftPosition.position, Quaternion.identity);
                bura.transform.SetParent(this.transform);
                //サウンド再生
                audioSource.PlayOneShot(stepSoundClip);
            }
            //右
            else if (step == StepDirection.RightStep)
            {
                Debug.Log("Right");

                animator.SetTrigger("RightDance");
                //右方向にステップエフェクトを再生しこのオブジェクトの親を親にする
                GameObject bura = Instantiate(throwRightObject, throwRightPosition.position, Quaternion.identity);
                bura.transform.SetParent(this.transform);
                //サウンド再生
                audioSource.PlayOneShot(stepSoundClip);
            }
            //ステップの間隔を待つループ
            while (stepInterval > 0)
            {
                // frameで指定したフレームだけループ
                yield return null;
                stepInterval -= 0.01f;
            }
            //次のステップがある。終わりでないとき次のステップまでphraseTime待機させる
            if (step != StepDirection.NoStep)
            {
                stepInterval = (float)_phrase.phraseTime;
            }
        }
    }

    /// <summary>
    /// ダンスの結果によってトリガーをセットする
    /// </summary>
    /// <param name="_result">結果[1=成功,-1=失敗]</param>
    public void ResultDancing(int _result)
    {
        if (_result == 1)
        {
            animator.SetTrigger("Happy");
            StopCoroutine("AutoDance");
        }
        else if (_result == -1)
        {
            animator.SetTrigger("Missed");
            StopCoroutine("AutoDance");
        }
    }

    /// <summary>
    /// ダンスの処理を中断させる
    /// コルーチンを中断させるだけでアニメーションを切り替えさせるわけではない
    /// </summary>
    public void InterruptStopDance()
    {
        StopCoroutine("AutoDance");
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

    /// <summary>
    /// ゲームオーバー、転倒させる
    /// </summary>
    public void GameOverDance()
    {
        animator.SetTrigger("OverDance");
    }
}

