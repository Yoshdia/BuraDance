using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeaversOwner : MonoBehaviour
{
    /// <summary>
    /// フィーバー共通の演出オブジェクト
    /// </summary>
    [SerializeField]
    GameObject FeaverEffects;

    /// <summary>
    /// フィーバー時の黒い背景
    /// </summary>
    [SerializeField]
    GameObject FeaverBlack;

    /// <summary>
    /// フィーバー達
    /// フィーバー毎にここから抽選で選ばれる
    /// </summary>
    [SerializeField]
    Feaver[] feavers = null;

    /// <summary>
    /// feaversを抽選、停止させるための引数
    /// </summary>
    int activeFeaverNumber = 0;

    /// <summary>
    /// FeaverEffects、feaversオブジェクトを全て非アクティブに
    /// </summary>
    void Start()
    {
        foreach (Feaver feaver in feavers)
        {
            feaver.gameObject.SetActive(false);
        }
        FeaverEffects.SetActive(false);
        FeaverBlack.SetActive(false);
    }

    /// <summary>
    /// 所有しているフィーバーからランダムでアクティブ化する
    /// </summary>
    public void ActiveFeaver()
    {
        ///乱数
        int rand = Random.Range(1, feavers.Length) - 1;

        if (feavers[activeFeaverNumber] != null)
        {
            //演出を開始
            FeaverEffects.SetActive(true);
            FeaverBlack.SetActive(true);
            //フィーバー開始
            feavers[activeFeaverNumber].gameObject.SetActive(true);
            //アクティブ化したfeaversの引数保存
            activeFeaverNumber = rand;
        }
    }

    /// <summary>
    /// フィーバーが終わったか
    /// 一つでもアクティブなオブジェクトがあるとfalseを返す
    /// </summary>
    /// <returns>フィーバーが終わったか</returns>
    public bool EndFeavers()
    {
        foreach (Feaver feaver in feavers)
        {
            if (feaver.gameObject.activeInHierarchy == true)
            {
                return false;
            }
        }
        FeaverEffects.SetActive(false);
        FeaverBlack.SetActive(false);
        return true;
    }

    /// <summary>
    /// フィーバーが終わった時に成果を返す
    /// </summary>
    /// <returns>フィーバーの成果スコア</returns>
    public int GetFeaversScore()
    {
        return feavers[activeFeaverNumber].GetScore();
    }
}
