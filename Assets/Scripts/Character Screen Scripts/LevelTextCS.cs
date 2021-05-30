using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTextCS : MonoBehaviour
{
    void Update()
    {
        int currentLvl = 0;
        if(CharacterScreen.characterName.Replace("(Clone)", "").Trim() == "Urp")
            currentLvl = Team.urp;
        else if (CharacterScreen.characterName.Replace("(Clone)", "").Trim() == "Chrisa")
            currentLvl = Team.chrisa;
        else if (CharacterScreen.characterName.Replace("(Clone)", "").Trim() == "Kurtzle")
            currentLvl = Team.kurtzle;

        if(currentLvl == 1)
            this.GetComponent<TextMesh>().text = "1";
        else if(currentLvl == 2 || currentLvl == 3)
            this.GetComponent<TextMesh>().text = "2";
        else if(currentLvl >= 4 && currentLvl <= 6)
            this.GetComponent<TextMesh>().text = "3";
        else if(currentLvl == 7)
            this.GetComponent<TextMesh>().text = "4";
        else   
            this.GetComponent<TextMesh>().text = "";
            
    }
}
