using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FontScript : MonoBehaviour
{
    //public int SpriteNumber; //入れるための番号を設置
    public GameObject TextDisplay; //表示するためのテキストを指定

    [SerializeField]
    GameObject unit;

    Vector3 defaultPosition;

    private void Start()
    {
        unit.SetActive(false);
        defaultPosition = new Vector3(6.9f, 1.7f, 100);
    }
    void Update()
    {
 
    }

    public void SetSpriteNumber(int _num)
    {
        string SpriteText = _num.ToString();
        TextDisplay.GetComponent<TextMeshProUGUI>().text = "";
        ///桁
        int digits = 0;
        for (digits = 0; digits <= SpriteText.Length - 1; digits++)
        {
            TextDisplay.GetComponent<TextMeshProUGUI>().text += "<sprite=" + SpriteText[digits] + ">";
        }
        //桁-1回左にズラし数字がいくら増えても単位に文字が被らないようにする
        TextDisplay.transform.position = defaultPosition - new Vector3(0.6f * (digits - 1), 0, 0);
        unit.SetActive(true);
    }
}
