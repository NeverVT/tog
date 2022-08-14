using UnityEngine;
using System.Collections;
using System;

public class PredictDamageText : MonoBehaviour
{		
	void Update ()
    {
        if (this.GetComponentInParent<Enemy>().predictedDamage == 0)
            this.GetComponent<TextMesh>().text = "";
        else if (this.gameObject.transform.parent.name == "Skull(Clone)")
        {
            if(Enemy.currentHealth <= (Math.Ceiling(this.GetComponentInParent<Enemy>().predictedDamage / 2)))
            {
                this.GetComponent<TextMesh>().text = "X";
            }
            else
            {
                this.GetComponent<TextMesh>().text = ((int)(Math.Ceiling(this.GetComponentInParent<Enemy>().predictedDamage / 2))).ToString();
            }
        }
        else if (Enemy.currentHealth <= (this.GetComponentInParent<Enemy>().predictedDamage))
            this.GetComponent<TextMesh>().text = "X";
        else
            this.GetComponent<TextMesh>().text = (this.GetComponentInParent<Enemy>().predictedDamage).ToString();
    }
}
