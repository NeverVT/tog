using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterName : MonoBehaviour
{
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "TitleScreen")
            this.GetComponent<TextMesh>().text = CharacterScreen.characterName.Replace("(Clone)", "").Trim();
        //else
            //this.GetComponent<TextMesh>().text = CharacterControl.getName();
    }
}
