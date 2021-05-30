using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollAnimationFunctions : MonoBehaviour
{
    public void turnOnButtons()
    {
        this.transform.Find("Swap").gameObject.SetActive(true);
        this.transform.Find("Inspect").gameObject.SetActive(true);
    }
    public void turnOffButtons()
    {
        this.transform.Find("Swap").gameObject.SetActive(false);
        this.transform.Find("Inspect").gameObject.SetActive(false);
    }
    public void fadeIn()
    {
        this.transform.parent.Find("Icon").gameObject.SetActive(true);
    }
}
