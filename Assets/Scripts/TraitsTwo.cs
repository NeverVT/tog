using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitsTwo : MonoBehaviour
{
    void Update()
    {
        if(CharacterScreen.characterName == "Urp")
        {
            if(PlayerPrefs.GetString("UrpTraitTwo") != this.transform.name)
            {
                this.transform.gameObject.SetActive(false);
            }
        }
        if (CharacterScreen.characterName == "Chrisa")
        {
            if (PlayerPrefs.GetString("ChrisaTraitTwo") != this.transform.name)
            {
                this.transform.gameObject.SetActive(false);
            }
        }
        if (CharacterScreen.characterName == "Kurtzle")
        {
            if (PlayerPrefs.GetString("KurtzleTraitTwo") != this.transform.name)
            {
                this.transform.gameObject.SetActive(false);
            }
        }
    }
}
