using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    public static int score;
    private int currentScore;
    void FixedUpdate()
    {
        if (currentScore < score)
            currentScore++;
        this.GetComponent<TextMesh>().text = currentScore.ToString();
    }
   
}
