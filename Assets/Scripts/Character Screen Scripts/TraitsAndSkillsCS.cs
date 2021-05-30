using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitsAndSkillsCS : MonoBehaviour
{
    bool leveled = false;
    public int level;
    void Start()
    {       
        if (this.transform.parent.transform.name == "Urp")
            if (Team.urp >= level)
                leveled = true;
        if (this.transform.parent.transform.name == "Chrisa")
            if (Team.chrisa >= level)
                leveled = true;
        if (this.transform.parent.transform.name == "Kurtzle")
            if (Team.kurtzle >= level)
                leveled = true;
        if (this.transform.parent.transform.name == "Dobby")
            if (Team.dobby >= level)
                leveled = true;
        if (this.transform.parent.transform.name == "Bear")
            if (Team.bear >= level)
                leveled = true;
        if (this.transform.parent.transform.name == "Wolf")
            if (Team.wolf >= level)
                leveled = true;      
    }

    private void Update()
    {
        if (leveled)
        {
            if(CharacterScreen.characterName == "Urp")
            {
                if (PlayerPrefs.GetString("UrpTraitOne") == this.name || PlayerPrefs.GetString("UrpTraitTwo") == this.name ||
               PlayerPrefs.GetString("UrpSkillOne") == this.name || PlayerPrefs.GetString("UrpSkillTwo") == this.name)
                {
                    this.transform.Find("Icon").transform.gameObject.SetActive(true);
                    this.transform.Find("Icon").transform.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
                    this.transform.Find("Text").transform.GetComponent<TextMesh>().text = "";
                }
                else
                {
                    this.transform.Find("Icon").transform.gameObject.SetActive(true);
                    this.transform.Find("Icon").transform.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .25f);
                    this.transform.Find("Text").transform.GetComponent<TextMesh>().text = "";
                }
            }
            else if (CharacterScreen.characterName == "Chrisa")
            {
                if (PlayerPrefs.GetString("ChrisaTraitOne") == this.name || PlayerPrefs.GetString("ChrisaTraitTwo") == this.name ||
               PlayerPrefs.GetString("ChrisaSkillOne") == this.name || PlayerPrefs.GetString("ChrisaSkillTwo") == this.name)
                {
                    this.transform.Find("Icon").transform.gameObject.SetActive(true);
                    this.transform.Find("Icon").transform.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
                    this.transform.Find("Text").transform.GetComponent<TextMesh>().text = "";
                }
                else
                {
                    this.transform.Find("Icon").transform.gameObject.SetActive(true);
                    this.transform.Find("Icon").transform.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .25f);
                    this.transform.Find("Text").transform.GetComponent<TextMesh>().text = "";
                }
            }
            else if (CharacterScreen.characterName == "Kurtzle")
            {
                if (PlayerPrefs.GetString("KurtzleTraitOne") == this.name || PlayerPrefs.GetString("KurtzleTraitTwo") == this.name ||
               PlayerPrefs.GetString("KurtzleSkillOne") == this.name || PlayerPrefs.GetString("KurtzleSkillTwo") == this.name)
                {
                    this.transform.Find("Icon").transform.gameObject.SetActive(true);
                    this.transform.Find("Icon").transform.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
                    this.transform.Find("Text").transform.GetComponent<TextMesh>().text = "";
                }
                else
                {
                    this.transform.Find("Icon").transform.gameObject.SetActive(true);
                    this.transform.Find("Icon").transform.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .25f);
                    this.transform.Find("Text").transform.GetComponent<TextMesh>().text = "";
                }
            }
        }
        else
        {
            this.transform.Find("Icon").transform.gameObject.SetActive(false);
            this.transform.Find("Text").transform.GetComponent<TextMesh>().text = "?";
        }
    }
}
