using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 指定された時間を超えると削除されるオブジェクト
/// </summary>
public class DestroyInTime : MonoBehaviour
{
    int time = 0;
    int TimeMax = 50;

    private void Start()
    {
        time = 0;
    }

    private void OnEnable()
    {
        time = 0;
    }

    private void Update()
    {
        time ++;
        if(time>TimeMax)
        {
            Destroy(this.gameObject);
        }
    }

}
