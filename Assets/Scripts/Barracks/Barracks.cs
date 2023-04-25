using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : MonoBehaviour
{
    public string[] names;
    private Vector3[] positions;
    private GameObject[] gameObjects;

    public static string cOneName;
    public static string cTwoName;
    public static string cThreeName;
    public static string cFourName;
    public static string cFiveName;
    public static string cSixName;
    public static string cSevenName;
    public static string cEightName;
    public static string cNineName;

    public GameObject content;
    public GameObject urp;
    public GameObject chrisa;
    public GameObject kurtzle;

    void Start()
    {
        positions = new Vector3[9];
        gameObjects = new GameObject[9];
        loadNames();
        loadPositions();
        StartCoroutine(loadPrefabs());     
    }

    private void loadNames()
    {
        for(int i = 0; i < names.Length; i++)
        {
            Debug.Log(names[i]);
            names[i] = PlayerPrefs.GetString((i).ToString());
        }
       
        cOneName = names[0];
        cTwoName = names[1];
        cThreeName = names[2];
        Team.selectedCharacter = cOneName;
    }

    private void loadPositions()
    {
        positions[0] = new Vector3(-110F, 15F, -9.59F);
        positions[1] = new Vector3(-110F, -2F, -9.59F);
        positions[2] = new Vector3(-110F, -17F, -9.59F);
    }

    private IEnumerator loadPrefabs()
    {
        yield return null;     
        for (int i = 0; i < names.Length; i++)
        {         
            if (names[i] == urp.name)
                gameObjects[i] = (GameObject)Instantiate(urp, positions[i], Quaternion.identity);
            else if (names[i] == chrisa.name)
                gameObjects[i] = (GameObject)Instantiate(chrisa, positions[i], Quaternion.identity);
            else if (names[i] == kurtzle.name)
                gameObjects[i] = (GameObject)Instantiate(kurtzle, positions[i], Quaternion.identity);

            if(gameObjects[i] != null)
            {
                gameObjects[i].transform.parent = content.transform;
                gameObjects[i].name = names[i];
                if (i == 0)
                {
                    gameObjects[i].transform.GetChild(2).gameObject.SetActive(true);
                    /*if (PlayerPrefs.GetString(names[i] + "Skill") == "" || PlayerPrefs.GetString(names[i] + "Skill") == gameObjects[i].transform.GetChild(2).transform.GetChild(0).name) //use first
                    {
                        gameObjects[i].transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(true);
                    }*/
                }
            }                               
        }
    }

    void OnDestroy()
    {
        PlayerPrefs.SetString("0", cOneName);
        PlayerPrefs.SetString("1", cTwoName);
        PlayerPrefs.SetString("2", cThreeName);
    }
}
