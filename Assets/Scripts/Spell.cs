using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Spell : MonoBehaviour
{
    public GameObject gameScript;
    public GameObject tooltip;
    public string spellType;
    public int coolDown = 0;
    public int turnStart = 0;
    public int turnEnd = 0;
    private void Start()
    {
        gameScript = GameObject.Find("GameScript");
    }
    private void Update()
    {
        if (coolDown == 0)
        {
            this.transform.Find("Text").gameObject.SetActive(false);
            //this.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
            //gameScript.GetComponent<GameScript>().spellsOnCD.Remove(this.gameObject);
        }          
    }

    public void triggerTooltip()
    {
        tooltip.SetActive(true);
    }

    public void turnOffTooltip()
    {
        tooltip.SetActive(false);
    }

    public void triggerSpell()
    {
        tooltip.SetActive(false);
        gameScript.GetComponent<GameScript>().castSpell(this.gameObject);
    }
}
