using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellText : MonoBehaviour
{
	void Update ()
    {
        if (GetComponentInParent<Spell>().coolDown > 0)
            this.GetComponent<TextMesh>().text = GetComponentInParent<Spell>().coolDown.ToString();
        else
        {
            this.GetComponent<TextMesh>().text = "";
            this.GetComponentInParent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
        }
            
    }
}
