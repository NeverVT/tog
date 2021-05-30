using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCostText : MonoBehaviour
{
	void Update ()
    {
        this.GetComponent<TextMesh>().text = Item.cost.ToString();
    }
}
