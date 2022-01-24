using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public int health = 4;
    public int damage = 2;
    public double pDamage = 0;
    public bool frozen = false;
    public bool burning = false;
    public bool poisoned = false;
    public bool skull = false;
    public bool justSpawned = false;
    public bool shouldntAttack = false;
    public bool justFrozen = false;
    public bool cursed = false;

    public static int currentHealth;
    public static int currentDamage;
    public double predictedDamage;
    public bool isSkull = false;

    void Update()
    {
        if(!shouldntAttack && !justSpawned && this.transform.GetChild(9) != null)// && this.GetComponent<Tile>().boss == "")
        {
            this.transform.GetChild(8).gameObject.SetActive(false);
            this.transform.GetChild(9).gameObject.SetActive(true);
        }
        currentHealth = health;
        if(cursed)
        {
            currentDamage = damage-1;
        }
        else
            currentDamage = damage;
    }

    
}
