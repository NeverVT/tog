using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponGS : MonoBehaviour
{
    public CharacterControl characterControl;
    public GameObject damage;
    public GameObject icon;
    public GameObject attributeOne;
    public GameObject attributeTwo;
    public GameObject attributeThree;
    public GameObject tooltip;
    void Update()
    {
        damage.GetComponent<TextMesh>().text = characterControl.selectedCharacter.GetComponent<Character>().weapon.damage.ToString(); //Set damage text
        tooltip.transform.GetChild(1).GetComponent<TextMesh>().text = characterControl.selectedCharacter.GetComponent<Character>().weapon.damage.ToString(); //Set damage value in tooltip
        icon.GetComponent<SpriteRenderer>().sprite = characterControl.selectedCharacter.GetComponent<Character>().weapon.GetComponent<CharacterWeapon>().icon.GetComponent<SpriteRenderer>().sprite; //Set Sprite  
        //tooltip.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = characterControl.selectedCharacter.GetComponent<Character>().weapon.icon.GetComponent<SpriteRenderer>().sprite; //Set the sprite in the tooltip
        if (characterControl.selectedCharacter.GetComponent<Character>().weapon.traitOne != null ) //Turn on Attributes if they exists
        {
            attributeOne.transform.Find(characterControl.selectedCharacter.GetComponent<Character>().weapon.traitOne).gameObject.SetActive(true);
            if(characterControl.selectedCharacter.GetComponent<Character>().weapon.traitTwo != null)
            {
                attributeTwo.transform.Find(characterControl.selectedCharacter.GetComponent<Character>().weapon.traitTwo).gameObject.SetActive(true);
                if(characterControl.selectedCharacter.GetComponent<Character>().weapon.traitThree != null)
                {
                    attributeThree.transform.Find(characterControl.selectedCharacter.GetComponent<Character>().weapon.traitThree).gameObject.SetActive(true);
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
