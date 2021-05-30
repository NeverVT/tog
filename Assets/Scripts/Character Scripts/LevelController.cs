using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public GameObject lvlOne;
    public GameObject lvlTwo;
    public GameObject lvlThree;
    public GameObject lvlFour;

    private int currentLvl;
  
    void Update()
    {
        currentLvl = Team.getLevel(this.name);
        switch(currentLvl)
        {
            case 1: //Level One
                lvlOne.gameObject.SetActive(true);              
                break;
            case 2: //Level One Full
                lvlOne.transform.GetChild(1).gameObject.SetActive(true);
                break;
            case 3: //Level Two
                lvlOne.gameObject.SetActive(false);
                lvlTwo.gameObject.SetActive(true);
                break;
            case 4: //Level Two part One Full
                lvlOne.gameObject.SetActive(false);
                lvlTwo.gameObject.SetActive(true);
                lvlTwo.transform.GetChild(1).gameObject.SetActive(true);
                break;
            case 5: //Level Two part Two Full
                lvlOne.gameObject.SetActive(false);
                lvlTwo.gameObject.SetActive(true);
                lvlTwo.transform.GetChild(1).gameObject.SetActive(true);
                lvlTwo.transform.GetChild(3).gameObject.SetActive(true);
                break;
            case 6: //Level Three
                lvlOne.gameObject.SetActive(false);
                lvlTwo.gameObject.SetActive(false);
                lvlThree.gameObject.SetActive(true);
                break;
            case 7: //Level Three part One Full
                lvlOne.gameObject.SetActive(false);
                lvlTwo.gameObject.SetActive(false);
                lvlThree.gameObject.SetActive(true);
                lvlThree.transform.GetChild(1).gameObject.SetActive(true);
                break;
            case 8: //Level Three part Two Full
                lvlOne.gameObject.SetActive(false);
                lvlTwo.gameObject.SetActive(false);
                lvlThree.gameObject.SetActive(true);
                lvlThree.transform.GetChild(1).gameObject.SetActive(true);
                lvlThree.transform.GetChild(3).gameObject.SetActive(true);
                break;
            case 9: //Level Three part Three Full
                lvlOne.gameObject.SetActive(false);
                lvlTwo.gameObject.SetActive(false);
                lvlThree.gameObject.SetActive(true);
                lvlThree.transform.GetChild(1).gameObject.SetActive(true);
                lvlThree.transform.GetChild(3).gameObject.SetActive(true);
                lvlThree.transform.GetChild(5).gameObject.SetActive(true);
                break;
            case 10: //Level Four
                lvlOne.gameObject.SetActive(false);
                lvlTwo.gameObject.SetActive(false);
                lvlThree.gameObject.SetActive(false);
                lvlFour.gameObject.SetActive(true);
                break;
        }
    }
}
