using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierNumberText : MonoBehaviour
{
    
    void Update()
    {
        this.GetComponent<TextMesh>().text = Team.urp.ToString();
    }
}
