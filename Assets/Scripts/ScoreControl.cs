using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreControl : MonoBehaviour
{
    public static int playerGold = PlayerPrefs.GetInt("PlayerGold");

    public static int healthScore;
    public static int swordScore;
    public static int goldScore;
    public static int goblinScore;
    public static int bossScore;
    public static int totalScore;
    public static string partyOne;
    public static string partyTwo;
    public static string partyThree;

    public static void saveHighScore()
    {
        totalScore = goldScore + healthScore + swordScore + goblinScore + bossScore;

        if (totalScore > PlayerPrefs.GetInt("ScoreOneTotalScore"))
        {
            switchScores("ScoreSeven", "ScoreEight");
            switchScores("ScoreSix", "ScoreSeven");
            switchScores("ScoreFive", "ScoreSix");
            switchScores("ScoreFour", "ScoreFive");
            switchScores("ScoreThree", "ScoreFour");
            switchScores("ScoreTwo", "ScoreThree");
            switchScores("ScoreOne", "ScoreTwo");
            setScores("ScoreOne");
        }
        else if (totalScore > PlayerPrefs.GetInt("ScoreTwoTotalScore"))
        {
            switchScores("ScoreSeven", "ScoreEight");
            switchScores("ScoreSix", "ScoreSeven");
            switchScores("ScoreFive", "ScoreSix");
            switchScores("ScoreFour", "ScoreFive");
            switchScores("ScoreThree", "ScoreFour");
            switchScores("ScoreTwo", "ScoreThree");
            setScores("ScoreTwo");
        }
        else if (totalScore > PlayerPrefs.GetInt("ScoreThreeTotalScore"))
        {
            switchScores("ScoreSeven", "ScoreEight");
            switchScores("ScoreSix", "ScoreSeven");
            switchScores("ScoreFive", "ScoreSix");
            switchScores("ScoreFour", "ScoreFive");
            switchScores("ScoreThree", "ScoreFour");
            setScores("ScoreThree");
        }
        else if (totalScore > PlayerPrefs.GetInt("ScoreFourTotalScore"))
        {
            switchScores("ScoreSeven", "ScoreEight");
            switchScores("ScoreSix", "ScoreSeven");
            switchScores("ScoreFive", "ScoreSix");
            switchScores("ScoreFour", "ScoreFive");
            setScores("ScoreFour");
        }
        else if (totalScore > PlayerPrefs.GetInt("ScoreFiveTotalScore"))
        {
            switchScores("ScoreSeven", "ScoreEight");
            switchScores("ScoreSix", "ScoreSeven");
            switchScores("ScoreFive", "ScoreSix");
            setScores("ScoreFive");
        }
        else if (totalScore > PlayerPrefs.GetInt("ScoreSixTotalScore"))
        {
            switchScores("ScoreSeven", "ScoreEight");
            switchScores("ScoreSix", "ScoreSeven");
            setScores("ScoreSix");
        }
        else if (totalScore > PlayerPrefs.GetInt("ScoreSevenTotalScore"))
        {
            switchScores("ScoreSeven", "ScoreEight");
            setScores("ScoreSeven");
        }
        else if (totalScore > PlayerPrefs.GetInt("ScoreEightTotalScore"))
        {
            setScores("ScoreEight");
        }
        PlayerPrefs.Save();
    }

    public static void addPayment(int gold)
    {
        playerGold += gold;
        PlayerPrefs.SetInt("PlayerGold", playerGold);
    }

    private static void switchScores(string slotOne, string slotTwo)
    {
        int tempTotalScore = PlayerPrefs.GetInt(slotOne + "TotalScore");
        int tempGoldScore = PlayerPrefs.GetInt(slotOne + "GoldScore");
        int tempGoblinScore = PlayerPrefs.GetInt(slotOne + "GoblinScore");
        int tempBossScore = PlayerPrefs.GetInt(slotOne + "BossScore");
        //int tempFloorScore = PlayerPrefs.GetInt(slotOne + "FloorScore");
        string tempPartyOne = PlayerPrefs.GetString(slotOne + "PartyOne");
        string tempPartyTwo = PlayerPrefs.GetString(slotOne + "PartyTwo");
        string tempPartyThree = PlayerPrefs.GetString(slotOne + "PartyThree");

        PlayerPrefs.SetInt(slotTwo + "TotalScore", tempTotalScore);
        PlayerPrefs.SetInt(slotTwo + "GoldScore", tempGoldScore);
        PlayerPrefs.SetInt(slotTwo + "GoblinScore", tempGoblinScore);
        PlayerPrefs.SetInt(slotTwo + "BossScore", tempBossScore);
        //PlayerPrefs.SetInt(slotTwo + "FloorScore", tempFloorScore);
        PlayerPrefs.SetString(slotTwo + "PartyOne", tempPartyOne);
        PlayerPrefs.SetString(slotTwo + "PartyTwo", tempPartyTwo);
        PlayerPrefs.SetString(slotTwo + "PartyThree", tempPartyThree);
    }

    private static void setScores(string slot)
    {
        PlayerPrefs.SetInt(slot + "TotalScore", totalScore);
       // PlayerPrefs.SetInt(slot + "FloorScore", floorScore);
        PlayerPrefs.SetInt(slot + "GoblinScore", goblinScore);
        PlayerPrefs.SetInt(slot + "BossScore", bossScore);
        PlayerPrefs.SetInt(slot + "GoldScore", goldScore);
        PlayerPrefs.SetString(slot + "PartyOne", partyOne);
        PlayerPrefs.SetString(slot + "PartyTwo", partyTwo);
        PlayerPrefs.SetString(slot + "PartyThree", partyThree);
    }

    public static void resetScores()
    {
        healthScore = 0;
        swordScore = 0;
        goldScore = 0;
        goblinScore = 0;
        bossScore = 0;
        totalScore = 0;
    }
}
