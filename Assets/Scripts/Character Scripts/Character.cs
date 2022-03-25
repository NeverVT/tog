using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public Sprite portrait;
    public string characterName;
    public string tribe;
    public double maxHealth;
    public double currentHealth;
    public CharacterWeapon weapon;
    public CharacterArmor armor;
    public GameObject skillOne;
    public GameObject skillTwo;
    public GameObject traitOne;
    public GameObject traitTwo;
    public GameObject bonusOne;
    public GameObject bonusTwo;
    public GameObject relicOne;
    public GameObject relicTwo; 
    public GameObject tempSkill;

    public void Awake()
    {
        /*
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = portrait;
        switch (Team.getLevel(characterName)) //Check the level of the selected character and set up the selected build for that character
        {
            case 0:
                goto case 1;
            case 1:
                setupBuild(skillOne, skillTwo, "Skill");
                break;
            case 2:
                setupBuild(traitOne, traitTwo, "Trait");
                goto case 1;
            case 3:
                setupBuild(bonusOne, bonusTwo, "Bonus");
                goto case 2;
            case 4:
                setupBuild(relicOne, relicTwo, "Relic");
                goto case 3;
        }*/
    }

    private void setupBuild(GameObject objOne, GameObject objTwo, string type) //Determine wether the character should be using the first or second version of their build
    {
        if (PlayerPrefs.GetString(characterName + type) == null || PlayerPrefs.GetString(characterName + type) == objOne.name) //use first
        {
            objOne.SetActive(true);
            objTwo.SetActive(false);
        }
        else //use second
        {
            objOne.SetActive(false);
            objTwo.SetActive(true);
        }
    }
}