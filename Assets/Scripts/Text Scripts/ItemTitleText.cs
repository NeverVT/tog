using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTitleText : MonoBehaviour
{
	void Update ()
    {
        this.GetComponent<TextMesh>().text = Item.title;

    }
}
