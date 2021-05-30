using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitsCS : MonoBehaviour
{
    void Update()
    {
        if(CharacterScreen.characterName == "Urp")
        {
            if(transform.Find("Trait One").Find(PlayerPrefs.GetString("UrpTraitOne")) != null)
            {
                transform.Find("Trait One").Find(PlayerPrefs.GetString("UrpTraitOne")).gameObject.SetActive(true);
            }
            if(transform.Find("Trait Two").Find(PlayerPrefs.GetString("UrpTraitTwo")) != null)
            {
                transform.Find("Trait Two").Find(PlayerPrefs.GetString("UrpTraitTwo")).gameObject.SetActive(true);
            }
        }
        else if(CharacterScreen.characterName == "Chrisa")
        {
            if(transform.Find("Trait One").Find(PlayerPrefs.GetString("ChrisaTraitOne")) != null)
            {
                transform.Find("Trait One").Find(PlayerPrefs.GetString("ChrisaTraitOne")).gameObject.SetActive(true);
            }
            if(transform.Find("Trait Two").Find(PlayerPrefs.GetString("ChrisaTraitTwo")) != null)
            {
                transform.Find("Trait Two").Find(PlayerPrefs.GetString("ChrisaTraitTwo")).gameObject.SetActive(true);
            }
        }
        else if (CharacterScreen.characterName == "Kurtzle")
        {
            if (transform.Find("Trait One").Find(PlayerPrefs.GetString("KurtzleTraitOne")) != null)
            {
                transform.Find("Trait One").Find(PlayerPrefs.GetString("KurtzleTraitOne")).gameObject.SetActive(true);
            }
            if (transform.Find("Trait Two").Find(PlayerPrefs.GetString("KurtzleTraitTwo")) != null)
            {
                transform.Find("Trait Two").Find(PlayerPrefs.GetString("KurtzleTraitTwo")).gameObject.SetActive(true);
            }
        }
    }
}
