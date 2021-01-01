using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ToDisplay : MonoBehaviour
{
    /// <summary>
    /// スケールを全て0にする
    /// 0にする前にScoreDisplayerがスケールを保存するためこの関数が必要
    /// </summary>
    public void ResetScale()
    {
        this.transform.localScale = new Vector3(0, 0, 0);
    }

    /// <summary>
    /// この画像のスケールを返させる
    /// </summary>
    /// <param name="_changeScale">目標サイズ</param>
    /// <param name="_time">かける時間</param>
    public void ScaleChange(Vector3 _changeScale,float _time)
    {
        this.transform.DOScale(_changeScale, _time);

    }
}
