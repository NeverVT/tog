using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterControl : MonoBehaviour
{
    public GameObject loadingDoors;
    public GameScript game;
    public GameObject[] characters;
    public GameObject selectedCharacter;
    public GameObject[] skills;
    public GameObject cameraObject;
    public Ship ship;
    private int totalMaxHealth;
    private int totalCurrentHealth;
    public string characterName;
    public string tribe;
    public int maxHealth;
    public int currentHealth;
    public int weaponAttack;
    public int armorDefense;
    public string traitOne;
    public string traitTwo;
    public string skillOne;
    public string skillTwo;

    public void setUpCharacters()
    {
        if(Team.selectedCharacter != null)
            characterName = Team.selectedCharacter;
        else
        {
            if(PlayerPrefs.GetString("Skeleton Killed") == "true")
            {
                characterName = "Urp";
            }
            else
            {
                characterName = "Empty";
            }           
            Team.urpLvl = 1;
        }
        for(int i = 0; i < characters.Length; i++)
        {
            if(characters[i].name == characterName)
            {
                selectedCharacter = Instantiate(characters[i], selectedCharacter.transform);
            }
        }        
        selectedCharacter.transform.position = new Vector3(1.15f, -9.25f, -1);
        setStats(characterName);
        selectedCharacter.GetComponent<Character>().maxHealth = maxHealth;
        totalMaxHealth += maxHealth;
        selectedCharacter.GetComponent<Character>().currentHealth = selectedCharacter.GetComponent<Character>().maxHealth;
        selectedCharacter.GetComponent<Character>().weapon.damage = weaponAttack;
        selectedCharacter.GetComponent<Character>().armor.defense = armorDefense;
           
        
        totalCurrentHealth = totalMaxHealth;
     
    }
    public IEnumerator checkGameOver() //Does a check to see if the game is over
    {
        if (totalCurrentHealth <= 0)
        {
            if (game.GetComponent<Artifacts>().reanimateStone)
            {
                game.GetComponent<Artifacts>().reanimateStone = false;
                setCurrentHealth(getMaxHealth() / 2);
            }
            else
            {
                cameraObject.GetComponent<Animator>().SetTrigger("FadeToBlack");
                yield return new WaitForSeconds(2f);
                loadingDoors.GetComponent<Animator>().SetBool("close", true);
                yield return new WaitForSeconds(2.5f);
                Debug.Log("----- GAME OVER -----");
                ScoreControl.saveHighScore();
                GameControl.gold = 0;
                setCurrentHealth(getMaxHealth());
                SceneManager.LoadScene("Scenes/GameOver");
                
                AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameOver");
                asyncLoad.allowSceneActivation = false;
                while (!asyncLoad.isDone)
                {
                    if (asyncLoad.progress >= 0.9f)
                    {
                        if (loadingDoors.GetComponent<loadingDoors>().doneClosing)
                        {
                            loadingDoors.GetComponent<Animator>().SetBool("close", false);
                            asyncLoad.allowSceneActivation = true;
                        }
                    }
                    yield return null;
                }
            }
        }
    }
    public void manageSkillCDs(string collectedTileType)
    {
        
        if (selectedCharacter.GetComponent<Character>().skillOne.GetComponent<Spell>().coolDown > 0)
            if (selectedCharacter.GetComponent<Character>().skillOne.GetComponent<Spell>().spellType == collectedTileType || collectedTileType == "Mana")
                selectedCharacter.GetComponent<Character>().skillOne.GetComponent<Spell>().coolDown -= 1;

        if (selectedCharacter.GetComponent<Character>().skillTwo.GetComponent<Spell>().coolDown > 0)
            if (selectedCharacter.GetComponent<Character>().skillTwo.GetComponent<Spell>().spellType == collectedTileType || collectedTileType == "Mana")
                selectedCharacter.GetComponent<Character>().skillTwo.GetComponent<Spell>().coolDown -= 1;
        
    }

    public bool searchActiveCharacterTraits(string trait) //Searches the active character if they have a specific trait
    {
        /*
        if(selectedCharacter.GetComponent<Character>().traitOne.name == trait || selectedCharacter.GetComponent<Character>().traitTwo.name == trait)
        {
            return true;
        }
        else
        {
            return false;
        }       */
        return false;
    }

    public bool searchSkillExists(string skill) //Checks if the Skill is on the Active Character
    {
        /*
        if (selectedCharacter.GetComponent<Character>().skillOne.name == skill || selectedCharacter.GetComponent<Character>().skillTwo.name == skill)
        {
            return true;
        }
        else
        {
            return false;
        }*/
        return false;
    }
    public double getAttack() //Returns the active character's Attack value
    {
        return selectedCharacter.GetComponent<Character>().weapon.damage;
    }
    public double getLowestAttack() //Returns the lowest Attack value on the team
    {
        double attack = 0;
        for(int i = 0; i < characters.Length; i++)
        {
            if (attack == 0 || selectedCharacter.GetComponent<Character>().weapon.damage < attack)
                attack = selectedCharacter.GetComponent<Character>().weapon.damage;
        }
        return attack;
    }
    public double getArmor() //Returns the active character's Armor value
    {
        return selectedCharacter.GetComponent<Character>().armor.defense;
    }
    public double getLowestArmor() //Returns the lowest Armor value on the team
    {
        double attack = 0;
        for(int i = 0; i < characters.Length; i++)
        {
            if (attack == 0 || selectedCharacter.GetComponent<Character>().weapon.damage < attack)
                attack = selectedCharacter.GetComponent<Character>().weapon.damage;
        }
        return attack;
    }
    public double getMaxHealth()
    {
        return totalMaxHealth;
    }
    public double getCurrentHealth()
    {
        return totalCurrentHealth;
    }
    public string getTraitOne()
    {
        return selectedCharacter.GetComponent<Character>().traitOne.name;
    }
    public string getTraitTwo()
    {
        return selectedCharacter.GetComponent<Character>().traitTwo.name;
    }
    public void setCurrentHealth(double newHealth)
    {
        if (newHealth > totalMaxHealth)
            totalCurrentHealth = totalMaxHealth;
        else
            totalCurrentHealth = (int)newHealth;
    }
    public GameObject getWeaponIcon()
    {
        return selectedCharacter.GetComponent<Character>().weapon.icon;
    }

    public void setStats(string name)
    {
        if (name != "")
            name = name.Replace("(Clone)", "").Trim();
        characterName = name;
        if (name == "Empty")
        {
            if (Team.urpLvl == 1 || Team.urpLvl == 2) //Level One
            {
                maxHealth = 30;
                currentHealth = 30;
                weaponAttack = 2 + PlayerPrefs.GetInt("UrpWeaponBonus");
                armorDefense = 2 + PlayerPrefs.GetInt("UrpArmorBonus");
            }
        }
            if (name == "Urp")
        {
            if (Team.urpLvl == 1 || Team.urpLvl == 2) //Level One
            {
                maxHealth = 35;
                currentHealth = 35;
                weaponAttack = 2 + PlayerPrefs.GetInt("UrpWeaponBonus");
                armorDefense = 3 + PlayerPrefs.GetInt("UrpArmorBonus");
            }
            else if (Team.urpLvl >= 3 && Team.urpLvl <= 5) //Level Two
            {
                maxHealth = 35;
                currentHealth = 35;
                weaponAttack = 3 + PlayerPrefs.GetInt("UrpWeaponBonus");
                armorDefense = 3 + PlayerPrefs.GetInt("UrpArmorBonus");
            }
            else if (Team.urpLvl >= 6 && Team.urpLvl <= 9) //Level Three
            {
                maxHealth = 35;
                currentHealth = 35;
                weaponAttack = 3 + PlayerPrefs.GetInt("UrpWeaponBonus");
                armorDefense = 3 + PlayerPrefs.GetInt("UrpArmorBonus");
            }
            else if (Team.urpLvl == 10) //Level Four
            {
                maxHealth = 40;
                currentHealth = 40;
                weaponAttack = 4 + PlayerPrefs.GetInt("UrpWeaponBonus");
                armorDefense = 3 + PlayerPrefs.GetInt("UrpArmorBonus");
            }
        }
        if (name == "Chrisa")
        {
            tribe = "Pirate";
            if (Team.chrisaLvl == 1 || Team.chrisaLvl == 2) //Level One
            {
                maxHealth = 15;
                currentHealth = 15;
                weaponAttack = 2 + PlayerPrefs.GetInt("ChrisaWeaponBonus");
                armorDefense = 1 + PlayerPrefs.GetInt("ChrisaArmorBonus");
                traitOne = "";
                PlayerPrefs.SetString("ChrisaTraitOne", traitOne);
                traitTwo = "";
                PlayerPrefs.SetString("ChrisaTraitTwo", traitTwo);
                if (PlayerPrefs.GetString("ChrisaSkillOne") != "")
                    skillOne = PlayerPrefs.GetString("ChrisaSkillOne");
                else
                {
                    skillOne = "Powder Keg";
                    PlayerPrefs.SetString("ChrisaSkillOne", skillOne);
                }
                skillTwo = "";
                PlayerPrefs.SetString("ChrisaSkillTwo", skillTwo);
            }
            else if (Team.chrisaLvl >= 3 && Team.chrisaLvl <= 5) //Level Two
            {
                maxHealth = 20;
                currentHealth = 20;
                weaponAttack = 3 + PlayerPrefs.GetInt("ChrisaWeaponBonus");
                armorDefense = 2 + PlayerPrefs.GetInt("ChrisaArmorBonus");
                if (PlayerPrefs.GetString("ChrisaTraitOne") != "")
                    traitOne = PlayerPrefs.GetString("ChrisaTraitOne");
                else
                {
                    traitOne = "Cunning";
                    PlayerPrefs.SetString("ChrisaTraitOne", traitOne);
                }
                traitTwo = "";
                PlayerPrefs.SetString("ChrisaTraitTwo", traitTwo);
                if (PlayerPrefs.GetString("ChrisaSkillOne") != "")
                    skillOne = PlayerPrefs.GetString("ChrisaSkillOne");
                else
                {
                    skillOne = "Powder Keg";
                    PlayerPrefs.SetString("ChrisaSkillOne", skillOne);
                }
                skillTwo = "";
                PlayerPrefs.SetString("ChrisaSkillTwo", skillTwo);
            }
            else if (Team.chrisaLvl >= 6 && Team.chrisaLvl <= 9) //Level Three
            {
                maxHealth = 20;
                currentHealth = 20;
                weaponAttack = 3 + PlayerPrefs.GetInt("ChrisaWeaponBonus");
                armorDefense = 2 + PlayerPrefs.GetInt("ChrisaArmorBonus");
                if (PlayerPrefs.GetString("ChrisaTraitOne") != "")
                    traitOne = PlayerPrefs.GetString("ChrisaTraitOne");
                else
                {
                    traitOne = "Cunning";
                    PlayerPrefs.SetString("ChrisaTraitOne", traitOne);
                }
                traitTwo = "";
                PlayerPrefs.SetString("ChrisaTraitTwo", traitTwo);
                if (PlayerPrefs.GetString("ChrisaSkillOne") != "")
                    skillOne = PlayerPrefs.GetString("ChrisaSkillOne");
                else
                {
                    skillOne = "Powder Keg";
                    PlayerPrefs.SetString("ChrisaSkillOne", skillOne);
                }
                skillTwo = "";
                PlayerPrefs.SetString("ChrisaSkillTwo", skillTwo);
            }
            else if (Team.chrisaLvl == 10) //Level Four
            {
                maxHealth = 22;
                currentHealth = 22;
                weaponAttack = 3 + PlayerPrefs.GetInt("ChrisaWeaponBonus");
                armorDefense = 3 + PlayerPrefs.GetInt("ChrisaArmorBonus");
                if (PlayerPrefs.GetString("ChrisaTraitOne") != "")
                    traitOne = PlayerPrefs.GetString("ChrisaTraitOne");
                else
                {
                    traitOne = "Cunning";
                    PlayerPrefs.SetString("ChrisaTraitOne", traitOne);
                }
                if (PlayerPrefs.GetString("ChrisaTraitTwo") != "")
                    traitTwo = PlayerPrefs.GetString("ChrisaTraitTwo");
                else
                {
                    traitTwo = "Sleight of Hand";
                    PlayerPrefs.SetString("ChrisaTraitTwo", traitTwo);
                }
                if (PlayerPrefs.GetString("ChrisaSkillOne") != "")
                    skillOne = PlayerPrefs.GetString("ChrisaSkillOne");
                else
                {
                    skillOne = "Powder Keg";
                    PlayerPrefs.SetString("ChrisaSkillOne", skillOne);
                }
                skillTwo = "";
                PlayerPrefs.SetString("ChrisaSkillTwo", skillTwo);
            }
        }
        if (name == "Kurtzle")
        {
            tribe = "Pirate";
            if (Team.kurtzleLvl == 1 || Team.kurtzleLvl == 2) //Level One
            {
                maxHealth = 16;
                currentHealth = 16;
                weaponAttack = 2 + PlayerPrefs.GetInt("KurtzleWeaponBonus");
                armorDefense = 1 + PlayerPrefs.GetInt("KurtzleArmorBonus");
                traitOne = "";
                PlayerPrefs.SetString("KurtzleTraitOne", traitOne);
                traitTwo = "";
                PlayerPrefs.SetString("KurtzleTraitTwo", traitTwo);

                if (PlayerPrefs.GetString("KurtzleSkillOne") != "")
                    skillOne = PlayerPrefs.GetString("KurtzleSkillOne");
                else
                {
                    skillOne = "Hex Ball";
                    PlayerPrefs.SetString("KurtzleSkillOne", skillOne);
                }

                skillTwo = "";
                PlayerPrefs.SetString("KurtzleSkillTwo", skillTwo);
            }
            else if (Team.kurtzleLvl >= 3 && Team.kurtzleLvl <= 5) //Level Two
            {
                maxHealth = 20;
                currentHealth = 20;
                weaponAttack = 3 + PlayerPrefs.GetInt("KurtzleWeaponBonus");
                armorDefense = 2 + PlayerPrefs.GetInt("KurtzleArmorBonus");

                if (PlayerPrefs.GetString("KurtzleTraitOne") != "")
                    traitOne = PlayerPrefs.GetString("KurtzleTraitOne");
                else
                {
                    traitOne = "Slasher";
                    PlayerPrefs.SetString("KurtzleTraitOne", traitOne);
                }

                traitTwo = "";
                PlayerPrefs.SetString("KurtzleTraitTwo", traitTwo);

                if (PlayerPrefs.GetString("KurtzleSkillOne") != "")
                    skillOne = PlayerPrefs.GetString("KurtzleSkillOne");
                else
                {
                    skillOne = "Hex Ball";
                    PlayerPrefs.SetString("KurtzleSkillOne", skillOne);
                }

                skillTwo = "";
                PlayerPrefs.SetString("KurtzleSkillTwo", skillTwo);
            }
            else if (Team.kurtzleLvl >= 6 && Team.kurtzleLvl <= 9) //Level Three
            {
                maxHealth = 20;
                currentHealth = 20;
                weaponAttack = 3 + PlayerPrefs.GetInt("KurtzleWeaponBonus");
                armorDefense = 2 + PlayerPrefs.GetInt("KurtzleArmorBonus");

                if (PlayerPrefs.GetString("KurtzleTraitOne") != "")
                    traitOne = PlayerPrefs.GetString("KurtzleTraitOne");
                else
                {
                    traitOne = "Slasher";
                    PlayerPrefs.SetString("KurtzleTraitOne", traitOne);
                }

                traitTwo = "";
                PlayerPrefs.SetString("KurtzleTraitTwo", traitTwo);

                if (PlayerPrefs.GetString("KurtzleSkillOne") != "")
                    skillOne = PlayerPrefs.GetString("KurtzleSkillOne");
                else
                {
                    skillOne = "Hex Ball";
                    PlayerPrefs.SetString("KurtzleSkillOne", skillOne);
                }

                if (PlayerPrefs.GetString("KurtzleSkillTwo") != "")
                    skillTwo = PlayerPrefs.GetString("KurtzleSkillTwo");
                else
                {
                    skillTwo = "Double Shot";
                    PlayerPrefs.SetString("KurtzleSkillTwo", skillTwo);
                }
            }
            else if (Team.kurtzleLvl == 10) //Level Four
            {
                maxHealth = 22;
                currentHealth = 22;
                weaponAttack = 4 + PlayerPrefs.GetInt("KurtzleWeaponBonus");
                armorDefense = 2 + PlayerPrefs.GetInt("KurtzleArmorBonus");
                if (PlayerPrefs.GetString("KurtzleTraitOne") != "")
                    traitOne = PlayerPrefs.GetString("KurtzleTraitOne");
                else
                {
                    traitOne = "Slasher";
                    PlayerPrefs.SetString("KurtzleTraitOne", traitOne);
                }

                if (PlayerPrefs.GetString("KurtzleTraitTwo") != "")
                    traitTwo = PlayerPrefs.GetString("KurtzleTraitTwo");
                else
                {
                    traitTwo = "Hacker";
                    PlayerPrefs.SetString("KurtzleTraitTwo", traitTwo);
                }

                if (PlayerPrefs.GetString("KurtzleSkillOne") != "")
                    skillOne = PlayerPrefs.GetString("KurtzleSkillOne");
                else
                {
                    skillOne = "Hex Ball";
                    PlayerPrefs.SetString("KurtzleSkillOne", skillOne);
                }

                if (PlayerPrefs.GetString("KurtzleSkillTwo") != "")
                    skillTwo = PlayerPrefs.GetString("KurtzleSkillTwo");
                else
                {
                    skillTwo = "Double Shot";
                    PlayerPrefs.SetString("KurtzleSkillTwo", skillTwo);
                }
            }
        }
        if (name == "Dobby")
        {
            maxHealth = 17;
            currentHealth = 17;
            weaponAttack = 2;
            armorDefense = 1;
            traitOne = "Looter";
            traitTwo = "Sleight of Hand";
            skillOne = "Appraise";
            skillTwo = "Gold Rush";
        }
        if (name == "Bear")
        {
            maxHealth = 25;
            currentHealth = 25;
            weaponAttack = 2;
            armorDefense = 1;
            traitOne = "Tough";
            traitTwo = "Intimidating";
            skillOne = "Overpower";
            skillTwo = "Bloodshot";
        }
        if (name == "Wolf")
        {
            maxHealth = 20;
            currentHealth = 20;
            weaponAttack = 3;
            armorDefense = 2;
            traitOne = "Hunter";
            traitTwo = "Killer";
            skillOne = "Bloodlust";
            skillTwo = "Execute";
        }
    }
}
