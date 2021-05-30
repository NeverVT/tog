using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CircleScript : MonoBehaviour 
{
    public GameObject gameScript;
    private Vector3 target;
    private bool notCreated;
    public static bool collidedWithButton = false;
    int row;
    int col;
	void Awake() 
    {
        Screen.SetResolution(720, 1280, true);
        target = transform.position;
    }
    void Update()
    {
        if (!Input.GetMouseButton(0))
        {
            transform.position = new Vector3(-100, -100, 0);
        }
        //else
            draw();           
    }

    private void draw()
    {
        if(Input.GetMouseButton(0))//Pressed left click
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;
            transform.position = target;

            if (gameScript != null && gameScript.GetComponent<GameScript>().collected.Count == 1)
            {
                if (gameScript.GetComponent<GameScript>().collected.Peek().CompareTag("Spell"))
                {}
                else
                {
                    row = gameScript.GetComponent<GameScript>().collected.Peek().GetComponent<Tile>().mRow;
                    col = gameScript.GetComponent<GameScript>().collected.Peek().GetComponent<Tile>().mCol;
                }                
                StartCoroutine(WaitActivate());
                
            }
            else if (gameScript != null && gameScript.GetComponent<GameScript>().collected.Count > 1)
            {
                resetToolTips();
            }                 
        }
        else if (gameScript != null && gameScript.GetComponent<GameScript>().collected.Count > 0)
        {
            transform.position = new Vector3(-1,-1,0);
            StartCoroutine(gameScript.GetComponent<GameScript>().collect());
            resetToolTips();
            //gameScript.GetComponent<GameScript>().predict();
        }
        else
            collidedWithButton = false;
    }
    private void activateToolTips()
    {
        if (gameScript.GetComponent<GameScript>().collected.Peek().CompareTag("Spell"))
        {
            gameScript.GetComponent<GameScript>().collected.Peek().transform.GetChild(12).gameObject.SetActive(true);
        }
        else
        {
            gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(12).gameObject.SetActive(true);
            if (col == 0)
                gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(12).localPosition = new Vector3(1F, gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(12).localPosition.y, gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(12).localPosition.z);
            else if (col == 5)
                gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(12).localPosition = new Vector3(-1F, gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(12).localPosition.y, gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(12).localPosition.z);
        }
    }
    private void resetToolTips()
    {
        if(gameScript.GetComponent<GameScript>().board[row, col] != null)
            if(gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(12).gameObject != null)
                gameScript.GetComponent<GameScript>().board[row, col].transform.GetChild(12).gameObject.SetActive(false);                                                                          
    }

    IEnumerator WaitActivate()
    {
        yield return new WaitForSecondsRealtime(0.50F);
        if(gameScript != null)
        {
            if (gameScript.GetComponent<GameScript>().collected.Count == 1)
            {
                activateToolTips();
                yield return null;
            }
            else
                yield return null;
        }      
    }
}
