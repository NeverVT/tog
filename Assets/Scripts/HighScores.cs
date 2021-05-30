using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScores : MonoBehaviour
{
    public Sprite chrisa;
    public Sprite kurtzle;
    public Sprite urp;
    public Sprite bear;
    public Sprite dobby;
    public Sprite wolf;

    void Awake()
    {
        loadScores("ScoreOne");
        loadScores("ScoreTwo");
        loadScores("ScoreThree");
        loadScores("ScoreFour");
        loadScores("ScoreFive");
        loadScores("ScoreSix");
        loadScores("ScoreSeven");
        loadScores("ScoreEight");
    }

    private void loadScores(string score)
    {
        if (PlayerPrefs.GetInt(score + "TotalScore").ToString() != "0")
        {
            transform.Find(score).gameObject.transform.Find("TotalScore").GetComponent<TextMesh>().text = PlayerPrefs.GetInt(score + "TotalScore").ToString();
            transform.Find(score).gameObject.transform.Find("FloorScore").GetComponent<TextMesh>().text = PlayerPrefs.GetInt(score + "FloorScore").ToString();
            transform.Find(score).gameObject.transform.Find("GoldScore").GetComponent<TextMesh>().text = PlayerPrefs.GetInt(score + "GoldScore").ToString();
            transform.Find(score).gameObject.transform.Find("GoblinScore").GetComponent<TextMesh>().text = PlayerPrefs.GetInt(score + "GoblinScore").ToString();
            transform.Find(score).gameObject.transform.Find("BossScore").GetComponent<TextMesh>().text = PlayerPrefs.GetInt(score + "BossScore").ToString();
        }           
        else
        {
            transform.Find(score).gameObject.transform.Find("TotalScore").GetComponent<TextMesh>().text = "";
            transform.Find(score).gameObject.transform.Find("FloorScore").GetComponent<TextMesh>().text = "";
            transform.Find(score).gameObject.transform.Find("GoldScore").GetComponent<TextMesh>().text = "";
            transform.Find(score).gameObject.transform.Find("GoblinScore").GetComponent<TextMesh>().text = "";
            transform.Find(score).gameObject.transform.Find("BossScore").GetComponent<TextMesh>().text = "";
        }                       
        if (PlayerPrefs.GetString(score + "PartyOne") != "")
            changeSprite(PlayerPrefs.GetString(score + "PartyOne"), transform.Find(score).gameObject.transform.Find("PartyOne").gameObject);
        else
            transform.Find(score).gameObject.transform.Find("PartyOne").gameObject.SetActive(false);
        if (PlayerPrefs.GetString(score + "PartyTwo") != "")
            changeSprite(PlayerPrefs.GetString(score + "PartyTwo"), transform.Find(score).gameObject.transform.Find("PartyTwo").gameObject);
        else
            transform.Find(score).gameObject.transform.Find("PartyTwo").gameObject.SetActive(false);
        if (PlayerPrefs.GetString(score + "PartyThree") != "")
            changeSprite(PlayerPrefs.GetString(score + "PartyThree"), transform.Find(score).gameObject.transform.Find("PartyThree").gameObject);
        else
            transform.Find(score).gameObject.transform.Find("PartyThree").gameObject.SetActive(false);

    }

    void changeSprite(string name, GameObject icon)
    {
        switch(name)
        {
            case "Chrisa":
                icon.GetComponent<SpriteRenderer>().sprite = chrisa;
                break;
            case "Kurtzle":
                icon.GetComponent<SpriteRenderer>().sprite = kurtzle;
                break;
            case "Urp":
                icon.GetComponent<SpriteRenderer>().sprite = urp;
                break;
            case "Bear":
                icon.GetComponent<SpriteRenderer>().sprite = bear;
                break;
            case "Dobby":
                icon.GetComponent<SpriteRenderer>().sprite = dobby;
                break;
            case "Wolf":
                icon.GetComponent<SpriteRenderer>().sprite = wolf;
                break;
        }
    }
}
