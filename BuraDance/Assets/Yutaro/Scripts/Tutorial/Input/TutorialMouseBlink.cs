//--------------------------------------------------------------+
// チュートリアル時マウスUIのクリックによる明滅処理
// 2020/1/4 YuatroOno
//--------------------------------------------------------------+
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMouseBlink : MonoBehaviour
{

    [SerializeField]
    TutorialInputManager inputManager;    // inputManagerクラス<ScriptableObejcts>
    
    public GameObject leftClickEff;
    public GameObject rightClickEff;


    // Start is called before the first frame update
    void Start()
    {
        leftClickEff = GameObject.Find("Mouse_LeftClickBlink");
        rightClickEff = GameObject.Find("Mouse_RightClickBlink");
        inputManager = GameObject.Find("TutorialInputManager").GetComponent<TutorialInputManager>();

    }

    // Update is called once per frame
    void Update()
    {

        OnClickActive(leftClickEff, Input.GetMouseButton(0));
        OnClickActive(rightClickEff, Input.GetMouseButton(1));

    }

    /// <summary>
    /// クリック時に光るエフェクトを表示する
    /// クリックされていないときは表示しない
    /// </summary>
    /// <param name="in_obj"></param>表示するオブジェクト
    /// <param name="in_flg"></param>表示するかどうか
    void OnClickActive(GameObject in_obj, bool in_flg)
    {
        if (in_flg)
        {
            in_obj.SetActive(true);
        }
        else
        {
            in_obj.SetActive(false);
        }
    }
}
