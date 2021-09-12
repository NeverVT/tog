using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpellText : MonoBehaviour
{
	void Update ()
    {
        if (GetComponentInParent<Spell>().coolDown > 0)
            this.GetComponent<TextMeshProUGUI>().text = GetComponentInParent<Spell>().coolDown.ToString();
        else
        {
            this.GetComponent<TextMeshProUGUI>().text = "";
            //this.GetComponentInParent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
        }
            
    }
}
