using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScreen : MonoBehaviour
{
    public static string characterName;
    public static string tribe;
    public static int maxHealth;
    public static int currentHealth;
    public static int weaponAttack;
    public static int armorDefense;
    public static string traitOne;
    public static string traitTwo;
    public static string skillOne;
    public static string skillTwo;  

    public static void setStats(string name)
    {
        if(name != "")
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
                traitTwo = "";

                if (PlayerPrefs.GetString("UrpSkillOne") != "")
                    skillOne = PlayerPrefs.GetString("UrpSkillOne");
                else
                {
                    skillOne = "Flash Bang";
                    PlayerPrefs.SetString("UrpSkillOne", skillOne);
                }

                skillTwo = "";
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

                if (PlayerPrefs.GetString("UrpSkillOne") != "")
                    skillOne = PlayerPrefs.GetString("UrpSkillOne");
                else
                {
                    skillOne = "Flash Bang";
                    PlayerPrefs.SetString("UrpSkillOne", skillOne);
                }

                skillTwo = "";
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
                traitTwo = "";
                if (PlayerPrefs.GetString("ChrisaSkillOne") != "")
                    skillOne = PlayerPrefs.GetString("ChrisaSkillOne");
                else
                {
                    skillOne = "Powder Keg";
                    PlayerPrefs.SetString("ChrisaSkillOne", skillOne);
                }
                skillTwo = "";
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
                if (PlayerPrefs.GetString("ChrisaSkillOne") != "")
                    skillOne = PlayerPrefs.GetString("ChrisaSkillOne");
                else
                {
                    skillOne = "Powder Keg";
                    PlayerPrefs.SetString("ChrisaSkillOne", skillOne);
                }
                skillTwo = "";
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
                if (PlayerPrefs.GetString("ChrisaSkillOne") != "")
                    skillOne = PlayerPrefs.GetString("ChrisaSkillOne");
                else
                {
                    skillOne = "Powder Keg";
                    PlayerPrefs.SetString("ChrisaSkillOne", skillOne);
                }
                skillTwo = "";
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
                traitTwo = "";

                if (PlayerPrefs.GetString("KurtzleSkillOne") != "")
                    skillOne = PlayerPrefs.GetString("KurtzleSkillOne");
                else
                {
                    skillOne = "Hex Ball";
                    PlayerPrefs.SetString("KurtzleSkillOne", skillOne);
                }

                skillTwo = "";
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

                if (PlayerPrefs.GetString("KurtzleSkillOne") != "")
                    skillOne = PlayerPrefs.GetString("KurtzleSkillOne");
                else
                {
                    skillOne = "Hex Ball";
                    PlayerPrefs.SetString("KurtzleSkillOne", skillOne);
                }

                skillTwo = "";
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
