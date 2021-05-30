using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portraitsOnGS : MonoBehaviour
{
    public CharacterControl characterControl;
    void Update()
    {
        this.transform.Find(characterControl.characters[characterControl.activeCharacter].characterName).gameObject.SetActive(true);
    }
}
