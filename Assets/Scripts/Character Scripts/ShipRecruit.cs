using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ShipRecruit : MonoBehaviour
{ 
    void Update()
    {
        if (Ship.character == this.name)
            this.transform.gameObject.SetActive(true);
        else
            this.transform.gameObject.SetActive(false);
    }
}
