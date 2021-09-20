using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Artifacts : MonoBehaviour
{
    public bool amuletOfPain = false;
    public bool bait = false;
    public bool bombBag = false;
    public bool chaosStone = false;
    public bool coupon = false;
    public bool dragonSickness = false;
    public bool faetouchedAmulet = false;
    public bool fourLeafClover = false;
    public bool hitList = false;
    public bool isaacsBinding = false;
    public bool leatherGloves = false;
    public bool loadedDie = false;
    public bool potionOfGiantsStrength = false;
    public bool prism = false;
    public bool reanimateStone = false;
    public bool stoneShell = false;
    public bool thorns = false;
    public bool vampireFang = false;
    public bool whetstone = false;
    public bool yellowBird = false;

    public bool frost = false;
    public bool venomVial = false;
    public bool fireStone = false;

    public Stack<string> artifacts = new Stack<string>();

    public void createArtifact(int col, int row, Vector3 pos, string artifact)
    {
        GetComponent<GameScript>().board[row, col] = (GameObject)Instantiate(Resources.Load("Artifacts/" + artifact), pos, Quaternion.identity);
        GetComponent<GameScript>().board[row, col].GetComponent<Tile>().mType = "Artifact";
        GetComponent<GameScript>().board[row, col].GetComponent<Tile>().mName = artifact;
        GetComponent<GameScript>().board[row, col].GetComponent<Tile>().isTrinket = true;
        GetComponent<GameScript>().board[row, col].GetComponent<Tile>().mCol = col;
        GetComponent<GameScript>().board[row, col].GetComponent<Tile>().mRow = row;
    }

    public string rollArtifact()
    {
        string artifact = "";
        bool reroll = true;
        while (reroll)
        {
            reroll = false;
            int roll = UnityEngine.Random.Range(1, 21);
            if (roll == 1 && !amuletOfPain)
                artifact = "amuletOfPain";
            else if (roll == 2 && !bait)
                artifact = "bait";
            else if (roll == 3 && !bombBag)
                artifact = "bombBag";
            else if (roll == 4 && !chaosStone)
                artifact = "chaosStone";
            else if (roll == 5 && !coupon)
                artifact = "coupon";
            else if (roll == 6 && !dragonSickness)
                artifact = "dragonSickness";
            else if (roll == 7 && !faetouchedAmulet)
                artifact = "faetouchedAmulet";
            else if (roll == 8 && !fourLeafClover)
                artifact = "fourLeafClover";
            else if (roll == 9 && !hitList)
                artifact = "hitList";
            else if (roll == 10 && !isaacsBinding)
                artifact = "isaacsBinding";
            else if (roll == 11 && !leatherGloves)
                artifact = "leatherGloves";
            else if (roll == 12 && !loadedDie)
                artifact = "loadedDie";
            else if (roll == 13 && !potionOfGiantsStrength)
                artifact = "potionOfGiantsStrength";
            else if (roll == 14 && !prism)
                artifact = "prism";
            else if (roll == 15 && !reanimateStone)
                artifact = "reanimateStone";
            else if (roll == 16 && !stoneShell)
                artifact = "stoneShell";
            else if (roll == 17 && !thorns)
                artifact = "thorns";
            else if (roll == 18 && !vampireFang)
                artifact = "vampireFang";
            else if (roll == 19 && !whetstone)
                artifact = "whetstone";
            else if (roll == 20 && !yellowBird)
                artifact = "yellowBird";
            else reroll = true;    
        }
        return artifact;
    }

    public string rollArtifactRubble()
    {
        string artifact = "";
        bool reroll = true;
        while (reroll)
        {
            reroll = false;
            int roll = Random.Range(1, 8);
            while (roll != 3 && roll != 5 && roll != 7)
                roll = Random.Range(1, 8);
            if (roll == 1 && !bait)
                artifact = "bait";
            else if (roll == 2 && !coupon)
                artifact = "coupon";
            else if (roll == 3 && !fourLeafClover)
                artifact = "fourLeafClover";
            else if (roll == 4 && !potionOfGiantsStrength)
                artifact = "potionOfGiantsStrength";
            else if (roll == 5 && !thorns)
                artifact = "thorns";
            else if (roll == 6 && !vampireFang)
                artifact = "vampireFang";
            else if (roll == 7 && !whetstone)
                artifact = "whetstone";
            else reroll = true;    
        }
        return artifact;
    }

    public string rollArtifactBoss()
    {
        string artifact = "";
        bool reroll = true;
        while (reroll)
        {
            reroll = false;
            int roll = UnityEngine.Random.Range(1, 14);
            while (roll != 2 && roll != 13 && roll != 11 && roll != 8)
                roll = Random.Range(1, 14);
            if (roll == 1 && !amuletOfPain)
                artifact = "amuletOfPain";
            else if (roll == 2 && !bombBag)
                artifact = "bombBag";
            else if (roll == 3 && !chaosStone)
                artifact = "chaosStone";
            else if (roll == 4 && !dragonSickness)
                artifact = "dragonSickness";
            else if (roll == 5 && !faetouchedAmulet)
                artifact = "faetouchedAmulet";
            else if (roll == 6 && !hitList)
                artifact = "hitList";
            else if (roll == 7 && !isaacsBinding)
                artifact = "isaacsBinding";
            else if (roll == 8 && !leatherGloves)
                artifact = "leatherGloves";
            else if (roll == 9 && !loadedDie)
                artifact = "loadedDie";
            else if (roll == 10 && !potionOfGiantsStrength)
                artifact = "potionOfGiantsStrength";
            else if (roll == 11 && !prism)
                artifact = "prism";
            else if (roll == 12 && !reanimateStone)
                artifact = "reanimateStone";
            else if (roll == 13 && !stoneShell)
                artifact = "stoneShell";
            else reroll = true;    
        }
        return artifact;
    }

    public void collectArtifact(GameObject artifact)
    {
        string artifactName = artifact.name;
        Debug.Log(artifactName);
        Debug.Log(artifacts.Count);
        switch (artifactName)
        {
            case "amuletOfPain(Clone)":
                amuletOfPain = true;
                artifacts.Push("Amulet Of Pain");
                break;
            case "bait(Clone)":
                bait = true;
                artifacts.Push("Bait");
                break;
            case "bombBag(Clone)":
                bombBag = true;
                artifacts.Push("Bomb Bag");
                break;
            case "chaosStone(Clone)":
                chaosStone = true;
                artifacts.Push("Chaos Stone");
                break;
            case "coupon(Clone)":
                coupon = true;
                artifacts.Push("Coupon");
                break;
            case "dragonSickness(Clone)":
                dragonSickness = true;
                artifacts.Push("Dragon Sickness");
                break;
            case "faetouchedAmulet(Clone)":
                faetouchedAmulet = true;
                artifacts.Push("Faetouched Amulet");
                break;
            case "fourLeafClover(Clone)":
                fourLeafClover = true;
                artifacts.Push("Four Leaf Clover");
                break;
            case "hitlist(Clone)":
                hitList = true;
                artifacts.Push("Hit List");
                break;
            case "isaacsBinding(Clone)":
                isaacsBinding = true;
                artifacts.Push("Isaacs Binding");
                break;
            case "leatherGloves(Clone)":
                leatherGloves = true;
                artifacts.Push("Leather Gloves");
                break;
            case "loadedDie(Clone)":
                loadedDie = true;
                artifacts.Push("Loaded Die");
                break;
            case "potionOfGiantsStrength(Clone)":
                potionOfGiantsStrength = true;
                artifacts.Push("Potion of Giants Strength");
                break;
            case "prism(Clone)":
                prism = true;
                artifacts.Push("Prism");
                break;
            case "reanimateStone(Clone)":
                reanimateStone = true;
                artifacts.Push("Reanimate Stone");
                break;
            case "stoneShell(Clone)":
                stoneShell = true;
                artifacts.Push("Stone Shell");
                break;
            case "thorns(Clone)":
                thorns = true;
                artifacts.Push("Thorns");
                break;
            case "vampireFang(Clone)":
                vampireFang = true;
                artifacts.Push("Vampire Fang");
                break;
            case "whetstone(Clone)":
                whetstone = true;
                artifacts.Push("Whetstone");
                break;
            case "yellowBird(Clone)":
                yellowBird = true;
                artifacts.Push("Yellow Bird");
                break;
        }
        Debug.Log(artifacts.Count);
    }
    public void displayArtifacts()
    {
        Debug.Log(artifacts.Count);
        string[] tempArtifacts = artifacts.ToArray();
        //new GameObject[artifacts.Count];
        Debug.Log(tempArtifacts[0]);
        for (int i = 0; i < artifacts.Count; i++)
        {
            switch(i)
            {
                case 0:
                    GameObject.Find("ArtifactsPage(Clone)").transform.Find("ArtifactOne").transform.Find(tempArtifacts[i]).gameObject.SetActive(true);
                    break;
                case 1:
                    GameObject.Find("ArtifactsPage(Clone)").transform.Find("ArtifactTwo").transform.Find(tempArtifacts[i]).gameObject.SetActive(true);
                    break;
                case 2:
                    GameObject.Find("ArtifactsPage(Clone)").transform.Find("ArtifactThree").transform.Find(tempArtifacts[i]).gameObject.SetActive(true);
                    break;
                case 3:
                    GameObject.Find("ArtifactsPage(Clone)").transform.Find("ArtifactFour").transform.Find(tempArtifacts[i]).gameObject.SetActive(true);
                    break;
                case 4:
                    GameObject.Find("ArtifactsPage(Clone)").transform.Find("ArtifactFive").transform.Find(tempArtifacts[i]).gameObject.SetActive(true);
                    break;
                case 5:
                    GameObject.Find("ArtifactsPage(Clone)").transform.Find("ArtifactSix").transform.Find(tempArtifacts[i]).gameObject.SetActive(true);
                    break;
            }
        }
    }
}
