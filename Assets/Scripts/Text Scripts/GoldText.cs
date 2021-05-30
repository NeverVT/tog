using UnityEngine;
using System.Collections;
using System;

public class GoldText : MonoBehaviour
{
    void Update()
    {
        double gold = Math.Round(GameControl.gold);
        this.GetComponent<TextMesh>().text = gold.ToString();
    }
}
