using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultSceneTransition : MonoBehaviour
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
    /// タイトルシーンへの遷移
    /// </summary>
    public void ToTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }

    /// <summary>
    /// ゲームシーンへの遷移
    /// </summary>
    public void ToGameScene()
    {
        SceneManager.LoadScene("GameMain");
    }
}
