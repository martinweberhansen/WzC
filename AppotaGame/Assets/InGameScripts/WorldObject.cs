using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;

public enum FadeDirection { Fadein, Fadeout };
public enum Resource { Gold, Crystal };
public enum Location { None = 0, Head = 1, Body = 2, Weapon = 3 };


public class WorldObject
{
    private static WorldObject _worldObject;

    public static WorldObject GetworldObject
    {
        get
        {
            if (_worldObject == null)
            {
                Debug.Log("creating new worldObject");
                _worldObject = new WorldObject();
            }
            return _worldObject;
        }
    }

    public List<UnitClass> unitClasses;
    public List<Unit> units;
    public List<Level> levels;
    public List<PartySlot> partySlots;
    public List<Item> items;
    public List<Unit> currentParty;

    public World worldInfo;
    public bool initialized;
    [SerializeField]
    bool inBattle;
    [SerializeField]
    float fadeSpeed;
    [SerializeField]
    public bool isFading;

    //start battle
    //float difficultyModifier;
    int goldForWinning;
    int crystalsForWinning;



    void Start()
    {
        units = new List<Unit>();
        items = new List<Item>();
        //difficultyModifier = 0;
        goldForWinning = 0;
        crystalsForWinning = 0;
        partySlots = new List<PartySlot>();

    } //init
    public int GetGoldForWinning()
    {
        return goldForWinning;
    }

    public void DestroySingeltonInstance()
    {
        _worldObject = null;
    }

    private void Initialze()
    {
        units = new List<Unit>();
        items = new List<Item>();
        //difficultyModifier = 0;
        goldForWinning = 0;
        crystalsForWinning = 0;
        partySlots = new List<PartySlot>();
    }
    public void ShowStats()
    {
        Debug.Log("UNITS count in world: " + units.Count);
    }

    //public void CheckInitState()//need this because the singeltons doesnt get deleted when changing scene. But the list doesnt get reset.
    //{
    //    if(units == null)
    //    {
    //        Debug.Log("init state in world");
    //        Initialze();
    //    }
    //}

    public Item GetItem(string name, int number)
    {
        foreach(Item item in items)
        {
            if (item.assetName == name + number)
            {
                return item;
            }
        }
        return null;
    }


    public IEnumerator ChangeScene(float StartColor, FadeDirection direction, System.Action<bool> callback)
    {
        isFading = true;
        GameObject fadeImage = GameObject.Find("Fader");
        fadeImage.GetComponent<Image>().color = new Color(0, 0, 0, StartColor);
        bool doneFadingOut = false;
        float color = StartColor;

        while (!doneFadingOut)
        {
            if (direction == FadeDirection.Fadeout)
            {
                color += fadeSpeed + Time.deltaTime;
                fadeImage.GetComponent<Image>().color = new Color(0, 0, 0, color);
                if (color > 1.1f)
                {
                    doneFadingOut = true;
                }
            }
            if (direction == FadeDirection.Fadein)
            {
                color -= fadeSpeed + Time.deltaTime;
                fadeImage.GetComponent<Image>().color = new Color(0, 0, 0, color);
                if (color <= 0.0f)
                {
                    doneFadingOut = true;
                }
            }
            yield return null;
        }
        isFading = false;
        yield return null;
        callback(true);
    }
    public IEnumerator LoadFromXML(System.Action<bool> callback)
    {
        Debug.Log("start loading from xml");
        currentParty = new List<Unit>();
        //create new helper class
        XMLHelper h = new XMLHelper();
        //load world
        worldInfo = h.LoadGameWorld().worldInfo[0];
        //load levels
        levels = h.LoadLevels().levels;
        //load items
        items = h.LoadItems().Items;
        //load classes
        unitClasses = h.LoadClasses().classes;
        //load units
        units = h.LoadUnits().Units;

        //load party
        partySlots = h.LoadPartySlots().slots;



        Debug.Log("finished loading from xml");
        initialized = true;
        yield return null; // 
        callback(true);

    } //loads assets from XML
    public int FindUnitPosition(int id)
    {
        return units.FindIndex(unit => unit.id == id);
    }
    public void CompletedLevel(int levelID)
    {
        //change gold, crystals.
        Level lvl = levels.Find(level => level.id == levelID);
        worldInfo.gold += lvl.rewardGold;
        worldInfo.crystal += lvl.rewardCrystal;
        //unlock next level and save it
        Level nextLevel = levels.Find(level => level.id == levelID + 1);
        if (nextLevel != null && nextLevel.locked)
        {
            nextLevel.locked = false;
            int nextlevelposition = levels.FindIndex(level => level.id == nextLevel.id);
            levels[nextlevelposition] = nextLevel;
        }
        //save currentLevel;
        int levelposition = levels.FindIndex(level => level.id == lvl.id);
        levels[levelposition] = lvl;
        SaveWorld();
        SaveLevels();
    }

    

    public void SaveInfoToBattle(float difficultyModifier, int goldForWinning, int crystalsForWinning)
    {
        //this.difficultyModifier = difficultyModifier;
        this.goldForWinning = goldForWinning;
        this.crystalsForWinning = crystalsForWinning;
    }

    public void SaveWorld()
    {
        XMLHelper h = new XMLHelper();
        WorldContainer wc = new WorldContainer();
        wc.worldInfo.Add(worldInfo);
        h.SaveWorld(wc);
    }
    public void SaveUnits()
    {
        XMLHelper h = new XMLHelper();
        UnitContainer uc = new UnitContainer();
        uc.Units = units;
        h.SaveUnits(uc);
    }
    
    public void SaveLevels()
    {
        XMLHelper h = new XMLHelper();
        LevelContainer lc = new LevelContainer();
        lc.levels = levels;
        h.SaveLevels(lc);
    }
    public void SaveParty()
    {
        XMLHelper h = new XMLHelper();
        PartySlotContainer pc = new PartySlotContainer();
        pc.slots = partySlots;
        h.SaveCurrentParty(pc);
    }
    //public void SaveCurrentTeam(List<GameObject> currentParty)
    //{

    //    List<PartySlot> slots = new List<PartySlot>();
    //    for (int i = 0; i < currentParty.Count; i++)
    //    {
    //        PartySlot item = new PartySlot();
    //        item.unitID = currentParty[i].GetComponent<UnitSlot>().unitID;
    //        this.partySlots[i].unitID = item.unitID;
    //        item.buttonID = i;
    //        slots.Add(item);
    //    }
    //    XMLHelper h = new XMLHelper();
    //    PartySlotContainer pc = new PartySlotContainer();
    //    pc.slots = slots;
    //    h.SaveCurrentParty(pc);

    //}
    public void NewUnit()
    {
        Unit newUnit = new Unit();
        newUnit.Initialize();
        newUnit.id = units.Count;
        units.Add(newUnit);
        SaveUnits();
    }


    public void AddcurrentCoinsAndCrystalToGameWorld(int coins, int crystals)
    {
        worldInfo.gold = coins;
        worldInfo.crystal = crystals;
    }
}





public static class ExtensionMethods
{
    public static int RoundOff(this int number)
    {
        int remainder = number % 10;
        number += (remainder < 10 / 2) ? -remainder : (10 - remainder);
        return number;
    }
}
