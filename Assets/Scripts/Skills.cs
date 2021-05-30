using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    public CharacterControl characterControl;

    public GameObject borderOne;
    public GameObject borderTwo;

    public GameObject[] skillsOne = new GameObject[6];
    public GameObject[] skillsTwo = new GameObject[6];

    public void setSkills()
    {
        for(int i=0;i<skillsOne.Length;i++)
        {
            if (characterControl.characters[characterControl.activeCharacter].skillOne != null)
            {
                if (characterControl.characters[characterControl.activeCharacter].skillOne == skillsOne[i].name)
                {
                    skillsOne[i].SetActive(true);
                }
            }
            if (characterControl.characters[characterControl.activeCharacter].skillTwo != null)
            {
                if (characterControl.characters[characterControl.activeCharacter].skillTwo == skillsTwo[i].name)
                {
                    skillsTwo[i].SetActive(true);
                }
            }
        }
    }

    private void Update()
    {
        if (characterControl.characters[characterControl.activeCharacter].skillOne == "Bomber Shot")
        {
            if (GameControl.bomberShot)
                borderOne.SetActive(true);
            else
                borderOne.SetActive(false);
        }
        else if (characterControl.characters[characterControl.activeCharacter].skillOne == "Hex Ball")
        {
            if (GameControl.hexBall)
                borderOne.SetActive(true);
            else
                borderOne.SetActive(false);
        }
        else if (characterControl.characters[characterControl.activeCharacter].skillOne == "Powder Keg")
        {
            if (GameControl.powderKeg)
                borderOne.SetActive(true);
            else
                borderOne.SetActive(false);
        }
        else
            borderOne.SetActive(false);

        if (characterControl.characters[characterControl.activeCharacter].skillTwo == "Double Shot")
        {
            if (GameControl.doubleShot > 0)
                borderTwo.SetActive(true);
            else
                borderTwo.SetActive(false);
        }
        else if (characterControl.characters[characterControl.activeCharacter].skillTwo == "Overpower")
        {
            if (GameControl.overpowered)
                borderTwo.SetActive(true);
            else
                borderTwo.SetActive(false);
        }
        else if (characterControl.characters[characterControl.activeCharacter].skillTwo == "Dragon Shot")
        {
            if (GameControl.dragonShot)
                borderTwo.SetActive(true);
            else
                borderTwo.SetActive(false);
        }
        else if (characterControl.characters[characterControl.activeCharacter].skillTwo == "Bomber Shot")
        {
            if (GameControl.bomberShot)
                borderTwo.SetActive(true);
            else
                borderTwo.SetActive(false);
        }
        else
            borderTwo.SetActive(false);
    }
}
