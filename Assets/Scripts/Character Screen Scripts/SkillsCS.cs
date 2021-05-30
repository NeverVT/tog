using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsCS : MonoBehaviour
{
    void Update()
    {
        if (CharacterScreen.characterName == "Urp")
        {
            if (transform.Find("Ability One").Find(PlayerPrefs.GetString("UrpSkillOne")) != null)
            {
                transform.Find("Ability One").Find(PlayerPrefs.GetString("UrpSkillOne")).gameObject.SetActive(true);
            }
            if (transform.Find("Ability Two").Find(PlayerPrefs.GetString("UrpSkillTwo")) != null)
            {
                transform.Find("Ability Two").Find(PlayerPrefs.GetString("UrpSkillTwo")).gameObject.SetActive(true);
            }
        }
        if (CharacterScreen.characterName == "Chrisa")
        {
            if (transform.Find("Ability One").Find(PlayerPrefs.GetString("ChrisaSkillOne")) != null)
            {
                transform.Find("Ability One").Find(PlayerPrefs.GetString("ChrisaSkillOne")).gameObject.SetActive(true);
            }
            if (transform.Find("Ability Two").Find(PlayerPrefs.GetString("ChrisaSkillTwo")) != null)
            {
                transform.Find("Ability Two").Find(PlayerPrefs.GetString("ChrisaSkillTwo")).gameObject.SetActive(true);
            }
        }
        if (CharacterScreen.characterName == "Kurtzle")
        {
            if (transform.Find("Ability One").Find(PlayerPrefs.GetString("KurtzleSkillOne")) != null)
            {
                transform.Find("Ability One").Find(PlayerPrefs.GetString("KurtzleSkillOne")).gameObject.SetActive(true);
            }
            if (transform.Find("Ability Two").Find(PlayerPrefs.GetString("KurtzleSkillTwo")) != null)
            {
                transform.Find("Ability Two").Find(PlayerPrefs.GetString("KurtzleSkillTwo")).gameObject.SetActive(true);
            }
        }
    }
}
