//----------------------------------------------------------------------------------------+
// チュートリアルのダンス入力処理 (左右クリック・左右タッチで対応したポーズ再生)
// 2020/12/12 YutaroOno
//----------------------------------------------------------------------------------------+
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class TutorialDanceInput : MonoBehaviour
{

    [SerializeField]
    TutorialInputManager inputManager;    // inputManagerクラス<ScriptableObejcts>
    private Animator animator;            // Animatorハンドル


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();    // Animator取得
        inputManager = GameObject.Find("TutorialInputManager").GetComponent<TutorialInputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // クリックに応じたアニメーションの再生
        PlayLeftPose(inputManager.GetLeftTrigger());
        PlayRightPose(inputManager.GetRightTrigger());
    }


    /// <summary>
    /// 左クリック時にポーズ(左)アニメーションのトリガーをオン
    /// </summary>
    /// <param name="in_Lclick">左クリックしたかどうか</param>
    void PlayLeftPose(bool in_Lclick)
    {
        if(in_Lclick)
        {
            animator.SetTrigger("LeftClick");
        }
    }

    /// <summary>
    /// 右クリック時にポーズ(右)アニメーションのトリガーをオン
    /// </summary>
    /// <param name="in_Rclick">右クリックしたかどうか</param>
    void PlayRightPose(bool in_Rclick)
    {
        if (in_Rclick)
        {
            animator.SetTrigger("RightClick");
        }
    }
}
