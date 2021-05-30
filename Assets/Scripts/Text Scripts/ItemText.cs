using UnityEngine;
using System;

public class ItemText : MonoBehaviour
{
	void Update ()
    {
        /*
        if (this.GetComponentInParent<Item>().mDamage > 0)
        {
            if (this.GetComponentInParent<Item>().mEquipped)
            {               
                this.GetComponent<TextMesh>().text = Item.title + Environment.NewLine + "Damage: " + Item.damage.ToString();               
            }
            else
            {                
                this.GetComponent<TextMesh>().text = Item.title + Environment.NewLine + "Damage: " + Item.damage.ToString() + Environment.NewLine + "Cost: " + Item.cost.ToString();              
            }
        }
        else if (this.GetComponentInParent<Item>().mArmor > 0)
        {
            if (this.GetComponentInParent<Item>().mEquipped)
            {
                this.GetComponent<TextMesh>().text = Item.title + Environment.NewLine + "Armor: " + Item.armor.ToString();               
            }
            else
            {                
                this.GetComponent<TextMesh>().text = Item.title + Environment.NewLine + "Armor: " + Item.armor.ToString() + Environment.NewLine + "Cost: " + Item.cost.ToString();               
            }           
        }       */
        if(Item.damage > 0)
            this.GetComponent<TextMesh>().text = Item.damage.ToString();
        else
            this.GetComponent<TextMesh>().text = Item.armor.ToString();


    }
}
