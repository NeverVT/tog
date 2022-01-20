using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class CharacterScreen : MonoBehaviour
{
    public CharacterControl characterControl;
    public GameObject[] characters;
    public GameObject characterName;
    public GameObject abilites;
    public Sprite[] abilitySprites;
    public GameObject traits;
    public Sprite[] traitSprites;
    public GameObject level;
    public GameObject equipmentUpgradePrompt;

    private int characterIndex;
    private GameObject currentEquipmentSelected;

    private void Start()
    {
        characterControl = GameObject.Find("CharacterControl").GetComponent<CharacterControl>();
    }

    void Update()
    {
        //Sets the character name text
        characterName.GetComponent<TextMesh>().text = characterControl.characterName.Replace("(Clone)", "").Trim(); 

        //Activates the proper character
        for (int i = 0; i < characters.Length; i++) 
        {
            if (characterControl.characterName.Replace("(Clone)", "").Trim() == characters[i].transform.name)
            {
                characters[i].transform.gameObject.SetActive(true);
                characterIndex = i;
            }
            else
                characters[i].transform.gameObject.SetActive(false);
        }

        //sets the character lvl text
        int currentLvl = 0;  
        if (characterControl.characterName.Replace("(Clone)", "").Trim() == "Urp")
            currentLvl = Team.urp;
        else if (characterControl.characterName.Replace("(Clone)", "").Trim() == "Chrisa")
            currentLvl = Team.chrisa;
        else if (characterControl.characterName.Replace("(Clone)", "").Trim() == "Kurtzle")
            currentLvl = Team.kurtzle;

        if (currentLvl == 1)
            level.GetComponent<TextMesh>().text = "1";
        else if (currentLvl == 2 || currentLvl == 3)
            level.GetComponent<TextMesh>().text = "2";
        else if (currentLvl >= 4 && currentLvl <= 6)
            level.GetComponent<TextMesh>().text = "3";
        else if (currentLvl == 7)
            level.GetComponent<TextMesh>().text = "4";
        else
            level.GetComponent<TextMesh>().text = "";

        //sets character's weapon text
        characters[characterIndex].transform.GetChild(0).transform.GetChild(2).GetComponent<TextMesh>().text = characterControl.weaponAttack.ToString();

        //sets character's armor text
        characters[characterIndex].transform.GetChild(1).transform.GetChild(2).GetComponent<TextMesh>().text = characterControl.armorDefense.ToString();


        //activates the selected characters currently active traits and skills
        if (characterControl.characterName == "Urp")
        {
            if (PlayerPrefs.GetString("UrpSkillOne") != null)
            {
                for (int i = 0; i < abilitySprites.Length; i++)
                {
                    if (PlayerPrefs.GetString("UrpSkillOne") == abilitySprites[i].name)
                    {
                        abilites.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
                        abilites.transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = abilitySprites[i];
                    }                   
                }             
            }
            if (PlayerPrefs.GetString("UrpSkillTwo") != null)
            {
                
                for (int i = 0; i < abilitySprites.Length; i++)
                {
                    if (PlayerPrefs.GetString("UrpSkillTwo") == abilitySprites[i].name)
                    {
                        abilites.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(true);
                        abilites.transform.GetChild(1).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = abilitySprites[i];
                    }                    
                }
            }
            if (PlayerPrefs.GetString("UrpTraitOne") != null)
            {             
                for (int i = 0; i < traitSprites.Length; i++)
                {
                    if (PlayerPrefs.GetString("UrpTraitOne") == traitSprites[i].name)
                    {
                        traits.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
                        traits.transform.GetChild(0).transform.GetChild(1).GetComponent<TextMesh>().text = PlayerPrefs.GetString("UrpTraitOne");
                        traits.transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = traitSprites[i];                      
                    }                      
                }
            }
            if (PlayerPrefs.GetString("UrpTraitTwo") != null)
            {
                
                for (int i = 0; i < traitSprites.Length; i++)
                {
                    if (PlayerPrefs.GetString("UrpTraitTwo") == traitSprites[i].name)
                    {
                        traits.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(true);
                        traits.transform.GetChild(1).transform.GetChild(1).GetComponent<TextMesh>().text = PlayerPrefs.GetString("UrpTraitTwo");
                        traits.transform.GetChild(1).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = traitSprites[i];
                    }                      
                }
            }
            
            if(Team.urp >= 2)
            {
                characters[characterIndex].transform.GetChild(4).transform.GetChild(0).gameObject.SetActive(true);
                characters[characterIndex].transform.GetChild(4).transform.GetChild(1).transform.GetComponent<TextMesh>().text = "";
            }
            if(Team.urp >= 3)
            {
                characters[characterIndex].transform.GetChild(5).transform.GetChild(0).gameObject.SetActive(true);
                characters[characterIndex].transform.GetChild(5).transform.GetChild(1).transform.GetComponent<TextMesh>().text = "";
                characters[characterIndex].transform.GetChild(6).transform.GetChild(0).gameObject.SetActive(true);              
                characters[characterIndex].transform.GetChild(6).transform.GetChild(1).transform.GetComponent<TextMesh>().text = "";
                if (PlayerPrefs.GetString("UrpSkillTwo") == characters[characterIndex].transform.GetChild(5).name)
                {                  
                    characters[characterIndex].transform.GetChild(5).transform.GetChild(0).transform.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
                    characters[characterIndex].transform.GetChild(6).transform.GetChild(0).transform.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .25f);
                }
                else
                {
                    characters[characterIndex].transform.GetChild(5).transform.GetChild(0).transform.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .25f);
                    characters[characterIndex].transform.GetChild(6).transform.GetChild(0).transform.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
                }
            }
            if(Team.urp == 4)
            {
                characters[characterIndex].transform.GetChild(7).transform.GetChild(0).gameObject.SetActive(true);
                characters[characterIndex].transform.GetChild(7).transform.GetChild(1).transform.GetComponent<TextMesh>().text = "";
                characters[characterIndex].transform.GetChild(8).transform.GetChild(0).gameObject.SetActive(true);
                characters[characterIndex].transform.GetChild(8).transform.GetChild(1).transform.GetComponent<TextMesh>().text = "";
                if (PlayerPrefs.GetString("UrpSkillTwo") == characters[characterIndex].transform.GetChild(7).name)
                {
                    characters[characterIndex].transform.GetChild(7).transform.GetChild(0).transform.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
                    characters[characterIndex].transform.GetChild(8).transform.GetChild(0).transform.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .25f);
                }
                else
                {
                    characters[characterIndex].transform.GetChild(7).transform.GetChild(0).transform.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .25f);
                    characters[characterIndex].transform.GetChild(8).transform.GetChild(0).transform.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
                }
            }        
        }
        if (characterControl.characterName == "Chrisa")
        {
            if (PlayerPrefs.GetString("ChrisaSkillOne") != null)
            {
                for (int i = 0; i < abilitySprites.Length; i++)
                {
                    if (PlayerPrefs.GetString("ChrisaSkillOne") == abilitySprites[i].name)
                    {
                        abilites.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
                        abilites.transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = abilitySprites[i];
                    }                      
                }
            }
            if (PlayerPrefs.GetString("ChrisaSkillTwo") != null)
            {
                for (int i = 0; i < abilitySprites.Length; i++)
                {
                    if (PlayerPrefs.GetString("ChrisaSkillTwo") == abilitySprites[i].name)
                    {
                        abilites.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(true);
                        abilites.transform.GetChild(1).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = abilitySprites[i];
                    }
                }
            }
            if (PlayerPrefs.GetString("ChrisaTraitOne") != null)
            {
                for (int i = 0; i < traitSprites.Length; i++)
                {
                    if (PlayerPrefs.GetString("ChrisaTraitOne") == traitSprites[i].name)
                    {
                        traits.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
                        traits.transform.GetChild(0).transform.GetChild(1).GetComponent<TextMesh>().text = PlayerPrefs.GetString("ChrisaTraitOne");
                        traits.transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = traitSprites[i];
                    }
                }
            }
            if (PlayerPrefs.GetString("ChrisaTraitTwo") != null)
            {             
                for (int i = 0; i < traitSprites.Length; i++)
                {
                    if (PlayerPrefs.GetString("ChrisaTraitTwo") == traitSprites[i].name)
                    {
                        traits.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(true);
                        traits.transform.GetChild(1).transform.GetChild(1).GetComponent<TextMesh>().text = PlayerPrefs.GetString("ChrisaTraitTwo");
                        traits.transform.GetChild(1).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = traitSprites[i];
                    }                     
                }
            }

            if (Team.chrisa >= 2)
            {
                characters[characterIndex].transform.GetChild(4).transform.GetChild(0).gameObject.SetActive(true);
                characters[characterIndex].transform.GetChild(4).transform.GetChild(1).transform.GetComponent<TextMesh>().text = "";
            }
            if (Team.chrisa >= 3)
            {
                characters[characterIndex].transform.GetChild(5).transform.GetChild(0).gameObject.SetActive(true);
                characters[characterIndex].transform.GetChild(5).transform.GetChild(1).transform.GetComponent<TextMesh>().text = "";
            }
            if (Team.chrisa == 4)
            {
                characters[characterIndex].transform.GetChild(6).transform.GetChild(0).gameObject.SetActive(true);
                characters[characterIndex].transform.GetChild(6).transform.GetChild(1).transform.GetComponent<TextMesh>().text = "";
                characters[characterIndex].transform.GetChild(7).transform.GetChild(0).gameObject.SetActive(true);
                characters[characterIndex].transform.GetChild(7).transform.GetChild(1).transform.GetComponent<TextMesh>().text = "";
                if (PlayerPrefs.GetString("ChrisaSkillTwo") == characters[characterIndex].transform.GetChild(7).name)
                {
                    characters[characterIndex].transform.GetChild(6).transform.GetChild(0).transform.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
                    characters[characterIndex].transform.GetChild(7).transform.GetChild(0).transform.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .25f);
                }
                else
                {
                    characters[characterIndex].transform.GetChild(6).transform.GetChild(0).transform.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .25f);
                    characters[characterIndex].transform.GetChild(7).transform.GetChild(0).transform.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
                }
            }
        }
        if (characterControl.characterName == "Kurtzle")
        {
            if (PlayerPrefs.GetString("KurtzleSkillOne") != null)
            {
                for (int i = 0; i < abilitySprites.Length; i++)
                {
                    if (PlayerPrefs.GetString("KurtzleSkillOne") == abilitySprites[i].name)
                    {
                        abilites.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
                        abilites.transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = abilitySprites[i];
                    }                       
                }
            }
            if (PlayerPrefs.GetString("KurtzleSkillTwo") != null)
            {
                for (int i = 0; i < abilitySprites.Length; i++)
                {
                    if (PlayerPrefs.GetString("KurtzleSkillTwo") == abilitySprites[i].name)
                    {
                        abilites.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(true);
                        abilites.transform.GetChild(1).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = abilitySprites[i];
                    }
                }
            }
            if (PlayerPrefs.GetString("KurtzleTraitOne") != null)
            {             
                for (int i = 0; i < traitSprites.Length; i++)
                {
                    if (PlayerPrefs.GetString("KurtzleTraitOne") == traitSprites[i].name)
                    {
                        traits.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
                        traits.transform.GetChild(0).transform.GetChild(1).GetComponent<TextMesh>().text = PlayerPrefs.GetString("KurtzleTraitOne");
                        traits.transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = traitSprites[i];
                    }
                }
            }
            if (PlayerPrefs.GetString("KurtzleTraitTwo") != null)
            {
                
                for (int i = 0; i < traitSprites.Length; i++)
                {
                    if (PlayerPrefs.GetString("KurtzleTraitTwo") == traitSprites[i].name)
                    {
                        traits.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(true);
                        traits.transform.GetChild(1).transform.GetChild(1).GetComponent<TextMesh>().text = PlayerPrefs.GetString("KurtzleTraitTwo");
                        traits.transform.GetChild(1).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = traitSprites[i];
                    }                       
                }
            }

            if (Team.kurtzle >= 2)
            {
                characters[characterIndex].transform.GetChild(4).transform.GetChild(0).gameObject.SetActive(true);
                characters[characterIndex].transform.GetChild(4).transform.GetChild(1).transform.GetComponent<TextMesh>().text = "";
            }
            if (Team.kurtzle >= 3)
            {
                characters[characterIndex].transform.GetChild(5).transform.GetChild(0).gameObject.SetActive(true);
                characters[characterIndex].transform.GetChild(5).transform.GetChild(1).transform.GetComponent<TextMesh>().text = "";
                characters[characterIndex].transform.GetChild(6).transform.GetChild(0).gameObject.SetActive(true);
                characters[characterIndex].transform.GetChild(6).transform.GetChild(1).transform.GetComponent<TextMesh>().text = "";
                if (PlayerPrefs.GetString("KurtzleSkillTwo") == characters[characterIndex].transform.GetChild(5).name)
                {
                    characters[characterIndex].transform.GetChild(5).transform.GetChild(0).transform.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
                    characters[characterIndex].transform.GetChild(6).transform.GetChild(0).transform.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .25f);
                }
                else
                {
                    characters[characterIndex].transform.GetChild(5).transform.GetChild(0).transform.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .25f);
                    characters[characterIndex].transform.GetChild(6).transform.GetChild(0).transform.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
                }
            }
            if (Team.kurtzle == 4)
            {
                characters[characterIndex].transform.GetChild(7).transform.GetChild(0).gameObject.SetActive(true);
                characters[characterIndex].transform.GetChild(7).transform.GetChild(1).transform.GetComponent<TextMesh>().text = "";
                characters[characterIndex].transform.GetChild(8).transform.GetChild(0).gameObject.SetActive(true);
                characters[characterIndex].transform.GetChild(8).transform.GetChild(1).transform.GetComponent<TextMesh>().text = "";
                if (PlayerPrefs.GetString("KurtzleSkillTwo") == characters[characterIndex].transform.GetChild(7).name)
                {
                    characters[characterIndex].transform.GetChild(7).transform.GetChild(0).transform.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
                    characters[characterIndex].transform.GetChild(8).transform.GetChild(0).transform.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .25f);
                }
                else
                {
                    characters[characterIndex].transform.GetChild(7).transform.GetChild(0).transform.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .25f);
                    characters[characterIndex].transform.GetChild(8).transform.GetChild(0).transform.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
                }
            }
        }
    }

    public void equipmentSelect()
    {
        currentEquipmentSelected = EventSystem.current.currentSelectedGameObject.gameObject;
        Debug.Log(currentEquipmentSelected.name);
        EventSystem.current.currentSelectedGameObject.transform.GetChild(3).gameObject.SetActive(true); //Turn on gold selected border for the equipment that was clicked on
        equipmentUpgradePrompt.SetActive(true); //Turn on the prompt to ask if you want to spend crystals to upgrade your equipment
        if (EventSystem.current.currentSelectedGameObject.name == "Weapon")
            characters[characterIndex].transform.GetChild(1).transform.GetChild(3).gameObject.SetActive(false); //Turns off other equipment selected border
        if (EventSystem.current.currentSelectedGameObject.name == "Armor")
            characters[characterIndex].transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(false); //Turns off other equipment selected border
    }

    public void upgradeEquipment()
    {
        if (PlayerPrefs.GetInt("PlayerShard") >= 50)
        {
            PlayerPrefs.SetInt("PlayerShard", PlayerPrefs.GetInt("PlayerShard") - 50);
            if (currentEquipmentSelected.name == "Weapon")
            {
                characterControl.weaponAttack += 1;
                if (characterControl.characterName == "Urp")
                {
                    PlayerPrefs.SetInt("UrpWeaponBonus", (PlayerPrefs.GetInt("UrpWeaponBonus") + 1));
                }
                if (characterControl.characterName == "Chrisa")
                {
                    PlayerPrefs.SetInt("ChrisaWeaponBonus", (PlayerPrefs.GetInt("ChrisaWeaponBonus") + 1));
                }
                if (characterControl.characterName == "Kurtzle")
                {
                    PlayerPrefs.SetInt("KurtzleWeaponBonus", (PlayerPrefs.GetInt("KurtzleWeaponBonus") + 1));
                }
            }
            if (currentEquipmentSelected.name == "Armor")
            {
                characterControl.armorDefense += 1;
                if (characterControl.characterName == "Urp")
                {
                    PlayerPrefs.SetInt("UrpArmorBonus", (PlayerPrefs.GetInt("UrpArmorBonus") + 1));
                }
                if (characterControl.characterName == "Chrisa")
                {
                    PlayerPrefs.SetInt("ChrisaArmorBonus", (PlayerPrefs.GetInt("ChrisaArmorBonus") + 1));
                }
                if (characterControl.characterName == "Kurtzle")
                {
                    PlayerPrefs.SetInt("KurtzleArmorBonus", (PlayerPrefs.GetInt("KurtzleArmorBonus") + 1));
                }
            }
            currentEquipmentSelected.transform.GetChild(3).gameObject.SetActive(false);
            EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.SetActive(false);
        }
    }
}
