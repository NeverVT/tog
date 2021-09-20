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
    public GameObject cOneSkillOne;
    public GameObject cOneSkillTwo;
    public GameObject cTwoSkillOne;
    public GameObject cTwoSkillTwo;
    public GameObject cThreeSkillOne;
    public GameObject cThreeSkillTwo;
    public int activeCharacter;
    public Ship ship;
    public Skills skills;
    private int totalMaxHealth;
    private int totalCurrentHealth;
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
            CharacterScreen.setStats(characters[i].characterName);
            characters[i].maxHealth = CharacterScreen.maxHealth;
            totalMaxHealth += CharacterScreen.maxHealth;
            characters[i].tribe = CharacterScreen.tribe;
            characters[i].currentHealth = characters[i].maxHealth;
            characters[i].weapon.damage = CharacterScreen.weaponAttack;
            characters[i].armor.defense = CharacterScreen.armorDefense;
            characters[i].traitOne = CharacterScreen.traitOne;
            characters[i].traitTwo = CharacterScreen.traitTwo;
            characters[i].skillOne = CharacterScreen.skillOne;
            characters[i].skillTwo = CharacterScreen.skillTwo;
        }
        totalCurrentHealth = totalMaxHealth;
        characters[0].transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
        characters[1].transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .5f);
        characters[2].transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .5f);
    }

    public void switchCharacter()
    {
        StartCoroutine(checkGameOver());
        if(activeCharacter == 0) //Change from cOne -> cTwo
        {
            activeCharacter = 1;
            characters[0].transform.GetChild(3).transform.GetChild(1).gameObject.SetActive(false);
            characters[0].transform.GetChild(4).transform.GetChild(1).gameObject.SetActive(false);
            characters[0].transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .5f);
            characters[1].transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
            characters[2].transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .5f);
            cOneSkillOne.SetActive(false);
            cOneSkillTwo.SetActive(false);
            if(characters[1].skillOne != "")
                cTwoSkillOne.SetActive(true);
            if(characters[1].skillTwo != "")
                cTwoSkillTwo.SetActive(true);
        }
        else if (activeCharacter == 1) //Change from cTwo -> cThree
        {
            activeCharacter = 2;
            characters[1].transform.GetChild(3).transform.GetChild(1).gameObject.SetActive(false);
            characters[1].transform.GetChild(4).transform.GetChild(1).gameObject.SetActive(false);
            characters[0].transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .5f);
            characters[1].transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .5f);
            characters[2].transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
            cTwoSkillOne.SetActive(false);
            cTwoSkillTwo.SetActive(false);
            if(characters[2].skillOne != "")
                cThreeSkillOne.SetActive(true);
            if(characters[2].skillTwo != "")
                cThreeSkillTwo.SetActive(true);
        }
        else if (activeCharacter == 2) //Change from cThree -> cOne
        {
            activeCharacter = 0;
            characters[2].transform.GetChild(3).transform.GetChild(1).gameObject.SetActive(false);
            characters[2].transform.GetChild(4).transform.GetChild(1).gameObject.SetActive(false);
            characters[0].transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
            characters[1].transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .5f);
            characters[2].transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .5f);
            cThreeSkillOne.SetActive(false);
            cThreeSkillTwo.SetActive(false);
            if(characters[0].skillOne != "")
                cOneSkillOne.SetActive(true);
            if(characters[0].skillTwo != "")
                cOneSkillTwo.SetActive(true);
        }
        //skills.setSkills();
    }

    private IEnumerator checkGameOver()
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
                cOneSkillOne.GetComponent<Spell>().coolDown = 0;
                cOneSkillTwo.GetComponent<Spell>().coolDown = 0;
                cTwoSkillOne.GetComponent<Spell>().coolDown = 0;
                cTwoSkillTwo.GetComponent<Spell>().coolDown = 0;
                cThreeSkillOne.GetComponent<Spell>().coolDown = 0;
                cThreeSkillTwo.GetComponent<Spell>().coolDown = 0;
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
    public bool searchPirates() //Searches the party for a trait, active or inactive, and return which character has it
    { 
        if (characters[0].tribe == "Pirate")
        {
            if (characters[1].tribe == "Pirate")
            {
                if (characters[2].tribe == "Pirate")
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
            if (characters[i].traitOne == trait || characters[i].traitTwo == trait)
                return activeCharacter;
        }
        return -1;      
    }

    public bool searchActiveCharacterTraits(string trait) //Searches the active character if they have a specific trait
    {
        if(characters[activeCharacter].traitOne == trait || characters[activeCharacter].traitTwo == trait)
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
        if (characters[activeCharacter].skillOne == skill || characters[activeCharacter].skillTwo == skill)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public double getAttack()
    {
        return characters[activeCharacter].weapon.damage;
    }
    public double getLowestAttack()
    {
        double attack = 0;
        for(int i = 0; i < characters.Length; i++)
        {
            if (attack == 0 || characters[i].weapon.damage < attack)
                attack = characters[i].weapon.damage;
        }
        return attack;
    }
    public double getArmor()
    {
        return characters[activeCharacter].armor.defense;
    }
    public double getLowestArmor()
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
        return characters[activeCharacter].traitOne;
    }

    public string getTraitTwo()
    {
        return characters[activeCharacter].traitTwo;
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
}
