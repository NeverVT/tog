using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public GameObject gameScript;
    public int coolDown = 0;
    public int turnStart = 0;
    public int turnEnd = 0;
    private void Update()
    {
        if (turnEnd - gameScript.GetComponent<GameScript>().turnCounter < 0)
            coolDown = 0;
        else
            coolDown = turnEnd - gameScript.GetComponent<GameScript>().turnCounter;
        if (turnEnd == gameScript.GetComponent<GameScript>().turnCounter)
        {
            this.transform.Find("Text").gameObject.SetActive(false);
            this.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
            gameScript.GetComponent<GameScript>().spellsOnCD.Remove(this.gameObject);
        }
            
    }

}
