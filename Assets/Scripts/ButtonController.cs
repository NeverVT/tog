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

    public void selectArtifactInShop() //Used on Artifact Tiles in the shop to tell GameControl which artifact the player is buying
    {
        if (GameControl.gold > 20)
        {
            if(GameControl.shopArtifact != null) //Artifact is selected
            {
                if (GameControl.shopArtifact == EventSystem.current.currentSelectedGameObject) //Same artififact is selected, de-select it
                {
                    swapBackgrounds(GameControl.shopArtifact, false);
                    GameControl.shopArtifact = null;                   
                }                 
                else
                {
                    swapBackgrounds(GameControl.shopArtifact, false);
                    GameControl.shopArtifact = EventSystem.current.currentSelectedGameObject;
                    swapBackgrounds(GameControl.shopArtifact, true);
                }                 
            }                       
            else
            {
                GameControl.shopArtifact = EventSystem.current.currentSelectedGameObject;
                swapBackgrounds(GameControl.shopArtifact, true);
            }
        }
        else
            Debug.Log("Not Enough gold (20)");       
      
    }

    private void swapBackgrounds(GameObject obj, bool on)
    {
        if(on)
        {
            obj.transform.GetChild(0).gameObject.SetActive(false);
            obj.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            obj.transform.GetChild(0).gameObject.SetActive(true);
            obj.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}
