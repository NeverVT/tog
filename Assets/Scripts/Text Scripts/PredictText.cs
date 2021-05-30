using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredictText : MonoBehaviour
{
    public GameObject background;

    public int pHealth;
    public int pGold;
    public int pDamage;

    void Update()
    {
        if(pHealth > 0)
        {
            this.GetComponent<TextMesh>().text = pHealth.ToString();
            background.SetActive(true);
        }
        else if (pGold > 0 && pDamage > 0)
        {
            this.GetComponent<TextMesh>().text = (pGold/2).ToString();
            background.SetActive(true);
        }
        else if (pGold > 0)
        {
            this.GetComponent<TextMesh>().text = pGold.ToString();
            background.SetActive(true);
        }
        else if (pDamage > 0)
        {
            this.GetComponent<TextMesh>().text = pDamage.ToString();
            background.SetActive(true);
        }
        else
        {
            this.GetComponent<TextMesh>().text = "";
            background.SetActive(false);
        }
    }
}
