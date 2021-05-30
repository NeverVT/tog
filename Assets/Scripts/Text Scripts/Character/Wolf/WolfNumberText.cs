using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfNumberText : MonoBehaviour
{
    void Update()
    {
        this.GetComponent<TextMesh>().text = Team.wolf.ToString();
    }
}
