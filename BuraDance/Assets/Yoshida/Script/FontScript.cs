using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FontScript : MonoBehaviour
{
    //public int SpriteNumber; //入れるための番号を設置
    public GameObject TextDisplay; //表示するためのテキストを指定

    int beforeScore;

    [SerializeField]
    GameObject unit;

    Vector3 defaultPosition;

    private void Start()
    {
        unit.SetActive(false);
        beforeScore = 0;
        defaultPosition = new Vector3(5.6f, 1.7f, 100);
    }
    void Update()
    {

    }

    public void SetSpriteNumber(int _num)
    {
        StopCoroutine("GrowScore");
        StartCoroutine("GrowScore", _num);
    }

    IEnumerator GrowScore(int _afterScore)
    {
        yield return new WaitForSeconds(1);
        unit.SetActive(true);

        for (; beforeScore <= _afterScore; beforeScore++)
        {
            string SpriteText = beforeScore.ToString();
            //初期化
            TextDisplay.GetComponent<TextMeshProUGUI>().text = "";

            ///桁
            int digits = 0;
            for (digits = 0; digits <= SpriteText.Length - 1; digits++)
            {
                TextDisplay.GetComponent<TextMeshProUGUI>().text += "<sprite=" + SpriteText[digits] + ">";
            }
            //桁-1回左にズラし数字がいくら増えても単位に文字が被らないようにする
            TextDisplay.transform.position = defaultPosition - new Vector3(0.6f * (digits - 1), 0, 0);

            yield return new WaitForSeconds(0.08f);
        }
    }
}
