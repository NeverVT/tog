using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public void changeY()
    {
        this.transform.localPosition = new Vector3(this.transform.localPosition.x, (this.transform.localPosition.y - 1.35f), this.transform.localPosition.z);
    }
}
