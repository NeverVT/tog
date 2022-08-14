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
        if(!GameControl.screenUp)
        {
            if (!this.CompareTag("Artifact"))
                tooltip.SetActive(true);
        }
    }

    public void turnOffTooltip()
    {
        tooltip.SetActive(false);
    }

    public void tutorialTextBoxClicked()
    {
        if (PlayerPrefs.GetString("Tracing Tutorial Done") != "")    
            this.gameObject.SetActive(false);
    }

    public void turnOffTracingHand()
    {
        this.transform.GetChild(1).gameObject.SetActive(false);
    }

    public void changeCharacterBuild() //Used in the character screen of the barracks to select one of two possible build paths for a character
    {
        PlayerPrefs.SetString(EventSystem.current.currentSelectedGameObject.gameObject.transform.parent.name + buildType, EventSystem.current.currentSelectedGameObject.gameObject.name);
    }

    public void selectArtifactInShop() //Used on Artifact Tiles in the shop to tell GameControl which artifact the player is buying
    {
        if (GameControl.screenUp)
        {
            if (GameControl.gold >= 30)
            {
                if (GameControl.shopArtifact != null) //Artifact is selected
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
        else
        {
            int col = this.gameObject.GetComponent<Tile>().mCol;
            int row = this.gameObject.GetComponent<Tile>().mRow;
            GameControl.artifactScript.collectArtifact(this.gameObject);

            Destroy(this.gameObject);
            Destroy(GameControl.gameScript.board[row, col].gameObject);
            GameControl.gameScript.board[row, col].mType = "Collected";
            GameControl.gameScript.shiftBoard();
        }
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
