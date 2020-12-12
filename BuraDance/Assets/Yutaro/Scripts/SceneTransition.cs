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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    }
}
