using UnityEngine;
using System.Collections;

public class SkillText : MonoBehaviour
{
	void Update ()
    {
        if (GetComponent<GameScript>().SkillOne.transform.name == this.transform.parent.name)
        {
            if (GetComponent<GameScript>().skillOne - GetComponent<GameScript>().turnCounter > 0)
                this.GetComponent<TextMesh>().text = (GetComponent<GameScript>().skillOne - GetComponent<GameScript>().turnCounter).ToString();
            else
                this.GetComponent<TextMesh>().text = "";
        }
        else if (GetComponent<GameScript>().SkillTwo.transform.name == this.transform.parent.name)
        {
            if (GetComponent<GameScript>().skillTwo - GetComponent<GameScript>().turnCounter > 0)
                this.GetComponent<TextMesh>().text = (GetComponent<GameScript>().skillTwo - GetComponent<GameScript>().turnCounter).ToString();
            else
                this.GetComponent<TextMesh>().text = "";
        }
    }
}
