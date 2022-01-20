using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRecruitLevel : MonoBehaviour
{
    void Update()
    {
        if (Ship.character == "Urp")
            this.GetComponent<TextMesh>().text = Team.urpLvl.ToString();
        else if (Ship.character == "Chrisa")
            this.GetComponent<TextMesh>().text = Team.chrisaLvl.ToString();
        else if (Ship.character == "Kurtzle")
            this.GetComponent<TextMesh>().text = Team.kurtzleLvl.ToString();
        else
            this.GetComponent<TextMesh>().text = Ship.character;
    }
}
