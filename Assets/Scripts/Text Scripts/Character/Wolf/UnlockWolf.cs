﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockWolf : MonoBehaviour
{
    void Update()
    {
        if (Team.wolf > 0)
        {
            this.transform.gameObject.SetActive(false);
            this.transform.parent.gameObject.tag = "BarracksCharacter";
        }
    }
}
