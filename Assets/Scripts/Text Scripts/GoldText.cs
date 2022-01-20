using UnityEngine;
using System.Collections;
using System;

public class GoldText : MonoBehaviour
{
    void Update()
    {
        double gold = Math.Round(GameControl.gold);
        this.GetComponent<TextMesh>().text = gold.ToString();
        if (GameControl.gold > 25)
            this.transform.GetChild(4).gameObject.SetActive(true);
        else if(GameControl.gold > 50)
        {
            this.transform.GetChild(4).gameObject.SetActive(true);
            this.transform.GetChild(5).gameObject.SetActive(true);
        }
        else
        {
            this.transform.GetChild(4).gameObject.SetActive(false);
            this.transform.GetChild(5).gameObject.SetActive(false);
        }
    }
}
