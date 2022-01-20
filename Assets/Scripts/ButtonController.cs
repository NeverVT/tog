using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour
{
    public GameObject tooltip;
    public string buildType;
    public void triggerTooltip()
    {
        tooltip.SetActive(true);
    }

    public void turnOffTooltip()
    {
        tooltip.SetActive(false);
    }

    public void changeCharacterBuild() //Used in the character screen of the barracks to select one of two possible build paths for a character
    {
        PlayerPrefs.SetString(EventSystem.current.currentSelectedGameObject.gameObject.transform.parent.name + buildType, EventSystem.current.currentSelectedGameObject.gameObject.name);
    }
}
