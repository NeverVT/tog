using UnityEngine;
using System.Collections;

public class AttackText : MonoBehaviour
{
    void Update ()
    {
        this.GetComponent<TextMesh>().text = Enemy.currentDamage.ToString();
    }
}
