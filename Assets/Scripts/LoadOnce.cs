using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadOnce : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!GameControl.firstTime)
            Destroy(this.gameObject);
    }

   
}
