using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DobbyNumberText : MonoBehaviour
{
    void Update()
    {
        this.GetComponent<TextMesh>().text = Team.dobby.ToString();
    }
}
