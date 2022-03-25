using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{
    public static Team team;
    public static string selectedCharacter;
    public static int urpLvl;
    public static int chrisaLvl;
    public static int kurtzleLvl;

    private void Start()
    {
        selectedCharacter = PlayerPrefs.GetString("selectedCharacter");
        if (selectedCharacter == "") selectedCharacter = "Urp";

        urpLvl = PlayerPrefs.GetInt("UrpLvl");
        if(urpLvl == 0)
            urpLvl = 1;
        chrisaLvl = PlayerPrefs.GetInt("ChrisaLvl");
        if(chrisaLvl == 0)
            chrisaLvl = 1;
        kurtzleLvl = PlayerPrefs.GetInt("KurtzleLvl");
        if(kurtzleLvl == 0)
            kurtzleLvl = 1;    
    }

    public void addLevelToCharacter(string character) //Adds a level to the character bought in the Ship Screen
    {
        switch(character)
        {
            case "Urp":
                if(urpLvl < 10)
                    urpLvl++;
                PlayerPrefs.SetInt("UrpLvl", urpLvl);
                break;
            case "Chrisa":
                if(chrisaLvl < 10)
                    chrisaLvl++;
                PlayerPrefs.SetInt("ChrisaLvl", chrisaLvl);
                break;
            case "Kurtzle":
                if(kurtzleLvl < 10)
                    kurtzleLvl++;
                PlayerPrefs.SetInt("KurtzleLvl", kurtzleLvl);
                break;          
        }
    }   

    public static int getLevel(string character)
    {
        switch(character)
        {
            case "UrpLvl":
                return urpLvl;
            case "ChrisaLvl":
                return chrisaLvl;
            case "KurtzleLvl":
                return kurtzleLvl;
        }
        return 0;
    }
}
