using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    
    void Start()
    {
        transform.GetChild(0).GetComponent<TextMesh>().text = ScoreControl.healthScore.ToString();
        transform.GetChild(1).GetComponent<TextMesh>().text = ScoreControl.goldScore.ToString();
        transform.GetChild(2).GetComponent<TextMesh>().text = ScoreControl.swordScore.ToString();
        transform.GetChild(3).GetComponent<TextMesh>().text = ScoreControl.goblinScore.ToString();
        transform.GetChild(4).GetComponent<TextMesh>().text = ScoreControl.bossScore.ToString();
        transform.GetChild(5).GetComponent<TextMesh>().text = ScoreControl.totalScore.ToString();
        ScoreControl.addPayment(ScoreControl.goldScore);
    }

  
}
