using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portraitsOffGS : MonoBehaviour
{
    public CharacterControl characterControl;
    void FixedUpdate()
    {
        if (characterControl.selectedCharacter.GetComponent<Character>().characterName != this.transform.name)
            this.gameObject.SetActive(false);
    }
}
