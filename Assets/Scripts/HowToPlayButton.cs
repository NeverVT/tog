using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayButton : MonoBehaviour
{
     public void spawnScreen()
     {
        this.transform.GetChild(0).gameObject.SetActive(true);
     }
}
