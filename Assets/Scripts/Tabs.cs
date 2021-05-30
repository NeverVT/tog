using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tabs : MonoBehaviour
{
    public CharacterControl characterControl;
    public GameObject tabOne;
    public GameObject tabTwo;
    public GameObject tabThree;

    // Update is called once per frame
    void Update()
    {
        if(characterControl.activeCharacter == 0)
        {
            tabOne.SetActive(true);
            tabTwo.SetActive(false);
            tabThree.SetActive(false);
        }
        else if (characterControl.activeCharacter == 1)
        {
            tabOne.SetActive(false);
            tabTwo.SetActive(true);
            tabThree.SetActive(false);
        }
        else if (characterControl.activeCharacter == 2)
        {
            tabOne.SetActive(false);
            tabTwo.SetActive(false);
            tabThree.SetActive(true);
        }
    }
}
