using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armorGS : MonoBehaviour
{
    public CharacterControl characterControl;
    void Update()
    {
        this.transform.Find("Defense").GetComponent<TextMesh>().text = characterControl.characters[characterControl.activeCharacter].armor.defense.ToString(); //Set damage text
        this.transform.Find("Icon").GetComponent<SpriteRenderer>().sprite = characterControl.characters[characterControl.activeCharacter].armor.icon.GetComponent<SpriteRenderer>().sprite; //Set Sprite   
        if (characterControl.characters[characterControl.activeCharacter].armor.traitOne != null) //Turn on Attributes if they exists
        {
            this.transform.Find("Attribute One").transform.Find(characterControl.characters[characterControl.activeCharacter].armor.traitOne).gameObject.SetActive(true);
            if (characterControl.characters[characterControl.activeCharacter].armor.traitTwo != null)
            {
                this.transform.Find("Attribute Two").transform.Find(characterControl.characters[characterControl.activeCharacter].armor.traitTwo).gameObject.SetActive(true);
                if (characterControl.characters[characterControl.activeCharacter].armor.traitThree != null)
                {
                    this.transform.Find("Attribute Three").transform.Find(characterControl.characters[characterControl.activeCharacter].armor.traitThree).gameObject.SetActive(true);
                }
            }
        }

    }
}
