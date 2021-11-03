using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    public GameScript gameScript;
    public CharacterControl characterControl;

    public GameObject playerHealth;


    void Start()
    {
        
    }

    void Update()
    {
        playerHealth.GetComponent<TextMesh>().text = characterControl.getCurrentHealth().ToString(); //Changes the Health Bar Text on the Game Screen
    }
}
