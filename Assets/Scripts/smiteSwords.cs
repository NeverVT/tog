using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smiteSwords : MonoBehaviour
{
    public Vector3 endPos;
    private Vector3 v_diff;
    private float atan2;
    void FixedUpdate()
    {
        if(endPos != new Vector3(0,0,0))
        {
            //get the direction of the other object from current object
            Vector3 dir = endPos - transform.position;
            //get the angle from current direction facing to desired target
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            //set the angle into a quaternion + sprite offset depending on initial sprite facing direction
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle + -60f));
            //Roatate current game object to face the target using a slerp function which adds some smoothing to the move
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 4f * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, endPos, .07f);

            if (Mathf.Abs(transform.position.x - endPos.x) <= 0.05 && Mathf.Abs(transform.position.y - endPos.y) <= 0.05)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
