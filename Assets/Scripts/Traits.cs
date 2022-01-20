using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traits : MonoBehaviour
{
    public GameObject tooltip;

    public void triggerTooltip()
    {
        tooltip.SetActive(true);
    }
    public void turnOffTooltip()
    {
        tooltip.SetActive(false);
    }
}
