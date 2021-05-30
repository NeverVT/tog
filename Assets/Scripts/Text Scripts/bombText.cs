using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombText : MonoBehaviour
{
    void Update()
    {
        this.GetComponent<TextMesh>().text = this.GetComponentInParent<Tile>().bombTimer.ToString();
    }
}
