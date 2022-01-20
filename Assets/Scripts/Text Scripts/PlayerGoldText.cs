using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGoldText : MonoBehaviour
{
    void Update()
    {
        if(this.transform.parent.name == "Coin icon")
            this.GetComponent<TextMesh>().text = ScoreControl.playerGold.ToString();

        if (this.transform.parent.name == "Shard icon")
            this.GetComponent<TextMesh>().text = PlayerPrefs.GetInt("PlayerShard").ToString();
    }
}
