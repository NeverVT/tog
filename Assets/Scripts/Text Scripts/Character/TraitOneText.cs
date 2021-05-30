using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitOneText : MonoBehaviour
{
    void Update()
    {
        this.GetComponent<TextMesh>().text = Traits.getTraitText(CharacterScreen.traitOne);
    }
}
