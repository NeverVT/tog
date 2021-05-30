using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUpdateText : MonoBehaviour
{
    public static string damageUpdateText = "";

	void Update ()
    {
        this.GetComponent<TextMesh>().text = damageUpdateText;

        
        if (damageUpdateText != "")
            this.transform.GetChild(0).gameObject.SetActive(true);
        else
            this.transform.GetChild(0).gameObject.SetActive(false);
            
    }
}
