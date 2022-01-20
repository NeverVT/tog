using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armorGS : MonoBehaviour
{
    public CharacterControl characterControl;
    public GameObject defense;
    public GameObject icon;
    public GameObject attributeOne;
    public GameObject attributeTwo;
    public GameObject attributeThree;
    public GameObject tooltip;
    void Update()
    {
        defense.GetComponent<TextMesh>().text = characterControl.characters[characterControl.activeCharacter].armor.defense.ToString(); //Set damage text
        tooltip.transform.GetChild(1).GetComponent<TextMesh>().text = characterControl.characters[characterControl.activeCharacter].armor.defense.ToString();
        icon.GetComponent<SpriteRenderer>().sprite = characterControl.characters[characterControl.activeCharacter].armor.icon.GetComponent<SpriteRenderer>().sprite; //Set Sprite   
        tooltip.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = characterControl.characters[characterControl.activeCharacter].armor.icon.GetComponent<SpriteRenderer>().sprite;
        if (characterControl.characters[characterControl.activeCharacter].armor.traitOne != null) //Turn on Attributes if they exists
        {
            attributeOne.transform.Find(characterControl.characters[characterControl.activeCharacter].armor.traitOne).gameObject.SetActive(true);
            if (characterControl.characters[characterControl.activeCharacter].armor.traitTwo != null)
            {
                attributeTwo.transform.Find(characterControl.characters[characterControl.activeCharacter].armor.traitTwo).gameObject.SetActive(true);
                if (characterControl.characters[characterControl.activeCharacter].armor.traitThree != null)
                {
                    attributeThree.transform.Find(characterControl.characters[characterControl.activeCharacter].armor.traitThree).gameObject.SetActive(true);
                }
            }
        }
    }
    public void turnOnTooltip()
    {
        tooltip.transform.gameObject.SetActive(true);
    }

    public void turnOffTooltip()
    {
        tooltip.transform.gameObject.SetActive(false);
    }
}
