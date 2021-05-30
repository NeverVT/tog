using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CADText : MonoBehaviour
{
 
    void Update()
    {
        this.GetComponent<TextMesh>().text = this.transform.parent.gameObject.GetComponent<Item>().mArmor.ToString();
    }
}
