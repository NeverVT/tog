using UnityEngine;
using System.Collections;

public class SkillText2 : MonoBehaviour
{
    void Update()
    {
        if (GetComponent<GameScript>().skillTwo - GetComponent<GameScript>().turnCounter > 0)
            this.GetComponent<TextMesh>().text = (GetComponent<GameScript>().skillTwo - GetComponent<GameScript>().turnCounter).ToString();
        else
            this.GetComponent<TextMesh>().text = "";
    }
}
