using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class traitsGS : MonoBehaviour
{
    public CharacterControl characterControl;
    void Update()
    {       
        if(this.transform.name == "Traits One")
        {
            if(characterControl.getTraitOne() != null && characterControl.getTraitOne() != "")
            {
                if (this.transform.Find(characterControl.getTraitOne()) != null)
                    this.transform.Find(characterControl.getTraitOne()).gameObject.SetActive(true);
            }                                            
        }
        else if (this.transform.name == "Traits Two")
        {
            if (characterControl.getTraitTwo() != null && characterControl.getTraitTwo() != "")
                if(this.transform.Find(characterControl.getTraitTwo()) != null)
                    this.transform.Find(characterControl.getTraitTwo()).gameObject.SetActive(true);
        }
        else if (this.transform.parent.name == "Traits One")
        {
            if (this.transform.name != characterControl.getTraitOne())
                this.gameObject.SetActive(false);
        }
        else if (this.transform.parent.name == "Traits Two")
        {
            if (this.transform.name != characterControl.getTraitTwo())
                this.gameObject.SetActive(false);
        }
    }
}
