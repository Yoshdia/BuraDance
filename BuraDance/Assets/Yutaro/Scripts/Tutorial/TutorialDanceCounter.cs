//----------------------------------------------------------------------+
// チュートリアル画面でのダンスカウント
// 一定数ダンス(クリックorタッチ)すると、次のシーンへ
// 2021 1/5 YutaroOno.
//----------------------------------------------------------------------+
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDanceCounter : MonoBehaviour
{

    const int maxCount = 12;
    private int m_danceCount;

    [SerializeField]
    SceneTransition scene;

    // Start is called before the first frame update
    void Start()
    {
        m_danceCount = 0;
        scene = GameObject.Find("SceneManager").GetComponent<SceneTransition>();
    }

    // Update is called once per frame
    void Update()
    {
        // クリックまたはタッチでカウントアップ
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            m_danceCount++;
        }

        // 一定数ダンスすると次のシーンへ遷移
        if(m_danceCount >= maxCount)
        {
            scene.ChangeScene();
        }
    }
}
