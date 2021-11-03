using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public Sprite[] portraits;
    public Sprite[] skills = new Sprite[4];
    public Sprite[] traits = new Sprite[6];

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

    private void Awake()
    {
        for (int i = 0; i < portraits.Length; i++)
        {
            if (characterName == portraits[i].name)
            {
                this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = portraits[i];
                weapon.icon.GetComponent<SpriteRenderer>().sprite = weapon.weapons[i];
                armor.icon.GetComponent<SpriteRenderer>().sprite = armor.armors[i];
            }
        }
    }

    public void init()
    {
        for (int i = 0; i < skills.Length; i++)
        {
            if (skillOne.name == skills[i].name.Replace("(UnityEngine.Sprite)", ""))
            {
                skillOne.GetComponent<Image>().sprite = skills[i];
                skillOne.name = skills[i].name.Replace("(UnityEngine.Sprite)", "");
            }

            if (skillTwo.name == skills[i].name.Replace("(UnityEngine.Sprite)", ""))
            {
                skillTwo.GetComponent<Image>().sprite = skills[i];
                skillTwo.name = skills[i].name.Replace("(UnityEngine.Sprite)", "");
            }
        }
        for (int i = 0; i < skills.Length; i++)
        {
            if (traitOne.name == traits[i].name.Replace("(UnityEngine.Sprite)", ""))
            {
                traitOne.GetComponent<Image>().sprite = traits[i];
                traitOne.name = traits[i].name.Replace("(UnityEngine.Sprite)", "");
            }

            if (traitTwo.name == traits[i].name.Replace("(UnityEngine.Sprite)", ""))
            {
                traitTwo.GetComponent<Image>().sprite = traits[i];
                traitTwo.name = traits[i].name.Replace("(UnityEngine.Sprite)", "");
            }
        }
    }
}