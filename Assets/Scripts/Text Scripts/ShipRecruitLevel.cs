using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRecruitLevel : MonoBehaviour
{
    void Update()
    {
        if (Ship.character == "Urp")
            this.GetComponent<TextMesh>().text = Team.urp.ToString();
        else if (Ship.character == "Chrisa")
            this.GetComponent<TextMesh>().text = Team.chrisa.ToString();
        else if (Ship.character == "Kurtzle")
            this.GetComponent<TextMesh>().text = Team.kurtzle.ToString();
        else if (Ship.character == "Dobby")
            this.GetComponent<TextMesh>().text = Team.dobby.ToString();
        else if (Ship.character == "Bear")
            this.GetComponent<TextMesh>().text = Team.bear.ToString();
        else if (Ship.character == "Wolf")
            this.GetComponent<TextMesh>().text = Team.wolf.ToString();
        else
            this.GetComponent<TextMesh>().text = Ship.character;
    }
}
