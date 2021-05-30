using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearNumberText : MonoBehaviour
{
    void Update()
    {
        this.GetComponent<TextMesh>().text = Team.bear.ToString();
    }
}
