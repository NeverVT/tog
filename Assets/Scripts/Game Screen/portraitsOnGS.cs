using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portraitsOnGS : MonoBehaviour
{
    public CharacterControl characterControl;
    void Update()
    {
        this.transform.Find(characterControl.selectedCharacter.GetComponent<Character>().characterName).gameObject.SetActive(true);
    }
}
