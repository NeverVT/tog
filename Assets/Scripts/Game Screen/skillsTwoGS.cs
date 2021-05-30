using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillsTwoGS : MonoBehaviour
{
    public CharacterControl characterControl;
    public GameObject border;
    void Update()
    {
        if((characterControl.characters[characterControl.activeCharacter].skillTwo) != null)
            transform.Find((characterControl.characters[characterControl.activeCharacter].skillTwo)).gameObject.SetActive(true);
        if (characterControl.characters[characterControl.activeCharacter].skillTwo == "Double Shot")
        {
            if (GameControl.doubleShot > 0)
                border.SetActive(true);
            else
                border.SetActive(false);
        }
        else if (characterControl.characters[characterControl.activeCharacter].skillTwo == "Overpower")
        {
            if (GameControl.overpowered)
                border.SetActive(true);
            else
                border.SetActive(false);
        }
        if (characterControl.characters[characterControl.activeCharacter].skillTwo == "Dragon Shot")
        {
            if (GameControl.dragonShot)
                border.SetActive(true);
            else
                border.SetActive(false);
        }
        else if (characterControl.characters[characterControl.activeCharacter].skillTwo == "Bomber Shot")
        {
            if (GameControl.bomberShot)
                border.SetActive(true);
            else
                border.SetActive(false);
        }
    }
}
