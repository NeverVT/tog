using UnityEngine;
using System.Collections;

public class UpdateText : MonoBehaviour
{
    public static string updateText = "";
	void Update ()
    {
        this.GetComponent<TextMesh>().text = updateText;
        if(updateText == "")
            this.transform.GetChild(0).gameObject.SetActive(false);
        else
            this.transform.GetChild(0).gameObject.SetActive(true);
    }
}
