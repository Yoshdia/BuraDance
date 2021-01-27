using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{
    [SerializeField]
    GameObject sakuraBomb=null;
    public void CallBombEffect()
    {
        sakuraBomb.SetActive(false);
        sakuraBomb.SetActive(true);
    }
}
