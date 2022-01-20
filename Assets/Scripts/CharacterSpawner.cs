using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public GameObject[] characters;
    public void spawnCharacter()
    {
        for(int i = 0; i < characters.Length; i++)
        {
            if(characters[i].name == PlayerPrefs.GetString("selectedCharacter"))
            {
                Instantiate(characters[i]);
            }
        }
    }
       
}
