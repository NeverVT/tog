using UnityEngine;
using System.Collections;

public class HealthText : MonoBehaviour
{ 
    void Update ()
    {
        this.GetComponent<TextMesh>().text = Enemy.currentHealth.ToString();
    }
}
