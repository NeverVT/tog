using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{
    public static Team team;
    public static string characterOne;
    public static string characterTwo;
    public static string characterThree;
    public static int urp;
    public static int chrisa;
    public static int kurtzle;
    public static int dobby;
    public static int bear;
    public static int wolf;

    private void Start()
    {/*
        characterOne = PlayerPrefs.GetString("0");
        characterTwo = PlayerPrefs.GetString("1");
        characterThree = PlayerPrefs.GetString("2");
        if (characterOne == "") characterOne = "Urp";
        if (characterTwo == "") characterTwo = "Chrisa";
        if (characterThree == "") characterThree = "Kurtzle";

        urp = PlayerPrefs.GetInt("Urp");
        if(urp == 0)
            urp = 1;
        chrisa = PlayerPrefs.GetInt("Chrisa");
        if(chrisa == 0)
            chrisa = 1;
        kurtzle = PlayerPrefs.GetInt("Kurtzle");
        if(kurtzle == 0)
            kurtzle = 1;
        dobby = PlayerPrefs.GetInt("Dobby");
        bear = PlayerPrefs.GetInt("Bear");
        wolf = PlayerPrefs.GetInt("Wolf");*/
    }

    public void addLevelToCharacter(string character) //Adds a level to the character bought in the Ship Screen
    {
        switch(character)
        {
            case "Urp":
                if(urp < 10)
                    urp++;
                PlayerPrefs.SetInt("Urp", urp);
                break;
            case "Chrisa":
                if(chrisa < 10)
                    chrisa++;
                PlayerPrefs.SetInt("Chrisa", chrisa);
                break;
            case "Kurtzle":
                if(kurtzle < 10)
                    kurtzle++;
                PlayerPrefs.SetInt("Kurtzle", kurtzle);
                break;
            case "Dobby":
                dobby++;
                PlayerPrefs.SetInt("Dobby", dobby);
                break;
            case "Bear":
                bear++;
                PlayerPrefs.SetInt("Bear", bear);
                break;
            case "Wolf":
                wolf++;
                PlayerPrefs.SetInt("Wolf", wolf);
                break;
        }
    }   

    public static int getLevel(string character)
    {
        switch(character)
        {
            case "Urp":
                return urp;
            case "Chrisa":
                return chrisa;
            case "Kurtzle":
                return kurtzle;
        }
        return 0;
    }
}
