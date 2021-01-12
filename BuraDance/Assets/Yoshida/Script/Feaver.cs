using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feaver : MonoBehaviour
{
    int count = 0;

    int score = 0;

    private void OnEnable()
    {
        count = 0;
        score = 0;
    }
    
    // Update is called once per frame
    void Update()
    {
        count++;
        if(count>100)
        {
            this.gameObject.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            score += 10;
        }
    }

    public int GetScore()
    {
        Debug.Log("Plus:"+score);
        return score;
    }
}
