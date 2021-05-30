using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitsOne : MonoBehaviour
{

    void Update()
    {
        if (CharacterScreen.characterName == "Urp")
        {
            if (PlayerPrefs.GetString("UrpTraitOne") != this.transform.name)
            {
                this.transform.gameObject.SetActive(false);
            }
        }
        if (CharacterScreen.characterName == "Chrisa")
        {
            if (PlayerPrefs.GetString("ChrisaTraitOne") != this.transform.name)
            {
                this.transform.gameObject.SetActive(false);
            }
        }
        if (CharacterScreen.characterName == "Kurtzle")
        {
            if (PlayerPrefs.GetString("KurtzleTraitOne") != this.transform.name)
            {
                this.transform.gameObject.SetActive(false);
            }
        }
    }
}
