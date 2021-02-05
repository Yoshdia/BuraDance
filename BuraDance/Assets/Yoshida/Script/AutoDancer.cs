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
    RectTransform throwRightPosition;

    /// <summary>
    /// 左にステップしたときのエフェクト再生位置
    /// </summary>
    [SerializeField]
    RectTransform throwLeftPosition;

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

    /// <summary>
    /// 現在のフレーズ
    /// </summary>
    Phrase nowPhrase;

    /// <summary>
    /// ダンス中
    /// </summary>
    bool dancing;

    /// <summary>
    /// ステップの番号
    /// </summary>
    int stepNumber;

    /// <summary>
    /// ステップの間隔
    /// </summary>
    float stepInterval;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        dancing = false;
        stepNumber = 0;
        stepInterval = 0;
        nowPhrase = new Phrase();
    }

    private void Update()
    {
        //ダンス中
        if (dancing)
        {
            AutoStep();
        }
    }

    /// <summary>
    /// ダンスを開始させる
    /// </summary>
    /// <param name="_phrase"></param>
    public void RestartPhrase(Phrase _phrase)
    {
        dancing = true;
        nowPhrase = _phrase;
        stepNumber = 0;
        stepInterval = nowPhrase.phraseTime;
    }

    /// <summary>
    /// ダンスを停止させる
    /// </summary>
    public void StopPhrase()
    {
        dancing = false;
    }

    /// <summary>
    /// ステップをステップ間隔分待たせながらすべて再生させる
    /// </summary>
    void AutoStep()
    {
        //カウント終了
        if (stepInterval <= 0)
        {
            //ステップ情報を基にアニメーションさせる
            var step = nowPhrase.stepTable[stepNumber];
            //左
            if (step == StepDirection.LeftStep)
            {
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
                animator.SetTrigger("RightDance");
                //右方向にステップエフェクトを再生しこのオブジェクトの親を親にする
                GameObject bura = Instantiate(throwRightObject, throwRightPosition.position, Quaternion.identity);
                bura.transform.SetParent(this.transform);
                //サウンド再生
                audioSource.PlayOneShot(stepSoundClip);
            }

            //次のステップがある。終わりでないとき次のステップまでphraseTime待機させる
            if (step != StepDirection.NoStep)
            {
                stepInterval = nowPhrase.phraseTime;
            }
            else
            {
                dancing = false;
            }
            //次のステップへ
            stepNumber++;
        }
        else
        {
            stepInterval -= 0.01f;
            return;
        }
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
            dancing = false;
        }
        else if (_result == -1)
        {
            animator.SetTrigger("Missed");
            dancing = false;
        }
    }

    /// <summary>
    /// ダンスの処理を中断させる
    /// コルーチンを中断させるだけでアニメーションを切り替えさせるわけではない
    /// </summary>
    public void InterruptStopDance()
    {
        StopPhrase();
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

