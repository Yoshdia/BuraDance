//---------------------------------------------------------------------------------+
// ダンスの失敗数をカウントし、一定回数以上でリザルトへ遷移させる
// 2021 Yutaro Ono.
//---------------------------------------------------------------------------------+
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailureCounter : MonoBehaviour
{
    /// <summary>
    /// ダンス失敗数の上限
    /// </summary>
    public const int failLimit = 3;
    public int failNum;

    bool fail;
    bool success;

    [SerializeField]
    SceneTransition scene;
    [SerializeField]
    MatchDancer matchDancer;
    [SerializeField]
    Animator animator;
    [SerializeField]
    SaveScore scoreSaver;
    // Start is called before the first frame update
    void Start()
    {
        failNum = 0;
        fail = false;
        success = false;
        animator = matchDancer.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // ダンス失敗を検知したら失敗数をカウントアップ
        // ダンスアニメーターの名前に応じて成功・失敗判定
        // アニメーションの情報取得
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        // 再生中のクリップ名
        string clipName = clipInfo[0].clip.name;
        if (clipName == "Wither_Ko" && !fail)
        {
            Debug.Log("失敗");
            failNum++;
            fail = true;
        }
        else if(clipName != "Wither_Ko")
        {
            fail = false;
        }
        // 成功時スコア加算
        if (clipName == "Happy_Ko" && !success)
        {
            Debug.Log("スコア追加");
            scoreSaver.AddScore(40);
            success = true;
        }
        else if(clipName != "Happy_Ko")
        {
            success = false;
        }

        // 失敗数上限になったら次のシーンへ
        if (failNum == failLimit)
        {
            scene.ChangeScene();
        }
    }
}
