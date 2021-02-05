using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitPointDisplayer : MonoBehaviour
{
    /// <summary>
    /// HitPointを表示させるための画像達
    /// </summary>
    [SerializeField]
    GameObject[] lifeImages = new GameObject[3];

    /// <summary>
    /// 体力表示の更新
    /// </summary>
    /// <param name="_hitPoint"></param>
    public void UpdateDisplay(int _hitPoint)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        //　現在の体力数分のライフゲージを作成
        for (int i = 0; i < _hitPoint; i++)
        {
            GameObject lifeImage;
            if (_hitPoint == 1)
            {
                lifeImage = lifeImages[0];
            }
            else if (_hitPoint == 2)
            {
                lifeImage = lifeImages[1];
            }
            else
            {
                lifeImage = lifeImages[2];
            }
            Instantiate<GameObject>(lifeImage, transform);
        }
    }

    public void NoDisplay()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
