using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterControl : MonoBehaviour
{
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
        characters[0].characterName = Team.characterOne;
        characters[1].characterName = Team.characterTwo;
        characters[2].characterName = Team.characterThree;
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
    }

    public void switchCharacter()
    {
        checkGameOver();
        if(activeCharacter == 0) //Change from cOne -> cTwo
        {
            activeCharacter = 1;
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
            cThreeSkillOne.SetActive(false);
            cThreeSkillTwo.SetActive(false);
            if(characters[0].skillOne != "")
                cOneSkillOne.SetActive(true);
            if(characters[0].skillTwo != "")
                cOneSkillTwo.SetActive(true);
        }
        //skills.setSkills();
    }

    private void checkGameOver()
    {
        if(totalCurrentHealth <= 0)
        {
            if(GetComponent<Artifacts>().reanimateStone)
            {
                GetComponent<Artifacts>().reanimateStone = false;
                setCurrentHealth(getMaxHealth / 2);
            }
            else
            {
                Debug.Log("----- GAME OVER -----");
                ScoreControl.saveHighScore();
                GameControl.gold = 0;
                characterControl.setCurrentHealth(characterControl.getMaxHealth());
                characterControl.cOneSkillOne.GetComponent<Spell>().coolDown = 0;
                characterControl.cOneSkillTwo.GetComponent<Spell>().coolDown = 0;
                characterControl.cTwoSkillOne.GetComponent<Spell>().coolDown = 0;
                characterControl.cTwoSkillTwo.GetComponent<Spell>().coolDown = 0;
                characterControl.cThreeSkillOne.GetComponent<Spell>().coolDown = 0;
                characterControl.cThreeSkillTwo.GetComponent<Spell>().coolDown = 0;
                SceneManager.LoadScene("Scenes/GameOver");
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
    public double getArmor()
    {
        return characters[activeCharacter].armor.defense;
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
