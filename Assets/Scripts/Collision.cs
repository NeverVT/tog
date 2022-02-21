using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
//using UnityEditor.PackageManager.Requests;

public class Collision : MonoBehaviour
{
    public GameObject cameraObj;
    public CharacterControl characterControl;
    public GameObject selectedItem;
    public GameObject characterScreen;
    public GameObject loadingDoors;
    GameObject selectedCharacter;
    GameObject swapCharacter;
    GameObject cunningLine;
    int pCunningRow;
    int pCunningCol;
    public GameObject gameScript;
    Vector3 swapPos;
    int characterSelected = -1;
    Vector3 pos = new Vector3(0, 0, 0);
    static int row = 0;
    static int col = 0;
    int pRow = 0;
    int pCol = 0;
    string type = "";
    string previousType = "";
    string boss = "";
    bool swapActive = false;
    int length = 0;
    int health = 0;
    int damage = 0;
    bool trinket = false;
    string currentClass = "";
    int otherSlot;
    int swapSlot;
    string tempName;
    public bool cunningUsed = false;
    string savedPreviousType;
    private bool upgrading = false;
    private string itemType = "";
    private string equipmentType = "";
    private string equipmentCharacter = "";

    private void Start()
    {
        PlayerPrefs.SetInt("PlayerShard", 100);
    }

    void Update()
    {
        if (gameScript != null && gameScript.GetComponent<GameScript>().collected.Count > 0 && !gameScript.GetComponent<GameScript>().attackPhase)
        {         
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    type = gameScript.GetComponent<GameScript>().board[i, j].GetComponent<Tile>().mType;
                    if(gameScript.GetComponent<GameScript>().collected.Peek().GetComponent<Tile>().mType == "Collected")
                    {
                        previousType = savedPreviousType;
                    }
                    else 
                        previousType = gameScript.GetComponent<GameScript>().collected.Peek().GetComponent<Tile>().mType;
                    if (type != "Collected")
                    {
                        if (isCollectable(type, previousType))
                        {
                            if (gameScript.GetComponent<GameScript>().board[i, j].transform.childCount > 9)
                            { 
                                if (gameScript.GetComponent<GameScript>().board[i, j].transform.GetChild(8) != null)
                                    if (gameScript.GetComponent<GameScript>().board[i, j].transform.GetChild(8).GetComponent<Renderer>() != null)
                                        gameScript.GetComponent<GameScript>().board[i, j].transform.GetChild(8).GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
                                if (type == "Goblin" && gameScript.GetComponent<GameScript>().board[i, j].transform.GetChild(9) != null)
                                {
                                    if (gameScript.GetComponent<GameScript>().board[i, j].transform.GetChild(9).GetComponent<Renderer>() != null)
                                        gameScript.GetComponent<GameScript>().board[i, j].transform.GetChild(9).GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
                                }   
                            }
                        }
                        else
                        {
                            if (gameScript.GetComponent<GameScript>().board[i, j].transform.childCount > 9)
                            {
                                if (gameScript.GetComponent<GameScript>().board[i, j].transform.GetChild(8) != null)
                                    if (gameScript.GetComponent<GameScript>().board[i, j].transform.GetChild(8).GetComponent<Renderer>() != null)
                                        gameScript.GetComponent<GameScript>().board[i, j].transform.GetChild(8).GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .25f);
                                if (type == "Goblin" && gameScript.GetComponent<GameScript>().board[i, j].transform.GetChild(9) != null)
                                {
                                    if (gameScript.GetComponent<GameScript>().board[i, j].transform.GetChild(9).GetComponent<Renderer>() != null)
                                        gameScript.GetComponent<GameScript>().board[i, j].transform.GetChild(9).GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .25f);
                                }
                            }
                        }
                    }
                }
            }       
        }      

        else if (gameScript != null && gameScript.GetComponent<GameScript>() != null && SceneManager.GetActiveScene().name == "GameScreen")
        {
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 6; j++)
                {
                    if(gameScript.GetComponent<GameScript>().board[i, j] != null && gameScript.GetComponent<GameScript>().board[i, j].transform.childCount > 9)
                    {
                        if (gameScript.GetComponent<GameScript>().board[i, j].transform.GetChild(8) != null)
                        {
                            if(gameScript.GetComponent<GameScript>().board[i, j].transform.GetChild(8).GetComponent<Renderer>() != null)
                                gameScript.GetComponent<GameScript>().board[i, j].transform.GetChild(8).GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
                        }
                        if (gameScript.GetComponent<GameScript>().board[i, j].GetComponent<Tile>().mType == "Goblin" && gameScript.GetComponent<GameScript>().board[i, j].transform.GetChild(9) != null)
                        {
                            if (gameScript.GetComponent<GameScript>().board[i, j].transform.GetChild(9).GetComponent<Renderer>() != null)
                                gameScript.GetComponent<GameScript>().board[i, j].transform.GetChild(9).GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
                        }
                    }                   
                }

        }      
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("OptionsGame"))
        {
            if (!gameScript.GetComponent<GameScript>().screenUp)
            {
                GameObject screen = (GameObject)Instantiate(Resources.Load("Options"), new Vector3(3.21F, -5.31F, -2.93f), Quaternion.identity);
                gameScript.GetComponent<GameScript>().screenUp = true;
            }
        }
        else if (other.CompareTag("Exit"))
        {
            if (other.transform.parent.name == "PopUp")
            {
                other.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                UpdateText.updateText = "";
                Destroy(other.transform.parent.gameObject);
                Destroy(other.gameObject);
                gameScript.GetComponent<GameScript>().screenUp = false;
            }
            for (int i = 0; i < 3; i++)
            {
                if (gameScript.GetComponent<GameScript>().items[i] != null)
                    Destroy(gameScript.GetComponent<GameScript>().items[i].gameObject);
            }
        }
        else if (other.transform.name == "ToTitleScreen" || other.transform.name == "ToShip" || other.transform.name == "ToBarracks")
        {
            other.GetComponent<Animator>().SetTrigger("Pressed");
            if (gameObject.transform.Find("CharacterScreen(Clone)") != null)
            {
                Debug.Log("Exists");
                Destroy(gameObject.transform.Find("CharacterScreen(Clone)"));
            }
        }
        else if (other.transform.name == "ToTSfromGS")
        {
            GameControl.gold = 0;
            StartCoroutine(loadScene("TitleScreen"));
            //other.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (other.transform.name == "How to play")
        {
            other.gameObject.SetActive(false);
        }
        else if (other.transform.name == "ToHighScores")
        {

        }
        else if (other.transform.name == "ToSettings")
        {

        }
        else if (other.transform.name == "ToGameScreen")
        {
            //other.GetComponent<Animator>().SetTrigger("Pressed");
        }
        else if (other.transform.name == "ExitArtifact")
        {
            Debug.Log("ExitArtifact");
            Destroy(GameObject.Find("Artifact Screen(Clone)"));
        }

        else if (other.transform.name == "ArtifactsButton")
        {
            GameObject artifactsPage = (GameObject)Instantiate(Resources.Load("ArtifactsPage"), new Vector3(3.2F, -5.33F, -23.61F), Quaternion.identity);
            gameScript.GetComponent<GameScript>().screenUp = true;
            gameScript.GetComponent<Artifacts>().displayArtifacts();
        }
        else if (other.CompareTag("Trinkets"))
        {
            other.gameObject.transform.Find("PopUp").gameObject.SetActive(true);
        }
        else if (other.CompareTag("Buy"))
        {
            if (selectedItem.GetComponent<Item>().mCost <= gameScript.GetComponent<GameScript>().gold)
            {
                string type = selectedItem.GetComponent<Item>().mTitle;
                gameScript.GetComponent<GameScript>().gold -= selectedItem.GetComponent<Item>().mCost;
                Destroy(other.transform.parent.gameObject);
                for (int i = 0; i < 3; i++)
                {
                    if (gameScript.GetComponent<GameScript>().items[i] != null)
                        gameScript.GetComponent<GameScript>().items[i].gameObject.SetActive(false);
                }
                gameScript.GetComponent<GameScript>().shopUp = false;
                gameScript.GetComponent<GameScript>().screenUp = false;
            }
        }
        else if (other.CompareTag("Attributes"))
        {
            GameObject attributeDescription = (GameObject)Instantiate(Resources.Load("Attribute Description"), new Vector3(3.34F, -4.86F, -25.91F), Quaternion.identity);
            attributeDescription.transform.Find("Title").GetComponent<TextMesh>().text = other.name;
            switch (other.name)
            {
                case "Chopper":
                    attributeDescription.transform.Find("Text").GetComponent<TextMesh>().text = "Attacks done vertically \n do increased damage";
                    break;
                case "Cunning":
                    attributeDescription.transform.Find("Text").GetComponent<TextMesh>().text = "Allows you to retrace one \n tile and continue your line";
                    break;
                case "Empath":
                    attributeDescription.transform.Find("Text").GetComponent<TextMesh>().text = "Distributes extra health crystals \n to damaged party members";
                    break;
                case "Hacker":
                    attributeDescription.transform.Find("Text").GetComponent<TextMesh>().text = "Attacks done Horizontally \n do increased damage";
                    break;
                case "Hunter":
                    attributeDescription.transform.Find("Text").GetComponent<TextMesh>().text = "Tougher enemies spawn more \n often and deals greater damage \n to bosses";
                    break;
                case "Impatient":
                    attributeDescription.transform.Find("Text").GetComponent<TextMesh>().text = "Will sometimes spawn \n Mana crystals that can be \n collected with Health crystals \n and lower the cooldown of skills";
                    break;
                case "Lucky":
                    attributeDescription.transform.Find("Text").GetComponent<TextMesh>().text = "Might just be lucky enough \n to come across treasure in \n unlikely places";
                    break;
                case "Meek":
                    attributeDescription.transform.Find("Text").GetComponent<TextMesh>().text = "When this character traces 6* \n or more crystals enemies don’t \n attack for that turn";
                    break;
                case "Pickpocket":
                    attributeDescription.transform.Find("Text").GetComponent<TextMesh>().text = "The character using this item \n can teace through Coins and \n Enemies with a reduced effect";
                    break;
                case "Protector":
                    attributeDescription.transform.Find("Text").GetComponent<TextMesh>().text = "Half of the damage that would \n be delt to another character \n is delt to the character using \n this item instead";
                    break;
                case "Slasher":
                    attributeDescription.transform.Find("Text").GetComponent<TextMesh>().text = "Attacks done Diagonally \n do increased damage";
                    break;
                case "Survivalist":
                    attributeDescription.transform.Find("Text").GetComponent<TextMesh>().text = "When this character traces 6 \n or more rubble enemies don’t \n attack for that turn";
                    break;
                case "Tough":
                    attributeDescription.transform.Find("Text").GetComponent<TextMesh>().text = "Regenerates health each turn";
                    break;
                case "Vamp":
                    attributeDescription.transform.Find("Text").GetComponent<TextMesh>().text = "Gain health when fighting \n enemies";
                    break;
                case "Spiked":
                    attributeDescription.transform.Find("Text").GetComponent<TextMesh>().text = "Deal some damage back \n when hit";
                    break;
                case "Crit":
                    attributeDescription.transform.Find("Text").GetComponent<TextMesh>().text = "Chance to deal double \n double damage when attacking";
                    break;
            }

        }
        else if (other.CompareTag("Trait"))
        {
            other.gameObject.transform.Find("PopUp").gameObject.SetActive(true);
        }
        else if (other.CompareTag("TraitAndSkillsCS"))
        {
            if (other.transform.parent.name == "Urp")
            {
                if (other.transform.name == "Dragon Shot" || other.transform.name == "Bomber Shot")
                {
                    PlayerPrefs.SetString("UrpSkillTwo", other.transform.name);
                }
                if (other.transform.name == "Tough" || other.transform.name == "Shrapnel")
                {
                    PlayerPrefs.SetString("UrpTraitTwo", other.transform.name);
                }
            }
            if (other.transform.parent.name == "Chrisa")
            {
                if (other.transform.name == "Sleight of Hand" || other.transform.name == "Pickpocket")
                {
                    PlayerPrefs.SetString("ChrisaTraitTwo", other.transform.name);
                }
            }
            if (other.transform.parent.name == "Kurtzle")
            {
                if (other.transform.name == "Double Shot" || other.transform.name == "Overpower")
                {
                    PlayerPrefs.SetString("KurtzleSkillTwo", other.transform.name);
                }
                if (other.transform.name == "Hacker" || other.transform.name == "Chopper")
                {
                    PlayerPrefs.SetString("KurtzleTraitTwo", other.transform.name);
                }
            }
        }
        else if (other.CompareTag("BarracksCharacter"))
        {
            //other.transform.Find("Icon").GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .5f);
        }
        else if (other.transform.name == "Reset")
        {
            Debug.Log("Player Prefs Reset");
            PlayerPrefs.DeleteAll();
        }
        else if (other.transform.name == "Recruit Button") //Buying a Character in the Ship Screen
        {
            if (ScoreControl.playerGold >= 0)
            {
                other.GetComponent<Animator>().SetTrigger("Pressed");
                other.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Open");
                ScoreControl.playerGold -= 1000;
                PlayerPrefs.SetInt("PlayerGold", ScoreControl.playerGold);
            }
        }
        else if (other.CompareTag("Spell"))
        {
            gameScript.GetComponent<GameScript>().castSpell(other.gameObject);
        }
        else if (other.CompareTag("OK"))
        {
            Destroy(other.transform.parent.gameObject);
        }
        else if (other.transform.name == "Inspect")
        {
            other.transform.Find("InspectSelected").gameObject.SetActive(true);
        }
        else if (other.transform.name == "Swap")
        {
            other.transform.Find("SwapSelected").gameObject.SetActive(true);
        }

        // ---------- SHOP ---------- //
        else if (other.transform.name == "Shopkeeper Exit")
        {
            Destroy(other.transform.parent.gameObject);
            Destroy(gameScript.GetComponent<GameScript>().items[0].gameObject);
            Destroy(gameScript.GetComponent<GameScript>().items[1].gameObject);
            Destroy(gameScript.GetComponent<GameScript>().items[2].gameObject);
            gameScript.GetComponent<GameScript>().screenUp = false;
        }
        else if (other.transform.name == "Shop Back")
        {
            respawnShopKeeper();
        }
        else if (other.transform.name == "Crystal Shop")
        {
            if (GameControl.gold >= 15)
            {
                characterControl.setCurrentHealth(characterControl.getCurrentHealth() + 15);
                GameControl.gold -= 15;
            }
        }
        else if (other.transform.name == "Item Stat Upgrade")
        {
            if (GameControl.gold >= 15)
            {
                characterSelected = -1;
                upgrading = true;
                Destroy(gameScript.GetComponent<GameScript>().Shop.gameObject);
                for (int i = 0; i < 3; i++)
                {
                    gameScript.GetComponent<GameScript>().items[i].transform.gameObject.SetActive(false);
                }
                gameScript.GetComponent<GameScript>().Shop = (GameObject)Instantiate(Resources.Load("Shop/Shop"), new Vector3(-4.11F, -7.99F, -9.38F), Quaternion.identity);

                for (int i = 0; i < 3; i++)
                {
                    characterControl.selectedCharacter.GetComponent<Character>().weapon.gameObject.SetActive(true);
                    characterControl.selectedCharacter.GetComponent<Character>().weapon.transform.GetChild(0).GetComponent<TextMesh>().text = characterControl.selectedCharacter.GetComponent<Character>().weapon.damage.ToString();
                    characterControl.selectedCharacter.GetComponent<Character>().armor.gameObject.SetActive(true);
                    characterControl.selectedCharacter.GetComponent<Character>().armor.transform.GetChild(0).GetComponent<TextMesh>().text = characterControl.selectedCharacter.GetComponent<Character>().armor.defense.ToString();                
                    characterControl.selectedCharacter.GetComponent<Character>().armor.gameObject.transform.position = new Vector3(characterControl.selectedCharacter.GetComponent<Character>().armor.transform.position.x, characterControl.selectedCharacter.GetComponent<Character>().armor.transform.position.y + 1.5f, characterControl.selectedCharacter.GetComponent<Character>().armor.transform.position.z);
                }
            }
        }
        else if (other.CompareTag("Item"))
        {
            if (gameScript.GetComponent<GameScript>().shopUp)
            {
                if (GameControl.gold < other.GetComponent<Item>().mCost)
                    Debug.Log("Not enough gold");
                else
                {
                    selectedItem = other.gameObject;
                    selectedItem.GetComponent<Item>().mTitle = other.GetComponent<Item>().mTitle;
                    selectedItem.GetComponent<Item>().mDamage = other.GetComponent<Item>().mDamage;
                    selectedItem.GetComponent<Item>().mArmor = other.GetComponent<Item>().mArmor;
                    selectedItem.GetComponent<Item>().mCost = other.GetComponent<Item>().mCost;
                    selectedItem.gameObject.GetComponent<SpriteRenderer>().sprite = other.gameObject.transform.Find("Icon").GetComponent<SpriteRenderer>().sprite;
                    string type = selectedItem.GetComponent<Item>().mTitle;
                    /*
                    //selectedItem.transform.position = new Vector3(3.24F, -4.03F, -5.86F);
                    //Destroy(gameScript.GetComponent<GameScript>().Shop.gameObject);
                    for (int i = 0; i < 3; i++)
                    {
                        if (i != other.GetComponent<Item>().index)
                            gameScript.GetComponent<GameScript>().items[i].transform.gameObject.SetActive(false);
                    }
                    //gameScript.GetComponent<GameScript>().Shop = (GameObject)Instantiate(Resources.Load("Shop/Shop"), new Vector3(-4.11F, -7.99F, -9.38F), Quaternion.identity);
                    
                    if (other.GetComponent<Item>().mDamage > 0)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            characterControl.selectedCharacter.GetComponent<Character>().weapon.gameObject.SetActive(true);
                            characterControl.selectedCharacter.GetComponent<Character>().weapon.transform.GetChild(0).GetComponent<TextMesh>().text = characterControl.selectedCharacter.GetComponent<Character>().weapon.damage.ToString();
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            characterControl.selectedCharacter.GetComponent<Character>().armor.gameObject.SetActive(true);
                            characterControl.selectedCharacter.GetComponent<Character>().armor.transform.GetChild(0).GetComponent<TextMesh>().text = characterControl.selectedCharacter.GetComponent<Character>().armor.defense.ToString();
                            /*
                            if (i == 0)
                                characterControl.characters[i].armor.gameObject.transform.position = new Vector3(7.34F, 26.5F, -3.11F);
                            else if (i == 1)
                                characterControl.characters[i].armor.gameObject.transform.position = new Vector3(17.11F, 25.96F, -3.11F);
                            else
                                characterControl.characters[i].armor.gameObject.transform.position = new Vector3(15.56F, 22.99F, -3.11F); 
                        }
                    }
                    */
                }
            }
        }
        else if (other.transform.name == "WeaponOne")
        {
            characterSelected = 0;
            itemType = "Weapon";
            other.transform.Find("BorderGold").gameObject.SetActive(true);       
        }
        else if (other.transform.name == "WeaponTwo")
        {
            characterSelected = 1;
            itemType = "Weapon";
            other.transform.Find("BorderGold").gameObject.SetActive(true);          
        }
        else if (other.transform.name == "WeaponThree")
        {
            characterSelected = 2;
            itemType = "Weapon";
            other.transform.Find("BorderGold").gameObject.SetActive(true);           
        }
        else if (other.transform.name == "ArmorOne")
        {
            characterSelected = 0;
            itemType = "Armor";
            other.transform.Find("BorderGold").gameObject.SetActive(true);           
        }
        else if (other.transform.name == "ArmorTwo")
        {
            characterSelected = 1;
            itemType = "Armor";
            other.transform.Find("BorderGold").gameObject.SetActive(true);           
        }
        else if (other.transform.name == "ArmorThree")
        {
            characterSelected = 2;
            itemType = "Armor";
            other.transform.Find("BorderGold").gameObject.SetActive(true);
        }
        else if (other.transform.name == "Replace")
        {
            Debug.Log("Here");    
            if(GameControl.shopArtifact != null)
            {
                gameScript.GetComponent<Artifacts>().collectArtifact(GameControl.shopArtifact);
                Destroy(GameControl.shopArtifact.gameObject);
                GameControl.gold -= 20;
            }              
            else if (itemType == "Armor")
            {
                Debug.Log("Here Armor");
                characterControl.selectedCharacter.GetComponent<Character>().armor.icon.GetComponent<SpriteRenderer>().sprite = selectedItem.gameObject.GetComponent<SpriteRenderer>().sprite;
                characterControl.selectedCharacter.GetComponent<Character>().armor.defense = selectedItem.GetComponent<Item>().mArmor;
                characterControl.selectedCharacter.GetComponent<Character>().armor.traitOne = selectedItem.GetComponent<Item>().mAttributeOne;
                characterControl.selectedCharacter.GetComponent<Character>().armor.traitTwo = selectedItem.GetComponent<Item>().mAttributeTwo;
                characterControl.selectedCharacter.GetComponent<Character>().armor.traitThree = selectedItem.GetComponent<Item>().mAttributeThree;
                GameControl.gold -= selectedItem.GetComponent<Item>().mCost;
                    
            }
            else if (itemType == "Weapon")
            {
                Debug.Log("Here Weapon");
                characterControl.selectedCharacter.GetComponent<Character>().weapon.icon.GetComponent<SpriteRenderer>().sprite = selectedItem.gameObject.GetComponent<SpriteRenderer>().sprite;
                characterControl.selectedCharacter.GetComponent<Character>().weapon.damage = selectedItem.GetComponent<Item>().mDamage;
                characterControl.selectedCharacter.GetComponent<Character>().weapon.traitOne = selectedItem.GetComponent<Item>().mAttributeOne;
                characterControl.selectedCharacter.GetComponent<Character>().weapon.traitTwo = selectedItem.GetComponent<Item>().mAttributeTwo;
                characterControl.selectedCharacter.GetComponent<Character>().weapon.traitThree = selectedItem.GetComponent<Item>().mAttributeThree;
                GameControl.gold -= selectedItem.GetComponent<Item>().mCost;                   
            }          
        }
       
        else if (other.transform.name == "AcceptButton")
        {
            StartCoroutine(loadScene("TitleScreen"));
            ScoreControl.resetScores();
            ScoreText.score = 0;
        }
        else if (GameControl.smite)
        {
            pos = other.gameObject.transform.position;
            col = other.GetComponent<Tile>().mCol;
            row = other.GetComponent<Tile>().mRow;
            type = other.GetComponent<Tile>().mType;
            if (type == "Goblin")
            {
                if (other.GetComponent<Tile>().boss == "Skull")
                {
                    other.GetComponent<Enemy>().health -= (gameScript.GetComponent<GameScript>().smiteSwords.Count / 2);
                }
                else
                {
                    other.GetComponent<Enemy>().health -= gameScript.GetComponent<GameScript>().smiteSwords.Count;
                }
                if (other.GetComponent<Enemy>().health <= 0)
                {
                    other.GetComponent<Enemy>().health = 0;
                    other.GetComponent<Tile>().mType = "Collected";
                }
                int count = gameScript.GetComponent<GameScript>().smiteSwords.Count;
                for (int i = 0; i < count; i++)
                {
                    gameScript.GetComponent<GameScript>().smiteSwords.Pop().GetComponent<smiteSwords>().endPos = pos;
                }
                GameControl.smite = false;
                gameScript.GetComponent<GameScript>().Invoke("shiftBoard", .80f);
            }
        }
        else
        {
            pos = other.gameObject.transform.position;
            col = other.GetComponent<Tile>().mCol;
            row = other.GetComponent<Tile>().mRow;
            type = other.GetComponent<Tile>().mType;

            length = gameScript.GetComponent<GameScript>().collected.Count;

            if (!gameScript.GetComponent<GameScript>().screenUp)
            {

                if (length == 0 && other.GetComponent<Tile>().frozen == false)
                {
                    cunningUsed = false;
                    previousType = type;
                    pRow = row;
                    pCol = col;
                }
                else if (length > 0)
                {
                    Tile obj = gameScript.GetComponent<GameScript>().collected.Peek();
                    if (obj.GetComponent<Tile>().mType == "Collected")
                    {
                        previousType = savedPreviousType;
                    }
                    else
                        previousType = obj.GetComponent<Tile>().mType;
                    pRow = obj.GetComponent<Tile>().mRow;
                    pCol = obj.GetComponent<Tile>().mCol;
                }

                if (type == "Collected")
                {
                    gameScript.GetComponent<GameScript>().predict();
                    if (row != pRow || col != pCol)
                    {
                        if (characterControl.searchActiveCharacterTraits("Cunning") && row - pRow >= -1 && row - pRow <= 1 && col - pCol >= -1 && col - pCol <= 1 && !cunningUsed)
                        {
                            cunningUsed = true;
                            pCunningCol = pCol;
                            pCunningRow = pRow;
                            gameScript.GetComponent<GameScript>().cunningInCollected = true;
                            savedPreviousType = previousType;
                            gameScript.GetComponent<GameScript>().collected.Push(gameScript.GetComponent<GameScript>().board[row, col]);
                            gameScript.GetComponent<GameScript>().board[row, col] = gameScript.GetComponent<GameScript>().collected.Peek();
                            if (row - pRow == 0 && col - pCol == 1) //Right
                            {
                                gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(1).gameObject.SetActive(true);
                                gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                                cunningLine = gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(1).gameObject;
                            }
                            if (row - pRow == 0 && col - pCol == -1) //Left
                            {
                                gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(0).gameObject.SetActive(true);
                                gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                                cunningLine = gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(0).gameObject;
                            }
                            if (row - pRow == 1 && col - pCol == 0) //Up
                            {
                                gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(2).gameObject.SetActive(true);
                                gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                                cunningLine = gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(2).gameObject;
                            }
                            if (row - pRow == -1 && col - pCol == 0) //Down
                            {
                                gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(3).gameObject.SetActive(true);
                                gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(3).gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                                cunningLine = gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(3).gameObject;
                            }
                            if (row - pRow == -1 && col - pCol == -1) //NorthWest
                            {
                                gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(7).gameObject.SetActive(true);
                                gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(7).gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                                cunningLine = gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(7).gameObject;
                            }
                            if (row - pRow == -1 && col - pCol == 1) //NorthEast
                            {
                                gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(6).gameObject.SetActive(true);
                                gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(6).gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                                cunningLine = gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(6).gameObject;
                            }
                            if (row - pRow == 1 && col - pCol == -1) //SouthWest
                            {
                                gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(5).gameObject.SetActive(true);
                                gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(5).gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                                cunningLine = gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(5).gameObject;
                            }
                            if (row - pRow == 1 && col - pCol == 1) //SouthEast
                            {
                                gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(4).gameObject.SetActive(true);
                                gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(4).gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                                cunningLine = gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(4).gameObject;
                            }
                        }
                        else
                        {
                            while (gameScript.GetComponent<GameScript>().collected.Peek().GetComponent<Tile>().mRow != row || gameScript.GetComponent<GameScript>().collected.Peek().GetComponent<Tile>().mCol != col)
                            {
                                Tile obj = gameScript.GetComponent<GameScript>().collected.Pop();
                                if (obj.GetComponent<Tile>().mType != "Collected")
                                {
                                    Destroy(gameScript.GetComponent<GameScript>().board[obj.GetComponent<Tile>().mRow, obj.GetComponent<Tile>().mCol].gameObject);
                                    gameScript.GetComponent<GameScript>().board[obj.GetComponent<Tile>().mRow, obj.GetComponent<Tile>().mCol] = obj;
                                    gameScript.GetComponent<GameScript>().predict();
                                    obj.transform.gameObject.SetActive(true);
                                    if (obj.transform.GetChild(11))
                                        obj.transform.GetChild(11).gameObject.SetActive(false);
                                    if (obj.GetComponent<Tile>().boss != "")
                                        gameScript.GetComponent<GameScript>().board[obj.GetComponent<Tile>().mRow, obj.GetComponent<Tile>().mCol].transform.GetChild(15).gameObject.SetActive(false);
                                }
                                if (gameScript.GetComponent<GameScript>().collected.Peek().GetComponent<Tile>().mRow == pCunningRow &&
                                       gameScript.GetComponent<GameScript>().collected.Peek().GetComponent<Tile>().mCol == pCunningCol)
                                {
                                    cunningLine.transform.gameObject.SetActive(false);
                                    cunningUsed = false;
                                    gameScript.GetComponent<GameScript>().cunningInCollected = false;
                                }
                            }
                        }
                    }
                }
                else if (row - pRow >= -1 && row - pRow <= 1 && col - pCol >= -1 && col - pCol <= 1 && other.GetComponent<Tile>().frozen == false)
                {
                    if (isCollectable(type, previousType))
                    {
                        trinket = gameScript.GetComponent<GameScript>().board[row, col].GetComponent<Tile>().isTrinket;
                        if (type == "Goblin")
                        {
                            gameScript.GetComponent<GameScript>().board[row, col].transform.gameObject.SetActive(false);
                            health = gameScript.GetComponent<GameScript>().board[row, col].GetComponent<Enemy>().health;
                            damage = gameScript.GetComponent<GameScript>().board[row, col].GetComponent<Enemy>().damage;
                            boss = gameScript.GetComponent<GameScript>().board[row, col].GetComponent<Tile>().boss;
                        }
                        else
                            gameScript.GetComponent<GameScript>().board[row, col].transform.gameObject.SetActive(false);

                        gameScript.GetComponent<GameScript>().collected.Push(gameScript.GetComponent<GameScript>().board[row, col]);

                        if (type == "Goblin")
                        {
                            if (boss != "")
                            {
                                switch (boss)
                                {
                                    case "Rat":
                                        gameScript.GetComponent<GameScript>().board[row, col] = (Tile)Instantiate(gameScript.GetComponent<GameScript>().ratLarge, pos, Quaternion.identity);
                                        break;
                                    case "RatClone":
                                        gameScript.GetComponent<GameScript>().board[row, col] = (Tile)Instantiate(gameScript.GetComponent<GameScript>().ratSmall, pos, Quaternion.identity);
                                        break;
                                    case "Slime":
                                        gameScript.GetComponent<GameScript>().board[row, col] = (Tile)Instantiate(gameScript.GetComponent<GameScript>().slime, pos, Quaternion.identity);
                                        break;
                                    case "BlueGenie":
                                        gameScript.GetComponent<GameScript>().board[row, col] = (Tile)Instantiate(gameScript.GetComponent<GameScript>().blueGenie, pos, Quaternion.identity);
                                        break;
                                    case "GreenGenie":
                                        gameScript.GetComponent<GameScript>().board[row, col] = (Tile)Instantiate(gameScript.GetComponent<GameScript>().greenGenie, pos, Quaternion.identity);
                                        break;
                                    case "Lich":
                                        gameScript.GetComponent<GameScript>().board[row, col] = (Tile)Instantiate(gameScript.GetComponent<GameScript>().lich, pos, Quaternion.identity);
                                        break;
                                    case "Skull":
                                        gameScript.GetComponent<GameScript>().board[row, col] = (Tile)Instantiate(gameScript.GetComponent<GameScript>().skeleton, pos, Quaternion.identity);
                                        break;
                                }
                                gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(15).gameObject.SetActive(true);
                            }
                            else
                            {
                                if (other.gameObject.name == "Goblin 1(Clone)")
                                    gameScript.GetComponent<GameScript>().board[row, col] = (Tile)Instantiate(gameScript.GetComponent<GameScript>().collectedGoblins[0], pos, Quaternion.identity);
                                else if (other.gameObject.name == "Goblin 2(Clone)")
                                    gameScript.GetComponent<GameScript>().board[row, col] = (Tile)Instantiate(gameScript.GetComponent<GameScript>().collectedGoblins[1], pos, Quaternion.identity);
                                else if (other.gameObject.name == "Goblin 3(Clone)")
                                    gameScript.GetComponent<GameScript>().board[row, col] = (Tile)Instantiate(gameScript.GetComponent<GameScript>().collectedGoblins[2], pos, Quaternion.identity);
                                else if (other.gameObject.name == "Goblin 4(Clone)")
                                    gameScript.GetComponent<GameScript>().board[row, col] = (Tile)Instantiate(gameScript.GetComponent<GameScript>().collectedGoblins[3], pos, Quaternion.identity);
                            }

                            gameScript.GetComponent<GameScript>().board[row, col].GetComponent<Enemy>().health = health;
                            gameScript.GetComponent<GameScript>().board[row, col].GetComponent<Enemy>().damage = damage;
                            gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(10).gameObject.SetActive(false);
                            gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(11).gameObject.SetActive(true);
                            //gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(9).GetComponent<Animator>().SetTrigger("sleep"); 
                        }
                        else if (trinket)
                        {
                            gameScript.GetComponent<GameScript>().board[row, col] = (Tile)Instantiate(gameScript.GetComponent<GameScript>().artifacts[gameScript.GetComponent<Artifacts>().getIndex(gameScript.GetComponent<GameScript>().board[row, col].mName)], pos, Quaternion.identity);
                            gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(0).gameObject.SetActive(false);
                            gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(1).gameObject.SetActive(true);
                        }
                        else if (type == "Shopkeeper")
                        {
                            gameScript.GetComponent<GameScript>().board[row, col] = (Tile)Instantiate(gameScript.GetComponent<GameScript>().shopkeeper, pos, Quaternion.identity);
                            gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(0).gameObject.SetActive(false);
                            gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(1).gameObject.SetActive(true);
                        }
                        else if (type == "Chest")
                        {
                            gameScript.GetComponent<GameScript>().board[row, col] = (Tile)Instantiate(gameScript.GetComponent<GameScript>().chest, pos, Quaternion.identity);
                            gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(0).gameObject.SetActive(false);
                            gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(1).gameObject.SetActive(true);
                        }
                        else if (type == "Health")
                        {
                            if (gameScript.GetComponent<GameScript>().board[row, col].name == "Health One(Clone)")
                            {
                                gameScript.GetComponent<GameScript>().board[row, col] = (Tile)Instantiate(gameScript.GetComponent<GameScript>().healthCrystals[0], pos, Quaternion.identity);
                            }
                            else if (gameScript.GetComponent<GameScript>().board[row, col].name == "Health Two(Clone)")
                            {
                                gameScript.GetComponent<GameScript>().board[row, col] = (Tile)Instantiate(gameScript.GetComponent<GameScript>().healthCrystals[1], pos, Quaternion.identity);
                            }
                            else if (gameScript.GetComponent<GameScript>().board[row, col].name == "Health Three(Clone)")
                            {
                                gameScript.GetComponent<GameScript>().board[row, col] = (Tile)Instantiate(gameScript.GetComponent<GameScript>().healthCrystals[2], pos, Quaternion.identity);
                            }
                            gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(10).gameObject.SetActive(false);
                            gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(11).gameObject.SetActive(true);
                        }
                        else if (type == "Coin")
                        {
                            if (gameScript.GetComponent<GameScript>().board[row, col].name == "Coin One(Clone)")
                            {
                                gameScript.GetComponent<GameScript>().board[row, col] = (Tile)Instantiate(gameScript.GetComponent<GameScript>().coins[0], pos, Quaternion.identity);
                            }
                            else if (gameScript.GetComponent<GameScript>().board[row, col].name == "Coin Two(Clone)")
                            {
                                gameScript.GetComponent<GameScript>().board[row, col] = (Tile)Instantiate(gameScript.GetComponent<GameScript>().coins[1], pos, Quaternion.identity);
                            }
                            else if (gameScript.GetComponent<GameScript>().board[row, col].name == "Coin Three(Clone)")
                            {
                                gameScript.GetComponent<GameScript>().board[row, col] = (Tile)Instantiate(gameScript.GetComponent<GameScript>().coins[2], pos, Quaternion.identity);
                            }
                            gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(10).gameObject.SetActive(false);
                            gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(11).gameObject.SetActive(true);
                        }
                        else if (type == "Sword")
                        {
                            if (gameScript.GetComponent<GameScript>().board[row, col].name == "Sword One(Clone)")
                            {
                                gameScript.GetComponent<GameScript>().board[row, col] = (Tile)Instantiate(gameScript.GetComponent<GameScript>().swords[0], pos, Quaternion.identity);
                                gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(8).GetComponent<SpriteRenderer>().sprite = characterControl.getWeaponIcon().GetComponent<SpriteRenderer>().sprite;
                            }
                            else if (gameScript.GetComponent<GameScript>().board[row, col].name == "Sword Two(Clone)")
                            {
                                gameScript.GetComponent<GameScript>().board[row, col] = (Tile)Instantiate(gameScript.GetComponent<GameScript>().swords[1], pos, Quaternion.identity);
                            }
                            else if (gameScript.GetComponent<GameScript>().board[row, col].name == "Sword Three(Clone)")
                            {
                                gameScript.GetComponent<GameScript>().board[row, col] = (Tile)Instantiate(gameScript.GetComponent<GameScript>().swords[2], pos, Quaternion.identity);
                            }
                            gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(10).gameObject.SetActive(false);
                            gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(11).gameObject.SetActive(true);
                        }
                        else if (type == "Rubble")
                        {                         
                            gameScript.GetComponent<GameScript>().board[row, col] = (Tile)Instantiate(gameScript.GetComponent<GameScript>().rubble, pos, Quaternion.identity);                                                                                                           
                            gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(10).gameObject.SetActive(false);
                            gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(11).gameObject.SetActive(true);
                        }
                        else if (type == "Mana")
                        {
                            if (gameScript.GetComponent<GameScript>().board[row, col].name == "Mana One(Clone)")
                            {
                                gameScript.GetComponent<GameScript>().board[row, col] = (Tile)Instantiate(gameScript.GetComponent<GameScript>().manaCrystals[0], pos, Quaternion.identity);
                            }
                            else if (gameScript.GetComponent<GameScript>().board[row, col].name == "Mana Two(Clone)")
                            {
                                gameScript.GetComponent<GameScript>().board[row, col] = (Tile)Instantiate(gameScript.GetComponent<GameScript>().manaCrystals[1], pos, Quaternion.identity);
                            }
                            else if (gameScript.GetComponent<GameScript>().board[row, col].name == "Mana Three(Clone)")
                            {
                                gameScript.GetComponent<GameScript>().board[row, col] = (Tile)Instantiate(gameScript.GetComponent<GameScript>().manaCrystals[2], pos, Quaternion.identity);
                            }
                            gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(10).gameObject.SetActive(false);
                            gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(11).gameObject.SetActive(true);
                        }
                        else
                        {
                            gameScript.GetComponent<GameScript>().board[row, col] = (Tile)Instantiate(Resources.Load("Tiles/" + type), pos, Quaternion.identity);
                            gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(10).gameObject.SetActive(false);
                            gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(11).gameObject.SetActive(true);
                        }

                        gameScript.GetComponent<GameScript>().board[row, col].GetComponent<Tile>().mType = "Collected";
                        gameScript.GetComponent<GameScript>().board[row, col].GetComponent<Tile>().mCol = col;
                        gameScript.GetComponent<GameScript>().board[row, col].GetComponent<Tile>().mRow = row;
                        gameScript.GetComponent<GameScript>().board[row, col].GetComponent<Tile>().boss = boss;
                        boss = "";

                        Tile obj = gameScript.GetComponent<GameScript>().collected.Peek();

                        if (row - pRow == 0 && col - pCol == 1) //Right
                            gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(1).gameObject.SetActive(true);
                        if (row - pRow == 0 && col - pCol == -1) //Left
                            gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(0).gameObject.SetActive(true);
                        if (row - pRow == 1 && col - pCol == 0) //Up
                            gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(2).gameObject.SetActive(true);
                        if (row - pRow == -1 && col - pCol == 0) //Down
                            gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(3).gameObject.SetActive(true);
                        if (row - pRow == -1 && col - pCol == -1) //NorthWest
                            gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(7).gameObject.SetActive(true);
                        if (row - pRow == -1 && col - pCol == 1) //NorthEast
                            gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(6).gameObject.SetActive(true);
                        if (row - pRow == 1 && col - pCol == -1) //SouthWest
                            gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(5).gameObject.SetActive(true);
                        if (row - pRow == 1 && col - pCol == 1) //SouthEast
                            gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(4).gameObject.SetActive(true);

                        gameScript.GetComponent<GameScript>().predict();
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.name == "ToTitleScreen")
        {
            cameraObj.transform.position = new Vector3(0, cameraObj.transform.position.y, cameraObj.transform.position.z);
            if( other.GetComponent<Animator>() != null)
                other.GetComponent<Animator>().SetTrigger("UnPressed");
        }
        else if (other.transform.name == "ToShip")
        {
            cameraObj.transform.position = new Vector3(100, cameraObj.transform.position.y, cameraObj.transform.position.z);
            if( other.GetComponent<Animator>() != null)
                other.GetComponent<Animator>().SetTrigger("UnPressed");
        }
        else if (other.transform.name == "ToBarracks")
        {
            cameraObj.transform.position = new Vector3(-100, cameraObj.transform.position.y, cameraObj.transform.position.z);
            if( other.GetComponent<Animator>() != null)
                other.GetComponent<Animator>().SetTrigger("UnPressed");

        }
        else if (other.transform.name == "ToHighScores")
        {
            cameraObj.transform.position = new Vector3(150, cameraObj.transform.position.y, cameraObj.transform.position.z);
        }
        else if (other.transform.name == "ToSettings")
        {
            cameraObj.transform.position = new Vector3(200, cameraObj.transform.position.y, cameraObj.transform.position.z);
        }
        else if (other.transform.name == "ToGameScreen")
        {
            StartCoroutine(loadScene("GameScreen"));
        }
        else if (other.CompareTag("BarracksCharacter") && GameObject.Find("CharacterScreen(Clone)") == null)
        {
            other.transform.name = other.transform.name.Replace("(Clone)", "").Trim();           
            if (swapActive)
            {
                if (swapCharacter != null)
                {
                    StartCoroutine(BarracksScrollUp(other.gameObject));
                }
                    
                if (other.transform.name == Barracks.cOneName) otherSlot = 1;
                else if (other.transform.name == Barracks.cTwoName) otherSlot = 2;
                else if (other.transform.name == Barracks.cThreeName) otherSlot = 3;
                else if (other.transform.name == Barracks.cFourName) otherSlot = 4;
                else if (other.transform.name == Barracks.cFiveName) otherSlot = 5;
                else if (other.transform.name == Barracks.cSixName) otherSlot = 6;
                if (swapCharacter.transform.parent.name == Barracks.cOneName) swapSlot = 1;
                else if (swapCharacter.transform.parent.name == Barracks.cTwoName) swapSlot = 2;
                else if (swapCharacter.transform.parent.name == Barracks.cThreeName) swapSlot = 3;
                else if (swapCharacter.transform.parent.name == Barracks.cFourName) swapSlot = 4;
                else if (swapCharacter.transform.parent.name == Barracks.cFiveName) swapSlot = 5;
                else if (swapCharacter.transform.parent.name == Barracks.cSixName) swapSlot = 6;
                tempName = other.transform.name;
                if (otherSlot == 1)
                {
                    if (swapSlot == 2)
                    {
                        Barracks.cOneName = Barracks.cTwoName;
                        Barracks.cTwoName = tempName;
                    }
                    else if (swapSlot == 3)
                    {
                        Barracks.cOneName = Barracks.cThreeName;
                        Barracks.cThreeName = tempName;
                    }
                    else if (swapSlot == 4)
                    {
                        Barracks.cOneName = Barracks.cFourName;
                        Barracks.cFourName = tempName;
                    }
                    else if (swapSlot == 5)
                    {
                        Barracks.cOneName = Barracks.cFiveName;
                        Barracks.cFiveName = tempName;
                    }
                    else if (swapSlot == 6)
                    {
                        Barracks.cOneName = Barracks.cSixName;
                        Barracks.cSixName = tempName;
                    }
                }
                else if (otherSlot == 2)
                {
                    if (swapSlot == 1)
                    {
                        Barracks.cTwoName = Barracks.cOneName;
                        Barracks.cOneName = tempName;
                    }
                    else if (swapSlot == 3)
                    {
                        Barracks.cTwoName = Barracks.cThreeName;
                        Barracks.cThreeName = tempName;
                    }
                    else if (swapSlot == 4)
                    {
                        Barracks.cTwoName = Barracks.cFourName;
                        Barracks.cFourName = tempName;
                    }
                    else if (swapSlot == 5)
                    {
                        Barracks.cTwoName = Barracks.cFiveName;
                        Barracks.cFiveName = tempName;
                    }
                    else if (swapSlot == 6)
                    {
                        Barracks.cTwoName = Barracks.cSixName;
                        Barracks.cSixName = tempName;
                    }
                }
                else if (otherSlot == 3)
                {
                    if (swapSlot == 1)
                    {
                        Barracks.cThreeName = Barracks.cOneName;
                        Barracks.cOneName = tempName;
                    }
                    else if (swapSlot == 2)
                    {
                        Barracks.cThreeName = Barracks.cTwoName;
                        Barracks.cTwoName = tempName;
                    }
                    else if (swapSlot == 4)
                    {
                        Barracks.cThreeName = Barracks.cFourName;
                        Barracks.cFourName = tempName;
                    }
                    else if (swapSlot == 5)
                    {
                        Barracks.cThreeName = Barracks.cFiveName;
                        Barracks.cFiveName = tempName;
                    }
                    else if (swapSlot == 6)
                    {
                        Barracks.cThreeName = Barracks.cSixName;
                        Barracks.cSixName = tempName;
                    }
                }
                else if (otherSlot == 4)
                {
                    if (swapSlot == 1)
                    {
                        Barracks.cFourName = Barracks.cOneName;
                        Barracks.cOneName = tempName;
                    }
                    else if (swapSlot == 2)
                    {
                        Barracks.cFourName = Barracks.cTwoName;
                        Barracks.cTwoName = tempName;
                    }
                    else if (swapSlot == 3)
                    {
                        Barracks.cFourName = Barracks.cThreeName;
                        Barracks.cThreeName = tempName;
                    }
                    else if (swapSlot == 5)
                    {
                        Barracks.cFourName = Barracks.cFiveName;
                        Barracks.cFiveName = tempName;
                    }
                    else if (swapSlot == 6)
                    {
                        Barracks.cFourName = Barracks.cSixName;
                        Barracks.cSixName = tempName;
                    }
                }
                else if (otherSlot == 5)
                {
                    if (swapSlot == 1)
                    {
                        Barracks.cFiveName = Barracks.cOneName;
                        Barracks.cOneName = tempName;
                    }
                    else if (swapSlot == 2)
                    {
                        Barracks.cFiveName = Barracks.cTwoName;
                        Barracks.cTwoName = tempName;
                    }
                    else if (swapSlot == 3)
                    {
                        Barracks.cFiveName = Barracks.cThreeName;
                        Barracks.cThreeName = tempName;
                    }
                    else if (swapSlot == 4)
                    {
                        Barracks.cFiveName = Barracks.cFourName;
                        Barracks.cFourName = tempName;
                    }
                    else if (swapSlot == 6)
                    {
                        Barracks.cFiveName = Barracks.cSixName;
                        Barracks.cSixName = tempName;
                    }
                }
                else if (otherSlot == 6)
                {
                    if (swapSlot == 1)
                    {
                        Barracks.cSixName = Barracks.cOneName;
                        Barracks.cOneName = tempName;
                    }
                    else if (swapSlot == 2)
                    {
                        Barracks.cSixName = Barracks.cTwoName;
                        Barracks.cTwoName = tempName;
                    }
                    else if (swapSlot == 3)
                    {
                        Barracks.cSixName = Barracks.cThreeName;
                        Barracks.cThreeName = tempName;
                    }
                    else if (swapSlot == 4)
                    {
                        Barracks.cSixName = Barracks.cFourName;
                        Barracks.cFourName = tempName;
                    }
                    else if (swapSlot == 5)
                    {
                        Barracks.cSixName = Barracks.cFiveName;
                        Barracks.cFiveName = tempName;
                    }
                }
                if (other.transform.name == Team.selectedCharacter)
                {
                    if (swapCharacter.transform.parent.name == Team.selectedCharacter)
                    {                     
                        StartCoroutine(BarracksScrollUp(other.gameObject));
                        swapActive = false;                                               
                    }
                    else //Swapping a PartyMember with a Non-PartyMember
                    {
                        Team.selectedCharacter = swapCharacter.transform.parent.name;
                        PlayerPrefs.SetString("selectedCharacter", Team.selectedCharacter);
                        Debug.Log("Selected Character: " + Team.selectedCharacter);
                        StartCoroutine(swapBarracksCharacter(swapCharacter.gameObject, other.gameObject));
                        swapActive = false;
                    }
                }
                else if (swapCharacter.transform.parent.name == Team.selectedCharacter)
                {                   
                    Team.selectedCharacter = other.transform.name;
                    PlayerPrefs.SetString("selectedCharacter", Team.selectedCharacter);
                    Debug.Log("Selected Character: " + Team.selectedCharacter);
                    StartCoroutine(swapBarracksCharacter(swapCharacter.gameObject, other.gameObject));
                    swapActive = false;
                }
                else //Both characters are Non-PartyMembers
                {
                    StartCoroutine(swapBarracksCharacter(swapCharacter.gameObject, other.gameObject));
                    swapActive = false;
                }
            }
            else
            {
                if (other.transform.Find("Selected Frame").GetComponent<Animator>().GetBool("selected")) //If character is already selected
                {
                    StartCoroutine(BarracksScrollUp(other.gameObject));
                }                   
                else
                {
                    StartCoroutine(BarracksScrollDown(other.gameObject));                   
                }                  
                if (selectedCharacter != null && other.transform.name != selectedCharacter.transform.name)
                {
                    StartCoroutine(BarracksScrollUp(selectedCharacter.gameObject));                   
                }                   
                selectedCharacter = other.gameObject;
                characterControl.setStats(other.transform.name);
            }
        }
        else if (other.transform.name == "Swap")
        {
            other.transform.Find("SwapSelected").gameObject.SetActive(false);
            if (swapActive == false)
            {
                swapActive = true;
                swapCharacter = other.gameObject.transform.parent.gameObject;
                swapPos = swapCharacter.transform.position;
            }

        }
        else if (other.transform.name == "Inspect")
        {
            other.transform.Find("InspectSelected").gameObject.SetActive(false);
            Instantiate(characterScreen, new Vector3(-100f, 0f, -30f), Quaternion.identity);
            other.transform.parent.GetComponent<Animator>().SetBool("selected", false);
            characterControl.setStats(other.transform.parent.transform.parent.name);
        }
    }

    private bool isCollectable(string type, string previousType)
    {
        if (type == previousType) //Tracing through the same Tile Type
        {
            return true;
        }

        if (characterControl.searchActiveCharacterTraits("Pickpocket")) //Pickpocket lets you trace through Coins and Enemies at the same time
        {
            if (type == "Sword" || type == "Goblin" || type == "Barrel" || type == "Coin")
            {
                if (previousType == "Sword" || previousType == "Goblin" || previousType == "Barrel" || previousType == "Coin")
                {
                    return true;
                }
            }
        }
        else if (type == "Sword" || type == "Goblin" || type == "Barrel") //Swords, Goblins, and Barrels can be traced together
        {
            if(previousType == "Sword" || previousType == "Goblin" || previousType == "Barrel")
            {
                return true;
            }
        }     

        if(type == "Health" || type == "Mana") //Health and Mana crystals can be traced together
        {
            if(previousType == "Health" || previousType == "Mana")
            {
                return true;
            }
        }      
        
        if(type == "Helix" || previousType == "Helix") //Helix fossils can be traced with anything
        {
            return true;
        }   
                        
       

        return false;
    }

    private IEnumerator BarracksScrollDown(GameObject obj)
    {
        obj.transform.Find("Selected Frame").GetComponent<Animator>().SetBool("selected", true);
        yield return null;
    }
    private IEnumerator BarracksScrollUp(GameObject obj)
    {
        yield return null;
        obj.transform.Find("Selected Frame").GetComponent<Animator>().SetBool("selected", false);       
    }

    private IEnumerator swapBarracksCharacter(GameObject objOne, GameObject objTwo)
    {
        Vector3 objOnePos = objOne.transform.parent.position;
        Vector3 objTwoPos = objTwo.transform.position;
        Debug.Log("objOne: " + objOne.name + " | objTwo: " + objTwo.name);
        objOne.transform.parent.Find("Selected Frame").GetComponent<Animator>().SetBool("selected", false);
        objTwo.transform.Find("Selected Frame").GetComponent<Animator>().SetBool("selected", false);
        objOne.transform.parent.Find("Icon").GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .75f);
        objTwo.transform.Find("Icon").GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .75f);
        yield return new WaitForSeconds(.15f);
        objOne.transform.parent.Find("Icon").GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .5f);
        objTwo.transform.Find("Icon").GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .5f);
        yield return new WaitForSeconds(.15f);
        objOne.transform.parent.Find("Icon").GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .25f);
        objTwo.transform.Find("Icon").GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .25f);
        yield return new WaitForSeconds(.15f);
        objOne.transform.parent.Find("Icon").GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 0f);
        objTwo.transform.Find("Icon").GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 0f);

        objOne.transform.parent.Find("Selected Frame").GetComponent<Animator>().SetBool("swapped", true);
        objTwo.transform.Find("Selected Frame").GetComponent<Animator>().SetBool("swapped", true);
        yield return null;
        objOne.transform.parent.Find("Selected Frame").GetComponent<Animator>().SetBool("swapped", false);
        objTwo.transform.Find("Selected Frame").GetComponent<Animator>().SetBool("swapped", false);
        yield return new WaitForSeconds(1f);
        objOne.transform.parent.position = objTwoPos;
        objTwo.transform.position = objOnePos;
        
        objOne.transform.parent.Find("Icon").GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .25f);
        objTwo.transform.Find("Icon").GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .25f);
        yield return new WaitForSeconds(.15f);
        objOne.transform.parent.Find("Icon").GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .5f);
        objTwo.transform.Find("Icon").GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .5f);
        yield return new WaitForSeconds(.15f);
        objOne.transform.parent.Find("Icon").GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .75f);
        objTwo.transform.Find("Icon").GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .75f);
        yield return new WaitForSeconds(.15f);
        objOne.transform.parent.Find("Icon").GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
        objTwo.transform.Find("Icon").GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
    }
    
    private IEnumerator loadScene(string scene)
    {
        loadingDoors.GetComponent<Animator>().SetBool("close", true);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
        asyncLoad.allowSceneActivation = false;
        while(!asyncLoad.isDone)
        {
            if(asyncLoad.progress >= 0.9f)
            {
                if(loadingDoors.GetComponent<loadingDoors>().doneClosing)
                {
                    loadingDoors.GetComponent<Animator>().SetBool("close", false);
                    asyncLoad.allowSceneActivation = true;                   
                }
            }
            yield return null;
        }         
    }
    public void respawnShopKeeper()
    {
        for (int i = 0; i < 3; i++)
        {
            characterControl.selectedCharacter.GetComponent<Character>().weapon.weaponObj.transform.Find("BorderGold").gameObject.SetActive(false);
            characterControl.selectedCharacter.GetComponent<Character>().weapon.weaponObj.SetActive(false);
            characterControl.selectedCharacter.GetComponent<Character>().armor.armorObj.transform.Find("BorderGold").gameObject.SetActive(false);
            characterControl.selectedCharacter.GetComponent<Character>().armor.armorObj.SetActive(false);
            if(upgrading)
            {
                if (i == 0)
                    characterControl.selectedCharacter.GetComponent<Character>().armor.gameObject.transform.position = new Vector3(characterControl.selectedCharacter.GetComponent<Character>().armor.transform.position.x, characterControl.selectedCharacter.GetComponent<Character>().armor.transform.position.y - 2, characterControl.selectedCharacter.GetComponent<Character>().armor.transform.position.z);

                else if (i == 1)
                    characterControl.selectedCharacter.GetComponent<Character>().armor.gameObject.transform.position = new Vector3(characterControl.selectedCharacter.GetComponent<Character>().armor.transform.position.x, characterControl.selectedCharacter.GetComponent<Character>().armor.transform.position.y - 2, characterControl.selectedCharacter.GetComponent<Character>().armor.transform.position.z);

                else
                    characterControl.selectedCharacter.GetComponent<Character>().armor.gameObject.transform.position = new Vector3(characterControl.selectedCharacter.GetComponent<Character>().armor.transform.position.x, characterControl.selectedCharacter.GetComponent<Character>().armor.transform.position.y - 2, characterControl.selectedCharacter.GetComponent<Character>().armor.transform.position.z);               
            }                      
        }
        upgrading = false;
        characterSelected = -1;
        Destroy(gameScript.GetComponent<GameScript>().Shop.gameObject);
        //Destroy(selectedItem.gameObject);
        selectedItem = null;
        gameScript.GetComponent<GameScript>().Shop = (GameObject)Instantiate(Resources.Load("Shop/Shopkeeper"), new Vector3(3.1F, -3.4F, -3.06F), Quaternion.identity);
        gameScript.GetComponent<GameScript>().items[0].transform.gameObject.SetActive(true);
        gameScript.GetComponent<GameScript>().items[1].transform.gameObject.SetActive(true);
        gameScript.GetComponent<GameScript>().items[2].transform.gameObject.SetActive(true);
        gameScript.GetComponent<GameScript>().items[0].transform.position = new Vector3(0.75F, -4.5F, -5.86F);
        gameScript.GetComponent<GameScript>().items[1].transform.position = new Vector3(2.5F, -3.5F, -5.86F);
        gameScript.GetComponent<GameScript>().items[2].transform.position = new Vector3(4.2F, -4.5F, -5.86F);
    }
}