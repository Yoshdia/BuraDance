using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// フィーバー基本クラス(派生は未制作
/// </summary>
public class Feaver : MonoBehaviour
{
    /// <summary>
    /// このオブジェクトを表示し続ける時間
    /// </summary>
    int lifeCount = 0;

    /// <summary>
    /// このフィーバーのスコア
    /// </summary>
    int feaversScore = 0;

    /// <summary>
    /// 画面クリック時に生成されるエフェクト
    /// </summary>
    [SerializeField]
    GameObject RendaEffect;

    private void OnEnable()
    {
        lifeCount = 0;
        feaversScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        lifeCount++;
        if (lifeCount > 100)
        {
            this.gameObject.SetActive(false);
        }

        //右･左クリック
        if (Input.GetMouseButtonDown(0)||Input.GetMouseButtonDown(1))
        {
            feaversScore += 10;
            //振動しているように見せるためクリックの度に指定された範囲にランダムで移動する
            transform.position = new Vector3(Random.Range(0, 3 * 0.1f), Random.Range(0, 3 * 0.1f), 100);
            //エフェクトを再生
            GameObject efftct=Instantiate(RendaEffect,transform);
            efftct.transform.SetParent(transform);
        }

        //振動しているように見せるため初期座標に補正させる
        if (transform.position.x >= 0.01f)
        {
            transform.position += new Vector3(-0.1f, 0, 0);
        }
        if (transform.position.x <= -0.01f)
        {
            transform.position += new Vector3(+0.1f, 0, 0);
        }
        if (transform.position.y >= 0.01f)
        {
            transform.position += new Vector3(0, -0.1f, 0);
        }
        if (transform.position.y <= 0.01f)
        {
            transform.position += new Vector3(0, 0.1f, 0);
        }
    }

    /// <summary>
    /// このフィーバーで得たスコアを返す
    /// </summary>
    /// <returns></returns>
    public int GetScore()
    {
        return feaversScore;
    }
}
