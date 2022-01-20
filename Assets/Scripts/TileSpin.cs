using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpin : MonoBehaviour
{
    public GameObject gameScript;
    int animationDelay;
    int frame;
    int numType;
    void Start()
    {
        gameScript = GameObject.Find("GameScript");
        animationDelay = UnityEngine.Random.Range(500, 5000);
        frame = 0;
    }
    void FixedUpdate()
    {
        if (frame == animationDelay) 
        {
            if(gameScript.GetComponent<GameScript>().collected.Count == 0 && !this.GetComponentInParent<Tile>().frozen)
            {
                numType = 0;
                if (this.GetComponentInParent<Tile>().mRow > 0 && this.GetComponentInParent<Tile>().mCol > 0)
                    if (gameScript.GetComponent<GameScript>().board[this.GetComponentInParent<Tile>().mRow - 1, this.GetComponentInParent<Tile>().mCol - 1].GetComponent<Tile>().mType == this.GetComponentInParent<Tile>().mType)
                        numType++;
                if (this.GetComponentInParent<Tile>().mRow > 0)
                    if (gameScript.GetComponent<GameScript>().board[this.GetComponentInParent<Tile>().mRow - 1, this.GetComponentInParent<Tile>().mCol].GetComponent<Tile>().mType == this.GetComponentInParent<Tile>().mType)
                        numType++;
                if (this.GetComponentInParent<Tile>().mRow > 0 && this.GetComponentInParent<Tile>().mCol < 5)
                    if (gameScript.GetComponent<GameScript>().board[this.GetComponentInParent<Tile>().mRow - 1, this.GetComponentInParent<Tile>().mCol + 1].GetComponent<Tile>().mType == this.GetComponentInParent<Tile>().mType)
                        numType++;
                if (this.GetComponentInParent<Tile>().mCol > 0)
                    if (gameScript.GetComponent<GameScript>().board[this.GetComponentInParent<Tile>().mRow, this.GetComponentInParent<Tile>().mCol - 1].GetComponent<Tile>().mType == this.GetComponentInParent<Tile>().mType)
                        numType++;
                if (this.GetComponentInParent<Tile>().mCol < 5)
                    if (gameScript.GetComponent<GameScript>().board[this.GetComponentInParent<Tile>().mRow, this.GetComponentInParent<Tile>().mCol + 1].GetComponent<Tile>().mType == this.GetComponentInParent<Tile>().mType)
                        numType++;
                if (this.GetComponentInParent<Tile>().mRow < 5 && this.GetComponentInParent<Tile>().mCol > 0)
                    if (gameScript.GetComponent<GameScript>().board[this.GetComponentInParent<Tile>().mRow + 1, this.GetComponentInParent<Tile>().mCol - 1].GetComponent<Tile>().mType == this.GetComponentInParent<Tile>().mType)
                        numType++;
                if (this.GetComponentInParent<Tile>().mRow < 5)
                    if (gameScript.GetComponent<GameScript>().board[this.GetComponentInParent<Tile>().mRow + 1, this.GetComponentInParent<Tile>().mCol].GetComponent<Tile>().mType == this.GetComponentInParent<Tile>().mType)
                        numType++;
                if (this.GetComponentInParent<Tile>().mRow < 5 && this.GetComponentInParent<Tile>().mCol < 5)
                    if (gameScript.GetComponent<GameScript>().board[this.GetComponentInParent<Tile>().mRow + 1, this.GetComponentInParent<Tile>().mCol + 1].GetComponent<Tile>().mType == this.GetComponentInParent<Tile>().mType)
                        numType++;

                if(numType >= 2)
                {
                    this.GetComponent<Animator>().SetTrigger("Spin");
                    frame = 0;
                }               
            }               
        }
        else
            frame++;

        if(gameScript.GetComponent<GameScript>().collected.Count > 0)
            frame = 0;
    }
}
