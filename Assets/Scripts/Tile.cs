using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{
    public GameObject gameScript;
    public CharacterControl characterControl;
    public ParticleSystem particle;
    public ParticleSystem particle2;

    public static int numCollectedG; //Number of Coins Collected
    public static int numCollectedH; //Number of Health Cystals Collected
    public static double amountCollectedG; //Total value of coins collected
    public static double amountCollectedH; //Total value of Health collected
    public int mRow = 0;
    public int mCol = 0;
    public string mType = "";
    public string mName = "";
    public bool collected = false;
    public bool empowered = false;
    public string boss = "";
    public float spacing = 0.5F;
    public bool frozen = false;
    public bool frozenLastTurn = false;
    public bool isTrinket = false;
    public int bombTimer = 5;
    public string lastAbilityUsed = "";
    bool waiting = false;

    private void Awake()
    {
        gameScript = GameObject.Find("GameScript");
        characterControl = GameObject.Find("CharacterControl").GetComponent<CharacterControl>();
        bombTimer = UnityEngine.Random.Range(2, 5);
    }
    void FixedUpdate()
    {
        float X = 0;
        float Y = 0;
        float Z = 0;
        if( this.boss == "BossBody")
             Z = -1;
        if (collected)
        {
            if (this.mType == "Health" || this.mType == "Mana")
            {            
                X = 3F;
                Y = -9F;
                Z = -5F;
            }
            else if (this.mType == "Coin")
            {
                X = 5.3F;
                Y = -9F;
                Z = -5F;
            }
            else
            {
                X = 3.22F;
                Y = -9.41F;
                Z = -3F;
            }          
        }       
        else
        {
            X = 0.515F + this.mCol + (this.mCol * 0.1f);
            Y = -1.7F - this.mRow - (this.mRow * 0.1f);
        }
        Vector3 endPos = new Vector3(X, Y, Z);
        Vector3 currentPos = this.gameObject.transform.position;             
        
        if (collected && !waiting)
        {
            this.transform.GetChild(10).gameObject.SetActive(false);
            Vector3 temp = transform.position;
            temp.z = -5.0f;
            transform.position = temp;

            if (this.gameObject.GetComponent<Tile>().mType == "Health")
            {
                if (characterControl.searchTraitAll("Empath") == -1)
                {
                    transform.position = Vector3.Lerp(currentPos, endPos, .07f);
                    transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0.8f, 0.8f, 1), .07f);
                }
                else
                {
                    if (numCollectedH != 0)
                    {
                        if ((characterControl.getCurrentHealth() + (amountCollectedH / numCollectedH)) > characterControl.getMaxHealth())
                        {
                            if (characterControl.GetComponent<CharacterControl>().searchActiveCharacterTraits("Empath")) // Empath function
                            {
                                if (characterControl.getCurrentHealth() == characterControl.getMaxHealth())
                                {
                                    if (characterControl.characters[0].currentHealth < characterControl.characters[0].maxHealth && characterControl.characters[0].currentHealth > 0)
                                    {
                                        endPos = new Vector3(0.41F, -9.23F, -5F);                                      
                                        characterControl.characters[0].currentHealth += (amountCollectedH / numCollectedH);
                                    }
                                    else if (characterControl.characters[1].currentHealth < characterControl.characters[1].maxHealth && characterControl.characters[1].currentHealth > 0)
                                    {
                                        endPos = new Vector3(1.4F, -9.23F, -5F);                                      
                                        characterControl.characters[1].currentHealth += (amountCollectedH / numCollectedH);
                                    }
                                    else if (characterControl.characters[2].currentHealth < characterControl.characters[2].maxHealth && characterControl.characters[2].currentHealth > 0)
                                    {
                                        endPos = new Vector3(2.33F, -9.23F, -5F);                                       
                                        characterControl.characters[2].currentHealth += (amountCollectedH / numCollectedH);
                                    }
                                }
                            }
                        }
                        transform.position = Vector3.Lerp(currentPos, endPos, .07f);
                        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0.8f, 0.8f, 1), .07f);
                    }
                }
            }
            else
            {
                transform.position = Vector3.Lerp(currentPos, endPos, .07f);
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0.8f, 0.8f, 1), .07f);
            }

            if(currentPos.x - endPos.x <= 0.15 && currentPos.y - endPos.y <= 0.15)
            {
                
                if(this.gameObject.GetComponent<Tile>().mType == "Coin")
                {
                    gameScript.GetComponent<GameScript>().counter++;
                    if (numCollectedG != 0)
                    {                      
                        GameControl.gold += (amountCollectedG / numCollectedG);                     
                    }                   
                    if (gameScript.GetComponent<GameScript>().counter >= numCollectedG)
                    {
                        amountCollectedG = 0;
                        numCollectedG = 0;
                    }
                }
                if(this.gameObject.GetComponent<Tile>().mType == "Health")
                {
                    gameScript.GetComponent<GameScript>().counter++;
                    if (numCollectedH != 0)
                    {                     
                        characterControl.setCurrentHealth(characterControl.getCurrentHealth() + (amountCollectedH / numCollectedH));
                    }
                    if (gameScript.GetComponent<GameScript>().counter >= numCollectedH)
                    {
                        amountCollectedH = 0;
                        numCollectedH = 0;
                    }                        
                }
                Destroy(this.gameObject);
            }               
        }     
        else
        {
            if (this.mType != "Collected")
                transform.position = Vector3.Lerp(currentPos, endPos, .07f);
            else
                transform.position = endPos;
        }
    }

    public void swapArt()
    {
        this.gameObject.transform.GetChild(12).GetComponent<SpriteRenderer>().sprite = characterControl.getWeaponIcon().GetComponent<SpriteRenderer>().sprite;
    }
}
