//------------------------------------------------------+
// チュートリアル用入力フラグ管理クラス
// 2020/12/12 YutaroOno
//------------------------------------------------------+
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialInputManager : MonoBehaviour
{

    // 左右トリガー (対応する入力が入った場合にオン)
    private bool LTrigger;
    private bool RTrigger;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 左
        if(Input.GetMouseButtonDown(0))
        {
            LTrigger = true;
        }
        else
        {
            LTrigger = false;
        }

        // 右
        if (Input.GetMouseButtonDown(1))
        {
            RTrigger = true;
        }
        else
        {
            RTrigger = false;
        }
    }

    /// <summary>
    /// 左クリック・タッチ入力ゲッター
    /// </summary>
    /// <returns></returns>
    public bool GetLeftTrigger()
    {
        return LTrigger;
    }

    /// <summary>
    /// 右クリック・タッチ入力ゲッター
    /// </summary>
    /// <returns></returns>
    public bool GetRightTrigger()
    {
        return RTrigger;
    }
}
