using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traits : MonoBehaviour
{
    public static string getTraitText(string trait)
    {
        switch(trait)
        {
            case "Protector":
                return "Protector: Half of the damage that would be dealt to\n another character is dealt to this character\n instead";
            case "Empath":
                return "Empath: When this character gathers\n health crystals anything past their own health\n is distributed evenly amongst companions";
            case "Impatient":
                return "Impatient: Some health crystals will spawn\n as mana crystals which reduce the cool\n down of skills when collected";
            case "Hunter":
                return "Hunter: Tougher enemies will appear more\n frequently. High risk with high reward!";
            case "Surivalist":
                return "Survivalist: If the character collects\n six or more Rubble at once they will\n avoid the enemy for a turn";
            case "Comforting":
                return "Comforting: Increase the Empathy of\n your party members.";
            case "Looter":
                return "Looter: Gain gold when killing enemies";
            case "Sleight of Hand":
                return "Sleight of Hand: If the character collects\n six or more Gold at once they will\n avoid the enemy for a turn";
            case "Meek":
                return "Meek: If the character collects\n six or more Crystals at once they will\n avoid the enemy for a turn";
            case "Intimidating":
                return "Intimidating: If the character collects\n six or more Swords at once they will\n avoid the enemy for a turn";
            case "Tough":
                return "Tough: Regenerate health after each turn";
            case "":
                return "";
        }
        return "";
    }
}
