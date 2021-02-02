using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 桜の爆弾のようなエフェクト(改名の必要
/// </summary>
public class Effects : MonoBehaviour
{
    /// <summary>
    /// このエフェクト
    /// </summary>
    [SerializeField]
    GameObject sakuraBomb=null;

    /// <summary>
    /// このエフェクトが再生される候補
    /// ランダムで決まる
    /// </summary>
    [SerializeField]
    List<GameObject> effectsPositions=new List<GameObject>();

    /// <summary>
    /// 桜の爆発のようなエフェクトを再生する
    /// </summary>
    public void CallBombEffect()
    {
        sakuraBomb.SetActive(false);
        //場所を候補から決定
        int rand = Random.Range(0, effectsPositions.Count);
        sakuraBomb.transform.position = effectsPositions[rand].transform.position;
        sakuraBomb.SetActive(true);
    }
}
