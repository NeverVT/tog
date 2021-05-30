using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillsOneGS : MonoBehaviour
{
    public CharacterControl characterControl;
    public GameObject border;
    void Update()
    {
        if(characterControl.characters[characterControl.activeCharacter].skillOne != null)
        {
            transform.Find((characterControl.characters[characterControl.activeCharacter].skillOne)).gameObject.SetActive(true);
            if (characterControl.characters[characterControl.activeCharacter].skillOne == "Bomber Shot")
            {
                if(GameControl.bomberShot)
                    border.SetActive(true);
                else
                    border.SetActive(false);
            }
            else if (characterControl.characters[characterControl.activeCharacter].skillOne == "Hex Ball")
            {
                if(GameControl.hexBall)
                    border.SetActive(true);
                else
                    border.SetActive(false);
            }
            else if (characterControl.characters[characterControl.activeCharacter].skillOne == "Powder Keg")
            {
                if (GameControl.powderKeg)
                    border.SetActive(true);
                else
                    border.SetActive(false);
            }           
        }
    }
}
