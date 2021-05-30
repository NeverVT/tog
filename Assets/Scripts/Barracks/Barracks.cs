using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : MonoBehaviour
{
    private string[] names;
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
        names = new string[9];
        positions = new Vector3[9];
        gameObjects = new GameObject[9];
        loadNames();
        loadPositions();
        StartCoroutine(loadPrefabs());     
    }

    private void loadNames()
    {
        for(int i = 0; i < 9; i++)
        {
            names[i] = PlayerPrefs.GetString((i).ToString());
        }
        
        if (names[0] == null || names[0] == "" || names[0] == "Soldier")
            names[0] = "Urp";
        if (names[1] == null || names[1] == "" || names[0] == "Mummy")
            names[1] = "Chrisa";
        if (names[2] == null || names[2] == "" || names[0] == "Doll")
            names[2] = "Kurtzle";
        cOneName = names[0];
        cTwoName = names[1];
        cThreeName = names[2];
    }

    private void loadPositions()
    {
        positions[0] = new Vector3(41F, 12F, -9.59F);
        positions[1] = new Vector3(49F, 12F, -9.59F);
        positions[2] = new Vector3(57F, 12F, -9.59F);
    }

    private IEnumerator loadPrefabs()
    {
        yield return null;
        for (int i = 0; i < 9; i++)
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
