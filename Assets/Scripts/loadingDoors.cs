using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class loadingDoors : MonoBehaviour
{
    public bool doneClosing;

    private void Start()
    {
        doneClosing = false;
    }

    public void closed()
    {
        doneClosing = true;
    }

    public void opened()
    {
        this.GetComponent<Animator>().SetBool("open", false);
    }
}
