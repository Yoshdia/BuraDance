//--------------------------------------------------------------+
// シーン遷移スクリプト (全シーン共通で使用する)
// 2020/12/12 YuatroOno
//--------------------------------------------------------------+
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{

    private bool nextScene = false;     // 次のシーンへ遷移するか
    /// <summary>
    /// 次のシーンへの遷移フラグをオン
    /// </summary>
    public void SetNextScene()
    {
        nextScene = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // シーン遷移のフラグが立っている
        // 左クリック・画面タッチで次のシーンへ遷移
        if(nextScene && Input.GetMouseButton(0))
        {
            ChangeScene();
        }
    }

    /// <summary>
    /// シーン遷移処理
    /// 現在のシーン名を取得し、対応する次のシーンをロードする
    /// </summary>
    public void ChangeScene()
    {
        Scene scene = SceneManager.GetActiveScene();

        if(scene.name == "TitleScene")
        {
            SceneManager.LoadScene("TutorialScene");
        }
            
        if(scene.name == "TutorialScene")
        {
            SceneManager.LoadScene("GameScene");
        }

        if(scene.name == "GameScene")
        {
            SceneManager.LoadScene("ResultScene");
        }

        if (scene.name == "ResultScene")
        {
            SceneManager.LoadScene("TitleScene");
        }

    }
}
