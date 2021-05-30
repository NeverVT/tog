using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using System.Reflection;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public static string character;

    void Start()
    {
        character = PlayerPrefs.GetString("ShipCharacter");
        if (character == "")
        {
            int r = UnityEngine.Random.Range(1, 4);
            switch (r)
            {
                case 1:
                    character = "Urp";
                    break;
                case 2:
                    character = "Chrisa";
                    break;
                case 3:
                    character = "Kurtzle";
                    break;
                case 4:
                    character = "Dobby";
                    break;
                case 5:
                    character = "Bear";
                    break;
                case 6:
                    character = "Wolf";
                    break;
            }
        }
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetString("ShipCharacter", character);
    }

    public void ChangeRecruit()
    {
        Stack<String> recruits = new Stack<String>();
        if(Team.urp < 10)
            recruits.Push("Urp");
        if(Team.chrisa < 10)
            recruits.Push("Chrisa");
        if(Team.kurtzle < 10)
            recruits.Push("Kurtzle");
        
        if(recruits.Count > 0)
        {
            int r = UnityEngine.Random.Range(1, recruits.Count + 1);
            for(int i = 0; i < r; i++)
            {
                character = recruits.Pop();
            }
        }       
    }
}


