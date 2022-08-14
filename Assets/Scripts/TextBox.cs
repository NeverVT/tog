using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBox : MonoBehaviour
{
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public IEnumerator typeText(string text)
    {
        this.transform.GetChild(0).GetComponent<TextMesh>().text = "";
        foreach (char letter in text.ToCharArray())
        {
            this.transform.GetChild(0).GetComponent<TextMesh>().text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

}
