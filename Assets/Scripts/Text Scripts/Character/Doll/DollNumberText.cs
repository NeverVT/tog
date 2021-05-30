using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollNumberText : MonoBehaviour
{
    void Update()
    {
        this.GetComponent<TextMesh>().text = Team.kurtzle.ToString();
    }
}
