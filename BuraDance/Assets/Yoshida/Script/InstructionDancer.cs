﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ダンサーたちに指示を送るクラス
/// </summary>
public class InstructionDancer : MonoBehaviour
{
    /// <summary>
    /// ゲーム本編が開始したかどうか
    /// 開幕の演出を管理するときに使う
    /// </summary>
    bool startedDance = false;

    /// <summary>
    /// ステップの最大数、増加する
    /// </summary>
    [SerializeField]
    private int stepCountMax = 3;

    /// <summary>
    /// フレーズの最短時間
    /// </summary>
    [SerializeField]
    private float phraseTimeMax = 0.4f;

    /// <summary>
    /// Phraseの情報に合わせお手本として踊るダンサー達
    /// Inspectorから追加する必要あり
    /// </summary>
    [SerializeField]
    List<AutoDancer> autoDancers = new List<AutoDancer>();

    /// <summary>
    /// ダンスを真似するクラス
    /// </summary>    
    [SerializeField]
    MatchDancer matchDancer = default;

    /// <summary>
    /// AutoDancerの処理が終わったフラグ
    /// </summary>
    bool endAutoDance = false;

    /// <summary>
    /// フレーズを終わらせるときに建つフラグ
    /// プレイヤーの判定が終わり成否結果を出すまでの間使われるフラグだった
    /// </summary>
    bool intervalLastDancing = false;

    /// <summary>
    /// プレイヤーの判定が終わり成否結果を出すまでの間隔 
    /// これが無いとプレイヤーの最後のダンスが再生されない
    /// </summary>
    [SerializeField]
    float IntervalLastDance = 0.15f;

    /// <summary>
    /// 結果を表示しているときに建つフラグ
    /// </summary>
    bool intervalResultDancing = false;
    /// <summary>
    /// 結果を表示している時間
    /// </summary>
    [SerializeField]
    float IntervalResultDance = 0.20f;

    /// <summary>
    /// 1フレーズが終わったときに建つフラグ
    /// プレイヤーの判定が終わった後のエフェクト再生や次のフレーズに移行する処理を書くために必要
    /// </summary>
    bool intervalRestartDancing = false;

    /// <summary>
    /// フレーズ間のインターバル
    /// </summary>
    [SerializeField]
    public float IntervalRestartDance = 0.3f;

    /// <summary>
    /// ダンスの結果を所持
    /// </summary>
    int danceResult = 0;

    /// <summary>
    /// スコアを表示させるためのクラス
    /// </summary>
    ScoreDisplayer scoreDisplayer;

    /// <summary>
    /// ダンスが成功したときの加算スコア
    /// </summary>
    [SerializeField]
    int successPlusScore = 40;

    /// <summary>
    /// ダンス・ゲーム本編の開始
    /// </summary>
    public void StartDance()
    {
        startedDance = true;
        //待機ダンス再生
        foreach (var dancer in autoDancers)
        {
            dancer.StartIdleDance();
        }
        matchDancer.StartIdleDance();
        //ダンス開始まで待機
        StartCoroutine("IntervalRestartDancing", IntervalRestartDance);
    }

    private void Start()
    {
        startedDance = false;
        //フレームレート固定
        Application.targetFrameRate = 20;
        scoreDisplayer = GetComponent<ScoreDisplayer>();
    }

    private void Update()
    {
        //すべての処理が始まっているか。外部からStartDanceを呼ばれるとこのフラグが建つ
        if (!startedDance)
        {
            return;
        }

        //AutoDancer達の処理が終了しMatchDancerとのダンスを照合する(お手本が終わって入力を待つ
        if (endAutoDance)
        {
            //MatchDancerに踊らせその入力を取得
            danceResult = matchDancer.MatchingWithModelDance();
            //全て成功
            if (danceResult == 1)
            {
                SuccessDance();
            }
            //失敗
            else if (danceResult == -1)
            {
                FailDance();
            }
        }
        else
        {
            if (!intervalLastDancing && !intervalResultDancing)
            {
                //お手本が終了していないときに入力すると失敗になる
                bool noFlag = false;
                matchDancer.InputDance(ref noFlag);
                if (noFlag)
                {
                    danceResult = -1;
                    FailDance();
                }
            }
        }
    }

    /// <summary>
    /// ダンサーそれぞれに躍らせるフレーズを生成
    /// </summary>
    /// <returns>生成した構造体フレーズ</returns>
    Phrase CreatePhrase()
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

        //前回までのダンスを終わらせる
        endAutoDance = false;
        StopCoroutine("UseDancers");

        //踊らせる
        StartCoroutine("UseDancers", onePhrase);
    }

    /// <summary>
    /// １フレーズ分の時間をあけてそれぞれに指示を送る
    /// </summary>
    /// <param name = "_onePhrase" > 踊らせたいフレーズ </ param >
    /// < returns ></ returns >
    IEnumerator UseDancers(Phrase _onePhrase)
    {
        //frameに代入するための一時的な定数
        float phraseTime = (float)_onePhrase.phraseTime;
        //増減する待機用の変数
        float frame = phraseTime;

        //AutoDancerに指示した回数
        int dancerCount = 0;

        //一人目からフレーズを渡し踊らせ、１フレーズ分の時間が経過すると次のダンサーに踊らせる
        foreach (var dancer in autoDancers)
        {
            dancer.Dance(_onePhrase);
            //１フレーズ分待たせる    
            while (frame > 0)
            {
                // frameで指定したフレームだけループ
                yield return null;
                frame -= 0.01f;
            }
            frame = phraseTime;
            dancerCount++;
            //最後のAutoDancerに踊らせるとき待機時間を１ステップ分減らす(お手本の最後のダンスが終わった直後に入力を許可するため
            if (dancerCount == autoDancers.Count - 1)
            {
                frame -= ((float)_onePhrase.phraseTime / ((float)_onePhrase.stepCount));
            }
        }
        //ダンスを真似するクラスにフレーズを渡す
        matchDancer.RefreshPhrase(_onePhrase);
        //AutoDancerの処理が終了
        endAutoDance = true;
        Debug.Log("DanceEnd");
    }

    /// <summary>
    /// プレイヤーが最後のダンスを終えて結果を待つ時間
    /// この時間無いと最後のダンスが表示される前に結果が出てしまう
    /// </summary>
    /// <param name="_interval">時間</param>
    /// <returns></returns>
    IEnumerator IntervalLastDancing(float _interval)
    {
        //増減する待機用変数
        float interval = _interval;
        intervalLastDancing = true;
        while (interval > 0)
        {
            yield return null;
            interval -= 0.01f;
        }
        intervalLastDancing = false;

        //結果を表示させる
        foreach (var dancer in autoDancers)
        {
            dancer.ResultDancing(danceResult);
        }
        matchDancer.ResultDancing(danceResult);
        //結果を表示する時間
        StartCoroutine("IntervalResultDancing", IntervalResultDance);
    }

    /// <summary>
    /// 結果を表示する時間
    /// これにより結果表示後に産まれていたアニメーションの再生速度などによるズレがなくなる
    /// </summary>
    /// <param name="_interval"></param>
    /// <returns></returns>
    IEnumerator IntervalResultDancing(float _interval)
    {
        //増減する待機用変数
        float interval = _interval;
        intervalResultDancing = true;
        while (interval > 0)
        {
            yield return null;
            interval -= 0.01f;
        }
        intervalResultDancing = false;

        foreach (var dancer in autoDancers)
        {
            dancer.RestartDance();
        }
        matchDancer.RestartDance();
        //次のダンスまでの時間
        StartCoroutine("IntervalRestartDancing", IntervalRestartDance);
    }

    /// <summary>
    /// 結果の表示から次のダンスまでの間隔
    /// </summary>
    /// <param name="_interval">時間</param>
    /// <returns></returns>
    IEnumerator IntervalRestartDancing(float _interval)
    {
        //増減する待機用変数
        float interval = _interval;
        intervalRestartDancing = true;
        while (interval > 0)
        {
            yield return null;
            interval -= 0.01f;
        }
        intervalRestartDancing = false;

        //フレーズ生成
        Instruction();
    }

    /// <summary>
    /// 手本通りのダンスに成功した処理
    /// </summary>
    void SuccessDance()
    {
        CommonEndDance();
        Debug.Log("Dance Clear!");
        scoreDisplayer.PlusScore(successPlusScore);
    }

    /// <summary>
    /// 手本通りのダンスに失敗
    /// </summary>
    void FailDance()
    {
        CommonEndDance();
        Debug.Log("Dance Missed...");
        foreach (var dancer in autoDancers)
        {
            dancer.InterruptStopDance();
        }
    }

    /// <summary>
    /// ダンスの成功･失敗時の共通処理
    /// 成否の演出はIntervalLastDancing内で
    /// </summary>
    void CommonEndDance()
    {
        endAutoDance = false;
        StopCoroutine("UseDancers");
        StartCoroutine("IntervalLastDancing", IntervalLastDance);
    }
}
