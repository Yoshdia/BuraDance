using System.Collections;
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
    /// ゲームが終了したかどうか
    /// </summary>
    bool endDance = false;

    /// <summary>
    /// ステップの最大数、増加する
    /// </summary>
    [SerializeField]
    private int stepCountMax = 3;

    /// <summary>
    /// フレーズの最短時間
    /// </summary>
    [SerializeField]
    float intervalStep = 0.08f;

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
    /// お手本のダンスが終わった後入力を受け付けているか
    /// </summary>
    bool intervalInputLimit = false;

    /// <summary>
    /// ゲームが始まってからダンサー達が立ち上がるまでの時間
    /// </summary>
    [SerializeField]
    float intervalStandUpDancers = 0.01f;

    /// <summary>
    /// ダンサー達が立ち上がりIdleモーション後から最初のダンスまでのイントロ
    /// </summary>
    [SerializeField]
    float intervalFirstDance = 1.15f;

    /// <summary>
    /// お手本のダンスが終わった後入力を受け付ける時間
    /// 0になると失敗になる
    /// </summary>
    [SerializeField]
    float IntervalInputLimit = 0.2f;

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
    float IntervalLastDance = 0.08f;

    /// <summary>
    /// 結果を表示しているときに建つフラグ
    /// </summary>
    bool intervalResultDancing = false;
    /// <summary>
    /// 結果を表示している時間
    /// </summary>
    [SerializeField]
    float IntervalResultDance = 0.01f;

    [SerializeField]
    float IntervalResultDanceLong = 0.12f;

    /// <summary>
    /// フレーズ間のインターバル
    /// </summary>
    [SerializeField]
    float IntervalRestartDance = 0.3f;

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
    int successPlusScore = 100;

    /// <summary>
    /// ゲームオーバー後ダンサーたちが転倒のアニメーションを再生する間隔
    /// </summary>
    [SerializeField]
    float IntervalGameOverFall = 0.13f;

    /// <summary>
    /// 残りの失敗していい数　残機
    /// </summary>
    [SerializeField]
    int hitPoint = 0;

    /// <summary>
    /// 残り残機の最大数 
    /// hitPointの初期化用
    /// </summary>
    [SerializeField]
    int HitPointMax = 3;

    /// <summary>
    /// 直接的に最終スコアに影響はなく、ダンスが成功するたびにスコアと並行で増え最大になるとまたゼロになるフィーバー用スコア
    /// </summary>
    int shortScoreGauge = 0;

    /// <summary>
    /// shortScoreGaugeの最大値
    /// </summary>
    [SerializeField]
    int ShortScoreMax = 2;

    /// <summary>
    /// フィーバー状態か。
    /// フィーバー中はこのクラスの更新を止めさせる必要がある
    /// </summary>
    bool feaver = false;
    FeaversOwner feaversOwner;

    /// <summary>
    /// ゲーム開始のカーテン、関数呼び出しや
    /// ゲーム終了のカーテン、関数呼び出しを行わせるアニメーター
    /// </summary>
    Animator animator;

    /// <summary>
    /// シーン遷移を行うクラス
    /// </summary>
    [SerializeField]
    SceneTransition sceneTransitioner;

    /// <summary>
    /// フィーバ終了時にアクティブ化させる観客
    /// </summary>
    [SerializeField]
    GameObject audienceObject;

    /// <summary>
    /// フィーバー成功時に発生する花吹雪のエフェクト
    /// </summary>
    [SerializeField]
    GameObject sakuraFeaver;

    /// <summary>
    /// 体力を表示する
    /// </summary>
    [SerializeField]
    HitPointDisplayer hitPointDisplayer;

    /// <summary>
    /// 観客が送る声援
    /// </summary>
    [SerializeField]
    Cheers cheersAudience;

    /// <summary>
    /// 今のフレーズ
    /// </summary>
    Phrase nowPhrase;

    /// <summary>
    /// ダンサー同士の間隔
    /// nowPhrase.stepInterval*nowPhrase.stepCountがフレーズ生成の度に計算される
    /// </summary>
    float dancerInterval;

    /// <summary>
    /// ダンサー同士の間隔をカウントする変数
    /// dancerIntervalでリセットされる
    /// </summary>
    float interval;

    /// <summary>
    /// 指示を送っているダンサーの番号
    /// </summary>
    int dancerNumber;

    /// <summary>
    /// 指示を送っているか
    /// </summary>
    bool instructuioning;

    /// <summary>
    /// イントロ中か　イントロ中の入力を制限する
    /// </summary>
    bool timeInIntro;

    /// <summary>
    /// ダンス・ゲーム本編の開始 このオブジェクトにアタッチされているAnimationから呼ばれる
    /// </summary>
    public void StartDance()
    {
        startedDance = true;
        hitPointDisplayer.UpdateDisplay(hitPoint);
        //待機ダンス再生
        foreach (var dancer in autoDancers)
        {
            dancer.StartIdleDance();
        }
        matchDancer.StartIdleDance();
        //ダンス開始まで待機
        StartCoroutine("GameStartPlayAnimation", 0.2f);
    }

    private void Awake()
    {
        feaversOwner = GetComponent<FeaversOwner>();
        scoreDisplayer = GetComponent<ScoreDisplayer>();
        animator = GetComponent<Animator>();

    }

    private void Start()
    {
        hitPoint = HitPointMax;
        shortScoreGauge = 0;
        feaver = false;
        cheersAudience.SetExciting(false);
        startedDance = false;
        audienceObject.SetActive(false);
        sakuraFeaver.SetActive(false);
        timeInIntro = true;
        //フレームレート固定
        Application.targetFrameRate = 20;
        instructuioning = false;
    }

    private void Update()
    {
        //すべての処理が始まっているか。終わっていないか。外部からStartDanceを呼ばれるとこのフラグが建つ
        if (!startedDance
            || endDance)
        {
            return;
        }

        //フィーバー中は処理を停止し結果を待つ
        if (feaver)
        {
            if (feaversOwner.EndFeavers())
            {
                //フィーバーのスコア
                int feaverScore = feaversOwner.GetFeaversScore();
                scoreDisplayer.PlusScore(feaverScore);

                //スコアがゼロだったとき失敗になる
                if (feaverScore > 0)
                {
                    //成功
                    danceResult = 2;
                    audienceObject.SetActive(true);
                    sakuraFeaver.SetActive(true);
                    cheersAudience.SetExciting(true);
                }
                else
                {
                    //失敗
                    danceResult = -1;
                    audienceObject.SetActive(false);
                    sakuraFeaver.SetActive(false);
                    cheersAudience.SetExciting(false);
                }

                //フィーバー用のゲージをリセット
                shortScoreGauge = 0;
                //停止
                feaver = false;

                //結果の処理へ
                StartCoroutine("IntervalLastDancing", 0);
            }
            else
            {
                return;
            }
        }

        //AutoDancer達の処理が終了しMatchDancerとのダンスを照合する(お手本が終わって入力を待つ
        if (endAutoDance)
        {
            //MatchDancerに踊らせその入力を取得
            danceResult = matchDancer.MatchingWithModelDance();
            //全て成功
            if (danceResult == 1 || danceResult == 2)
            {
                SuccessDance();
            }
            //失敗
            else if (danceResult == -1)
            {
                Debug.Log("normalMiss");
                FailDance();
            }
            //時間切れ　失敗
            else if (intervalInputLimit)
            {
                Debug.Log("timeOverMiss");
                danceResult = -1;
                FailDance();
            }
        }
        else
        {
            if (instructuioning)
            {
                InstructionDancers();
            }
            //結果が出る最後のステップ中と結果中、イントロ中は入力させない
            if (!intervalLastDancing && !intervalResultDancing && !timeInIntro)
            {
                //お手本が終了していないときに入力すると失敗になる
                bool noFlag = false;
                matchDancer.InputDance(ref noFlag);
                if (noFlag)
                {
                    Debug.Log("soFastMiss");
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
        return new Phrase(stepCount, intervalStep);
    }

    /// <summary>
    /// ダンサーたちに指示を送る
    /// </summary>
    public void Instruction()
    {
        timeInIntro = false;
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

        //踊らせる 定数倍率a*(ステップ数+(ステップ数*定数倍率b))
        dancerInterval = (float)onePhrase.phraseTime * ((float)onePhrase.stepCount+ ((float)onePhrase.stepCount*0.1f));
        interval = dancerInterval;
        dancerNumber = 0;
        nowPhrase = onePhrase;
        instructuioning = true;

    }

    /// <summary>
    /// ダンサーたちに指示を送る
    /// </summary>
    void InstructionDancers()
    {
        if (interval <= 0)
        {
            //全てのダンサーへ指示が終わったか
            if (dancerNumber == autoDancers.Count)
            {
                //プレイヤーにフレーズを渡す
                matchDancer.RefreshPhrase(nowPhrase);
                //プレイヤーのダンスを許可
                endAutoDance = true;
                //入力の時間制限を開始
                StartCoroutine("IntervalInputLimiter", IntervalInputLimit*nowPhrase.stepCount);
                //指示を終了
                instructuioning = false;
            }
            else
            {
                //指示を出す
                var dancer = autoDancers[dancerNumber];
                dancer.RestartPhrase(nowPhrase);
                dancerNumber++;
                interval = dancerInterval;
            }
        }
        else
        {
            interval -= 0.01f;
            return;
        }

    }

    /// <summary>
    /// お手本のダンスが終わった後入力を受け付ける時間
    /// </summary>
    /// <param name="_interval">時間</param>
    /// <returns></returns>
    IEnumerator IntervalInputLimiter(float _interval)
    {
        float interval = _interval;
        intervalInputLimit = false;
        while (interval > 0)
        {
            intervalInputLimit = false;
            yield return null;
            interval -= 0.01f;
        }
        intervalInputLimit = true;
    }

    /// <summary>
    /// プレイヤーが最後のダンスを終えて結果を待つ時間
    /// この時間無いと最後のダンスが表示される前に結果が出てしまう
    /// </summary>
    /// <param name="_interval">時間</param>
    /// <returns></returns>
    IEnumerator IntervalLastDancing(float _interval)
    {
        Debug.Log("LastDanceing");
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

        Debug.Log("LastDanced");
        //結果を表示する時間
        if (danceResult == 1)
        {
            StartCoroutine("IntervalResultDancing", IntervalResultDance);
        }
        //失敗、フィーバー成功時は長めにリザルトを表示する
        else if (danceResult == -1 || danceResult == 2)
        {
            StartCoroutine("IntervalResultDancing", IntervalResultDanceLong);
        }
    }

    /// <summary>
    /// 結果を表示する時間
    /// これにより結果表示後に産まれていたアニメーションの再生速度などによるズレがなくなる
    /// </summary>
    /// <param name="_interval"></param>
    /// <returns></returns>
    IEnumerator IntervalResultDancing(float _interval)
    {
        Debug.Log("Resulting");
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

        Debug.Log("Resulted");
        //shortScoreGaugeが一定以上なら特殊演出、そうでないなら通常通り繰り返す
        if (shortScoreGauge >= ShortScoreMax)
        {
            feaver = true;
            feaversOwner.ActiveFeaver();
        }
        else
        {
            //次のダンスまでの時間
            StartCoroutine("IntervalRestartDancing", IntervalRestartDance);

        }

    }

    /// <summary>
    /// 結果の表示から次のダンスまでの間隔
    /// </summary>
    /// <param name="_interval">時間</param>
    /// <returns></returns>
    IEnumerator IntervalRestartDancing(float _interval)
    {
        Debug.Log("ReStart Creating");
        //増減する待機用変数
        float interval = _interval;
        while (interval > 0)
        {
            yield return null;
            interval -= 0.01f;
        }

        if (!intervalLastDancing && !intervalResultDancing)
        {
            Debug.Log("Restart Created");
            //フレーズ生成
            Instruction();

        }
        else
        {
            Debug.Log("Restart Canceled");
        }
    }

    /// <summary>
    /// 手本通りのダンスに成功した処理
    /// </summary>
    void SuccessDance()
    {
        CommonEndDance();
        Debug.Log("Dance Clear!");
        scoreDisplayer.PlusScore(successPlusScore);
        shortScoreGauge++;


    }

    /// <summary>
    /// 手本通りのダンスに失敗
    /// </summary>
    void FailDance()
    {
        //残機を減らす
        hitPoint--;
        hitPointDisplayer.UpdateDisplay(hitPoint);
        StopCoroutine("IntervalLastDancing");
        StopCoroutine("IntervalResultDancing");
        StopCoroutine("IntervalRestartDancing");

        //ゲームオーバーか
        if (hitPoint > 0)
        {
            CommonEndDance();
            Debug.Log("Dance Missed...");
            foreach (var dancer in autoDancers)
            {
                dancer.InterruptStopDance();
            }
        }
        else
        {
            endDance = true;
            foreach (var dancer in autoDancers)
            {
                dancer.InterruptStopDance();
            }
            StartCoroutine("GameOverDance");
        }
        instructuioning = false;
        interval = 1000;
        dancerNumber = 1000;
    }

    /// <summary>
    /// ダンスの成功･失敗時の共通処理
    /// 成否の演出はIntervalLastDancing内で
    /// </summary>
    void CommonEndDance()
    {
        endAutoDance = false;
        StopCoroutine("IntervalInputLimiter");
        StartCoroutine("IntervalLastDancing", IntervalLastDance);
    }

    /// <summary>
    /// ゲームオーバーの演出
    /// プレイヤー側から順に倒れていくようにアニメーションを再生
    /// </summary>
    /// <returns></returns>
    IEnumerator GameOverDance()
    {
        float frame = IntervalGameOverFall;

        //倒れはじめ
        matchDancer.GameOverDance();
        //プレイヤー側から倒れる必要があるためリストの要素を反転
        //autoDancers.Reverse();

        for (int i = autoDancers.Count - 1; i >= 0; i--)
        {
            while (frame > 0)
            {
                yield return null;
                frame -= 0.01f;
            }
            autoDancers[i].GameOverDance();
            frame = IntervalGameOverFall;
        }

        animator.SetTrigger("Close");
        StartCoroutine("ChangeSceneLoading");
        sceneTransitioner.SetScore(scoreDisplayer.GetScore());
    }

    /// <summary>
    /// シーン移行までの待機
    /// </summary>
    /// <returns></returns>
    IEnumerator ChangeSceneLoading()
    {
        int frame = 30;
        while (frame > 0)
        {
            yield return null;
            frame--;
        }
        sceneTransitioner.ChangeScene();
    }

    /// <summary>
    /// 立ち上がるまでの待機
    /// </summary>
    /// <returns></returns>
    IEnumerator GameStartPlayAnimation()
    {
        float interval = intervalStandUpDancers;
        while (interval > 0)
        {
            yield return null;
            interval -= 0.01f;
        }

        foreach (var dancer in autoDancers)
        {
            dancer.RestartDance();
        }
        matchDancer.RestartDance();

        StartCoroutine("IntervalRestartDancing", intervalFirstDance);
    }
}
