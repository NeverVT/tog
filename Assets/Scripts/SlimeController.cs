using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeY()
    {
        this.transform.localPosition = new Vector3(this.transform.localPosition.x, (this.transform.localPosition.y - 1.35f), this.transform.localPosition.z);
    }
}
