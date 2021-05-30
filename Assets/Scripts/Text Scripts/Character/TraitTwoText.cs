using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitTwoText : MonoBehaviour
{
    void Update()
    {
        this.GetComponent<TextMesh>().text = Traits.getTraitText(CharacterScreen.traitTwo);
    }
}
