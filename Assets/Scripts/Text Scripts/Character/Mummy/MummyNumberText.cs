using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummyNumberText : MonoBehaviour
{
    void Update()
    {
        this.GetComponent<TextMesh>().text = Team.chrisa.ToString();
    }
}
