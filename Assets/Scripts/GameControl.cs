using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public static GameScript gameScript;
    public static Artifacts artifactScript;

    public static GameControl control;
    public static bool firstTime = true;

    public static int score;
    public static int monstersKilled;
    public static int bossessKilled;
    public static int goldScore;
    public static int floorsReached;

    public static bool bossUp = false;
    public static bool miniBossUp = false;
    public static bool screenUp = false;

    public static double gold = 0;

    public static bool bloodlust = false;
    public static bool vanish = false;
    public static bool overpowered = false;
    public static bool sacrifice = false;
    public static bool execute = false;
    public static bool blink = false;
    public static bool powderKeg = false;
    public static bool hexBall = false;
    public static int doubleShot = 0;
    public static bool bomb = false;
    public static bool dragonShot = false;
    public static bool bomberShot = false;
    public static bool smite = false;
    public static bool targeted = false;
    public static bool calculated = false;

    public static List<Sprite> characters = new List<Sprite>();
    public static List<string> names = new List<string>();
    public static List<CharacterWeapon> weapons = new List<CharacterWeapon>();
    public static List<CharacterArmor> armors = new List<CharacterArmor>();
    public static List<Spell> spells = new List<Spell>();
    public static List<Trait> traits = new List<Trait>();
    public static List<Artifacts> artifacts = new List<Artifacts>();

    public static GameObject shopArtifact;

    void Awake ()
    {
	    if(control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }




	}
}
