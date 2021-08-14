using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour
{
    public string mTitle = "";
    public string mBonus = "";
    public string mAttributeOne = "";
    public string mAttributeTwo = "";
    public string mAttributeThree = "";
    public int mDamage = 0;
    public int mArmor = 0;
    public int mCost = 0;
    public bool mEquipped = false;
    public int mTier = 0;
    public string mType = "";
    public string mArt = "";
    public int index;

    public static string title = "";
    public static string bonus = "";
    public static string attributeOne = "";
    public static string attributeTwo = "";
    public static string attributeThree = "";
    public static int damage = 0;
    public static int armor = 0;
    public static int cost = 0;
    public static bool equipped = false;

    private int temp;

	void Update ()
    {
        title = mTitle;
        bonus = mBonus;
        attributeOne = mAttributeOne;
        attributeTwo = mAttributeTwo;
        attributeThree = mAttributeThree;
        damage = mDamage;
        armor = mArmor;
        cost = mCost;
        equipped = mEquipped;
	}

    public int RollTier()
    {
        temp = UnityEngine.Random.Range(1, 95);
        if (temp <= 35)
            mTier = 1;
        else if (temp <= 65)
            mTier = 2;
        else if (temp <= 85)
            mTier = 3;
        else
            mTier = 4;       
        return mTier;     
    }

    public string RollType()
    {
        string type = "";
        temp = UnityEngine.Random.Range(1, 5);
        //temp = 1;
        switch (temp)
        {
            case 1:
                type = "Axes";
                break;
            case 2:
                type = "Swords";
                break;
            case 3:
                type = "Bows";
                break;
            case 4:
                type = "Armor";
                break;
        }
        return type;
    }

    public string RollAttributes(string type, string slot)
    {
        string bonus = "";
        if (slot == "One")
        {
            if (type == "Axes")
                return "Chopper";
            else if (type == "Swords")
                return "Slasher";
            else if (type == "Clubs")
                return "Hacker";
            else if (type == "Bows")
                return "Hunter";
            else
            {
                if (type == "Armor" || type == "Light Armor" || type == "Helmets" || type == "Shields")
                {
                    temp = UnityEngine.Random.Range(1, 2);
                    temp = 3;
                    switch (temp)
                    {
                        case 1:
                            bonus = "Intimidating";
                            break;
                        case 2:
                            bonus = "Protector";
                            break;
                        case 3:
                            bonus = "Tough";
                            break;
                        case 4:
                            bonus = "Survivalist";
                            break;
                        case 5:
                            bonus = "Spiked";
                            break;
                        case 6:
                            bonus = "Empath";
                            break;
                    }
                    return bonus;
                }
                else
                {
                    temp = UnityEngine.Random.Range(1, 2);
                    switch (temp)
                    {
                        case 1:
                            bonus = "Burn";
                            break;
                        case 2:
                            bonus = "Crit";
                            break;
                        case 3:
                            bonus = "Frost";
                            break;
                        case 4:
                            bonus = "Poison";
                            break;
                        case 5:
                            bonus = "Vamp";
                            break;
                        case 6:
                            bonus = "Impatient";
                            break;                       
                    }
                    return bonus;
                }
            }
        }
        else
        {
            if (type == "Armor" || type == "Light Armor" || type == "Helmets" || type == "Shields")
            {
                temp = UnityEngine.Random.Range(1, 7);
                switch (temp)
                {
                    case 1:
                        bonus = "Intimidating";
                        break;
                    case 2:
                        bonus = "Protector";
                        break;
                    case 3:
                        bonus = "Tough";
                        break;
                    case 4:
                        bonus = "Survivalist";
                        break;
                    case 5:
                        bonus = "Spiked";
                        break;
                    case 6:
                        bonus = "Empath";
                        break;
                }
                return bonus;
            }
            else
            {
                temp = UnityEngine.Random.Range(1, 7);
                switch (temp)
                {
                    case 1:
                        bonus = "Burn";
                        break;
                    case 2:
                        bonus = "Crit";
                        break;
                    case 3:
                        bonus = "Frost";
                        break;
                    case 4:
                        bonus = "Poison";
                        break;
                    case 5:
                        bonus = "Vamp";
                        break;
                    case 6:
                        bonus = "Impatient";
                        break;
                }
                return bonus;
            }
        }
    }         

    public string RollArt(string type, int tier)
    {
        switch (type)
        {
            case "2H Axes":
                switch(tier)
                {
                    case 1:
                        temp = UnityEngine.Random.Range(1, 3);
                        switch(temp)
                        {
                            case 1:
                                mArt = "2H Axe 1";
                                break;
                            case 2:
                                mArt = "2H Axe 2";
                                break;
                            case 3:
                                mArt = "2H Axe 3";
                                break;
                            case 4:
                                mArt = "2H Axe 5";
                                break;
                        }
                        break;
                    case 2:
                        temp = UnityEngine.Random.Range(1, 3);
                        switch (temp)
                        {
                            case 1:
                                mArt = "2H Axe 7";
                                break;
                            case 2:
                                mArt = "2H Axe 6";
                                break;
                            case 3:
                                mArt = "2H Axe 8";
                                break;                          
                        }
                        break;
                    case 3:
                        temp = UnityEngine.Random.Range(1, 3);
                        switch (temp)
                        {
                            case 1:
                                mArt = "2H Axe 4";
                                break;
                            case 2:
                                mArt = "2H Axe 10";
                                break;
                            case 3:
                                mArt = "2H Axe 11";
                                break;
                        }
                        break;
                    case 4:
                        mArt = "2H Axe 9";
                        break;
                }
                break;
            case "2H Swords":
                switch (tier)
                {
                    case 1:
                        temp = UnityEngine.Random.Range(1, 4);
                        switch (temp)
                        {
                            case 1:
                                mArt = "2H Sword 1";
                                break;
                            case 2:
                                mArt = "2H Sword 2";
                                break;
                            case 3:
                                mArt = "2H Sword 4";
                                break;
                            case 4:
                                mArt = "2H Sword 8";
                                break;
                        }
                        break;
                    case 2:
                        temp = UnityEngine.Random.Range(1, 3);
                        switch (temp)
                        {
                            case 1:
                                mArt = "2H Sword 5";
                                break;
                            case 2:
                                mArt = "2H Sword 6";
                                break;
                            case 3:
                                mArt = "2H Sword 7";
                                break;
                        }
                        break;
                    case 3:
                        mArt = "2H Sword 3";
                        break;
                    case 4:
                        mArt = "2H Sword 9";
                        break;
                }
                break;
            case "Axes":
                switch (tier)
                {
                    case 1:
                        temp = UnityEngine.Random.Range(1, 4);
                        switch (temp)
                        {
                            case 1:
                                mArt = "Axe 7";
                                break;
                            case 2:
                                mArt = "Axe 6";
                                break;
                            case 3:
                                mArt = "Axe 5";
                                break;
                        }
                        break;
                    case 2:
                        temp = UnityEngine.Random.Range(1, 5);
                        switch (temp)
                        {
                            case 1:
                                mArt = "Axe 3";
                                break;
                            case 2:
                                mArt = "Axe 1";
                                break;
                            case 3:
                                mArt = "Axe 8";
                                break;
                            case 4:
                                mArt = "Axe 13";
                                break;
                        }
                        break;
                    case 3:
                        temp = UnityEngine.Random.Range(1, 4);
                        switch (temp)
                        {
                            case 1:
                                mArt = "Axe 4";
                                break;
                            case 2:
                                mArt = "Axe 9";
                                break;
                            case 3:
                                mArt = "Axe 11";
                                break;                          
                        }
                        break;
                    case 4:
                        temp = UnityEngine.Random.Range(1, 3);
                        switch (temp)
                        {
                            case 1:
                                mArt = "Axe 2";
                                break;
                            case 2:
                                mArt = "Axe 10";
                                break;                           
                        }
                        break;
                }
                break;
            case "Bows":
                switch (tier)
                {
                    case 1:
                        temp = UnityEngine.Random.Range(1, 3);
                        switch (temp)
                        {
                            case 1:
                                mArt = "Bow 1";
                                break;
                            case 2:
                                mArt = "Bow 2";
                                break;                          
                        }
                        break;
                    case 2:
                        mArt = "Bow 3";
                        break;
                    case 3:
                        mArt = "Bow 4";
                        break;
                    case 4:
                        mArt = "Bow 5";
                        break;
                }
                break;
            case "Clubs":
                switch (tier)
                {
                    case 1:
                        temp = UnityEngine.Random.Range(1, 4);
                        switch (temp)
                        {
                            case 1:
                                mArt = "Club 1";
                                break;
                            case 2:
                                mArt = "Club 2";
                                break;
                            case 3:
                                mArt = "Club 3";
                                break;
                            case 4:
                                mArt = "Club 10";
                                break;
                        }
                        break;
                    case 2:
                        temp = UnityEngine.Random.Range(1, 3);
                        switch (temp)
                        {
                            case 1:
                                mArt = "Club 6";
                                break;
                            case 2:
                                mArt = "Club 7";
                                break;
                            case 3:
                                mArt = "Club 9";
                                break;
                            
                        }
                        break;
                    case 3:
                        temp = UnityEngine.Random.Range(1, 3);
                        switch (temp)
                        {
                            case 1:
                                mArt = "Club 4";
                                break;
                            case 2:
                                mArt = "Club 11";
                                break;
                            case 4:
                                mArt = "Club 8";
                                break;
                        }
                        break;
                    case 4:
                        temp = UnityEngine.Random.Range(1, 2);
                        switch (temp)
                        {
                            case 1:
                                mArt = "Club 5";
                                break;
                            case 2:
                                mArt = "Club 12";
                                break;
                        }
                        break;
                }
                break;
            case "Hammers":
                switch (tier)
                {
                    case 1:
                        temp = UnityEngine.Random.Range(1, 3);
                        switch (temp)
                        {
                            case 1:
                                mArt = "Hammer 3";
                                break;
                            case 2:
                                mArt = "Hammer 2";
                                break;
                            case 3:
                                mArt = "Hammer 7";
                                break;                           
                        }
                        break;
                    case 2:
                        temp = UnityEngine.Random.Range(1, 3);
                        switch (temp)
                        {
                            case 1:
                                mArt = "Hammer 1";
                                break;
                            case 2:
                                mArt = "Hammer 4";
                                break;
                            case 3:
                                mArt = "Hammer 11";
                                break;
                        }
                        break;
                    case 3:
                        temp = UnityEngine.Random.Range(1, 3);
                        switch (temp)
                        {
                            case 1:
                                mArt = "Hammer 5";
                                break;
                            case 2:
                                mArt = "Hammer 8";
                                break;
                            case 3:
                                mArt = "Hammer 9";
                                break;
                        }
                        break;
                    case 4:
                        temp = UnityEngine.Random.Range(1, 2);
                        switch (temp)
                        {
                            case 1:
                                mArt = "Hammer 6";
                                break;
                            case 2:
                                mArt = "Hammer 10";
                                break;                                                          
                        }
                        break;
                }
                break;
            case "Long Bow":
                switch (tier)
                {
                    case 1:
                        temp = UnityEngine.Random.Range(1, 2);
                        switch (temp)
                        {
                            case 1:
                                mArt = "Long Bow 1";
                                break;
                            case 2:
                                mArt = "Long Bow 2";
                                break;
                        }
                        break;
                    case 2:
                        mArt = "Long Bow 3";
                        break;
                    case 3:
                        mArt = "Long Bow 4";
                        break;
                    case 4:
                        mArt = "Long Bow 5";
                        break;
                }
                break;
            case "Swords":
                switch (tier)
                {
                    case 1:
                        temp = UnityEngine.Random.Range(1, 4);
                        switch (temp)
                        {
                            case 1:
                                mArt = "Sword 7";
                                break;
                            case 2:
                                mArt = "Sword 1";
                                break;
                            case 3:
                                mArt = "Sword 8";
                                break;
                        }
                        break;
                    case 2:
                        temp = UnityEngine.Random.Range(1, 3);
                        switch (temp)
                        {
                            case 1:
                                mArt = "Sword 2";
                                break;
                            case 2:
                                mArt = "Sword 6";
                                break;
                        }
                        break;
                    case 3:
                        temp = UnityEngine.Random.Range(1, 3);
                        switch (temp)
                        {
                            case 1:
                                mArt = "Sword 4";
                                break;
                            case 2:
                                mArt = "Sword 3";
                                break;                          
                        }
                        break;
                    case 4:
                        temp = UnityEngine.Random.Range(1, 3);
                        switch (temp)
                        {
                            case 1:
                                mArt = "Sword 5";
                                break;
                            case 2:
                                mArt = "Sword 9";
                                break;
                        }
                        break;
                }
                break;
            case "Staves":
                switch (tier)
                {
                    case 1:
                        temp = UnityEngine.Random.Range(1, 5);
                        switch (temp)
                        {
                            case 1:
                                mArt = "Staff 1";
                                break;
                            case 2:
                                mArt = "Staff 2";
                                break;
                            case 3:
                                mArt = "Staff 6";
                                break;
                            case 4:
                                mArt = "Staff 7";
                                break;
                            case 5:
                                mArt = "Staff 9";
                                break;
                        }
                        break;
                    case 2:
                        temp = UnityEngine.Random.Range(1, 4);
                        switch (temp)
                        {
                            case 1:
                                mArt = "Staff 3";
                                break;
                            case 2:
                                mArt = "Staff 4";
                                break;
                            case 3:
                                mArt = "Staff 5";
                                break;
                            case 4:
                                mArt = "Staff 8";
                                break;
                        }
                        break;
                    case 3:
                        temp = UnityEngine.Random.Range(1, 3);
                        switch (temp)
                        {
                            case 1:
                                mArt = "Staff 10";
                                break;
                            case 2:
                                mArt = "Staff 11";
                                break;
                            case 3:
                                mArt = "Staff 12";
                                break;
                        }
                        break;
                    case 4:
                        mArt = "Staff 13";
                        break;                        
                }
                break;
            case "Armor":
                switch (tier)
                {
                    case 1:
                        temp = UnityEngine.Random.Range(1, 4);
                        switch (temp)
                        {
                            case 1:
                                mArt = "Armor 7";
                                break;
                            case 2:
                                mArt = "Armor 8";
                                break;
                            case 3:
                                mArt = "Armor 9";
                                break;
                        }
                        break;
                    case 2:
                        temp = UnityEngine.Random.Range(1, 3);
                        switch (temp)
                        {
                            case 1:
                                mArt = "Armor 2";
                                break;
                            case 2:
                                mArt = "Armor 5";
                                break;                         
                        }
                        break;
                    case 3:
                        temp = UnityEngine.Random.Range(1, 3);
                        switch (temp)
                        {
                            case 1:
                                mArt = "Armor 1";
                                break;
                            case 2:
                                mArt = "Armor 4";
                                break;                         
                        }
                        break;
                    case 4:
                        temp = UnityEngine.Random.Range(1, 3);
                        switch (temp)
                        {
                            case 1:
                                mArt = "Armor 3";
                                break;
                            case 2:
                                mArt = "Armor 6";
                                break;
                        }
                        break;
                }
                break;
            case "Light Armor":
                switch (tier)
                {
                    case 1:
                        temp = UnityEngine.Random.Range(1, 4);
                        switch (temp)
                        {
                            case 1:
                                mArt = "Light Armor 1";
                                break;
                            case 2:
                                mArt = "Light Armor 6";
                                break;
                            case 3:
                                mArt = "Light Armor 7";
                                break;
                            case 4:
                                mArt = "Light Armor 8";
                                break;
                        }
                        break;
                    case 2:
                        temp = UnityEngine.Random.Range(1, 3);
                        switch (temp)
                        {
                            case 1:
                                mArt = "Light Armor 2";
                                break;
                            case 2:
                                mArt = "Light Armor 4";
                                break;
                            case 3:
                                mArt = "Light Armor 11";
                                break;
                        }
                        break;
                    case 3:
                        temp = UnityEngine.Random.Range(1, 2);
                        switch (temp)
                        {
                            case 1:
                                mArt = "Light Armor 3";
                                break;
                            case 2:
                                mArt = "Light Armor 10";
                                break;
                        }
                        break;
                    case 4:
                        temp = UnityEngine.Random.Range(1, 2);
                        switch (temp)
                        {
                            case 1:
                                mArt = "Light Armor 5";
                                break;
                            case 2:
                                mArt = "Light Armor 9";
                                break;
                        }
                        break;
                }
                break;
            case "Helmets":
                switch (tier)
                {
                    case 1:
                        temp = UnityEngine.Random.Range(1, 4);
                        switch (temp)
                        {
                            case 1:
                                mArt = "Helmet 1";
                                break;
                            case 2:
                                mArt = "Helmet 2";
                                break;
                            case 3:
                                mArt = "Helmet 6";
                                break;
                            case 4:
                                mArt = "Helmet 9";
                                break;
                        }
                        break;
                    case 2:
                        temp = UnityEngine.Random.Range(1, 2);
                        switch (temp)
                        {
                            case 1:
                                mArt = "Helmet 4";
                                break;
                            case 2:
                                mArt = "Helmet 8";
                                break;                           
                        }
                        break;
                    case 3:
                        temp = UnityEngine.Random.Range(1, 2);
                        switch (temp)
                        {
                            case 1:
                                mArt = "Helmet 3";
                                break;
                            case 2:
                                mArt = "Helmet 7";
                                break;
                        }
                        break;
                    case 4:
                        mArt = "Helmet 5";
                        break;
                }
                break;
            case "Shields":
                switch (tier)
                {
                    case 1:
                        temp = UnityEngine.Random.Range(1, 5);
                        switch (temp)
                        {
                            case 1:
                                mArt = "Shield 1";
                                break;
                            case 2:
                                mArt = "Shield 2";
                                break;
                            case 3:
                                mArt = "Shield 3";
                                break;
                            case 4:
                                mArt = "Shield 12";
                                break;
                            case 5:
                                mArt = "Shield 13";
                                break;
                        }
                        break;
                    case 2:
                        temp = UnityEngine.Random.Range(1, 3);
                        switch (temp)
                        {
                            case 1:
                                mArt = "Shield 5";
                                break;
                            case 2:
                                mArt = "Shield 6";
                                break;
                            case 3:
                                mArt = "Shield 8";
                                break;                          
                        }
                        break;
                    case 3:
                        temp = UnityEngine.Random.Range(1, 3);
                        switch (temp)
                        {
                            case 1:
                                mArt = "Shield 7";
                                break;
                            case 2:
                                mArt = "Shield 9";
                                break;
                            case 3:
                                mArt = "Shield 11";
                                break;
                        }
                        break;
                    case 4:
                        temp = UnityEngine.Random.Range(1, 2);
                        switch (temp)
                        {
                            case 1:
                                mArt = "Shield 4";
                                break;
                            case 2:
                                mArt = "Shield 10";
                                break;
                        }
                        break;
                }
                break;
        }
        return mArt;
    }

    public string createTitle(string type)
    {
        string title = "";      
        switch (type)
        {
            case "Axes":
                title = "Axe";
                break;
            case "Swords":
                title = "Sword";
                break;
            case "Bows":
                title = "Short Bow";
                break;
            case "Clubs":
                title = "Club";
                break;
            case "Staves":
                title = "Staff";
                break;
            case "Armor":
                title = "Armor";
                break;
        }      
        return title;
    }

    public string createBonus()
    {
        string bonus = "";
        if (mAttributeOne != "")
            bonus = mAttributeOne + " ";
        if (mAttributeTwo != "")
            bonus = mAttributeOne + " " + mAttributeTwo + " ";
        if (mAttributeThree != "")
            bonus = mAttributeOne + " " + mAttributeTwo + " " + mAttributeThree + " ";
        return bonus;
    }

}
