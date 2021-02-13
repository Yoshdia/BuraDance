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
    [SerializeField]
    public int score;

    private bool nextScene = false;     // 次のシーンへ遷移するか

    /// <summary>
    /// 次のシーンへの遷移フラグをオン
    /// </summary>
    public void SetNextScene()
    {
        nextScene = true;
    }

    public void SetScore(int _score)
    {
        score = _score;
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
        if (nextScene && Input.GetMouseButton(0))
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

        if (scene.name == "TitleScene")
        {
            SceneManager.LoadScene("TutorialScene");
        }

        if (scene.name == "TutorialScene")
        {
            SceneManager.LoadScene("GameMain");
        }

        //if(scene.name == "GameScene")
        if (scene.name == "GameMain")
        {
            SceneManager.sceneLoaded += GameSceneLoaded;
            SceneManager.LoadScene("ResultScene");
        }

        if (scene.name == "ResultScene")
        {
            SceneManager.LoadScene("TitleScene");
        }

    }

    /// <summary>
    /// スコアの受け渡しをしたいシーンのロード前にSceneManager.sceneLoadedに+することでLoad時にこの関数が呼ばれる
    /// </summary>
    /// <param name="nextScene"></param>
    /// <param name="mode"></param>
    void GameSceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        var gameManager = GameObject.FindWithTag("SceneManager").GetComponent<SceneTransition>();

        gameManager.score = score;

        SceneManager.sceneLoaded -= GameSceneLoaded;
    }



    //-------------------------------------------------------
    // 各シーンへの遷移関数
    //-------------------------------------------------------
    /// <summary>
    /// タイトル
    /// </summary>
    public void ToTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }
    /// <summary>
    /// ゲームシーン
    /// </summary>
    public void ToGameMain()
    {
        SceneManager.LoadScene("GameMain");
    }
    /// <summary>
    /// リザルトシーン
    /// </summary>
    public void ToResultScene()
    {
        SceneManager.LoadScene("ResultScene");
    }
    /// <summary>
    /// チュートリアルシーン
    /// </summary>
    public void ToTutorialScene()
    {
        SceneManager.LoadScene("TutorialScene");
    }
    /// <summary>
    /// 広告シーン
    /// </summary>
    public void ToAdScene()
    {
        SceneManager.LoadScene("AdScene");
    }
}
