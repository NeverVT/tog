using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    
    void Start()
    {
        //transform.Find("Floors").GetComponent<TextMesh>().text = ScoreControl.floorScore.ToString();
        transform.Find("Gold").GetComponent<TextMesh>().text = ScoreControl.goldScore.ToString();
        transform.Find("Goblins").GetComponent<TextMesh>().text = ScoreControl.goblinScore.ToString();
        transform.Find("Bosses").GetComponent<TextMesh>().text = ScoreControl.bossScore.ToString();
        transform.Find("Total").GetComponent<TextMesh>().text = ScoreControl.totalScore.ToString();
        ScoreControl.addPayment(ScoreControl.totalScore);
    }

  
}
