using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goblinAnimationControllerGS : MonoBehaviour
{
    public void stopAttackAnimation()
    {
        this.GetComponent<Animator>().SetBool("attack", false);
    }
}
