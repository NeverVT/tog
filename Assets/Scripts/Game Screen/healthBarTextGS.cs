using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthBarTextGS : MonoBehaviour
{
    public CharacterControl characterControl;

    void Update()
    {
        this.GetComponent<TextMesh>().text = characterControl.getCurrentHealth().ToString();
    }
}
