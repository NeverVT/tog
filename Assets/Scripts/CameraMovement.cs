using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    float newY = 0;
    void Start()
    {
        Input.gyro.enabled = true;
    }

    void FixedUpdate()
    {
        /*
        if(Input.GetButton("Q"))
        {
            change -= .01f;
            
        }
        if (Input.GetButton("E"))
        {
            change += .01f;
        }
        change = Mathf.Clamp(change, -2.5f, 2.5f);

        transform.eulerAngles = new Vector3(0, change, 0);
        */
        if(this.transform.position.x == 0 && this.transform.position.y == 0)
        {
            newY += (-Input.gyro.rotationRateUnbiased.y/8);
            newY = Mathf.Clamp(newY, -7f, 6f);
            transform.eulerAngles = new Vector3(0, newY, 0);
            this.transform.GetChild(1).gameObject.SetActive(false);
            this.transform.GetChild(2).gameObject.SetActive(false);
            this.transform.GetChild(3).gameObject.SetActive(false);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            this.transform.GetChild(1).gameObject.SetActive(true);
            this.transform.GetChild(2).gameObject.SetActive(true);
            this.transform.GetChild(3).gameObject.SetActive(true);
        }
        
    } 
}
