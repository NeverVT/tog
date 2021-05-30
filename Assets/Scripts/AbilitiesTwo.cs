using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AbilitiesTwo : MonoBehaviour
{
    public CharacterControl characterControl;
    void Update()
    { 
        if(SceneManager.GetActiveScene().name == "TitleScreen")
        {
            if (CharacterScreen.characterName == "Urp")
            {
                if (PlayerPrefs.GetString("UrpSkillTwo") != this.transform.name)
                {
                    this.transform.gameObject.SetActive(false);
                }
            }
            if (CharacterScreen.characterName == "Chrisa")
            {
                if (PlayerPrefs.GetString("ChrisaSkillTwo") != this.transform.name)
                {
                    this.transform.gameObject.SetActive(false);
                }
            }
            if (CharacterScreen.characterName == "Kurtzle")
            {
                if (PlayerPrefs.GetString("KurtzleSkillTwo") != this.transform.name)
                {
                    this.transform.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            if(characterControl != null)
            {
                if (characterControl.characters[characterControl.activeCharacter].skillTwo == this.transform.name)
                {
                    this.transform.gameObject.SetActive(true);
                }
                else
                {
                    this.transform.gameObject.SetActive(false);
                }
            }           
        }
    }
}
