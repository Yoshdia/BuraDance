using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDancer : MonoBehaviour
{
    /// <summary>
    /// 踊る
    /// </summary>
    /// <param name="_phrase">踊らせたいフレーズ</param>
    public void Dance(Phrase _phrase)
    {
        Debug.Log("AutoDancer["+_phrase.stepCount);
    }
}
