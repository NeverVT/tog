using UnityEngine;
using System.Collections;
using System;

public class Character : MonoBehaviour
{
    public Sprite[] portraits;  

    public string characterName;
    public string tribe;
    public double maxHealth;
    public double currentHealth;
    public CharacterWeapon weapon;
    public CharacterArmor armor;
    public string traitOne;
    public string traitTwo;
    public string skillOne;
    public string skillTwo;

    private void Awake()
    {
        for (int i = 0; i < portraits.Length; i++)
        {
            if (characterName == portraits[i].name)
            {
                this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = portraits[i];
                weapon.icon.GetComponent<SpriteRenderer>().sprite = weapon.weapons[i];
                armor.icon.GetComponent<SpriteRenderer>().sprite = armor.armors[i];
            }
        }

    }
}