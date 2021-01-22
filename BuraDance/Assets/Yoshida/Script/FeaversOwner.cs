using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeaversOwner : MonoBehaviour
{
    [SerializeField]
    Feaver[] feavers=null;

    int activeFeaverNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        //すべて非アクティブ
        foreach (Feaver feaver in feavers)
        {
            feaver.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 所有しているフィーバーからランダムでアクティブ化する
    /// </summary>
    public void ActiveFeaver()
    {
        int rand = Random.Range(1, feavers.Length) - 1;

        if (feavers[activeFeaverNumber] != null)
        {
            activeFeaverNumber = rand;
            feavers[activeFeaverNumber].gameObject.SetActive(true);
        }
    }

    /// <summary>
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
        return true;
    }

    public int GetFeaversScore()
    {
        return feavers[activeFeaverNumber].GetScore();
    }
}
