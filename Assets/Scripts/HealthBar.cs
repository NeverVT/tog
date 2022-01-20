using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    public CharacterControl characterControl;
    public GameObject bar;
    float healthPercent;
	
	void Update ()
    {
        healthPercent = (float)characterControl.getCurrentHealth() / (float)characterControl.getMaxHealth();
        if (float.IsNaN(healthPercent))       
            healthPercent = 1;      
        else
            bar.transform.localScale = new Vector3(healthPercent, bar.transform.localScale.y, bar.transform.localScale.z);
	}
}
