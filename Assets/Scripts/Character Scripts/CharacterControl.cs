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
    public Character[] characters;
    public int activeCharacter;
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
        activeCharacter = 0;
        if(Team.characterOne != null)
            characters[0].characterName = Team.characterOne;
        else
        {
            characters[0].characterName = "Urp";
            Team.urp = 1;
        }
            
        if (Team.characterTwo != null)
            characters[1].characterName = Team.characterTwo;
        else
        {
            characters[1].characterName = "Chrisa";
            Team.chrisa = 1;
        }
            
        if (Team.characterThree != null)
            characters[2].characterName = Team.characterThree;
        else
        {
            characters[2].characterName = "Kurtzle";
            Team.kurtzle = 1;
        }
            
        for (int i = 0; i< 3; i++)
        {
            setStats(characters[i].characterName);
            characters[i].maxHealth = maxHealth;
            totalMaxHealth += maxHealth;
            characters[i].tribe = tribe;
            characters[i].currentHealth = characters[i].maxHealth;
            characters[i].weapon.damage = weaponAttack;
            characters[i].armor.defense = armorDefense;
            characters[i].traitOne.name = traitOne;
            characters[i].traitTwo.name = traitTwo;
            characters[i].skillOne.name = skillOne;
            characters[i].skillTwo.name = skillTwo;
            characters[i].init();
        }
        totalCurrentHealth = totalMaxHealth;
        characters[0].transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
        characters[1].transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .25f);
        characters[2].transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .25f);
    }

    public void switchCharacter()
    {
        StartCoroutine(checkGameOver());
        //turn off current characters skills and skill tooltips
        characters[activeCharacter].transform.GetChild(3).transform.GetChild(1).gameObject.SetActive(false); //skillOne tooltip
        characters[activeCharacter].transform.GetChild(4).transform.GetChild(1).gameObject.SetActive(false); //skillTwo tooltip
        characters[activeCharacter].skillOne.SetActive(false);
        characters[activeCharacter].skillTwo.SetActive(false);
        //change the active character to the next party member
        if (activeCharacter == 2) 
            activeCharacter = 0;
        else
            activeCharacter++;
        //dim not active characters and brighten the active character
        for (int i = 0; i < 3; i++) 
        {
            if(i == activeCharacter)
                characters[i].transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
            else
                characters[i].transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .25f);
        }
        //check if the new active character has empty skill or traits slots and turn them off
        if (characters[activeCharacter].skillOne.name != "") 
            characters[activeCharacter].skillOne.SetActive(true);
        if (characters[activeCharacter].skillTwo.name != "")
            characters[activeCharacter].skillTwo.SetActive(true);
        if (characters[activeCharacter].traitOne.name != "")
            characters[activeCharacter].traitOne.SetActive(true);
        if (characters[activeCharacter].traitTwo.name != "")
            characters[activeCharacter].traitTwo.SetActive(true);

    }

    private IEnumerator checkGameOver() //Does a check to see if the game is over
    {
        if(totalCurrentHealth <= 0)
        {
            if(game.GetComponent<Artifacts>().reanimateStone)
            {
                game.GetComponent<Artifacts>().reanimateStone = false;
                setCurrentHealth(getMaxHealth() / 2);
            }
            else
            {
                Debug.Log("----- GAME OVER -----");
                ScoreControl.saveHighScore();
                GameControl.gold = 0;
                setCurrentHealth(getMaxHealth());
                characters[0].skillOne.GetComponent<Spell>().coolDown = 0;
                characters[0].skillTwo.GetComponent<Spell>().coolDown = 0;
                characters[1].skillOne.GetComponent<Spell>().coolDown = 0;
                characters[1].skillTwo.GetComponent<Spell>().coolDown = 0;
                characters[2].skillOne.GetComponent<Spell>().coolDown = 0;
                characters[2].skillTwo.GetComponent<Spell>().coolDown = 0;
                //SceneManager.LoadScene("Scenes/GameOver");
                loadingDoors.GetComponent<Animator>().SetBool("close", true);
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
    public bool searchTribe(string tribe) //Checks if all 3 party members belong to the same tribe
    { 
        if (characters[0].tribe == tribe)
        {
            if (characters[1].tribe == tribe)
            {
                if (characters[2].tribe == tribe)
                {
                    return true;
                }
            }
        }                   
        return false;
    }
    public int searchTraitAll(string trait) //Searches the party for a trait, active or inactive, and return which character has it
    {
        for(int i = 0; i < characters.Length; i++)
        {
            if (characters[i].traitOne.name == trait || characters[i].traitTwo.name == trait)
                return activeCharacter;
        }
        return -1;      
    }

    public bool searchActiveCharacterTraits(string trait) //Searches the active character if they have a specific trait
    {
        if(characters[activeCharacter].traitOne.name == trait || characters[activeCharacter].traitTwo.name == trait)
        {
            return true;
        }
        else
        {
            return false;
        }       
    }

    public bool searchSkillExists(string skill) //Checks if the Skill is on the Active Character
    {
        if (characters[activeCharacter].skillOne.name == skill || characters[activeCharacter].skillTwo.name == skill)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public double getAttack() //Returns the active character's Attack value
    {
        return characters[activeCharacter].weapon.damage;
    }
    public double getLowestAttack() //Returns the lowest Attack value on the team
    {
        double attack = 0;
        for(int i = 0; i < characters.Length; i++)
        {
            if (attack == 0 || characters[i].weapon.damage < attack)
                attack = characters[i].weapon.damage;
        }
        return attack;
    }
    public double getArmor() //Returns the active character's Armor value
    {
        return characters[activeCharacter].armor.defense;
    }
    public double getLowestArmor() //Returns the lowest Armor value on the team
    {
        double attack = 0;
        for(int i = 0; i < characters.Length; i++)
        {
            if (attack == 0 || characters[i].weapon.damage < attack)
                attack = characters[i].weapon.damage;
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
        return characters[activeCharacter].traitOne.name;
    }
    public string getTraitTwo()
    {
        return characters[activeCharacter].traitTwo.name;
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
        return characters[activeCharacter].weapon.icon;
    }

    public void setStats(string name)
    {
        if (name != "")
            name = name.Replace("(Clone)", "").Trim();
        characterName = name;
        if (name == "Urp")
        {
            tribe = "Pirate";
            if (Team.urp == 1 || Team.urp == 2) //Level One
            {
                maxHealth = 20;
                currentHealth = 25;
                weaponAttack = 1;
                armorDefense = 2;
                traitOne = "";
                PlayerPrefs.SetString("UrpTraitOne", traitOne);
                traitTwo = "";
                PlayerPrefs.SetString("UrpTraitTwo", traitTwo);

                if (PlayerPrefs.GetString("UrpSkillOne") != "")
                    skillOne = PlayerPrefs.GetString("UrpSkillOne");
                else
                {
                    skillOne = "Flash Bang";
                    PlayerPrefs.SetString("UrpSkillOne", skillOne);
                }
                skillTwo = "";
                PlayerPrefs.SetString("UrpSkillTwo", skillTwo);
            }
            else if (Team.urp >= 3 && Team.urp <= 5) //Level Two
            {
                maxHealth = 25;
                currentHealth = 25;
                weaponAttack = 2;
                armorDefense = 2;

                if (PlayerPrefs.GetString("UrpTraitOne") != "")
                    traitOne = PlayerPrefs.GetString("UrpTraitOne");
                else
                {
                    traitOne = "Protector";
                    PlayerPrefs.SetString("UrpTraitOne", traitOne);
                }

                traitTwo = "";
                PlayerPrefs.SetString("UrpTraitTwo", traitTwo);

                if (PlayerPrefs.GetString("UrpSkillOne") != "")
                    skillOne = PlayerPrefs.GetString("UrpSkillOne");
                else
                {
                    skillOne = "Flash Bang";
                    PlayerPrefs.SetString("UrpSkillOne", skillOne);
                }

                skillTwo = "";
                PlayerPrefs.SetString("UrpSkillTwo", skillTwo);
            }
            else if (Team.urp >= 6 && Team.urp <= 9) //Level Three
            {
                maxHealth = 25;
                currentHealth = 25;
                weaponAttack = 2;
                armorDefense = 2;

                if (PlayerPrefs.GetString("UrpTraitOne") != "")
                    traitOne = PlayerPrefs.GetString("UrpTraitOne");
                else
                {
                    traitOne = "Protector";
                    PlayerPrefs.SetString("UrpTraitOne", traitOne);
                }

                traitTwo = "";
                PlayerPrefs.SetString("UrpTraitTwo", traitTwo);

                if (PlayerPrefs.GetString("UrpSkillOne") != "")
                    skillOne = PlayerPrefs.GetString("UrpSkillOne");
                else
                {
                    skillOne = "Flash Bang";
                    PlayerPrefs.SetString("UrpSkillOne", skillOne);
                }

                if (PlayerPrefs.GetString("UrpSkillTwo") != "")
                    skillTwo = PlayerPrefs.GetString("UrpSkillTwo");
                else
                {
                    skillTwo = "Dragon Shot";
                    PlayerPrefs.SetString("UrpSkillTwo", skillTwo);
                }
            }
            else if (Team.urp == 10) //Level Four
            {
                maxHealth = 30;
                currentHealth = 30;
                weaponAttack = 3;
                armorDefense = 2;
                if (PlayerPrefs.GetString("UrpTraitOne") != "")
                    traitOne = PlayerPrefs.GetString("UrpTraitOne");
                else
                {
                    traitOne = "Protector";
                    PlayerPrefs.SetString("UrpTraitOne", traitOne);
                }

                if (PlayerPrefs.GetString("UrpTraitTwo") != "")
                    traitTwo = PlayerPrefs.GetString("UrpTraitTwo");
                else
                {
                    traitTwo = "Tough";
                    PlayerPrefs.SetString("UrpTraitTwo", traitTwo);
                }

                if (PlayerPrefs.GetString("UrpSkillOne") != "")
                    skillOne = PlayerPrefs.GetString("UrpSkillOne");
                else
                {
                    skillOne = "Flash Bang";
                    PlayerPrefs.SetString("UrpSkillOne", skillOne);
                }

                if (PlayerPrefs.GetString("UrpSkillTwo") != "")
                    skillTwo = PlayerPrefs.GetString("UrpSkillTwo");
                else
                {
                    skillTwo = "Dragon Shot";
                    PlayerPrefs.SetString("UrpSkillTwo", skillTwo);
                }
            }
        }
        if (name == "Chrisa")
        {
            tribe = "Pirate";
            if (Team.chrisa == 1 || Team.chrisa == 2) //Level One
            {
                maxHealth = 15;
                currentHealth = 15;
                weaponAttack = 2;
                armorDefense = 1;
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
            else if (Team.chrisa >= 3 && Team.chrisa <= 5) //Level Two
            {
                maxHealth = 20;
                currentHealth = 20;
                weaponAttack = 3;
                armorDefense = 2;
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
            else if (Team.chrisa >= 6 && Team.chrisa <= 9) //Level Three
            {
                maxHealth = 20;
                currentHealth = 20;
                weaponAttack = 3;
                armorDefense = 2;
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
            else if (Team.chrisa == 10) //Level Four
            {
                maxHealth = 22;
                currentHealth = 22;
                weaponAttack = 3;
                armorDefense = 3;
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
            if (Team.kurtzle == 1 || Team.kurtzle == 2) //Level One
            {
                maxHealth = 16;
                currentHealth = 16;
                weaponAttack = 2;
                armorDefense = 1;
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
            else if (Team.kurtzle >= 3 && Team.kurtzle <= 5) //Level Two
            {
                maxHealth = 20;
                currentHealth = 20;
                weaponAttack = 3;
                armorDefense = 2;

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
            else if (Team.kurtzle >= 6 && Team.kurtzle <= 9) //Level Three
            {
                maxHealth = 20;
                currentHealth = 20;
                weaponAttack = 3;
                armorDefense = 2;

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
            else if (Team.kurtzle == 10) //Level Four
            {
                maxHealth = 22;
                currentHealth = 22;
                weaponAttack = 4;
                armorDefense = 2;
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
