using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public static GameControl control;

    public static int score;
    public static int monstersKilled;
    public static int bossessKilled;
    public static int goldScore;
    public static int floorsReached;

    public static bool bossUp = false;
    public static bool miniBossUp = false;

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
	
	void Update ()
    {
		
	}
}
