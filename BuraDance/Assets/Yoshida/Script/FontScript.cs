using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FontScript : MonoBehaviour
{
    //public int SpriteNumber; //入れるための番号を設置
    public GameObject TextDisplay; //表示するためのテキストを指定
            
    void Update()
    {
 
    }

    public void SetSpriteNumber(int _num)
    {
        string SpriteText = _num.ToString();
        TextDisplay.GetComponent<TextMeshProUGUI>().text = "";
        for (int i = 0; i <= SpriteText.Length - 1; i++)
        {
            TextDisplay.GetComponent<TextMeshProUGUI>().text += "<sprite=" + SpriteText[i] + ">";
        }
    }
}
