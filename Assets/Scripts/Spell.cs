using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Spell : MonoBehaviour
{
    public GameObject tooltip;
    public string spellType;
    public int coolDown = 0;
    public int turnStart = 0;
    public int turnEnd = 0;

    private void Update()
    {
        if (coolDown == 0)
        {
            this.transform.Find("Text").gameObject.SetActive(false);
            //gameScript.GetComponent<GameScript>().spellsOnCD.Remove(this.gameObject);
        }          
    }
    public void triggerSpell()
    {
        tooltip.SetActive(false);
        GameControl.gameScript.castSpell(this.gameObject);
    }
}
