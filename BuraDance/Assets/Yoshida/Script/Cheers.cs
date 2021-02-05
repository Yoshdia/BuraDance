using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 観客が送る声援クラス
/// </summary>
public class Cheers : MonoBehaviour
{
    /// <summary>
    /// 声プレハブ
    /// </summary>
    [SerializeField]
    List<GameObject> cheersVoices = new List<GameObject>();

    /// <summary>
    /// 声が出現する候補のTransform
    /// </summary>
    [SerializeField]
    List<Transform> voiceTransform = new List<Transform>();

    /// <summary>
    /// 観客が盛り上がっている(Feaver終了後でオーディエンスがいる)か
    /// </summary>
    bool exciting;

    /// <summary>
    /// 台詞の間隔
    /// </summary>
    float intervalVoice;

    [SerializeField]
    float IntervalVoice = 8;

    private void Start()
    {
        exciting = false;
        intervalVoice = IntervalVoice;
    }

    // Update is called once per frame
    void Update()
    {
        //経過時間が台詞の間隔を超えたか
        if (Time.time >= intervalVoice)
        {
            ///盛り上がっているときは間隔を狭くする
            int minus = 0;
            if (exciting)
            {
                minus = 5;
                //ランダムで追加で呼び出す
                if (Random.Range(0, 2) == 0)
                {
                    CreateCheersVoice();
                }
            }
            //乱数+定数で間隔を更新する
            intervalVoice = Time.time + Random.Range(0, 3) + (IntervalVoice - minus);
            CreateCheersVoice();
        }
    }

    /// <summary>
    /// 候補の座標から候補の声を発生させる
    /// </summary>
    void CreateCheersVoice()
    {
        int voiceNum = Random.Range(0, cheersVoices.Count);
        int positionNum = Random.Range(0, voiceTransform.Count);
        GameObject voice = Instantiate(cheersVoices[voiceNum], voiceTransform[positionNum]);
    }

    /// <summary>
    /// 盛り上がりを決める
    /// </summary>
    /// <param name="_exiting"></param>
    public void SetExciting(bool _exiting)
    {
        exciting = _exiting;
    }
}
