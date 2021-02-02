using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 生成されるとランダムな方向に移動する
/// </summary>
public class RandomMove : MonoBehaviour
{
    /// <summary>
    /// 移動量
    /// </summary>
    Vector3 velocity;

    void Start()
    {
        //移動方向を乱数で
        velocity = new Vector3(Random.Range(0, 2) * 0.1f, Random.Range(0,2) * 0.1f, 0);
    }

    void Update()
    {
        transform.position += velocity;
    }
}
