using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheers : MonoBehaviour
{
    [SerializeField]
    List<GameObject> cheersVoices = new List<GameObject>();

    [SerializeField]
    List<Transform> voicePosition = new List<Transform>();

    bool exciting;

    float intervalVoice;

    private void Start()
    {
        exciting = false;
        intervalVoice = 8;

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= intervalVoice)
        {
            int minus = 0;
            if (exciting)
            {
                minus = 5;
                if (Random.Range(0, 2) == 0)
                {
                    CreateCheersVoice();
                }
            }
            intervalVoice = Time.time + Random.Range(0, 3) + (8 - minus);
            CreateCheersVoice();
        }
    }

    void CreateCheersVoice()
    {
        int voiceNum = Random.Range(0, cheersVoices.Count);
        int positionNum = Random.Range(0, voicePosition.Count);
        Transform tra = voicePosition[positionNum];
        GameObject voice = Instantiate(cheersVoices[voiceNum], voicePosition[positionNum]);
    }

    public void SetExciting(bool _exiting)
    {
        exciting = _exiting;
    }
}
