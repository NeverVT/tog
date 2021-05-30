using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCharacterCS : MonoBehaviour
{
    void Update()
    {       
        if(CharacterScreen.characterName.Replace("(Clone)", "").Trim() == this.transform.name)
            this.transform.gameObject.SetActive(true);
        else
            this.transform.gameObject.SetActive(false);
    }
}
