using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recruitChestAnimationController : MonoBehaviour
{
    public Team team;
    public Ship ship;
    public GameObject urp;
    public GameObject chrisa;
    public GameObject kurtzle;

    //These functions are called in the animation for Chest
    void activateRecruit()
    {
        switch(Ship.character)
        {
            case "Urp":
                urp.gameObject.SetActive(true);
                break;
            case "Chrisa":
                chrisa.gameObject.SetActive(true);
                break;
            case "Kurtzle":
                kurtzle.gameObject.SetActive(true);
                break;
        }    
    }

    void deactivateRecruit()
    {
        urp.gameObject.SetActive(false);
        chrisa.gameObject.SetActive(false);
        kurtzle.gameObject.SetActive(false);
        ship.ChangeRecruit();
    }

    void addLevelToRecruit()
    {                  
        team.addLevelToCharacter(Ship.character);  
    }
}
