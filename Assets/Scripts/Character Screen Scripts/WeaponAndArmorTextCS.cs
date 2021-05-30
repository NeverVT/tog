using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAndArmorTextCS : MonoBehaviour
{
    void Update()
    {
        if(this.transform.parent.name == "Weapon")
            this.GetComponent<TextMesh>().text = CharacterScreen.weaponAttack.ToString();
        if (this.transform.parent.name == "Armor")
            this.GetComponent<TextMesh>().text = CharacterScreen.armorDefense.ToString();
    }
}
