using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWDText : MonoBehaviour
{
	void Update ()
    {
        this.GetComponent<TextMesh>().text = this.transform.parent.gameObject.GetComponent<Item>().mDamage.ToString();
    }
}
