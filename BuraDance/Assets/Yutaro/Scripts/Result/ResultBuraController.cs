using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultBuraController : MonoBehaviour
{
    [SerializeField]
    Animator m_buraAnim;
    [SerializeField]
    GameObject m_flowerObj;    // 花吹雪オブジェクト

    // Start is called before the first frame update
    void Start()
    {
        if(m_buraAnim == null)
        {
            m_buraAnim = GameObject.Find("Result_Bura").GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 上級判定時のぶらおアニメーション再生フラグをオン
    /// </summary>
    public void BuraAnimationNormal()
    {
        m_buraAnim.SetBool("Normal", true);
    }
    /// <summary>
    /// 特級判定時のぶらおアニメーション再生フラグをオン
    /// </summary>
    public void BuraAnimationGood()
    {
        m_buraAnim.SetBool("Good", true);
    }
    /// <summary>
    /// 最上級判定時のぶらおアニメーション再生フラグをオン
    /// </summary>
    public void BuraAnimationGreat()
    {
        m_buraAnim.SetBool("Great", true);
        m_flowerObj.SetActive(true);
    }

}
