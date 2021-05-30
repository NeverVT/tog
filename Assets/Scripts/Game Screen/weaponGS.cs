using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponGS : MonoBehaviour
{
    public CharacterControl characterControl;
    void Update()
    {
        this.transform.Find("Damage").GetComponent<TextMesh>().text = characterControl.characters[characterControl.activeCharacter].weapon.damage.ToString(); //Set damage text
        this.transform.Find("Icon").GetComponent<SpriteRenderer>().sprite = characterControl.characters[characterControl.activeCharacter].weapon.icon.GetComponent<SpriteRenderer>().sprite; //Set Sprite   
        if (characterControl.characters[characterControl.activeCharacter].weapon.traitOne != null ) //Turn on Attributes if they exists
        {
            this.transform.Find("Attribute One").transform.Find(characterControl.characters[characterControl.activeCharacter].weapon.traitOne).gameObject.SetActive(true);
            if(characterControl.characters[characterControl.activeCharacter].weapon.traitTwo != null)
            {
                this.transform.Find("Attribute Two").transform.Find(characterControl.characters[characterControl.activeCharacter].weapon.traitTwo).gameObject.SetActive(true);
                if(characterControl.characters[characterControl.activeCharacter].weapon.traitThree != null)
                {
                    this.transform.Find("Attribute Three").transform.Find(characterControl.characters[characterControl.activeCharacter].weapon.traitThree).gameObject.SetActive(true);
                }
            }
        }
        
    }
}
