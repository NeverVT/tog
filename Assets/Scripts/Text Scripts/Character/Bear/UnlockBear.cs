using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockBear : MonoBehaviour
{
    void Update()
    {
        if (Team.bear > 0)
        {
            this.transform.gameObject.SetActive(false);
            this.transform.parent.gameObject.tag = "BarracksCharacter";
        }
    }
}
