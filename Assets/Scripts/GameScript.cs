using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameScript : MonoBehaviour
{
    //Artifacts artifacts = new Artifacts();
    //CharacterControl control = new CharacterControl();
    public GameObject Shop;
    public GameObject game;
    public PredictText predictText;
    public CharacterControl characterControl;
    public Camera cam;
    public  int gold = 0;

  
    public int goblinHealthConstant = 9;
    public int goblinDamageConstant = 0;
    public int goblinScalar = 1;
    public int turnToScale = 50;
    private GameObject[] splats = new GameObject[15];
    public GameObject bloodSplat;

    public Sprite[] skills = new Sprite[10];
    public GameObject[] gameScreenParts = new GameObject[5];
    public GameObject[] healthCrystals = new GameObject[3];
    public GameObject[] coins = new GameObject[3];
    public GameObject[] swords = new GameObject[3];
    public GameObject[] manaCrystals = new GameObject[3];
    public GameObject[] goblins = new GameObject[4];
    public GameObject rubble;
    public GameObject helix;
    public GameObject bomb;
    public GameObject barrel;
    public GameObject chest;
    public GameObject shopkeeper;
    public GameObject shop;
    public GameObject ghost;
    public GameObject ratLarge;
    public GameObject ratSmall;
    public GameObject slime;
    public GameObject blueGenie;
    public GameObject greenGenie;
    public GameObject lich;
    public GameObject skeleton;
    public GameObject bossArms;
    public GameObject bossBody;
    public GameObject scoreAddition;

    public int bossSpawner;
    public int shopSpawner;

    public int baitValue = 5;
    public int frostValue = 10;
    public int isaacsBindingValue = 70;
    public int prismValue = 2;
    public int thornsValue = 2;
    public int vampireFangValue = 10;
    public int whetstoneValue = 2;

    public GameObject[,] board = new GameObject[6, 6];
    GameObject[,] tempBoard = new GameObject[6, 6];
    public GameObject[] items = new GameObject[4];
    public Stack<GameObject> collected = new Stack<GameObject>();
    public Stack<GameObject> enemies = new Stack<GameObject>();
    public List<GameObject> spellsOnCD = new List<GameObject>();
    //public static List<GameObject> artifacts = new List<GameObject>();
    public bool screenUp = false;

    public float spacing = 0.01F;
    private int temp;
    private int turn = 1;
    public int turnCounter = 1;
    int count = 0;
    public bool shopUp = false;
    public bool shopKeeperUp = false;
    public bool spawnChest = false;

    private GameObject currentSpell;

    public GameObject SkillOne;
    public GameObject SkillTwo;
    public int skillOne = 0;
    public int skillTwo = 0;
    public int counter;
    public bool switchCharacters = false;

    int lastTurnSpellCast = 0;
    public Stack<GameObject> smiteSwords = new Stack<GameObject>();
    public bool cunningInCollected = false;

    public bool intimidating = false;
    public bool meek = false;
    public bool sleightOfHand = false;
    public bool survivalist = false;
    int rubbleCollected = 0;
    public bool spawnManaCrystals = false;
    private bool slimeNeedsToEat = true;
    private int numGhosts = 0;
    private bool bossAbilityUsed = false;
    private void Awake()
    {
        characterControl.setUpCharacters();
    }
    void Start () 
    {
        //collected = new Stack<GameObject>(0);
        //GetComponent<Collision>().gameScript = GameObject.Find("GameScript").gameObject;
        screenUp = false;
        init();  
    }
    void Update ()
    {      
        if(switchCharacters)
        {
            switchCharacters = false;
            characterControl.switchCharacter();
        }       
    }
    public void init()
    {       
        PlayerPrefs.SetString("Boss Stage", "Stage One");
        ScoreControl.partyOne = Team.characterOne;
        ScoreControl.partyTwo = Team.characterTwo;
        ScoreControl.partyThree = Team.characterThree;

        for(int i = 0; i < skills.Length; i++)
        {
            if(characterControl.characters[0].skillOne == skills[i].name.Replace("(UnityEngine.Sprite)", ""))
            {
                characterControl.cOneSkillOne.GetComponent<Image>().sprite = skills[i];
                characterControl.cOneSkillOne.name = skills[i].name.Replace("(UnityEngine.Sprite)", "");
            }
                
            if(characterControl.characters[0].skillTwo == skills[i].name.Replace("(UnityEngine.Sprite)", ""))
            {
                characterControl.cOneSkillTwo.GetComponent<Image>().sprite = skills[i];
                characterControl.cOneSkillTwo.name = skills[i].name.Replace("(UnityEngine.Sprite)", "");
            }
                

            if(characterControl.characters[1].skillOne == skills[i].name.Replace("(UnityEngine.Sprite)", ""))
            {
                characterControl.cTwoSkillOne.GetComponent<Image>().sprite = skills[i];
                characterControl.cTwoSkillOne.name = skills[i].name.Replace("(UnityEngine.Sprite)", "");
            }
                
            if(characterControl.characters[1].skillTwo == skills[i].name.Replace("(UnityEngine.Sprite)", ""))
            {
                characterControl.cTwoSkillTwo.GetComponent<Image>().sprite = skills[i];
                characterControl.cTwoSkillTwo.name = skills[i].name.Replace("(UnityEngine.Sprite)", "");
            }
                

            if(characterControl.characters[2].skillOne == skills[i].name.Replace("(UnityEngine.Sprite)", ""))
            {
                characterControl.cThreeSkillOne.GetComponent<Image>().sprite = skills[i];
                characterControl.cThreeSkillOne.name = skills[i].name.Replace("(UnityEngine.Sprite)", "");
            }
                
            if(characterControl.characters[2].skillTwo == skills[i].name.Replace("(UnityEngine.Sprite)", ""))
            {
                characterControl.cThreeSkillTwo.GetComponent<Image>().sprite = skills[i];
                characterControl.cThreeSkillTwo.name = skills[i].name.Replace("(UnityEngine.Sprite)", "");
            }
                
        }
        if(characterControl.characters[0].skillOne != "")
            characterControl.cOneSkillOne.SetActive(true);
        if(characterControl.characters[0].skillTwo != "")
            characterControl.cOneSkillTwo.SetActive(true);
        if(characterControl.searchPirates())
        {
            GetComponent<Artifacts>().bombBag = true;
            GetComponent<Artifacts>().artifacts.Push("Bomb Bag");
        }
        bossSpawner = UnityEngine.Random.Range(15, 21);
        shopSpawner = UnityEngine.Random.Range(9, 15);
        if(characterControl.searchTraitAll("Hunter") != -1)
        {
            bossSpawner -= 5;  //Hunter Function
        }
        if (characterControl.searchTraitAll("Impatient") != -1)
        {
            spawnManaCrystals = true;  //Impatient Function
        }
        if (characterControl.searchTraitAll("Comforting") != -1)
        {
            //Comforting Function
        }
        fillBoard();
    }
    public void predict()
    {   
        int pGold = 0;
        int pHealth = 0;
        double damage = characterControl.getAttack();
        int swords = 0;
        GameObject[] myArray = collected.ToArray();
        Stack<GameObject> myGoblins = new Stack<GameObject>();
        if (myArray.Length >= 3)
        {
            swords = 0;
            for (int i =0 ; i < myArray.Length; i++)
            {               
                if (myArray[i].GetComponent<Tile>().mType == "Health")
                {                   
                    if (myArray[i].GetComponent<Tile>().empowered)
                    {
                        pHealth += 2;
                    }
                    else
                        pHealth++;
                }
                if (myArray[i].GetComponent<Tile>().mType == "Coin")
                {
                    if (myArray[i].GetComponent<Tile>().empowered)
                    {
                        pGold += 2;
                    }
                    else
                        pGold++;
                }                                
                if(myArray[i].GetComponent<Tile>().mType == "Goblin")
                {
                    myGoblins.Push(myArray[i]);                                        
                }              
                if(myArray[i].GetComponent<Tile>().mType == "Sword")
                {
                    if (myArray[i].GetComponent<Tile>().empowered)
                    {
                        swords += 2;
                    }
                    else
                        swords++;
                }
                if (myArray[i].GetComponent<Tile>().mType == "Barrel")
                    swords+=2;               
            }
            if(myGoblins.Count > 0)
            {
                int count = myGoblins.Count;
                for (int i = 0; i <count; i++)
                {
                    GameObject tempGoblin = myGoblins.Pop();
                    int row = tempGoblin.GetComponent<Tile>().mRow;
                    int col = tempGoblin.GetComponent<Tile>().mCol;
                    board[row, col].GetComponent<Enemy>().predictedDamage = 0;
                    damage = characterControl.getAttack() + swords;
                    if (GetComponent<Artifacts>().amuletOfPain) 
                    {
                        damage += Math.Floor(characterControl.getMaxHealth() - characterControl.getCurrentHealth() / 10);
                    }
                    if(GetComponent<Artifacts>().faetouchedAmulet)
                    {
                        if (i % 3 == 0) 
                            damage += 2;
                    }
                    if(GetComponent<Artifacts>().whetstone)
                        damage += whetstoneValue;
                    if (GameControl.overpowered)
                        damage *= 2;
                    if (GameControl.sacrifice)
                        damage *= 3;
                    if (GameControl.targeted)
                    {
                        if (i == count - 1)
                            damage = (int)(damage * 2.5);
                        else if (i == count - 2)
                            damage = (int)(damage * 2);
                        else if (i == count - 3)
                            damage = (int)(damage * 1.5);
                    }
                    if (GameControl.calculated)
                    {
                        if (i == 2)
                            damage = (int)(damage * 1.5);
                        else if (i == 1)
                            damage = (int)(damage * 2);
                        else if (i == 0)
                            damage = (int)(damage * 2.5);
                    }
                    if(GameControl.doubleShot > 0)
                    {
                        damage *= .75;                      
                    }
                    if (i == myArray.Length - 1)
                    { }
                    else
                    {
                        if (!characterControl.searchActiveCharacterTraits("Cunning"))
                        {
                            if (myGoblins.Count > 0)
                            {
                                GameObject prevGoblin = myGoblins.Peek();
                                int pRow = prevGoblin.GetComponent<Tile>().mRow;
                                int pCol = prevGoblin.GetComponent<Tile>().mCol;

                                if (characterControl.searchActiveCharacterTraits("Slasher"))
                                {
                                    if (row - pRow == 1 && col - pCol == 1) //SE
                                    {
                                        board[pRow, pCol].GetComponent<Enemy>().pDamage = 2;
                                        board[pRow, pCol].transform.GetChild(7).gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                                    }
                                    if (row - pRow == 1 && col - pCol == -1) //SW
                                    {
                                        board[pRow, pCol].GetComponent<Enemy>().pDamage = 2;
                                        board[pRow, pCol].transform.GetChild(6).gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                                    }

                                    if (row - pRow == -1 && col - pCol == 1) //NE
                                    {
                                        board[pRow, pCol].GetComponent<Enemy>().pDamage = 2;
                                        board[pRow, pCol].transform.GetChild(5).gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                                    }
                                    if (row - pRow == -1 && col - pCol == -1) //NW
                                    {
                                        board[pRow, pCol].GetComponent<Enemy>().pDamage = 2;
                                        board[pRow, pCol].transform.GetChild(4).gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                                    }
                                }
                                if (characterControl.searchActiveCharacterTraits("Chopper"))
                                {
                                    if (row - pRow == 1 && col - pCol == 0 || row - pRow == -1 && col - pCol == 0)
                                    {
                                        board[pRow, pCol].GetComponent<Enemy>().pDamage = 2;
                                        if (row - pRow == 1 && col - pCol == 0) //Down
                                        {
                                            board[pRow, pCol].transform.GetChild(3).gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                                        }
                                        if (row - pRow == -1 && col - pCol == 0) //Up
                                        {
                                            board[pRow, pCol].transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                                        }
                                    }
                                }
                                if (characterControl.searchActiveCharacterTraits("Hacker"))
                                {
                                    if (col - pCol == 1 && row - pRow == 0 || col - pCol == -1 && row - pRow == 0)
                                    {
                                        board[pRow, pCol].GetComponent<Enemy>().pDamage = 2;
                                        if (row - pRow == 0 && col - pCol == 1) //Right
                                        {
                                            board[pRow, pCol].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                                        }
                                        if (row - pRow == 0 && col - pCol == -1) //Left
                                        {
                                            board[pRow, pCol].transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (checkIfPickPocket())
                        damage /= 2;
                    damage = Math.Round(damage);
                    board[row, col].GetComponent<Enemy>().predictedDamage = damage + board[row, col].GetComponent<Enemy>().pDamage;
                    predictText.pDamage = (int)damage;
                }
            }
            else
            {
                predictText.pDamage = 0;
            }

            if (pHealth > 0)
            {
                if (GetComponent<Artifacts>().prism)
                    pHealth *= prismValue;
                if (GameControl.doubleShot > 0)
                    pHealth = (int)(pHealth * .75);
                predictText.pHealth = pHealth;
            }              
            
            if (pGold > 0)
            {
                if (GameControl.doubleShot > 0)
                    pGold = (int)(pGold * .75);
                predictText.pGold = pGold;
            }
                

        }
        else if(!screenUp)
        {
            predictText.pHealth = 0;
            predictText.pGold = 0;
            predictText.pDamage = 0;
        }
        else
        {
            predictText.pHealth = 0;
            predictText.pGold = 0;
            predictText.pDamage = 0;
            for (int i = 0; i < myArray.Length; i++)
            {
                if (myArray[i].GetComponent<Tile>().mType == "Goblin")
                    board[myArray[i].GetComponent<Tile>().mRow, myArray[i].GetComponent<Tile>().mCol].GetComponent<Enemy>().predictedDamage = 0;
            }
        }
        
    }

    public void castSpell(GameObject temp)
    {
        if (temp.gameObject.transform.GetChild(0).gameObject.activeSelf)
        {
            temp.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            currentSpell = temp;
            temp.transform.GetChild(0).gameObject.SetActive(true);
            int bigCD = 15;
            int medCD = 10;
            int smallCD = 5;

            int CD = 0;

            // if (lastTurnSpellCast != turnCounter)
            //{
            if (temp.transform.name == ("Alchemy") && temp.gameObject.GetComponent<Spell>().coolDown == 0)
            {
                alchemy();
                CD = medCD;
                setSpellCD(temp, CD);
            }
            else if (temp.transform.name == ("Ambush") && temp.gameObject.GetComponent<Spell>().coolDown == 0)
            {
                ambush();
                CD = bigCD;
                setSpellCD(temp, CD);
            }
            else if (temp.transform.name == ("Appraise") && temp.gameObject.GetComponent<Spell>().coolDown == 0)
            {
                appraise();
                CD = bigCD;
                setSpellCD(temp, CD);
            }
            else if (temp.transform.name == "Blink" && temp.gameObject.GetComponent<Spell>().coolDown == 0)
            {
                GameControl.blink = true;
                CD = bigCD;
                setSpellCD(temp, CD);
            }
            else if (temp.transform.name == ("Bloodlust") && temp.gameObject.GetComponent<Spell>().coolDown == 0)
            {
                GameControl.bloodlust = true;
                CD = smallCD;
                setSpellCD(temp, CD);
            }
            else if (temp.transform.name == ("Bloodshot") && temp.gameObject.GetComponent<Spell>().coolDown == 0)
            {
                bloodshot();
                CD = bigCD;
                setSpellCD(temp, CD);
            }
            else if (temp.transform.name == ("Crystalmancy") && temp.gameObject.GetComponent<Spell>().coolDown == 0)
            {
                crystalmancy();
                CD = bigCD;
                setSpellCD(temp, CD);
            }
            else if (temp.transform.name == ("Discipline") && temp.gameObject.GetComponent<Spell>().coolDown == 0)
            {
                discipline();
                CD = medCD;
                setSpellCD(temp, CD);
            }
            else if (temp.transform.name == ("Double Shot") && temp.gameObject.GetComponent<Spell>().coolDown == 0)
            {
                temp.transform.GetChild(2).gameObject.SetActive(true);
                GameControl.doubleShot = 2;
                CD = medCD;
                setSpellCD(temp, CD);
            }
            else if (temp.transform.name == ("Elbow Grease") && temp.gameObject.GetComponent<Spell>().coolDown == 0)
            {
                elbowGrease();
                CD = medCD;
                setSpellCD(temp, CD);
            }
            else if (temp.transform.name == ("Execute") && temp.gameObject.GetComponent<Spell>().coolDown == 0)
            {
                GameControl.execute = true;
                CD = medCD;
                setSpellCD(temp, CD);
            }
            else if (temp.transform.name == ("Gather") && temp.gameObject.GetComponent<Spell>().coolDown == 0)
            {
                gather();
                CD = bigCD;
                setSpellCD(temp, CD);
            }
            else if (temp.transform.name == ("Gold Rush") && temp.gameObject.GetComponent<Spell>().coolDown == 0)
            {
                goldRush();
                CD = bigCD;
                setSpellCD(temp, CD);
            }
            else if (temp.transform.name == ("Hex Ball") && temp.gameObject.GetComponent<Spell>().coolDown == 0)
            {
                temp.transform.GetChild(2).gameObject.SetActive(true);
                GameControl.hexBall = true;
                CD = bigCD;
                setSpellCD(temp, CD);
            }
            else if (temp.transform.name == ("Mend") && temp.gameObject.GetComponent<Spell>().coolDown == 0)
            {
                mend();
                CD = bigCD;
                setSpellCD(temp, CD);
            }
            else if (temp.transform.name == ("Overpower") && temp.gameObject.GetComponent<Spell>().coolDown == 0)
            {
                temp.transform.GetChild(2).gameObject.SetActive(true);
                GameControl.overpowered = true;
                CD = medCD;
                setSpellCD(temp, CD);
            }
            else if (temp.transform.name == ("Powder Keg") && temp.gameObject.GetComponent<Spell>().coolDown == 0)
            {
                temp.transform.GetChild(2).gameObject.SetActive(true);
                GameControl.powderKeg = true;
                CD = medCD;
                setSpellCD(temp, CD);
            }
            else if (temp.transform.name == ("Vanish") && temp.gameObject.GetComponent<Spell>().coolDown == 0)
            {
                GameControl.vanish = true;
                CD = medCD;
                setSpellCD(temp, CD);
            }
            else if (temp.transform.name == ("Sacrifice") && temp.gameObject.GetComponent<Spell>().coolDown == 0)
            {
                GameControl.sacrifice = true;
                characterControl.setCurrentHealth((int)(characterControl.getCurrentHealth() / 2));
                CD = smallCD;
                setSpellCD(temp, CD);
            }
            else if (temp.transform.name == ("Vortex") && temp.gameObject.GetComponent<Spell>().coolDown == 0)
            {
                vortex();
                CD = 0;
                setSpellCD(temp, CD);
            }
            else if (temp.transform.name == ("Freeze") || temp.transform.name == ("Flash Bang") && temp.gameObject.GetComponent<Spell>().coolDown == 0)
            {
                freeze();
                CD = medCD;
                setSpellCD(temp, CD);
            }
            else if (temp.transform.name == ("Search") && temp.gameObject.GetComponent<Spell>().coolDown == 0)
            {
                search();
                CD = 0;
                setSpellCD(temp, CD);
            }
            else if (temp.transform.name == ("Bomb") && temp.gameObject.GetComponent<Spell>().coolDown == 0)
            {
                GameControl.bomb = true;
                CD = 0;
                setSpellCD(temp, CD);
            }
            else if (temp.transform.name == ("Dragon Shot") && temp.gameObject.GetComponent<Spell>().coolDown == 0)
            {
                temp.transform.GetChild(2).gameObject.SetActive(true);
                GameControl.dragonShot = true;
                CD = medCD;
                setSpellCD(temp, CD);
            }
            else if (temp.transform.name == ("Bomber Shot") && temp.gameObject.GetComponent<Spell>().coolDown == 0)
            {
                temp.transform.GetChild(2).gameObject.SetActive(true);
                GameControl.bomberShot = true;
                CD = medCD;
                setSpellCD(temp, CD);
            }
            else if (temp.transform.name == ("Targeted") && temp.gameObject.GetComponent<Spell>().coolDown == 0)
            {
                GameControl.targeted = true;
                CD = 0;
                setSpellCD(temp, CD);
            }
            else if (temp.transform.name == ("Calculated") && temp.gameObject.GetComponent<Spell>().coolDown == 0)
            {
                GameControl.calculated = true;
                CD = 0;
                setSpellCD(temp, CD);
            }
            else if (temp.transform.name == ("Missle") && temp.gameObject.GetComponent<Spell>().coolDown == 0)
            {
                int missles = UnityEngine.Random.Range(5, 16);
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        int r = UnityEngine.Random.Range(1, 101);
                        int chance = 33;
                        if (board[i, j].GetComponent<Tile>().mType == "Goblin" || board[i, j].GetComponent<Tile>().mType == "Rubble")
                            chance = 66;
                        if (r < chance && missles > 0)
                        {
                            board[i, j].GetComponent<Tile>().mType = "Collected";
                            Destroy(board[i, j]);
                            missles--;
                        }
                    }
                }
                shiftBoard();
                CD = 0;
                setSpellCD(temp, CD);
            }
            else if (temp.transform.name == ("Smite") && temp.gameObject.GetComponent<Spell>().coolDown == 0 && checkGoblinExists())
            {
                GameControl.smite = true;
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        if(board[i, j].GetComponent<Tile>().mType == "Sword")
                        {
                            GameObject smiteSword = (GameObject)Instantiate(Resources.Load("Tiles/Smite Sword"), new Vector3(board[i, j].transform.position.x, board[i, j].transform.position.y, -2.5f), Quaternion.identity);
                            board[i, j].GetComponent<Tile>().mType = "Collected";
                            board[i, j].gameObject.SetActive(false);
                            smiteSwords.Push(smiteSword);

                        }
                    }
                }
            }
                //}
                //else
                //  Debug.Log("Already used spell this turn. " + lastTurnSpellCast + " | " + turnCounter);

        }
    }

    public IEnumerator collect()
    {
        int collectAmount;
        if (cunningInCollected)
            collectAmount = 4;
        else
            collectAmount = 3;
        double healthGain = 0;
        count = collected.Count;
        Tile.numCollectedG = 0;
        Tile.numCollectedH = 0;        
        if (count < collectAmount) //Collected less than 3 tiles
        {
            if (collected.Peek().GetComponent<Tile>().mType == "Shopkeeper")
            {
                string type = "";
                string art = "";
                int tier = 0;
                string title = "";
                //string bonus = "";

                Destroy(collected.Pop().gameObject);
                shiftBoard();
                shopKeeperUp = false;
                screenUp = true;
                Shop = (GameObject)Instantiate(shop, new Vector3(3.29F, -3.36F, -3.06F), Quaternion.identity);
                shopUp = true;
                for (int i = 0; i < 3; i++)
                {
                    Item item = new Item();
                    Vector3 pos;
                    if (i == 0)
                        pos = new Vector3(0.75F, -4.5F, -5.86F);
                    else if (i == 1)
                        pos = new Vector3(2.5F, -3.5F, -5.86F);
                    else
                        pos = new Vector3(4.2F, -4.5F, -5.86F);

                    tier = item.RollTier();
                    type = item.RollType();
                    if (i == 1)
                        type = "Armor";
                    art = item.RollArt(type, tier);

                    items[i] = (GameObject)Instantiate(Resources.Load("Equipment/" + type + "/" + art), pos, Quaternion.identity);
                                     
                    temp = UnityEngine.Random.Range(1, 16);
                    if (tier == 1)
                        items[i].GetComponent<Item>().mCost = (int)(turnCounter) + temp; // Turn8 | min:9 max:23 avg:16
                    else if (tier == 2)
                        items[i].GetComponent<Item>().mCost = 10 + (int)(turnCounter) + temp; //Turn8 | min:19 max:33 avg:26
                    else if (tier == 3)
                        items[i].GetComponent<Item>().mCost = 20 + (int)(turnCounter) + temp; //Turn8 | min:29 max:43 avg:36
                    else if (tier == 4)
                        items[i].GetComponent<Item>().mCost = 30 + (int)(turnCounter) + temp; //Turn8 | min:39 max:53 avg:46

                    if(GetComponent<Artifacts>().coupon)
                    {
                        items[i].GetComponent<Item>().mCost = (int)(items[i].GetComponent<Item>().mCost * .2); //Coupon Function
                    }

                    if (type == "Armor" || type == "Light Armor" || type == "Helmets" || type == "Shields")
                        items[i].GetComponent<Item>().mArmor = (int)characterControl.getLowestArmor() + tier;
                    else
                        items[i].GetComponent<Item>().mDamage = (int)characterControl.getLowestAttack() + tier;

                    if (characterControl.searchActiveCharacterTraits("charasmatic")) //Charasmatic Function
                    {
                        items[i].GetComponent<Item>().mCost /= 2;
                    }

                    items[i].GetComponent<Item>().mType = type;
                    items[i].GetComponent<Item>().mTier = tier;
                    items[i].GetComponent<Item>().index = i;
                    items[i].GetComponent<Item>().mAttributeOne = "";
                    items[i].GetComponent<Item>().mAttributeTwo = "";
                    items[i].GetComponent<Item>().mAttributeThree = "";

                    if (tier == 2)
                    {
                        items[i].GetComponent<Item>().mAttributeOne = item.RollAttributes(items[i].GetComponent<Item>().mType, "One");
                        items[i].transform.Find("Attribute One").Find(items[i].GetComponent<Item>().mAttributeOne).gameObject.SetActive(true);                        
                    }
                    else if (tier == 3)
                    {
                        items[i].GetComponent<Item>().mAttributeOne = item.RollAttributes(items[i].GetComponent<Item>().mType, "One");
                        items[i].transform.Find("Attribute One").Find(items[i].GetComponent<Item>().mAttributeOne).gameObject.SetActive(true);
                        items[i].GetComponent<Item>().mAttributeTwo = item.RollAttributes(items[i].GetComponent<Item>().mType, "Two");
                        items[i].transform.Find("Attribute Two").Find(items[i].GetComponent<Item>().mAttributeTwo).gameObject.SetActive(true);
                    }
                    else if (tier == 4)
                    {
                        items[i].GetComponent<Item>().mAttributeOne = item.RollAttributes(items[i].GetComponent<Item>().mType, "One");
                        items[i].transform.Find("Attribute One").Find(items[i].GetComponent<Item>().mAttributeOne).gameObject.SetActive(true);
                        items[i].GetComponent<Item>().mAttributeTwo = item.RollAttributes(items[i].GetComponent<Item>().mType, "Two");
                        items[i].transform.Find("Attribute Two").Find(items[i].GetComponent<Item>().mAttributeTwo).gameObject.SetActive(true);
                        items[i].GetComponent<Item>().mAttributeThree = item.RollAttributes(items[i].GetComponent<Item>().mType, "Three");
                        items[i].transform.Find("Attribute Three").Find(items[i].GetComponent<Item>().mAttributeThree).gameObject.SetActive(true);
                    }
                    
                    title = item.createTitle(type);
                    string bonus = item.createBonus();
                    items[i].GetComponent<Item>().mTitle = title + " " + bonus;
                    items[i].GetComponent<Item>().mBonus = bonus;
                }
            }          
            else if (collected.Peek().GetComponent<Tile>().mType == "Chest")
            {
                GameObject temp = collected.Pop();
                int col = temp.GetComponent<Tile>().mCol;
                int row = temp.GetComponent<Tile>().mRow;
                float X = temp.transform.localPosition.x;
                float Y = temp.transform.localPosition.y;
                Destroy(temp);   
                board[row, col].transform.GetChild(2).GetComponent<Animator>().SetBool("Collected", true);
                Destroy(board[row, col].gameObject, 2.10F);
                Vector3 position = new Vector3(X, Y, 0.0F);
                GetComponent<Artifacts>().createArtifact(col, row, position, GetComponent<Artifacts>().rollArtifactBoss());
            }
            else if (collected.Peek().GetComponent<Tile>().mType == "Artifact")
            {
                GameObject temp = collected.Pop();
                int col = temp.GetComponent<Tile>().mCol;
                int row = temp.GetComponent<Tile>().mRow;
                GetComponent<Artifacts>().collectArtifact(temp);

                Destroy(temp);
                Destroy(board[row, col]);
                shiftBoard();
            }
            else if (collected.Peek().GetComponent<Tile>().mType == "Goblin" && GameControl.execute)
            {
                GameControl.execute = false;
                GameObject temp = collected.Pop();
                int col = temp.GetComponent<Tile>().mCol;
                int row = temp.GetComponent<Tile>().mRow;
                Destroy(temp);
                Destroy(board[row, col]);
                shiftBoard();
            }
            else if (collected.Peek().GetComponent<Tile>().mType == "Bomb" || GameControl.bomb)
            {
                GameControl.bomb = false;
                GameObject temp = collected.Pop();
                int col = temp.GetComponent<Tile>().mCol;
                int row = temp.GetComponent<Tile>().mRow;
                Destroy(temp);
                Destroy(board[row, col]);
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        if (i >= (row - 1) && i <= (row + 1))                           
                        {
                            if(j >= (col - 1) && j <= (col + 1))
                            {
                                board[i, j].GetComponent<Tile>().mType = "Collected";
                                Destroy(board[i, j]);
                            }                          
                        }
                    }
                }
                shiftBoard();
                checkGhosts();
                if (!intimidating && !meek && !sleightOfHand && !survivalist)
                {
                    yield return new WaitUntil(() => Tile.numCollectedG <= 0);
                    yield return new WaitUntil(() => Tile.numCollectedH <= 0);
                    yield return StartCoroutine(takeDamage());
                }
                characterControl.switchCharacter();
                unfreeze();
                turnCounter++;
                screenUp = false;
            }
            else if(GameControl.dragonShot)
            {
                GameControl.dragonShot = false;
                currentSpell.transform.GetChild(2).gameObject.SetActive(false);
                GameObject temp = collected.Pop();
                int col = temp.GetComponent<Tile>().mCol;
                int row = temp.GetComponent<Tile>().mRow;
                Destroy(temp);
                explode(row, col, "Dragon Shot");
                yield return new WaitForSeconds(1F);
                shiftBoard();
            }
            else if(GameControl.bomberShot)
            {
                GameControl.bomberShot = false;
                currentSpell.transform.GetChild(2).gameObject.SetActive(false);
                GameObject temp = collected.Pop();
                int col = temp.GetComponent<Tile>().mCol;
                int row = temp.GetComponent<Tile>().mRow;
                Destroy(temp);               
                explode(row, col, "Bomber Shot");
                yield return new WaitForSeconds(1F);
                shiftBoard();
            }
            else if (GameControl.blink)
            {
                GameControl.blink = false;
                GameObject temp = collected.Pop();
                int col = temp.GetComponent<Tile>().mCol;
                int row = temp.GetComponent<Tile>().mRow;
                Destroy(temp);
                Destroy(board[row, col]);
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        if (i == row || j == col)
                        {
                            board[i, j].GetComponent<Tile>().mType = "Collected";
                            Destroy(board[i, j]);
                        }                           
                    }
                }
                shiftBoard();
            }
            else if (GameControl.hexBall)
            {
                GameControl.hexBall = false;
                currentSpell.transform.GetChild(2).gameObject.SetActive(false);
                GameObject temp = collected.Pop();
                int col = temp.GetComponent<Tile>().mCol;
                int row = temp.GetComponent<Tile>().mRow;
                if(temp.GetComponent<Tile>().mType != "Goblin")
                {
                    Destroy(board[row, col]);
                    board[row, col] = temp;
                }                   
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        if (i >= (row - 1) && i <= (row + 1))
                        {
                            if (j >= (col - 1) && j <= (col + 1))
                            {
                                string type = board[i, j].GetComponent<Tile>().mType;
                                if (type == "Sword") //Empower
                                {
                                    board[i, j].GetComponent<Tile>().empowered = true;
                                    board[i, j].transform.GetChild(8).gameObject.SetActive(false);
                                    board[i, j].transform.GetChild(9).gameObject.SetActive(true);
                                }
                                if (type == "Health" || type == "Mana" || type == "Rubble" || type == "Coin") //Collect
                                {
                                    board[i, j].GetComponent<Tile>().mType = "Collected";
                                    if(type == "Health")
                                    {
                                        characterControl.setCurrentHealth(characterControl.getCurrentHealth() + 1);
                                    }
                                    else if (type == "Mana")
                                    {
                                        if (characterControl.searchSkillExists(spellsOnCD[i].transform.name))
                                        {
                                            if (spellsOnCD[i].GetComponent<Spell>().coolDown - 1 < 0)
                                                spellsOnCD[i].GetComponent<Spell>().turnEnd = turnCounter;
                                            else
                                                spellsOnCD[i].GetComponent<Spell>().turnEnd -= 1;
                                        }
                                    }
                                    else if (type == "Coin")
                                    {
                                        GameControl.gold++;
                                    }
                                }
                                if(type == "Goblin")
                                {
                                    board[i, j].GetComponent<Enemy>().cursed = true;
                                }
                                if (type == "Bomb" || type == "Barrel") //Expand Hex Ball
                                {
                                    HexBall(board[i, j]);
                                }
                            }
                        }
                    }
                }
                shiftBoard();
            }
            else if (GameControl.powderKeg)
            {
                         
                if (collected.Peek().GetComponent<Tile>().mType != "Goblin")
                {
                    GameObject temp = collected.Pop();
                    int row = temp.GetComponent<Tile>().mRow;
                    int col = temp.GetComponent<Tile>().mCol;
                    GameControl.powderKeg = false;  
                    currentSpell.transform.GetChild(2).gameObject.SetActive(false);                  
                    Destroy(temp.gameObject);
                    Destroy(board[row, col].gameObject);
                    Vector3 pos = temp.transform.position;
                    board[row, col] = (GameObject)Instantiate(barrel, pos, Quaternion.identity);
                    board[row, col].GetComponent<Tile>().mType = "Barrel";
                    board[row, col].GetComponent<Tile>().mCol = col;
                    board[row, col].GetComponent<Tile>().mRow = row;
                }
                else
                {
                    reverseBoard();
                }
                
            }
            else
            {
                reverseBoard();
            }
        }
        else //Collected more than 3 tiles
        {
            screenUp = true;
            for(int i = 0; i < gameScreenParts.Length; i++)
            {
                gameScreenParts[i].GetComponent<SpriteRenderer>().color = Color.gray;
            }
            for(int i = 0; i < 6; i++)
            {
                for(int j = 0; j < 6; j++)
                {
                    if (board[i, j].transform.childCount > 9)
                        board[i, j].transform.GetChild(8).GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .25f);
                }
            }
            slimeNeedsToEat = true;         
            //float exp = 0;
            double goldCollected = 0;           
            double swords = 0;
            int mana = 0;
            intimidating = false;
            meek = false;
            sleightOfHand = false;
            survivalist = false;           
            predictText.pHealth = 0;
            predictText.pGold = 0;
            predictText.pDamage = 0;           
            for (int i = 0; i < count; i++)
            {
                GameObject obj = collected.Pop();             
                String type = obj.GetComponent<Tile>().mType;
                if (board[obj.GetComponent<Tile>().mRow, obj.GetComponent<Tile>().mCol].GetComponent<Tile>().particle != null)
                {
                    board[obj.GetComponent<Tile>().mRow, obj.GetComponent<Tile>().mCol].GetComponent<Tile>().particle.Play();
                    board[obj.GetComponent<Tile>().mRow, obj.GetComponent<Tile>().mCol].GetComponent<Tile>().particle.transform.parent = null;
                    Destroy(board[obj.GetComponent<Tile>().mRow, obj.GetComponent<Tile>().mCol].GetComponent<Tile>().particle.gameObject, 3.0f);
                }
                if (board[obj.GetComponent<Tile>().mRow, obj.GetComponent<Tile>().mCol].GetComponent<Tile>().particle2 != null)
                {
                    board[obj.GetComponent<Tile>().mRow, obj.GetComponent<Tile>().mCol].GetComponent<Tile>().particle2.Play();
                    board[obj.GetComponent<Tile>().mRow, obj.GetComponent<Tile>().mCol].GetComponent<Tile>().particle2.transform.parent = null;
                    Destroy(board[obj.GetComponent<Tile>().mRow, obj.GetComponent<Tile>().mCol].GetComponent<Tile>().particle2.gameObject, 3.0f);
                }
                if (type == "Health")
                {
                    spawnScoreAddition(collected.Count, count, "Health");
                    Tile.numCollectedH++;
                    obj.SetActive(true);
                    obj.GetComponent<Tile>().collected = true;      
                    if(obj.GetComponent<Tile>().empowered)
                        healthGain += 2;
                    else            
                        healthGain++;                     
                }
                else if (type == "Coin")
                {
                    spawnScoreAddition(collected.Count, count, "Coin");
                    Tile.numCollectedG++;
                    obj.SetActive(true);
                    obj.GetComponent<Tile>().collected = true;                     
                    if (GetComponent<Artifacts>().dragonSickness) //Dragon Sickness Function 
                    {
                        if (count > 3 && count < 7)
                        {
                            if(obj.GetComponent<Tile>().empowered)
                            {
                                goldCollected += 2;
                            }
                            else
                                goldCollected++;
                        }                           
                        else if (count > 7)
                        {
                            if(obj.GetComponent<Tile>().empowered)
                            {
                                goldCollected += 3;
                            }
                            else
                                goldCollected += 2;
                        }
                    }
                    else
                        goldCollected++;
                }
                else if (type == "Sword")
                {
                    spawnScoreAddition(collected.Count, count, "Sword");
                    if (obj.GetComponent<Tile>().empowered)
                    {
                        swords += 2;
                    }
                    else
                        swords++;
                    Destroy(obj.gameObject);
                }
                else if (type == "Barrel")
                {
                    swords+=2;
                    Destroy(obj.gameObject);
                }
                else if (type == "Goblin")
                    enemies.Push(obj);   
                else if (type == "Ghost")
                {
                    Destroy(obj.gameObject);                  
                }
                else if (obj.GetComponent<Tile>().mType == "Rubble")
                {
                    rubbleCollected++;
                    temp = UnityEngine.Random.Range(1, 101);
                    if(GetComponent<Artifacts>().loadedDie) //Loaded Die Function
                        temp = UnityEngine.Random.Range(1, 51);
                    if(GetComponent<Artifacts>().leatherGloves) //Leather Gloves Function
                        temp = UnityEngine.Random.Range(1, 23); 
                    if (temp <= 10) //Turn Rubble into Health
                    {
                        Tile.numCollectedH++;
                        GameObject tempHealth;
                        Vector3 pos = obj.transform.position;
                        tempHealth = (GameObject)Instantiate(healthCrystals[0], pos, Quaternion.identity);
                        tempHealth.GetComponent<Tile>().mType = "Health";
                        tempHealth.SetActive(true);
                        tempHealth.GetComponent<Tile>().collected = true;
                        healthGain++;
                    }
                    else if (temp <= 20) //Turn Rubble into Gold
                    {
                        Tile.numCollectedG++;
                        GameObject tempGold;
                        Vector3 pos = obj.transform.position;
                        tempGold = (GameObject)Instantiate(coins[0], pos, Quaternion.identity);
                        tempGold.GetComponent<Tile>().mType = "Coin";
                        tempGold.SetActive(true);
                        tempGold.GetComponent<Tile>().collected = true;
                        goldCollected++;
                        gold++;
                    }
                    else if (temp <= 22) //Turn Rubble into Artifact
                    {
                        int col = obj.GetComponent<Tile>().mCol;
                        int row = obj.GetComponent<Tile>().mRow;
                        Vector3 pos = obj.transform.position;
                        Destroy(board[row, col].gameObject);
                        GetComponent<Artifacts>().createArtifact(col, row, pos, GetComponent<Artifacts>().rollArtifactRubble());
                    }
                    Destroy(obj.gameObject);
                }
                else if (obj.GetComponent<Tile>().mType == "Mana")
                {                   
                    obj.SetActive(true);
                    obj.GetComponent<Tile>().collected = true;
                    mana++;
                }
                if (count > 5)
                {
                    if (characterControl.searchActiveCharacterTraits("Intimidating") && swords > 5) //Intimidating Function
                    {
                        Debug.Log("Intimidating");
                        intimidating = true;
                    }
                    if (characterControl.searchActiveCharacterTraits("Meek") && (mana + healthGain) > 5) //Meek Function
                    {
                        Debug.Log("Meek");
                        meek = true;
                    }
                    if (characterControl.searchActiveCharacterTraits("Sleight of Hand") && goldCollected > 5) //Sleight of Hand Function
                    {
                        Debug.Log("Sleight of Hand");
                        sleightOfHand = true;
                    }
                    if (characterControl.searchActiveCharacterTraits("Survivalist") && rubbleCollected > 5) //Survivalist Function
                    {
                        Debug.Log("Survivalist");
                        survivalist = true;                       
                    }
                }
            }
           
            if (GameControl.doubleShot > 0)
            {
                if (healthGain > 0)
                    healthGain *= .75;
                if (goldCollected > 0)
                    goldCollected *= .75;
                if (swords > 0)
                    swords *= .75;
            }
            if (enemies.Count > 0) //Combat
            {              
                dealDamage(swords);
            }           
            if (healthGain > 0)
            {
                if (GetComponent<Artifacts>().prism) //Prism Function
                    healthGain *= prismValue;                                    
            }
            if(mana > 0)
            {
                for (int i = 0; i < spellsOnCD.Count; i++)
                {
                    if(characterControl.searchSkillExists(spellsOnCD[i].transform.name))
                    {
                        if (spellsOnCD[i].GetComponent<Spell>().coolDown - mana < 0)
                            spellsOnCD[i].GetComponent<Spell>().turnEnd = turnCounter;
                        else
                            spellsOnCD[i].GetComponent<Spell>().turnEnd -= mana;
                    }                
                }                    
            }
            if(GetComponent<Artifacts>().fourLeafClover) //Four Leaf Clover Function         
                goldCollected += (goldCollected / 3);
            if (goldCollected >= 1)
                UpdateText.updateText = "Gold Collected = " + goldCollected + " + " + goldCollected;
            else
                UpdateText.updateText = "Gold Collected = " + goldCollected;

            

            if (checkIfPickPocket())
                goldCollected /= 2;
            Tile.amountCollectedG = goldCollected;
                         
            if(characterControl.searchTraitAll("Tough") != -1) //Tough Function
            {
                if(characterControl.characters[characterControl.searchTraitAll("Tough")].currentHealth + 2 > characterControl.characters[characterControl.searchTraitAll("Tough")].maxHealth)
                {
                    characterControl.characters[characterControl.searchTraitAll("Tough")].currentHealth = characterControl.characters[characterControl.searchTraitAll("Tough")].maxHealth;
                }
                else
                {
                    characterControl.characters[characterControl.searchTraitAll("Tough")].currentHealth += 2;
                }                                   
            }

            if (checkBoardForTile("Bomb"))
            {
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        if (board[i, j].GetComponent<Tile>().mType == "Bomb" && !board[i, j].GetComponent<Tile>().frozen)
                        {
                            if (board[i, j].GetComponent<Tile>().bombTimer > 1)
                                board[i, j].GetComponent<Tile>().bombTimer--;
                            else
                            {
                                explode(i, j, "");
                                yield return new WaitForSeconds(1F);
                            }
                        }
                    }
                }
            }
            Tile.amountCollectedH = healthGain;
            //End of Turn

            //Collect Tiles           
            unfreeze();
            if (enemies.Count > 0)
            {   //Deal Damage                 
                characterControl.setCurrentHealth(characterControl.getCurrentHealth() + dealDamage(swords));
                yield return new WaitForSeconds(0.5F);
            }   
            //Shift Board
            shiftBoard();
            yield return new WaitForSeconds(1F);  
            //Reduce Skill CD's
            turnCounter++;                                        
            yield return new WaitUntil(() => Tile.numCollectedG <= 0);
            yield return new WaitUntil(() => Tile.numCollectedH <= 0);          
            healthGain = 0;
            goldCollected = 0;
            if (GameControl.doubleShot <= 1)
            {   //Take Damage              
                if (!intimidating && !meek && !sleightOfHand && !survivalist && checkGoblinExists())
                {
                    yield return StartCoroutine(takeDamage()); 
                    if(checkFightingGoblinExists())
                    {
                        if (checkBossWithSpellExists())
                        {   //Boss Casts Spell        
                            StartCoroutine(castBossesSpell());
                            yield return new WaitForSeconds(2F);
                        }
                        else
                            yield return new WaitForSeconds(1.25F);
                    }                                                      
                } //Change Character                              
                characterControl.switchCharacter();
                if(GameControl.doubleShot == 1)
                {
                    GameControl.doubleShot--;
                    currentSpell.transform.GetChild(2).gameObject.SetActive(false);
                }
            }
            else
                GameControl.doubleShot--;
            
                       
            checkGhosts();    
            if(GetComponent<Artifacts>().chaosStone)
            {
                chaosStone();
            }
            //exp = 0;
            swords = 0;
            rubbleCollected = 0;
            counter = 0;
            cunningInCollected = false;            
            
            screenUp = false;
            for(int i = 0; i < gameScreenParts.Length; i++)
            {
                gameScreenParts[i].GetComponent<SpriteRenderer>().color = Color.white;
            }
            for(int i = 0; i < 6; i++)
            {
                for(int j = 0; j < 6; j++)
                {
                    if (board[i, j].transform.childCount > 9)
                        board[i, j].transform.GetChild(8).GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
                }
            }
        }
        yield return null;
    }

    IEnumerator castBossesSpell()
    {
        GameObject temp = gameObject;
        int r = 0;
        int c = 0;
        for(int i = 0; i < 6; i++)
        {
            for(int j = 0; j < 6; j++)
            {
                if(board[i,j].GetComponent<Tile>().boss != "" && board[i,j].GetComponent<Tile>().boss != "RatClone")
                {
                    temp = board[i,j].gameObject;
                    r = i;
                    c = j;
                    break;
                }                  
            }
        }
        if (temp.GetComponent<Tile>().boss == "Slime" && slimeNeedsToEat)
        {                                    
            if (r < 5)
            {
                if (board[r + 1, c].GetComponent<Tile>().mType != "Shopkeeper" && board[r + 1, c].GetComponent<Tile>().mType != "Chest")
                {
                    //yield return new WaitForSeconds(.5F);
                    Transform icon = temp.transform.GetChild(8);
                    icon.parent = null;
                    icon.GetComponent<Animator>().SetTrigger("EatDown");
                    yield return new WaitForSeconds(2F);
                    if (board[r + 1, c].GetComponent<Tile>().mType == "Health")
                    {
                        temp.GetComponent<Enemy>().health++;
                    }
                    else if (board[r + 1, c].GetComponent<Tile>().mType == "Coin")
                    {
                        temp.GetComponent<Enemy>().health++;
                        temp.GetComponent<Enemy>().damage++;
                    }
                    else if (board[r + 1, c].GetComponent<Tile>().mType == "Sword")
                    {
                        temp.GetComponent<Enemy>().damage++;
                    }
                    else if (board[r + 1, c].GetComponent<Tile>().mType == "Goblin")
                    {
                        temp.GetComponent<Enemy>().health += board[r + 1, c].GetComponent<Enemy>().health;
                        temp.GetComponent<Enemy>().damage += board[r + 1, c].GetComponent<Enemy>().damage;
                    }
                    board[r + 1, c].GetComponent<Tile>().mType = "Collected";                  
                    shiftBoard();
                    yield return new WaitForSeconds(1F);
                    icon.parent = temp.transform;
                    icon.SetSiblingIndex(8);
                    slimeNeedsToEat = false;
                }
            }
            else if (r > 0)
            {
                if (board[r - 1, c].GetComponent<Tile>().mType != "Shopkeeper" && board[r - 1, c].GetComponent<Tile>().mType != "Chest")
                {
                    if (board[r - 1, c].GetComponent<Tile>().mType == "Health")
                    {
                        temp.GetComponent<Enemy>().health++;
                    }
                    else if (board[r - 1, c].GetComponent<Tile>().mType == "Coin")
                    {
                        temp.GetComponent<Enemy>().health++;
                        temp.GetComponent<Enemy>().damage++;
                    }
                    else if (board[r - 1, c].GetComponent<Tile>().mType == "Sword")
                    {
                        temp.GetComponent<Enemy>().damage++;
                    }
                    else if (board[r - 1, c].GetComponent<Tile>().mType == "Goblin")
                    {
                        temp.GetComponent<Enemy>().health += board[r - 1, c].GetComponent<Enemy>().health;
                        temp.GetComponent<Enemy>().damage += board[r - 1, c].GetComponent<Enemy>().damage;
                    }
                    board[r - 1, c].GetComponent<Tile>().mType = "Collected";
                    shiftBoard();
                    slimeNeedsToEat = false;
                }
            }
        }
        else if (temp.GetComponent<Tile>().boss == "BossBody" && !bossAbilityUsed)
        {
            if(temp.GetComponent<Enemy>().health < 15 && temp.GetComponent<Tile>().lastAbilityUsed != "Steal Health" && numOfTilesInBoard("Health") >= 5)
            {
                Debug.Log("Healing");
                temp.GetComponent<Tile>().lastAbilityUsed = "Steal Health";
                bossAbilityUsed = true;
                GameObject[] body = groupUpBossBody();
                int bodyIndex = 0;
                int lowestHealth = 20;
                for(int k = 0; k < 6; k++)
                {
                    for(int l = 0; l < 6; l++)
                    {
                        if(board[k,l].GetComponent<Tile>().mType == "Health")
                        {
                            lowestHealth = 20;
                            for(int m = 0; m < body.Length; m++)
                            {
                                
                                if(body[m].GetComponent<Enemy>().health < lowestHealth)
                                {
                                    lowestHealth = body[m].GetComponent<Enemy>().health;
                                    bodyIndex = m;
                                }                                                       
                            }
                            if(body[bodyIndex].GetComponent<Enemy>().health < 20)
                            {
                                board[k,l].GetComponent<Tile>().mType = "Collected";
                                body[bodyIndex].GetComponent<Enemy>().health += 1;                                                                                                           
                            }
                        }
                    }
                }
                shiftBoard();
            }
            else if (temp.GetComponent<Tile>().lastAbilityUsed != "Freeze")
            {
                Debug.Log("Freezing");
                temp.GetComponent<Tile>().lastAbilityUsed = "Freeze";
                bossAbilityUsed = true;
                GameObject[] body = groupUpBossBody();
                for(int k = 0; k < body.Length; k++)
                {
                    int row = body[k].GetComponent<Tile>().mRow;
                    int col = body[k].GetComponent<Tile>().mCol;
                    if(board[row - 1, col - 1].GetComponent<Tile>().mType != "Goblin" && !board[row - 1, col - 1].GetComponent<Tile>().frozenLastTurn)
                    {
                        board[row - 1, col - 1].GetComponent<Tile>().frozen = true;
                        board[row - 1, col - 1].gameObject.transform.GetChild(18).gameObject.SetActive(true);
                    }
                    if(board[row - 1, col].GetComponent<Tile>().mType != "Goblin" && !board[row - 1, col].GetComponent<Tile>().frozenLastTurn)
                    {
                        board[row - 1, col].GetComponent<Tile>().frozen = true;
                        board[row - 1, col].gameObject.transform.GetChild(18).gameObject.SetActive(true);
                    }
                    if(board[row - 1, col + 1].GetComponent<Tile>().mType != "Goblin" && !board[row - 1, col + 1].GetComponent<Tile>().frozenLastTurn)
                    {
                        board[row - 1, col + 1].GetComponent<Tile>().frozen = true;
                        board[row - 1, col + 1].gameObject.transform.GetChild(18).gameObject.SetActive(true);
                    }
                    if(board[row, col - 1].GetComponent<Tile>().mType != "Goblin" && !board[row, col - 1].GetComponent<Tile>().frozenLastTurn)
                    {
                        board[row, col - 1].GetComponent<Tile>().frozen = true;
                        board[row, col - 1].gameObject.transform.GetChild(18).gameObject.SetActive(true);
                    }
                    if(board[row, col + 1].GetComponent<Tile>().mType != "Goblin" && !board[row, col + 1].GetComponent<Tile>().frozenLastTurn)
                    {
                        board[row, col + 1].GetComponent<Tile>().frozen = true;
                        board[row, col + 1].gameObject.transform.GetChild(18).gameObject.SetActive(true);
                    }
                    if(board[row + 1, col - 1].GetComponent<Tile>().mType != "Goblin" && !board[row + 1, col - 1].GetComponent<Tile>().frozenLastTurn)
                    {
                        board[row + 1, col - 1].GetComponent<Tile>().frozen = true;
                        board[row + 1, col - 1].gameObject.transform.GetChild(18).gameObject.SetActive(true);
                    }
                    if(board[row + 1, col].GetComponent<Tile>().mType != "Goblin" && !board[row + 1, col].GetComponent<Tile>().frozenLastTurn)
                    {
                        board[row + 1, col].GetComponent<Tile>().frozen = true;
                        board[row + 1, col].gameObject.transform.GetChild(18).gameObject.SetActive(true);
                    }
                    if(board[row + 1, col + 1].GetComponent<Tile>().mType != "Goblin" && !board[row + 1, col + 1].GetComponent<Tile>().frozenLastTurn)
                    {
                        board[row + 1, col + 1].GetComponent<Tile>().frozen = true;
                        board[row + 1, col + 1].gameObject.transform.GetChild(18).gameObject.SetActive(true);
                    }                                       
                }
            }
            else if (temp.GetComponent<Tile>().lastAbilityUsed != "Spell Lock")
            {
                Debug.Log("Spell Lock");
                temp.GetComponent<Tile>().lastAbilityUsed = "Spell Lock";
                bossAbilityUsed = true;
                //Create system to track and lock character skills
            }
        }
        else if (temp.GetComponent<Tile>().boss == "Lich")
        {
            //if (!temp.GetComponent<Enemy>().justSpawned)
                //temp.gameObject.transform.GetChild(8).GetComponent<Animator>().SetTrigger("Attacking");
            Debug.Log("Freezing");
            if (r > 0)
                if (board[r - 1, c].GetComponent<Tile>().mType != "Goblin" && board[r - 1, c].GetComponent<Tile>().mType != "Shopkeeper")
                    if (board[r - 1, c].GetComponent<Tile>().frozenLastTurn == false)
                    {
                        board[r - 1, c].GetComponent<Tile>().frozen = true;
                        board[r - 1, c].gameObject.transform.GetChild(18).gameObject.SetActive(true);
                    }                                                
            if (c > 0)
                if (board[r, c - 1].GetComponent<Tile>().mType != "Goblin" && board[r, c - 1].GetComponent<Tile>().mType != "Shopkeeper")
                    if (board[r, c - 1].GetComponent<Tile>().frozenLastTurn == false)
                    {
                        board[r, c - 1].GetComponent<Tile>().frozen = true;
                        board[r, c - 1].gameObject.transform.GetChild(18).gameObject.SetActive(true);
                    }
            if (r > 0 && c > 0)
                if (board[r - 1, c - 1].GetComponent<Tile>().mType != "Goblin" && board[r - 1, c - 1].GetComponent<Tile>().mType != "Shopkeeper")
                    if (board[r - 1, c - 1].GetComponent<Tile>().frozenLastTurn == false)
                    {
                        board[r - 1, c - 1].GetComponent<Tile>().frozen = true;
                        board[r - 1, c - 1].gameObject.transform.GetChild(18).gameObject.SetActive(true);
                    }
            if (r < 5)
                if (board[r + 1, c].GetComponent<Tile>().mType != "Goblin" && board[r + 1, c].GetComponent<Tile>().mType != "Shopkeeper")
                    if (board[r + 1, c].GetComponent<Tile>().frozenLastTurn == false)
                    {
                        board[r + 1, c].GetComponent<Tile>().frozen = true;
                        board[r + 1, c].gameObject.transform.GetChild(18).gameObject.SetActive(true);
                    }
            if (c < 5)
                if (board[r, c + 1].GetComponent<Tile>().mType != "Goblin" && board[r, c + 1].GetComponent<Tile>().mType != "Shopkeeper")
                    if (board[r, c + 1].GetComponent<Tile>().frozenLastTurn == false)
                    {
                        board[r, c + 1].GetComponent<Tile>().frozen = true;
                        board[r, c + 1].gameObject.transform.GetChild(18).gameObject.SetActive(true);
                    }
            if (r < 5 && c < 5)
                if (board[r + 1, c + 1].GetComponent<Tile>().mType != "Goblin" && board[r + 1, c + 1].GetComponent<Tile>().mType != "Shopkeeper")
                    if (board[r + 1, c + 1].GetComponent<Tile>().frozenLastTurn == false)
                    {
                        board[r + 1, c + 1].GetComponent<Tile>().frozen = true;
                        board[r + 1, c + 1].gameObject.transform.GetChild(18).gameObject.SetActive(true);
                    }
            if (r > 0 && c < 5)
                if (board[r - 1, c + 1].GetComponent<Tile>().mType != "Goblin" && board[r - 1, c + 1].GetComponent<Tile>().mType != "Shopkeeper")
                    if (board[r - 1, c + 1].GetComponent<Tile>().frozenLastTurn == false)
                    {
                        board[r - 1, c + 1].GetComponent<Tile>().frozen = true;
                        board[r - 1, c + 1].gameObject.transform.GetChild(18).gameObject.SetActive(true);
                    }
            if (c > 0 && r < 5)
                if (board[r + 1, c - 1].GetComponent<Tile>().mType != "Goblin" && board[r + 1, c - 1].GetComponent<Tile>().mType != "Shopkeeper")
                    if (board[r + 1, c - 1].GetComponent<Tile>().frozenLastTurn == false)
                    {
                        board[r + 1, c - 1].GetComponent<Tile>().frozen = true;
                        board[r + 1, c - 1].gameObject.transform.GetChild(18).gameObject.SetActive(true);
                    }
        }              
        else if (temp.GetComponent<Tile>().boss == "Spider")                         
        {
            if (temp.GetComponent<Enemy>().health + temp.GetComponent<Enemy>().damage / 2 >= 7 + (goblinScalar * 2))
                temp.GetComponent<Enemy>().health = 7 + (goblinScalar * 2);
            else
                temp.GetComponent<Enemy>().health += temp.GetComponent<Enemy>().damage / 2;
        }
        else if (temp.GetComponent<Tile>().boss == "GreenGenie")
        {
            if (!temp.GetComponent<Enemy>().justSpawned)
                temp.gameObject.transform.GetChild(8).GetComponent<Animator>().SetTrigger("Attacking");
            if (turn <= 3)
            {
                if (r > turn - 1 && c > turn - 1)
                    if (board[r - 1, c - 1].GetComponent<Tile>().mType != "Goblin")
                        transformIntoGoblin(r - turn, c - turn);
                if (r > turn - 1 && c < 6 - turn)
                    if (board[r - 1, c + 1].GetComponent<Tile>().mType != "Goblin")
                        transformIntoGoblin(r - turn, c + turn);
                if (r < 6 - turn && c > turn - 1)
                    if (board[r + 1, c - 1].GetComponent<Tile>().mType != "Goblin")
                        transformIntoGoblin(r + turn, c - turn);
                if (r < 6 - turn && c < 6 - turn)
                    if (board[r + 1, c + 1].GetComponent<Tile>().mType != "Goblin")
                        transformIntoGoblin(r + turn, c + turn);
                turn++;
            }
            else
                turn = 1;
        }
        else if (temp.GetComponent<Tile>().boss == "BlueGenie")
        {                                  
            temp.gameObject.transform.GetChild(8).GetComponent<Animator>().SetTrigger("Attacking");
            if (turn <= 3)
            {
                if (r > turn - 1 && c > turn - 1)
                    if (board[r - turn, c - turn].GetComponent<Tile>().mType != "Coin")
                        transformIntoCoin(r - turn, c - turn);
                if (r > turn - 1 && c < 6 - turn)
                    if (board[r - turn, c + turn].GetComponent<Tile>().mType != "Coin")
                        transformIntoCoin(r - turn, c + turn);
                if (r < 6 - turn && c > turn - 1)
                    if (board[r + turn, c - turn].GetComponent<Tile>().mType != "Coin")
                        transformIntoCoin(r + turn, c - turn);
                if (r < 6 - turn && c < 6 - turn)
                    if (board[r + turn, c + turn].GetComponent<Tile>().mType != "Coin")
                        transformIntoCoin(r + turn, c + turn);
                turn++;
            }
            else
                turn = 1;
        }
    }

    void spawnScoreAddition(int currentCount, int totalCount, string type)
    {
        GameObject score = Instantiate(scoreAddition, new Vector3(4.3f+(.2f*(totalCount - currentCount)), -0.1f-(.2f*(totalCount - currentCount)), -2.5f), Quaternion.identity);
        if (type == "Health")
        {
            score.GetComponent<ScoreAddition>().number = 5 + (totalCount - currentCount);
            ScoreText.score += (5 + (totalCount - currentCount));
            ScoreControl.healthScore += (5 + (totalCount - currentCount));
        }
        if(type == "Coin")
        {
            score.GetComponent<ScoreAddition>().number = 5 + (totalCount - currentCount);
            ScoreText.score += (5 + (totalCount - currentCount));
            ScoreControl.goldScore += (5 + (totalCount - currentCount));
        }
        if(type == "Sword")
        {
            score.GetComponent<ScoreAddition>().number = 5 + (totalCount - currentCount);
            ScoreText.score += (5 + (totalCount - currentCount));
            ScoreControl.swordScore += (5 + (totalCount - currentCount));
        }
        if(type == "Goblin")
        {
            score.GetComponent<ScoreAddition>().number = 10;
            ScoreText.score += 10;
            ScoreControl.goblinScore += 10;
        }
        if(type == "Boss")
        {
            score.GetComponent<ScoreAddition>().number = 50;
            ScoreText.score += 50;
            ScoreControl.bossScore += 50;
        }
    }

    bool checkBossWithSpellExists()
    {
        for(int i = 0; i < 6; i++)
        {
            for(int j = 0; j < 6; j++)
            {
                if(board[i,j].GetComponent<Tile>().boss == "Lich")
                    return true;
                if(board[i,j].GetComponent<Tile>().boss == "BlueGenie")
                    return true;
                if(board[i,j].GetComponent<Tile>().boss == "GreenGenie")
                    return true;
                if(board[i,j].GetComponent<Tile>().boss == "BossArms")
                    return true;
                if(board[i,j].GetComponent<Tile>().boss == "BossBody")
                    return true;
                if(board[i,j].GetComponent<Tile>().boss == "Slime")
                    return true;
            }
        }
        return false;
    }

    bool checkBossArms(GameObject arm)
    {
        if(arm.GetComponent<Tile>().mRow - 1 < 6) //Check Tile Above
            if(board[arm.GetComponent<Tile>().mRow - 1, arm.GetComponent<Tile>().mCol].GetComponent<Tile>().boss == "BossArms")
                return true;
        if(arm.GetComponent<Tile>().mRow + 1 < 6) //Check Tile Below
            if(board[arm.GetComponent<Tile>().mRow + 1, arm.GetComponent<Tile>().mCol].GetComponent<Tile>().boss == "BossArms")
                return true;

        int numArms = 0;
        for(int i = 0; i < 6; i++) //Check if there is only one arm in the column
        {
            if(board[i, arm.GetComponent<Tile>().mCol].GetComponent<Tile>().boss == "BossArms")
                numArms++;
        }
        if(numArms == 1)
            return true;

        return false;
    }

    void checkIfGameWon()
    {
        bool bossDead = true;
        if(PlayerPrefs.GetString("Boss Stage") == "Stage One")
        {
            for(int i = 0; i < 6; i++)
            {
                for(int j = 0; j < 6; j++)
                {
                    if(board[i,j].GetComponent<Tile>().boss == "BossArms")                   
                        bossDead = false;                   
                }
            }
            if(bossDead)
            {
                PlayerPrefs.SetString("Boss Stage", "Stage Two");
                endRun();
            }           
        }
        else if (PlayerPrefs.GetString("Boss Stage") == "Stage Two")
        {
            for(int i = 0; i < 6; i++)
            {
                for(int j = 0; j < 6; j++)
                {
                    if(board[i,j].GetComponent<Tile>().boss == "BossBody")
                        bossDead = false;
                }
            }
            if(bossDead)
            {
                PlayerPrefs.SetString("Boss Stage", "Stage Three");
                endRun();
            }    
        }
        else if (PlayerPrefs.GetString("Boss Stage") == "Stage Three")
        {
            for(int i = 0; i < 6; i++)
            {
                for(int j = 0; j < 6; j++)
                {
                    if(board[i,j].GetComponent<Tile>().boss == "BossBody")
                        bossDead = false;
                }
            }
            if(bossDead)
            {               
                endRun();
            }    
        }
    }

    void endRun()
    {
        GameControl.gold = 0;
        characterControl.setCurrentHealth(characterControl.getMaxHealth());
        characterControl.cOneSkillOne.GetComponent<Spell>().coolDown = 0;
        characterControl.cOneSkillTwo.GetComponent<Spell>().coolDown = 0;
        characterControl.cTwoSkillOne.GetComponent<Spell>().coolDown = 0;
        characterControl.cTwoSkillTwo.GetComponent<Spell>().coolDown = 0;
        characterControl.cThreeSkillOne.GetComponent<Spell>().coolDown = 0;
        characterControl.cThreeSkillTwo.GetComponent<Spell>().coolDown = 0;
        SceneManager.LoadScene("TitleScreen");
    }

    int dealDamage(double swords)
    {
        int healthGain = 0;
        int damageDone = 0;               
        int count = enemies.Count;
        for (int i = 0; i < count; i++)
        {
            double damage = characterControl.getAttack() + swords;
            GameObject enemy = enemies.Pop();
            if (board[enemy.GetComponent<Tile>().mRow, enemy.GetComponent<Tile>().mCol].GetComponent<Enemy>().pDamage == 1) //Weapon Bonuses
                damage += 1;
            if (GetComponent<Artifacts>().amuletOfPain) //Amulet of Pain Function
            {
                damage += Math.Floor(characterControl.getMaxHealth() - characterControl.getCurrentHealth() / 10);
            }
            if(GetComponent<Artifacts>().faetouchedAmulet) //Faetouched Amulet Function
            {
                if(i % 3 == 0)
                    damage += 2;
            }
            if (enemy.GetComponent<Tile>().boss == "Skull")
                damage /= 2;
            //int critChance = UnityEngine.Random.Range(1, 100);
            /*if (critChance < Character.crit)
            {
                damage *= 2;
                UpdateText.updateText = "Critical Hit!";
            } */
            if(characterControl.searchActiveCharacterTraits("Crit")) //Crit Function
            {
                int critChance = UnityEngine.Random.Range(1, 101);
                if(critChance > 80)
                {
                    damage *= 2;
                }
            }
            if (GetComponent<Artifacts>().whetstone) 
                damage += whetstoneValue; //Whetstone Function

            if (GameControl.overpowered)//Overpower Function
            {
                damage *= 2;                       
            }
            if (GameControl.sacrifice)//Sacrifice Function
            {
                damage *= 3;                       
            }
            if (GameControl.targeted)
            {
                if (i == count - 1)
                    damage = damage * 2.5;
                else if(i == count - 2)
                    damage = damage * 2;
                else if (i == count - 3)
                    damage = damage * 1.5;
            }
            if (GameControl.calculated)
            {
                if (i == 0)
                    damage = damage * 2.5f;
                else if (i == 1)
                    damage = damage * 2f;
                else if (i == 2)
                    damage = damage * 1.5f;
            }
            if (checkIfPickPocket())
            {
                damage /= 2;
            }
            if(GameControl.doubleShot > 0)
            {
                damage *= .75;
            }
            damage = Math.Round(damage);
            if (damage < enemy.GetComponent<Enemy>().health)
            {
                if(enemy.GetComponent<Tile>().boss == "BossArms")
                {
                    if(checkBossArms(enemy))
                    {
                        enemy.GetComponent<Enemy>().health -= (int)damage;
                        damageDone += (int)damage;
                    }
                    else
                    {
                        enemy.GetComponent<Tile>().frozen = true;
                        enemy.transform.GetChild(18).gameObject.SetActive(true);
                    }                  
                    Destroy(board[enemy.GetComponent<Tile>().mRow, enemy.GetComponent<Tile>().mCol]);
                    board[enemy.GetComponent<Tile>().mRow, enemy.GetComponent<Tile>().mCol] = enemy;
                    enemy.SetActive(true);
                }
                else if (enemy.GetComponent<Tile>().boss == "BossBody")
                {
                    if(!checkBoardForTile("Goblin", "BossArms"))
                    {
                        enemy.GetComponent<Enemy>().health -= (int)damage;
                        damageDone += (int)damage;
                        Destroy(board[enemy.GetComponent<Tile>().mRow, enemy.GetComponent<Tile>().mCol]);
                        board[enemy.GetComponent<Tile>().mRow, enemy.GetComponent<Tile>().mCol] = enemy;
                        enemy.SetActive(true);
                    }
                    else
                    {
                        Destroy(board[enemy.GetComponent<Tile>().mRow, enemy.GetComponent<Tile>().mCol]);
                        board[enemy.GetComponent<Tile>().mRow, enemy.GetComponent<Tile>().mCol] = enemy;
                        enemy.SetActive(true);
                    }
                }
                else
                {
                    enemy.GetComponent<Enemy>().health -= (int)damage;
                    damageDone += (int)damage;

                    if (GetComponent<Artifacts>().frost || characterControl.searchActiveCharacterTraits("Frost")) //Frost Function
                    {
                        if (enemy.GetComponent<Enemy>().poisoned)
                        {
                            enemy.GetComponent<Enemy>().poisoned = false;
                            enemy.transform.GetChild(20).gameObject.SetActive(false);
                        }
                        enemy.GetComponent<Enemy>().frozen = true;
                        enemy.transform.GetChild(18).gameObject.SetActive(true);
                    }
                    if (characterControl.searchActiveCharacterTraits("Burn")) //Burn Function
                    {
                        if (enemy.GetComponent<Enemy>().frozen)
                        {
                            enemy.GetComponent<Enemy>().frozen = false;
                            enemy.transform.GetChild(18).gameObject.SetActive(false);
                        }
                        enemy.GetComponent<Enemy>().burning = true;
                        enemy.transform.GetChild(19).gameObject.SetActive(true);
                    }
                    if (characterControl.searchActiveCharacterTraits("Poison")) //Poison Function
                    {
                        enemy.GetComponent<Enemy>().poisoned = true;
                        enemy.transform.GetChild(20).gameObject.SetActive(true);
                    }
                    Destroy(board[enemy.GetComponent<Tile>().mRow, enemy.GetComponent<Tile>().mCol]);
                    board[enemy.GetComponent<Tile>().mRow, enemy.GetComponent<Tile>().mCol] = enemy;
                    enemy.SetActive(true);
                }                    
            }
            else
            {
                if (GameControl.bloodlust)//bloodlust function
                {
                    Vector3 pos = enemy.transform.position;
                    enemy = (GameObject)Instantiate(healthCrystals[0], pos, Quaternion.identity);
                    enemy.SetActive(true);
                    enemy.GetComponent<Tile>().collected = true;
                    healthGain++;
                    if (enemy.GetComponent<Tile>().boss != "")
                    {
                        spawnChest = true;
                        GameControl.miniBossUp = false;
                        spawnScoreAddition(0, 0, "Boss");                    
                    }
                    //exp++;
                    spawnScoreAddition(0, 0, "Goblin");
                    GameControl.bloodlust = false;
                }
                else
                {
                    string tempType = enemy.GetComponent<Tile>().boss;                        
                    if(tempType == "BossArms")
                    {
                        if(checkBossArms(enemy))
                        {
                            board[enemy.GetComponent<Tile>().mRow, enemy.GetComponent<Tile>().mCol].GetComponent<Tile>().boss = "";
                            Destroy(enemy.gameObject);
                            spawnScoreAddition(0, 0, "Goblin");
                            damageDone += enemy.GetComponent<Enemy>().health;
                            checkIfGameWon();
                        }
                        else
                        {
                            enemy.GetComponent<Tile>().frozen = true;
                            enemy.transform.GetChild(18).gameObject.SetActive(true);
                        }                      
                    }
                    else if (tempType == "BossBody")
                    {
                        if(!checkBoardForTile("Goblin", "BossArms"))
                        {
                            Destroy(enemy.gameObject);
                            spawnScoreAddition(0, 0, "Goblin");
                            damageDone += enemy.GetComponent<Enemy>().health;
                        }
                    }
                    else
                    {
                        if (tempType != "" && tempType != "RatClone" && tempType != "BossArms" && tempType != "BossBody" && tempType != "Ghost")
                        {
                            spawnChest = true;
                            GameControl.miniBossUp = false;
                            spawnScoreAddition(collected.Count, count, "Boss");
                        }
                        if(checkBoardForTile("Goblin", "BossBody") && tempType != "Ghost")
                        {
                            replaceTile(enemy.GetComponent<Tile>().mRow, enemy.GetComponent<Tile>().mCol, ghost, "Ghost", 1, 5);
                        }
                        else
                        {                           
                            Destroy(enemy.gameObject);
                        }                      
                        spawnScoreAddition(collected.Count, count, "Goblin");
                        damageDone += enemy.GetComponent<Enemy>().health;
                    }                     
                }
                if(characterControl.searchActiveCharacterTraits("Looter"))
                {
                    spawnScoreAddition(0, 0, "Coin"); //Looter Function
                }
            }
        }
        if (GetComponent<Artifacts>().vampireFang) //Vampire Fang Funtion
            healthGain += (int)Math.Floor((double)damageDone / vampireFangValue);
        if(characterControl.searchActiveCharacterTraits("Vamp")) //Vamp Function
        {
            if (characterControl.getCurrentHealth() + 1 >= characterControl.getMaxHealth())
                characterControl.setCurrentHealth(characterControl.getMaxHealth());
            else
                characterControl.setCurrentHealth(characterControl.getCurrentHealth() + 1);
        }
        GameControl.targeted = false;
        GameControl.calculated = false;
        GameControl.overpowered = false;
        GameControl.sacrifice = false;
        return healthGain;
    }
    
    void swapSwords()
    {
        for(int i=0; i<6; i++)
        {
            for(int j=0; j<6; j++)
            {
                if(board[i,j].GetComponent<Tile>().mType == "Sword")
                {
                    board[i, j].GetComponent<Animator>().SetTrigger("Flip");
                }
            }
        }
    }

    public bool checkBoardForTile(string type)
    {
        bool exists = false;
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if (board[i, j].GetComponent<Tile>().mType == type)
                {
                    exists = true;
                }
            }
        }
        return exists;
    }

    private bool checkBoardForTile(string type, string boss)
    {
        bool exists = false;
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if (board[i, j].GetComponent<Tile>().mType == type && board[i, j].GetComponent<Tile>().boss == boss)
                {
                    exists = true;
                }
            }
        }
        return exists;
    }

    public void checkGhosts()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if(board[i,j].GetComponent<Tile>().mType == "Ghost")
                {
                    if (i < 5)
                    {
                        if (board[i + 1, j].GetComponent<Tile>().mType == "Ghost")
                        {
                            board[i, j].GetComponent<Tile>().mType = "Collected";
                            board[i + 1, j].GetComponent<Tile>().mType = "Collected";
                            Destroy(board[i, j]);
                            Destroy(board[i + 1, j]);
                            numGhosts -=2 ;
                            if (numGhosts == 0)
                            {
                                spawnChest = true;
                                GameControl.miniBossUp = false;
                                spawnScoreAddition(0, 0, "Boss");
                            }
                            shiftBoard();
                        }
                    }
                }
            }
        }

    }

    public void unfreeze()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if (board[i, j].GetComponent<Tile>().frozen == true)
                {
                    board[i, j].GetComponent<Tile>().frozen = false;
                    board[i, j].GetComponent<Tile>().frozenLastTurn = true;
                    board[i, j].gameObject.transform.GetChild(18).gameObject.SetActive(false);
                }
                else if (board[i, j].GetComponent<Tile>().frozenLastTurn == true)
                {
                    board[i, j].GetComponent<Tile>().frozenLastTurn = false;
                }
            }
        }
    }
    void setSpellCD(GameObject other, int CD)
    {
        lastTurnSpellCast = turnCounter;
        other.transform.GetChild(0).gameObject.SetActive(true);
        //other.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .5f);
        other.GetComponent<Spell>().turnStart = turnCounter;
        other.GetComponent<Spell>().turnEnd = turnCounter + CD;
        spellsOnCD.Add(other.gameObject);
    }

    public void explode(int row, int col, string Shot)
    {      
        dealWithExplosion(row , col);
        if(row > 0)
        {           
            if(Shot != "Dragon Shot")
            {
                if(board[row - 1, col].GetComponent<Tile>().mType == "Bomb" || board[row - 1, col].GetComponent<Tile>().mType == "Barrel")            
                    explode(row - 1, col, ""); 
                dealWithExplosion(row - 1, col);
            }          
            if(col > 0 && Shot != "Bomber Shot")
            {                
                if(board[row - 1, col - 1].GetComponent<Tile>().mType == "Bomb" || board[row - 1, col - 1].GetComponent<Tile>().mType == "Barrel")            
                    explode(row - 1, col - 1, "");    
                dealWithExplosion(row - 1, col - 1);
            }
            if(col < 5 && Shot != "Bomber Shot")
            {               
                if(board[row - 1, col + 1].GetComponent<Tile>().mType == "Bomb" || board[row - 1, col + 1].GetComponent<Tile>().mType == "Barrel")            
                    explode(row - 1, col + 1, ""); 
                dealWithExplosion(row - 1, col + 1);
            }
        }
        if(row < 5)
        {           
            if(Shot != "Dragon Shot")
            {
                if(board[row + 1, col].GetComponent<Tile>().mType == "Bomb" || board[row + 1, col].GetComponent<Tile>().mType == "Barrel")            
                    explode(row + 1, col, ""); 
                dealWithExplosion(row + 1, col);
            }
            if(col > 0 && Shot != "Bomber Shot")
            {                
                if(board[row + 1, col - 1].GetComponent<Tile>().mType == "Bomb" || board[row + 1, col - 1].GetComponent<Tile>().mType == "Barrel")            
                    explode(row + 1, col - 1, ""); 
                dealWithExplosion(row + 1, col - 1);
            }
            if(col < 5 && Shot != "Bomber Shot")
            {               
                if(board[row + 1, col + 1].GetComponent<Tile>().mType == "Bomb" || board[row + 1, col + 1].GetComponent<Tile>().mType == "Barrel")            
                    explode(row + 1, col + 1, ""); 
                dealWithExplosion(row + 1, col + 1);
            }
        }
        if(col > 0 && Shot != "Dragon Shot")
        {            
            if(board[row, col - 1].GetComponent<Tile>().mType == "Bomb" || board[row, col - 1].GetComponent<Tile>().mType == "Barrel")            
                explode(row, col - 1, "");
            dealWithExplosion(row, col - 1);
        }
        if(col < 5 && Shot != "Dragon Shot")
        {          
            if(board[row, col + 1].GetComponent<Tile>().mType == "Bomb" || board[row, col + 1].GetComponent<Tile>().mType == "Barrel")            
                explode(row, col + 1, "");
            dealWithExplosion(row, col + 1);
        }
    }

    public void dealWithExplosion(int row, int col)
    {
        if(board[row, col].gameObject != null)
        {  
            if(board[row, col].GetComponent<Tile>().mType == "Goblin" && board[row, col].GetComponent<Tile>().boss == "")
            {
                board[row, col].transform.GetChild(8).gameObject.SetActive(false);
                board[row, col].transform.GetChild(9).gameObject.SetActive(true);
                board[row, col].transform.GetChild(9).GetComponent<Animator>().SetTrigger("Explode");
                if(board[row, col].GetComponent<Enemy>().health > characterControl.getAttack() + 2)
                {
                    board[row, col].GetComponent<Enemy>().health -= (int)(characterControl.getAttack() + 2);
                    return;
                }
            }
            else if (board[row, col].GetComponent<Tile>().boss != "")
            {
                if(board[row, col].GetComponent<Tile>().boss == "BossArms" || board[row, col].GetComponent<Tile>().boss == "BossBody")
                {
                    return;
                }
                board[row, col].transform.GetChild(8).GetComponent<Animator>().SetTrigger("Explode");
                if(board[row, col].GetComponent<Enemy>().health > characterControl.getAttack() + 2)
                {
                    board[row, col].GetComponent<Enemy>().health -= (int)(characterControl.getAttack() + 2);
                    return;
                }
                else
                {   //Kill Boss
                    spawnChest = true;
                    GameControl.miniBossUp = false;
                    spawnScoreAddition(0, 0, "Boss");
                }
            }
            else if (board[row, col].GetComponent<Tile>().mType == "Collected")
            {
                
            }
            else
            {               
                if(board[row, col].transform.GetChild(8) != null)
                    board[row, col].transform.GetChild(8).GetComponent<Animator>().SetTrigger("Explode");
            }           
            board[row, col].GetComponent<Tile>().mType = "Collected";
        }
    }

    public void chaosStone()
    {
        int r = UnityEngine.Random.Range(0, 6);
        int c = UnityEngine.Random.Range(0, 6);
        for(int i = 0; i < 6; i++)
        {
            for(int j = 0; j < 6; j++)
            {
                if(board[i,j].GetComponent<Tile>().mType != "goblin")
                {
                    if(r == i && c == j)
                    {
                        board[i,j].GetComponent<Tile>().empowered = true;
                        board[i,j].transform.GetChild(8).gameObject.SetActive(false);
                        board[i,j].transform.GetChild(9).gameObject.SetActive(true);
                    }
                    else
                    {
                        board[i,j].GetComponent<Tile>().empowered = false;
                        board[i,j].transform.GetChild(8).gameObject.SetActive(true);
                        board[i,j].transform.GetChild(9).gameObject.SetActive(false);
                    }           
                }               
            }
        }
    }
    public void HexBall(GameObject temp)
    {
        int col = temp.GetComponent<Tile>().mCol;
        int row = temp.GetComponent<Tile>().mRow;
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if (i >= (row - 1) && i <= (row + 1))
                {
                    if (j >= (col - 1) && j <= (col + 1))
                    {
                        string type = board[i, j].GetComponent<Tile>().mType;
                        if (type == "Sword") //Empower
                        {
                            board[i, j].GetComponent<Tile>().empowered = true;
                            board[i, j].transform.GetChild(8).gameObject.SetActive(false);
                            board[i, j].transform.GetChild(9).gameObject.SetActive(true);
                        }
                        if (type == "Health" || type == "Mana" || type == "Rubble" || type == "Coin") //Collect
                        {
                            board[i, j].GetComponent<Tile>().mType = "Collected";
                            if (type == "Health")
                            {
                                characterControl.setCurrentHealth(characterControl.getCurrentHealth() + 1);
                            }
                            else if (type == "Mana")
                            {
                                if (characterControl.searchSkillExists(spellsOnCD[i].transform.name))
                                {
                                    if (spellsOnCD[i].GetComponent<Spell>().coolDown - 1 < 0)
                                        spellsOnCD[i].GetComponent<Spell>().turnEnd = turnCounter;
                                    else
                                        spellsOnCD[i].GetComponent<Spell>().turnEnd -= 1;
                                }
                            }
                            else if (type == "Coin")
                            {
                                GameControl.gold++;
                            }
                        }
                        if(type == "Goblin")
                        {
                            board[i, j].GetComponent<Enemy>().cursed = true;
                        }
                        if (type == "Bomb" || type == "Barrel") //Expand Hex Ball
                        {
                            board[i, j].GetComponent<Tile>().mType = "Collected";
                            if (row != i && col != j)
                                HexBall(board[i, j]);                        
                        }
                    }
                }
            }
        }
    }
    public void discipline() //Turn Rubble into Swords
    {
        for(int i = 0; i < 6; i++)
        {
            for(int j = 0; j < 6; j++)
            {
                GameObject obj = board[i, j];
                if (obj.GetComponent<Tile>().mType == "Rubble")
                {
                    Destroy(obj.gameObject);
                    Vector3 pos = obj.transform.position;
                    board[i,j] = (GameObject)Instantiate(swords[0], pos, Quaternion.identity);
                    board[i, j].GetComponent<Tile>().mType = "Sword";
                    board[i, j].GetComponent<Tile>().mCol = j;
                    board[i, j].GetComponent<Tile>().mRow = i;
                }
            }
        }
    }
    public void appraise() //Turn rubble into gold
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                GameObject obj = board[i, j];
                if (obj.GetComponent<Tile>().mType == "Rubble")
                {
                    Destroy(obj.gameObject);
                    Vector3 pos = obj.transform.position;
                    board[i, j] = (GameObject)Instantiate(coins[0], pos, Quaternion.identity);
                    board[i, j].GetComponent<Tile>().mType = "Coin";
                    board[i, j].GetComponent<Tile>().mCol = j;
                    board[i, j].GetComponent<Tile>().mRow = i;
                }
            }
        }
    }
    public void crystalmancy() //Turn Rubble into mana
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                GameObject obj = board[i, j];
                if (obj.GetComponent<Tile>().mType == "Rubble")
                {
                    Destroy(obj.gameObject);
                    Vector3 pos = obj.transform.position;
                    board[i, j] = (GameObject)Instantiate(manaCrystals[0], pos, Quaternion.identity);
                    board[i, j].GetComponent<Tile>().mType = "Mana";
                    board[i, j].GetComponent<Tile>().mCol = j;
                    board[i, j].GetComponent<Tile>().mRow = i;
                }
            }
        }
    }
    public bool checkIfPickPocket()
    {
        if (characterControl.searchActiveCharacterTraits("Pickpocket"))
        {
            bool hasCoin = false;
            bool hasSword = false;
            GameObject[] tempArray = new GameObject[collected.Count];
            collected.CopyTo(tempArray, 0);
            for (int i = 0; i < collected.Count; i++)
            {
                string tempType = tempArray[i].GetComponent<Tile>().mType;
                if (tempType == "Coin")
                    hasCoin = true;
                if (tempType == "Sword" || tempType == "Goblin" || tempType == "Barrel")
                    hasSword = true;
            }
            if (hasCoin && hasSword)
            {
                return true;
            }
            else
                return false;
        }
        else
            return false;
    }
    public void ambush() //Turn Rubble into Goblins
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                GameObject obj = board[i, j];
                if (obj.GetComponent<Tile>().mType == "Rubble")
                {
                    Destroy(obj.gameObject);
                    Vector3 pos = obj.transform.position;
                    board[i, j] = (GameObject)Instantiate(goblins[0], pos, Quaternion.identity);
                    board[i, j].GetComponent<Tile>().mType = "Goblin";
                    board[i, j].GetComponent<Tile>().mCol = j;
                    board[i, j].GetComponent<Tile>().mRow = i;
                    board[i, j].GetComponent<Enemy>().health = goblinHealthConstant + (goblinScalar * 2);
                    board[i, j].GetComponent<Enemy>().damage = goblinDamageConstant + goblinScalar;
                }
            }
        }
    }
    public void bloodshot() //Turn Health and Mana into Swords
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                GameObject obj = board[i, j];
                if (obj.GetComponent<Tile>().mType == "Health" || obj.GetComponent<Tile>().mType == "Mana")
                {
                    Destroy(obj.gameObject);
                    Vector3 pos = obj.transform.position;
                    board[i, j] = (GameObject)Instantiate(swords[0], pos, Quaternion.identity);
                    board[i, j].GetComponent<Tile>().mType = "Sword";
                    board[i, j].GetComponent<Tile>().mCol = j;
                    board[i, j].GetComponent<Tile>().mRow = i;
                }              
            }
        }
    }
    public void gather() //Turn Rubble into Health
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                GameObject obj = board[i, j];
                if (obj.GetComponent<Tile>().mType == "Rubble")
                {
                    Destroy(obj.gameObject);
                    Vector3 pos = obj.transform.position;
                    board[i, j] = (GameObject)Instantiate(healthCrystals[0], pos, Quaternion.identity);
                    board[i, j].GetComponent<Tile>().mType = "Health";
                    board[i, j].GetComponent<Tile>().mCol = j;
                    board[i, j].GetComponent<Tile>().mRow = i;
                }
            }
        }
    }
    public void Balance() //Turn swords into Health
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                GameObject obj = board[i, j];
                if (obj.GetComponent<Tile>().mType == "Sword")
                {
                    Destroy(obj.gameObject);
                    Vector3 pos = obj.transform.position;
                    board[i, j] = (GameObject)Instantiate(healthCrystals[0], pos, Quaternion.identity);
                    board[i, j].GetComponent<Tile>().mType = "Health";
                    board[i, j].GetComponent<Tile>().mCol = j;
                    board[i, j].GetComponent<Tile>().mRow = i;
                }
            }
        }
    }
    public void freeze() 
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                GameObject obj = board[i, j];
                if (obj.GetComponent<Tile>().mType == "Goblin")
                {
                    obj.GetComponent<Enemy>().frozen = true;
                    obj.gameObject.transform.GetChild(18).gameObject.SetActive(true);
                }
            }
        }
    }
    public void elbowGrease() //collect all rubble
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                GameObject obj = board[i, j];
                if (obj.GetComponent<Tile>().mType == "Rubble")
                {
                    Destroy(obj.gameObject);
                    collected.Push(board[i, j]);
                    board[i, j].GetComponent<Tile>().mType = "Collected";                 
                    board[i, j].GetComponent<Tile>().mCol = j;
                    board[i, j].GetComponent<Tile>().mRow = i;
                }
            }
        }
        StartCoroutine(collect());
    }
    public void mend() //collect all health
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                GameObject obj = board[i, j];
                if (obj.GetComponent<Tile>().mType == "Health")
                {
                    Destroy(obj.gameObject);
                    collected.Push(board[i, j]);
                    board[i, j].GetComponent<Tile>().mType = "Collected";
                    board[i, j].GetComponent<Tile>().mCol = j;
                    board[i, j].GetComponent<Tile>().mRow = i;
                }
            }
        }
        StartCoroutine(collect());
    }
    public void goldRush() //Collect all gold
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                GameObject obj = board[i, j];
                if (obj.GetComponent<Tile>().mType == "Coin")
                {
                    Destroy(obj.gameObject);
                    collected.Push(board[i, j]);
                    board[i, j].GetComponent<Tile>().mType = "Collected";
                    board[i, j].GetComponent<Tile>().mCol = j;
                    board[i, j].GetComponent<Tile>().mRow = i;
                }
            }
        }
        StartCoroutine(collect());
    }
    public void alchemy() //Swap health and mana crystals
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                GameObject obj = board[i, j];
                if (obj.GetComponent<Tile>().mType == "Health")
                {
                    Destroy(obj.gameObject);
                    Vector3 pos = obj.transform.position;
                    board[i, j] = (GameObject)Instantiate(manaCrystals[0], pos, Quaternion.identity);
                    board[i, j].GetComponent<Tile>().mType = "Mana";
                    board[i, j].GetComponent<Tile>().mCol = j;
                    board[i, j].GetComponent<Tile>().mRow = i;
                }
                if (obj.GetComponent<Tile>().mType == "Mana")
                {
                    Destroy(obj.gameObject);
                    Vector3 pos = obj.transform.position;
                    board[i, j] = (GameObject)Instantiate(healthCrystals[0], pos, Quaternion.identity);
                    board[i, j].GetComponent<Tile>().mType = "Health";
                    board[i, j].GetComponent<Tile>().mCol = j;
                    board[i, j].GetComponent<Tile>().mRow = i;
                }
            }
        }
    }
    void search()
    {
        for(int i = 0; i < 6; i++)
        {
            for(int j = 0; j < 6; j++)
            {
                string type = board[i, j].GetComponent<Tile>().mType;
                if (type != "Shopkeeper" && type != "Chest" && type != "Artifact" && type != "Goblin")
                {
                    Vector3 pos = board[i, j].gameObject.transform.position;
                    Destroy(board[i, j].gameObject);
                    board[i, j] = (GameObject)Instantiate(chest, pos, Quaternion.identity);
                    board[i, j].GetComponent<Tile>().mType = "Chest";
                    board[i, j].GetComponent<Tile>().mCol = j;
                    board[i, j].GetComponent<Tile>().mRow = i;
                    return;
                }
            }
        }      
    }
    void fillBoard()
    {
        temp = 0;
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                float X = 0.515F + j + (j * 0.1F);
                float Y = 0.5F - i;// - (i * spacing);
                Vector3 position = new Vector3(X, Y, 0.0F);

                temp = UnityEngine.Random.Range(1, 100);
                //temp = 20;
                if (temp <= 30)
                {                     
                    temp = UnityEngine.Random.Range(1, 4);
                    //temp = 2;
                    if (temp == 1)
                        board[i, j] = (GameObject)Instantiate(healthCrystals[0], position, Quaternion.identity);
                    else if (temp == 2)
                        board[i, j] = (GameObject)Instantiate(healthCrystals[1], position, Quaternion.identity);
                    else if (temp == 3)
                        board[i, j] = (GameObject)Instantiate(healthCrystals[2], position, Quaternion.identity);
                    board[i, j].GetComponent<Tile>().mType = "Health";                                   
                }
                else if (temp <= 60)
                {
                    temp = UnityEngine.Random.Range(1, 4);
                    if (temp == 1)
                        board[i, j] = (GameObject)Instantiate(coins[0], position, Quaternion.identity);
                    else if (temp == 2)
                        board[i, j] = (GameObject)Instantiate(coins[1], position, Quaternion.identity);
                    else if (temp == 3)
                        board[i, j] = (GameObject)Instantiate(coins[2], position, Quaternion.identity);                    
                    board[i, j].GetComponent<Tile>().mType = "Coin";                   
                }
                else if (temp <= 75)
                {
                    temp = UnityEngine.Random.Range(1, 4);
                    if (temp == 1)
                        board[i, j] = (GameObject)Instantiate(swords[0], position, Quaternion.identity);
                    else if (temp == 2)
                        board[i, j] = (GameObject)Instantiate(swords[1], position, Quaternion.identity);
                    else if (temp == 3)
                        board[i, j] = (GameObject)Instantiate(swords[2], position, Quaternion.identity);
                    board[i, j].GetComponent<Tile>().mType = "Sword";
                    board[i, j].transform.GetChild(8).GetComponent<SpriteRenderer>().sprite = characterControl.getWeaponIcon().GetComponent<SpriteRenderer>().sprite;
                }                             
                else if (temp <= 99)
                {                  
                    board[i, j] = (GameObject)Instantiate(rubble, position, Quaternion.identity);
                    board[i, j].GetComponent<Tile>().mType = "Rubble";                  
                }
                else if(temp == 101)
                {
                    board[i, j] = (GameObject)Instantiate(goblins[3], position, Quaternion.identity);
                    board[i, j].GetComponent<Tile>().mType = "Goblin";
                    board[i, j].GetComponent<Enemy>().health = goblinHealthConstant + (goblinScalar * 2);
                    board[i, j].GetComponent<Enemy>().damage = goblinDamageConstant + goblinScalar;
                    board[i, j].GetComponent<Enemy>().justSpawned = true;
                }
                else
                {
                    temp = UnityEngine.Random.Range(1, 4);
                    if (temp == 1)
                        board[i, j] = (GameObject)Instantiate(manaCrystals[0], position, Quaternion.identity);
                    else if (temp == 2)
                        board[i, j] = (GameObject)Instantiate(manaCrystals[1], position, Quaternion.identity);
                    else if (temp == 3)
                        board[i, j] = (GameObject)Instantiate(manaCrystals[2], position, Quaternion.identity);
                    board[i, j].GetComponent<Tile>().mType = "Mana";
                }
                board[i, j].GetComponent<Tile>().mCol = j;
                board[i, j].GetComponent<Tile>().mRow = i;
            }
        }
    }
    void reverseBoard()
    {
        for(int i  = 0; i < collected.Count; i++)
        {
            GameObject obj = collected.Pop();
            Destroy(board[obj.GetComponent<Tile>().mRow, obj.GetComponent<Tile>().mCol]);
            board[obj.GetComponent<Tile>().mRow, obj.GetComponent<Tile>().mCol] = obj;
            obj.SetActive(true);         
        }
    }    
    private int numOfTilesInBoard(string type)
    {
        int numOfTiles = 0;
        for(int i = 0; i < 6; i++)
        {
            for(int j = 0; j < 6; j++)
            {
                if(board[i,j].GetComponent<Tile>().mType == type)
                    numOfTiles++;
            }
        }
        return numOfTiles;
    }
    private int numOfTilesInBoard(string type, string boss)
    {
        int numOfTiles = 0;
        for(int i = 0; i < 6; i++)
        {
            for(int j = 0; j < 6; j++)
            {
                if(board[i,j].GetComponent<Tile>().mType == type && board[i,j].GetComponent<Tile>().boss == boss)
                    numOfTiles++;
            }
        }
        return numOfTiles;
    }
    private GameObject[] groupUpBossBody()
    {     
        GameObject[] numTiles = new GameObject[numOfTilesInBoard("Goblin", "BossBody")];
        int bodyIndex = 0;
        for(int i = 0; i < 6; i++)
        {
            for(int j = 0; j < 6; j++)
            {
                if(board[i,j].GetComponent<Tile>().boss == "BossBody")
                {
                    numTiles[bodyIndex] = board[i,j];
                    bodyIndex++;
                }                
            }
        }
        return numTiles;
    }
    public IEnumerator takeDamage()
    {
        //bool animationTriggered = false;
        if (checkGoblin())
        {
            int enemyDamage = 0;
            int shrapnelRand = UnityEngine.Random.Range(1, 101);
            int armor = (int)characterControl.getArmor();
            
            if (GameControl.vanish) //Vanish Function 
                GameControl.vanish = false;         
            else
            {
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        GameObject temp = board[i, j];
                        if (temp.GetComponent<Tile>().mType == "Goblin" || temp.GetComponent<Tile>().mType == "Ghost")
                        {                           
                            if(!temp.GetComponent<Enemy>().frozen && !temp.GetComponent<Enemy>().shouldntAttack && !temp.GetComponent<Enemy>().justFrozen && !temp.GetComponent<Enemy>().justSpawned)
                            {
                                temp.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
                                enemyDamage += temp.GetComponent<Enemy>().damage;
                                if (temp.GetComponent<Tile>().boss != "" && !temp.GetComponent<Enemy>().justSpawned && temp.GetComponent<Tile>().boss != "BossArms")
                                {
                                    //if(temp.GetComponent<Enemy>().damage <= armor)
                                    //{   //Blocked
                                       // armor -= temp.GetComponent<Enemy>().damage;
                                        //temp.transform.GetChild(8).GetComponent<Animator>().SetTrigger("blocked");
                                    //}
                                    //else if (armor > 0)
                                    //{   //Breaks Through Last Bit Of Armor
                                       // armor = 0;
                                        //temp.transform.GetChild(8).GetComponent<Animator>().SetTrigger("break");
                                    //}
                                    //else
                                    //{   //No Armor, Full attack
                                        temp.transform.GetChild(8).GetComponent<Animator>().SetTrigger("Attacking");
                                    //}
                                }                                                                          
                                else if(temp.GetComponent<Tile>().boss == "")
                                {              
                                    if(temp.GetComponent<Enemy>().damage <= armor)
                                    {   //Blocked
                                        armor -= temp.GetComponent<Enemy>().damage;
                                        temp.transform.GetChild(9).GetComponent<Animator>().SetTrigger("blocked");
                                    }
                                    else if (armor > 0)
                                    {   //Breaks Through Last Bit Of Armor
                                        armor = 0;
                                        temp.transform.GetChild(9).GetComponent<Animator>().SetTrigger("break");
                                    }
                                    else
                                    {   //No Armor, Full attack
                                        temp.transform.GetChild(9).GetComponent<Animator>().SetBool("attack", true);
                                    }                                                     
                                    if ((int)(enemyDamage / 2.5 - 1) >= 0 && (int)(enemyDamage / 2.5) <= 15)
                                    {
                                        if(!cam.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("CameraShake"))
                                        {
                                            cam.GetComponent<Animator>().SetTrigger("Shake");
                                        }                                        
                                        float tempX = UnityEngine.Random.Range(0, 8);
                                        float tempY = UnityEngine.Random.Range(-10, 0);
                                        float tempScale = UnityEngine.Random.Range(0.05f, 0.25f);
                                        int tempAnim = UnityEngine.Random.Range(1, 11);
                                        splats[i] = Instantiate(bloodSplat, new Vector3(tempX, tempY, -3.75f), Quaternion.identity);
                                        splats[i].transform.localScale = new Vector3(tempScale, tempScale, 1f);
                                        switch(tempAnim)
                                        {
                                            case 1:                                               
                                                splats[i].GetComponent<Animator>().SetTrigger("One");
                                                break;
                                            case 2:
                                                splats[i].GetComponent<Animator>().SetTrigger("Two");
                                                break;
                                            case 3:
                                                splats[i].GetComponent<Animator>().SetTrigger("Three");                                               
                                                break;
                                            case 4:
                                                splats[i].GetComponent<Animator>().SetTrigger("Four");
                                                break;
                                            case 5:
                                                splats[i].GetComponent<Animator>().SetTrigger("Five");
                                                break;
                                            case 6:
                                                splats[i].GetComponent<Animator>().SetTrigger("Six");
                                                break;
                                            case 7:
                                                splats[i].GetComponent<Animator>().SetTrigger("Seven");
                                                break;
                                            case 8:
                                                splats[i].GetComponent<Animator>().SetTrigger("Eight");
                                                break;
                                            case 9:
                                                splats[i].GetComponent<Animator>().SetTrigger("Nine");
                                                break;
                                            case 10:
                                                splats[i].GetComponent<Animator>().SetTrigger("Ten");
                                                break;
                                        }                                      
                                    }                                   
                                    yield return null;
                                    if (temp.GetComponent<Enemy>().cursed)
                                    {
                                        temp.GetComponent<Enemy>().cursed = false;
                                    }
                                }
                                if (GetComponent<Artifacts>().thorns)
                                {
                                    Debug.Log("Thorns");
                                    temp.GetComponent<Enemy>().health -= thornsValue; //Thorns Function
                                }                                   
                                if (characterControl.searchActiveCharacterTraits("Spiked")) //Spiked Function
                                    temp.GetComponent<Enemy>().health -= 1;
                                if (characterControl.searchTraitAll("Shrapnel") != -1)
                                {                                   
                                    if(shrapnelRand >= 80 && temp.GetComponent<Tile>().boss == "")
                                    {
                                        temp.GetComponent<Enemy>().health -= 1;
                                    }
                                }
                                if (temp.GetComponent<Enemy>().burning)
                                    temp.GetComponent<Enemy>().health -= 1;
                                if (temp.GetComponent<Enemy>().poisoned)
                                    temp.GetComponent<Enemy>().health -= 1;
                                if (temp.GetComponent<Enemy>().health <= 0)
                                {
                                    if (temp.GetComponent<Tile>().boss != "" && temp.GetComponent<Tile>().boss != "RatClone" && temp.GetComponent<Tile>().boss != "BossArms")
                                    {
                                        spawnChest = true;
                                        GameControl.miniBossUp = false;
                                        spawnScoreAddition(0, 0, "Boss");
                                    }
                                    //Character.currentExp++;
                                    board[temp.GetComponent<Tile>().mRow, temp.GetComponent<Tile>().mCol].GetComponent<Tile>().mType = "Collected";
                                    Destroy(temp.gameObject);
                                    shiftBoard();
                                }
                                else if (GetComponent<Artifacts>().isaacsBinding) //Isaac's Binding Function
                                {
                                    int effectChance = UnityEngine.Random.Range(1, 100);
                                    if (effectChance < frostValue)
                                    {
                                        temp.GetComponent<Enemy>().frozen = true;
                                        temp.transform.GetChild(18).gameObject.SetActive(true);
                                    }
                                }

                            }
                            if (temp.GetComponent<Enemy>().justFrozen)
                            {
                                temp.GetComponent<Enemy>().frozen = true;
                                temp.GetComponent<Enemy>().justFrozen = false;
                            }
                            else if (temp.GetComponent<Enemy>().frozen) //Frozen Function
                            {
                                temp.GetComponent<Enemy>().frozen = false;
                                temp.transform.GetChild(18).gameObject.SetActive(false);
                            }
                            else if (temp.GetComponent<Enemy>().justSpawned)
                            {
                                temp.GetComponent<Enemy>().justSpawned = false;
                                temp.GetComponent<Enemy>().shouldntAttack = true;
                            }
                            else if (temp.GetComponent<Enemy>().shouldntAttack)
                            {
                                temp.GetComponent<Enemy>().shouldntAttack = false;
                            }
                        }
                       
                    }
                }
                if (enemyDamage - characterControl.characters[characterControl.activeCharacter].armor.defense > 0)
                {                  
                        if(characterControl.getCurrentHealth() - (enemyDamage - characterControl.characters[characterControl.activeCharacter].armor.defense) > 0)
                        {
                            characterControl.setCurrentHealth(characterControl.getCurrentHealth() - (enemyDamage - characterControl.characters[characterControl.activeCharacter].armor.defense));
                        }
                        else
                        {
                            characterControl.setCurrentHealth(0);
                        }                  
                }
                else if (enemyDamage >= 1)
                {                   
                }
            }
            
        }
        else
            changeGoblin();
        bossAbilityUsed = false;       
    }

    bool checkGoblin()
    {
        for(int i = 0; i < 6; i++)
        {
            for(int j = 0; j < 6; j++)
            {
                if(board[i, j].GetComponent<Tile>().mType == "Ghost")
                    return true;
                else if (board[i, j].GetComponent<Tile>().mType == "Goblin")
                {
                    if (!board[i, j].GetComponent<Enemy>().justSpawned && !board[i, j].GetComponent<Enemy>().shouldntAttack)
                        return true;
                }                                        
            }            
        }
        return false;
    }
    bool checkGoblinExists()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if (board[i, j].GetComponent<Tile>().mType == "Goblin")
                {                   
                        return true;
                }
            }
        }
        return false;
    }

    bool checkFightingGoblinExists()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if (board[i, j].GetComponent<Tile>().mType == "Goblin" && !board[i, j].GetComponent<Enemy>().justSpawned &&!board[i, j].GetComponent<Enemy>().shouldntAttack)
                {                   
                        return true;
                }
            }
        }
        return false;
    }
    void changeGoblin()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if (board[i, j].GetComponent<Tile>().mType == "Goblin")
                {                  
                    if (board[i, j].GetComponent<Enemy>().justSpawned)
                    {
                        board[i, j].GetComponent<Enemy>().justSpawned = false;
                        board[i, j].GetComponent<Enemy>().shouldntAttack = true;
                    }
                    else if (board[i, j].GetComponent<Enemy>().shouldntAttack)
                    {
                        board[i, j].GetComponent<Enemy>().shouldntAttack = false;
                    }
                }
            }
        }
    }
    void transformIntoGoblin( int i, int j)
    {
        GameObject obj = board[i, j];
        Destroy(obj.gameObject);
        Vector3 pos = obj.transform.position;
        board[i, j] = (GameObject)Instantiate(goblins[0], pos, Quaternion.identity);
        board[i, j].GetComponent<Tile>().mType = "Goblin";
        board[i, j].GetComponent<Tile>().mCol = j;
        board[i, j].GetComponent<Tile>().mRow = i;
        board[i, j].GetComponent<Enemy>().health = goblinHealthConstant + (goblinScalar * 2);
        board[i, j].GetComponent<Enemy>().damage = goblinDamageConstant + goblinScalar;
    }
    void transformIntoCoin(int i, int j)
    {
        GameObject obj = board[i, j];
        Destroy(obj.gameObject);
        Vector3 pos = obj.transform.position;
        board[i, j] = (GameObject)Instantiate(coins[0], pos, Quaternion.identity);
        board[i, j].GetComponent<Tile>().mType = "Coin";
        board[i, j].GetComponent<Tile>().mCol = j;
        board[i, j].GetComponent<Tile>().mRow = i;      
        
    }

    void vortex()
    {
        int[] columns = new int[6];
        int[] rows = new int[6];

        for (int i = 0; i < 6; i++)
        {
            if (i == 0)
            {
                rows[0] = UnityEngine.Random.Range(0, 6);
                columns[0] = UnityEngine.Random.Range(0, 6);
            }
            else if (i == 1)
            {
                rows[1] = UnityEngine.Random.Range(0, 6);
                while (rows[1] == rows[0])
                {
                    rows[1] = UnityEngine.Random.Range(0, 6);
                }
                columns[1] = UnityEngine.Random.Range(0, 6);
                while (columns[1] == columns[0])
                {
                    columns[1] = UnityEngine.Random.Range(0, 6);
                }
            }
            else if (i == 2)
            {
                rows[2] = UnityEngine.Random.Range(0, 6);
                while (rows[2] == rows[0] || rows[2] == rows[1])
                {
                    rows[2] = UnityEngine.Random.Range(0, 6);
                }
                columns[2] = UnityEngine.Random.Range(0, 6);
                while (columns[2] == columns[0] || columns[2] == columns[1])
                {
                    columns[2] = UnityEngine.Random.Range(0, 6);
                }
            }
            else if (i == 3)
            {
                rows[3] = UnityEngine.Random.Range(0, 6);
                while (rows[3] == rows[0] || rows[3] == rows[1] || rows[3] == rows[2])
                {
                    rows[3] = UnityEngine.Random.Range(0, 6);
                }
                columns[3] = UnityEngine.Random.Range(0, 6);
                while (columns[3] == columns[0] || columns[3] == columns[1] || columns[3] == columns[2])
                {
                    columns[3] = UnityEngine.Random.Range(0, 6);
                }
            }
            else if (i == 4)
            {
                rows[4] = UnityEngine.Random.Range(0, 6);
                while (rows[4] == rows[0] || rows[4] == rows[1] || rows[4] == rows[2] || rows[4] == rows[3])
                {
                    rows[4] = UnityEngine.Random.Range(0, 6);
                }
                columns[4] = UnityEngine.Random.Range(0, 6);
                while (columns[4] == columns[0] || columns[4] == columns[1] || columns[4] == columns[2] || columns[4] == columns[3])
                {
                    columns[4] = UnityEngine.Random.Range(0, 6);
                }
            }
            else if (i == 5)
            {
                rows[5] = UnityEngine.Random.Range(0, 6);
                while (rows[5] == rows[0] || rows[5] == rows[1] || rows[5] == rows[2] || rows[5] == rows[3] || rows[5] == rows[4])
                {
                    rows[5] = UnityEngine.Random.Range(0, 6);
                }
                columns[5] = UnityEngine.Random.Range(0, 6);
                while (columns[5] == columns[0] || columns[5] == columns[1] || columns[5] == columns[2] || columns[5] == columns[3] || columns[5] == columns[4])
                {
                    columns[5] = UnityEngine.Random.Range(0, 6);
                }
            }
        }

        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                float X = 0.515F + rows[j] + (rows[j] * 0.1F);
                float Y = 2 - columns[i] - (columns[i] * spacing);
                Vector3 position = new Vector3(X, Y, 0.0F);
                tempBoard[rows[i], columns[j]] = board[i, j];
                tempBoard[rows[i], columns[j]].transform.position = position;
                tempBoard[rows[i], columns[j]].GetComponent<Tile>().mRow = rows[i];
                tempBoard[rows[i], columns[j]].GetComponent<Tile>().mCol = columns[j];
            }
        }
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                board[i, j] = tempBoard[i, j];
                board[i, j].transform.position = tempBoard[i, j].transform.position;
                board[i, j].GetComponent<Tile>().mRow = tempBoard[i, j].GetComponent<Tile>().mRow;
                board[i, j].GetComponent<Tile>().mCol = tempBoard[i, j].GetComponent<Tile>().mCol;
            }
        }
    }
  
    public void shiftBoard()
    {
        //Moves all of the Collected tiles to the top
        for (int i = 5; i > 0; i--) 
        {
            for(int j = 0; j < 6; j++)
            {
                GameObject obj = board[i, j];
                if (obj.GetComponent<Tile>().mType == "Collected")
                {
                    for(int k = i-1; k >= 0; k--)
                    {
                        GameObject nextObj = board[k, j];
                        if(nextObj.GetComponent<Tile>().mType != "Collected" && nextObj.GetComponent<Tile>().boss != "BossBody")
                        {
                            obj.GetComponent<Tile>().mRow = k;
                            nextObj.GetComponent<Tile>().mRow = i;
                            board[i, j] = nextObj;
                            board[k, j] = obj;
                            break;
                        }
                    }
                }
            }
        }
        int ratSpawning = 0;
        temp = 0;
        if (turnCounter % turnToScale == 0)
        {
            if (turnCounter == turnToScale)
                goblinScalar = 2;
            else if (turnCounter == turnToScale * 2)
                goblinScalar = 3;
            else if (turnCounter == turnToScale * 3)
                goblinScalar = 4;
            else if (turnCounter == turnToScale * 4)
                goblinScalar = 5;
            else if (turnCounter == turnToScale * 5)
                goblinScalar = 6;
            else if (turnCounter == turnToScale * 6)
                goblinScalar = 7;
            else if (turnCounter == turnToScale * 7)
                goblinScalar = 8;
        }
        //bossSpawner = 1;
        for (int i = 0; i < 6; i++)
        {
            for(int j = 0; j < 6; j++)
            {
                GameObject obj = board[i, j];
                if(obj.GetComponent<Tile>().mType == "Collected")
                {
                    float X = 0.515F + j + (j * 0.1F);
                    float Y = 2 - i - (i * spacing);
                    Vector3 position = new Vector3(X, Y, 0.0F);
                   // if(board[i, j].GetComponent<Tile>().particle != null)
                   // {
                    //    Debug.Log("Explode");
                    //    board[i, j].GetComponent<Tile>().particle.Play();
                   // }
                        
                    Destroy(board[i, j].gameObject);
                    shopSpawner = -1;
                    //GameControl.gold = 100;
                    Debug.Log("Turn Counter: " + turnCounter + "  |  Boss Spawner: " + bossSpawner);
                    if (turnCounter == shopSpawner)
                    {
                        shopSpawner += UnityEngine.Random.Range(10, 16);
                        shopKeeperUp = true;
                        board[i, j] = (GameObject)Instantiate(shopkeeper, position, Quaternion.identity);
                        board[i, j].GetComponent<Tile>().mType = "Shopkeeper";
                    }
                    else if (turnCounter == bossSpawner) 
                    //else if (turnCounter == 2 && !GameControl.miniBossUp)
                    {
                        bossSpawner += UnityEngine.Random.Range(10, 16);
                            if(GetComponent<Artifacts>().bait)
                                bossSpawner -= baitValue;
                        temp = UnityEngine.Random.Range(1, 7);
                        if (PlayerPrefs.GetString("Boss Stage") == "")
                            PlayerPrefs.SetString("Boss Stage", "Stage One");
                        Debug.Log(PlayerPrefs.GetString("Boss Stage"));
                        //temp = 1;
                        /*
                        if (ScoreControl.bossScore == 150)
                        {
                            temp = 7;
                        }
                        if(ScoreControl.bossScore == 250 && PlayerPrefs.GetString("Boss Stage") != "Stage One")
                        {
                            temp = 8;
                        }
                        if(ScoreControl.bossScore == 400 && PlayerPrefs.GetString("Boss Stage") == "Stage Three")
                        {
                            temp = 9;
                        }  */                                        
                        //temp = 5;
                        if(temp == 1)
                        {
                            board[i, j] = (GameObject)Instantiate(slime, position, Quaternion.identity);
                            board[i, j].GetComponent<Tile>().boss = "Slime";
                            board[i, j].GetComponent<Enemy>().health = 7 + (goblinScalar * 2);
                            board[i, j].GetComponent<Enemy>().damage = 2 + goblinScalar;
                            slimeNeedsToEat = false;
                        }                         
                        else if (temp == 2)
                        {
                            board[i, j] = (GameObject)Instantiate(blueGenie, position, Quaternion.identity);
                            board[i, j].GetComponent<Tile>().boss = "BlueGenie";
                            board[i, j].GetComponent<Enemy>().health = 3 + (goblinScalar * 2);
                            board[i, j].GetComponent<Enemy>().damage = 5 + goblinScalar;
                        }
                        else if (temp == 3)
                        {
                            board[i, j] = (GameObject)Instantiate(greenGenie, position, Quaternion.identity);
                            board[i, j].GetComponent<Tile>().boss = "GreenGenie";
                            board[i, j].GetComponent<Enemy>().health = 9 + (goblinScalar * 2);
                            board[i, j].GetComponent<Enemy>().damage = 1 + goblinScalar;
                        }
                        else if (temp == 4)
                        {
                            board[i, j] = (GameObject)Instantiate(ratLarge, position, Quaternion.identity);
                            board[i, j].GetComponent<Tile>().boss = "Rat";
                            board[i, j].GetComponent<Enemy>().health = 7 + (goblinScalar * 2);
                            board[i, j].GetComponent<Enemy>().damage = 2 + goblinScalar;
                            ratSpawning++;
                        }                           
                        else if (temp == 5)
                        {
                            board[i, j] = (GameObject)Instantiate(lich, position, Quaternion.identity);
                            board[i, j].GetComponent<Tile>().boss = "Lich";
                            board[i, j].GetComponent<Enemy>().health = 5 + (goblinScalar * 2);
                            board[i, j].GetComponent<Enemy>().damage = 2 + goblinScalar;
                        }                          
                        else if (temp == 6)
                        {
                            board[i, j] = (GameObject)Instantiate(skeleton, position, Quaternion.identity);
                            board[i, j].GetComponent<Tile>().boss = "Skull";
                            board[i, j].GetComponent<Enemy>().isSkull = true;
                            board[i, j].GetComponent<Enemy>().health = 7 + (goblinScalar * 2);
                            board[i, j].GetComponent<Enemy>().damage = 2 + goblinScalar;
                        }
                        else if (temp == 7)
                        {
                            GameControl.bossUp = true;
                            board[i, j] = (GameObject)Instantiate(healthCrystals[0], position, Quaternion.identity);
                            board[i, j].GetComponent<Tile>().mType = "Health";
                            spawnBossArms();
                        }
                        else if (temp == 8)
                        {
                            GameControl.bossUp = true;
                            board[i, j] = (GameObject)Instantiate(healthCrystals[0], position, Quaternion.identity);
                            board[i, j].GetComponent<Tile>().mType = "Health";
                            spawnBossBody();
                        }
                        else if (temp == 9)
                        {
                            GameControl.bossUp = true;
                            board[i, j] = (GameObject)Instantiate(healthCrystals[0], position, Quaternion.identity);
                            board[i, j].GetComponent<Tile>().mType = "Health";
                            spawnBossArms();
                            spawnBossBody();
                        }
                        
                        if(temp != 7 && temp != 8 && temp != 9)
                        {
                            board[i, j].GetComponent<Tile>().mType = "Goblin";
                            board[i, j].GetComponent<Enemy>().justSpawned = true;
                            GameControl.miniBossUp = true;
                        }                      
                    }                   
                    else if (spawnChest)
                    {
                        spawnChest = false;
                        board[i, j] = (GameObject)Instantiate(chest, position, Quaternion.identity);
                        board[i, j].GetComponent<Tile>().mType = "Chest";
                    }
                    else if (ratSpawning > 0 && ratSpawning < 3)
                    {
                        if (ratSpawning < 3)
                        {
                            board[i, j] = (GameObject)Instantiate(ratSmall, position, Quaternion.identity);
                            board[i, j].GetComponent<Tile>().boss = "RatClone";
                            ratSpawning++;
                            board[i, j].GetComponent<Tile>().mType = "Goblin";
                            board[i, j].GetComponent<Enemy>().health = 4 + (goblinScalar * 2);
                            board[i, j].GetComponent<Enemy>().damage = 1 + goblinScalar;
                        }
                        else
                            ratSpawning = 0;                      
                    }
                    else
                    {
                        if(GetComponent<Artifacts>().stoneShell || GetComponent<Artifacts>().bombBag) //Stone Shell Function
                        {
                            if(GetComponent<Artifacts>().loadedDie)
                                temp = UnityEngine.Random.Range(1, 120);
                            else
                                temp = UnityEngine.Random.Range(1, 105);
                        }
                        else
                            temp = UnityEngine.Random.Range(1, 100);
                        if (temp <= 25)
                        {                           
                            temp = UnityEngine.Random.Range(1, 25);
                            
                            if (temp <= 5)
                            {
                                board[i, j] = (GameObject)Instantiate(healthCrystals[0], position, Quaternion.identity);
                                board[i, j].GetComponent<Tile>().mType = "Health";
                            }                              
                            else if (temp <= 10)
                            {
                                board[i, j] = (GameObject)Instantiate(healthCrystals[1], position, Quaternion.identity);
                                board[i, j].GetComponent<Tile>().mType = "Health";
                            }                               
                            else if (temp <= 15)
                            {
                                board[i, j] = (GameObject)Instantiate(healthCrystals[2], position, Quaternion.identity);
                                board[i, j].GetComponent<Tile>().mType = "Health";
                            }                                                              
                            else if (temp <= 25)
                            {
                                if(spawnManaCrystals)
                                {
                                    temp = UnityEngine.Random.Range(1, 4);
                                    if (temp == 1)
                                        board[i, j] = (GameObject)Instantiate(manaCrystals[0], position, Quaternion.identity);
                                    else if (temp == 2)
                                        board[i, j] = (GameObject)Instantiate(manaCrystals[1], position, Quaternion.identity);
                                    else if (temp == 3)
                                        board[i, j] = (GameObject)Instantiate(manaCrystals[2], position, Quaternion.identity);
                                    board[i, j].GetComponent<Tile>().mType = "Mana";
                                }                                   
                                else
                                {
                                    board[i, j] = (GameObject)Instantiate(healthCrystals[1], position, Quaternion.identity);
                                    board[i, j].GetComponent<Tile>().mType = "Health";
                                }                                   
                            }
                            
                        }
                        else if (temp <= 50)
                        {
                            temp = UnityEngine.Random.Range(1, 4);
                            if (temp == 1)
                                board[i, j] = (GameObject)Instantiate(coins[0], position, Quaternion.identity);
                            else if (temp == 2)
                                board[i, j] = (GameObject)Instantiate(coins[1], position, Quaternion.identity);
                            else if (temp == 3)
                                board[i, j] = (GameObject)Instantiate(coins[2], position, Quaternion.identity);
                            board[i, j].GetComponent<Tile>().mType = "Coin";                                                    
                        }
                        else if (temp <= 65)
                        {
                            temp = UnityEngine.Random.Range(1, 4);
                            if (temp == 1)
                                board[i, j] = (GameObject)Instantiate(swords[0], position, Quaternion.identity);
                            else if (temp == 2)
                                board[i, j] = (GameObject)Instantiate(swords[1], position, Quaternion.identity);
                            else if (temp == 3)
                                board[i, j] = (GameObject)Instantiate(swords[2], position, Quaternion.identity);
                            board[i, j].GetComponent<Tile>().mType = "Sword";
                            board[i, j].transform.GetChild(8).GetComponent<SpriteRenderer>().sprite = characterControl.getWeaponIcon().GetComponent<SpriteRenderer>().sprite;
                        }
                        else if (temp <= 90)
                        {                         
                            temp = UnityEngine.Random.Range(1, 5);
                            temp = 1;
                            if (temp == 1)
                                board[i, j] = (GameObject)Instantiate(goblins[0], position, Quaternion.identity);
                            else if (temp == 2)
                                board[i, j] = (GameObject)Instantiate(goblins[1], position, Quaternion.identity);
                            else if (temp == 3)
                                board[i, j] = (GameObject)Instantiate(goblins[2], position, Quaternion.identity);
                            else if (temp == 4)
                                board[i, j] = (GameObject)Instantiate(goblins[3], position, Quaternion.identity);                            
                                
                            board[i, j].GetComponent<Tile>().mType = "Goblin";
                            board[i, j].GetComponent<Enemy>().health = UnityEngine.Random.Range(2, 5) + (goblinScalar * 2);
                            board[i, j].GetComponent<Enemy>().damage = UnityEngine.Random.Range(0, 2) + goblinScalar;
                            board[i, j].GetComponent<Enemy>().justSpawned = true;
                        }
                        else if (temp <= 100)
                        {
                            board[i, j] = (GameObject)Instantiate(rubble, position, Quaternion.identity);
                            board[i, j].GetComponent<Tile>().mType = "Rubble";
                        }
                        else // Stone Shell Function
                        {
                            if(GetComponent<Artifacts>().stoneShell && GetComponent<Artifacts>().bombBag)
                            {
                                int flip = UnityEngine.Random.Range(1, 3);
                                if(flip == 1)
                                {
                                    board[i, j] = (GameObject)Instantiate(helix, position, Quaternion.identity);
                                    board[i, j].GetComponent<Tile>().mType = "Helix";
                                }
                                else
                                {
                                    board[i, j] = (GameObject)Instantiate(bomb, position, Quaternion.identity);
                                    board[i, j].GetComponent<Tile>().mType = "Bomb";
                                }
                            }
                            else if(GetComponent<Artifacts>().stoneShell)
                            {
                                board[i, j] = (GameObject)Instantiate(helix, position, Quaternion.identity);
                                board[i, j].GetComponent<Tile>().mType = "Helix";
                            }
                            else if (GetComponent<Artifacts>().bombBag)
                            {
                                board[i, j] = (GameObject)Instantiate(bomb, position, Quaternion.identity);
                                board[i, j].GetComponent<Tile>().mType = "Bomb";
                            }
                        }
                    }
                    board[i, j].GetComponent<Tile>().mCol = j;
                    board[i, j].GetComponent<Tile>().mRow = i; 
                }
            }
        }      
    }

    public void spawnBossArms()
    {
        replaceTile(0, 0, bossArms, "BossArms", 3, 15);
        replaceTile(0, 5, bossArms, "BossArms", 3, 15);
        replaceTile(5, 0, bossArms, "BossArms", 3, 15);
        replaceTile(5, 5, bossArms, "BossArms", 3, 15);
    }

    private void spawnBossBody()
    {
        replaceTile(1, 2, bossBody, "BossBody", 0, 20);
        replaceTile(1, 3, bossBody, "BossBody", 0, 20);
        replaceTile(2, 2, bossBody, "BossBody", 0, 20);
        replaceTile(2, 3, bossBody, "BossBody", 0, 20);
        replaceTile(3, 2, bossBody, "BossBody", 0, 20);
        replaceTile(3, 3, bossBody, "BossBody", 0, 20);
    }
    private void replaceTile(int row, int col, GameObject obj)
    {
        GameObject tempObj = board[row, col];
        Vector3 pos = tempObj.transform.position;
        Destroy(tempObj.gameObject);
        board[row, col] = (GameObject)Instantiate(obj, pos, Quaternion.identity);            
        board[row, col].GetComponent<Tile>().mRow = row;
        board[row, col].GetComponent<Tile>().mCol = col;
    }

    private void replaceTile(int row, int col, GameObject obj, string boss, int damage, int health)
    {
        GameObject tempObj = board[row, col];
        Vector3 pos = tempObj.transform.position;
        Destroy(tempObj.gameObject);
        board[row, col] = (GameObject)Instantiate(obj, pos, Quaternion.identity);            
        board[row, col].GetComponent<Tile>().mRow = row;
        board[row, col].GetComponent<Tile>().mCol = col;
        board[row, col].GetComponent<Tile>().mType = "Goblin";
        board[row, col].GetComponent<Tile>().boss = boss;
        board[row, col].GetComponent<Enemy>().damage = damage;
        board[row, col].GetComponent<Enemy>().health = health;
        board[row, col].GetComponent<Enemy>().shouldntAttack = true;
    }
}

