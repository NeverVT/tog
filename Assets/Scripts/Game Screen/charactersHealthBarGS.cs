using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charactersHealthBarGS : MonoBehaviour
{
    public CharacterControl characterControl;
    public GameObject bar;
    float healthPercent;
    private int character;
    void Update()
    {
        if (this.transform.parent.transform.parent.name == "CharacterOne")
            character = 0;
        else if (this.transform.parent.transform.parent.name == "CharacterTwo")
            character = 1;
        else if (this.transform.parent.transform.parent.name == "CharacterThree")
            character = 2;

        healthPercent = (float)characterControl.selectedCharacter.GetComponent<Character>().currentHealth / (float)characterControl.selectedCharacter.GetComponent<Character>().maxHealth;
        if (float.IsNaN(healthPercent))
            healthPercent = 1;
        else
            bar.transform.localScale = new Vector3(healthPercent, bar.transform.localScale.y, bar.transform.localScale.z);
    }
}
