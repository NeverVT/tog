using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDobby : MonoBehaviour
{
    void Update()
    {
        if (Team.dobby > 0)
        {
            this.transform.gameObject.SetActive(false);
            this.transform.parent.gameObject.tag = "BarracksCharacter";
        }        
    }
}
