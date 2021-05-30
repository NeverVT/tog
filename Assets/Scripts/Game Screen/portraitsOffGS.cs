using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portraitsOffGS : MonoBehaviour
{
    public CharacterControl characterControl;
    void FixedUpdate()
    {
        if (characterControl.characters[characterControl.activeCharacter].characterName != this.transform.name)
            this.gameObject.SetActive(false);
    }
}
