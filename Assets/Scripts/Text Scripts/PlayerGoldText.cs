using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGoldText : MonoBehaviour
{
    void Update()
    {
        this.GetComponent<TextMesh>().text = ScoreControl.playerGold.ToString();
    }
}
