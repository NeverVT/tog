using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    void Update()
    {
       int total = ScoreControl.goldScore + ScoreControl.floorScore + (ScoreControl.goblinScore * 2) + (ScoreControl.bossScore * 10);
       this.GetComponent<TextMesh>().text = total.ToString();
    }
}
