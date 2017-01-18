using UnityEngine;
using System.Collections;
using System.Xml;
using System;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;

public class XMLHelper
{
    //levels
    public LevelContainer LoadLevels()
    {
        if (File.Exists(Application.persistentDataPath + "/levels.xml"))
        {
            LevelContainer levels;
            levels = LoadLevelsFromPersistantPath();
            return levels;
        }
        else
        {
            TextAsset text = (TextAsset)Resources.Load("levels", typeof(TextAsset));
            File.WriteAllBytes(Application.persistentDataPath + "/levels.xml", text.bytes);
            LevelContainer levels;
            levels = LoadLevelsFromPersistantPath();
            return levels;
        }
    }
    LevelContainer  LoadLevelsFromPersistantPath()
    {
        LevelContainer levels;
        Debug.Log("Loading From - " + Application.persistentDataPath + "/levels.xml");
        string readAllText = File.ReadAllText(Application.persistentDataPath + "/levels.xml");
        var serializer = new XmlSerializer(typeof(LevelContainer));
        using (var reader = new StringReader(readAllText))
        {
            levels = serializer.Deserialize(reader) as LevelContainer;
        }
        return levels;
    }
    public void     SaveLevels(LevelContainer lc)
    {
        var serializer = new XmlSerializer(lc.GetType());
        using (var stream = new StreamWriter(Application.persistentDataPath + "/levels.xml", false))
        {
            serializer.Serialize(stream.BaseStream, lc);
        }
    }
    
    //units
    public UnitContainer LoadUnits()
    {
        if (File.Exists(Application.persistentDataPath + "/db.xml"))//load from persistent path
        {
            UnitContainer units;
            units = LoadUnitsFromPersistantPath();
            return units;
        }
        else
        {
            //move from resources to persistent path
            TextAsset text = (TextAsset)Resources.Load("db", typeof(TextAsset));
            File.WriteAllBytes(Application.persistentDataPath + "/db.xml", text.bytes);
            //load from persistantpath
            UnitContainer units;
            units = LoadUnitsFromPersistantPath();
            return units;
        }
    }
    UnitContainer        LoadUnitsFromPersistantPath()
    {
        //load Units
        UnitContainer units;
        Debug.Log("Loading From - " + Application.persistentDataPath + "/db.xml");
        string readAllText = File.ReadAllText(Application.persistentDataPath + "/db.xml");
        var serializer = new XmlSerializer(typeof(UnitContainer));
        using (var reader = new StringReader(readAllText))
        {
            units = serializer.Deserialize(reader) as UnitContainer;
        }
        foreach (Unit item in units.Units)
        {
            item.Initialize();
        }
        return units;
    }
    public void          SaveUnits(UnitContainer uc)
    {
        var serializer = new XmlSerializer(uc.GetType());
        using (var stream = new StreamWriter(Application.persistentDataPath + "/db.xml", false))
        {
            serializer.Serialize(stream.BaseStream, uc);
        }
    }
    
    //party slots
    public PartySlotContainer LoadPartySlots()
    {
        if (File.Exists(Application.persistentDataPath + "/party.xml"))
        {
            PartySlotContainer slots;
            slots = LoadCurrentPartyFromPersistantPath();
            return slots;
        }
        else
        {
            TextAsset text = (TextAsset)Resources.Load("party", typeof(TextAsset));
            File.WriteAllBytes(Application.persistentDataPath + "/party.xml", text.bytes);
            PartySlotContainer slots;
            slots = LoadCurrentPartyFromPersistantPath();
            return slots;
        }
    } 
    PartySlotContainer        LoadCurrentPartyFromPersistantPath()
    {
        PartySlotContainer pc;
        Debug.Log("Loading from - " + Application.persistentDataPath + "/party.xml");
        string readAllText = File.ReadAllText(Application.persistentDataPath + "/party.xml");
        var serializer = new XmlSerializer(typeof(PartySlotContainer));
        using (var reader = new StringReader(readAllText))
        {
            pc = serializer.Deserialize(reader) as PartySlotContainer;
        }
        return pc;
    }
    public void               SaveCurrentParty(PartySlotContainer pc)
    {
        var serializer = new XmlSerializer(pc.GetType());
        using (var stream = new StreamWriter(Application.persistentDataPath + "/party.xml", false))
        {
            serializer.Serialize(stream.BaseStream, pc);
        }
    }
    
    //WorldContent
    public WorldContainer LoadGameWorld()
    {
        if (File.Exists(Application.persistentDataPath + "/gameworld.xml"))//load from persistent path
        {
            WorldContainer worldinfo;
            worldinfo = LoadGameworldFromPersistantPath();
            return worldinfo;
        }
        else
        {
            //move from resources to persistent path
            TextAsset text = (TextAsset)Resources.Load("gameworld", typeof(TextAsset));
            File.WriteAllBytes(Application.persistentDataPath + "/gameworld.xml", text.bytes);
            //load from persistantpath
            WorldContainer worldinfo;
            worldinfo = LoadGameworldFromPersistantPath();
            return worldinfo;
        }
    }
    WorldContainer        LoadGameworldFromPersistantPath()
    {
        WorldContainer worldinfo;
        Debug.Log("Loading From - " + Application.persistentDataPath + "/gameworld.xml");
        string readAllText = File.ReadAllText(Application.persistentDataPath + "/gameworld.xml");
        var serializer = new XmlSerializer(typeof(WorldContainer));
        using (var reader = new StringReader(readAllText))
        {
            worldinfo = serializer.Deserialize(reader) as WorldContainer;
        }
        return worldinfo;
    }
    public void           SaveWorld(WorldContainer wc)
    {
        var serializer = new XmlSerializer(wc.GetType());
        using (var stream = new StreamWriter(Application.persistentDataPath + "/gameworld.xml", false))
        {
            serializer.Serialize(stream.BaseStream, wc);
        }
    }
    
    //items
    public ItemContainer LoadItems()
    {
        if (File.Exists(Application.persistentDataPath + "/items.xml"))//load from persistent path
        {
            ItemContainer iteminfo = LoadItemsFromPersistantPath();
            return iteminfo;
        }
        else
        {
            //move from resources to persistent path
            TextAsset text = (TextAsset)Resources.Load("items", typeof(TextAsset));
            File.WriteAllBytes(Application.persistentDataPath + "/items.xml", text.bytes);
            //load from persistantpath
            ItemContainer iteminfo;
            iteminfo = LoadItemsFromPersistantPath();
            return iteminfo;
    }
}
    ItemContainer        LoadItemsFromPersistantPath()
    {
        ItemContainer iteminfo;
        Debug.Log("Loading From - " + Application.persistentDataPath + "/items.xml");
        string readAllText = File.ReadAllText(Application.persistentDataPath + "/items.xml");
        var serializer = new XmlSerializer(typeof(ItemContainer));
        using (var reader = new StringReader(readAllText))
        {
            iteminfo = serializer.Deserialize(reader) as ItemContainer;
        }
        return iteminfo;
    }
    public void          SaveItems(ItemContainer wc)
    {
        var serializer = new XmlSerializer(wc.GetType());
        using (var stream = new StreamWriter(Application.persistentDataPath + "/items.xml", false))
        {
            serializer.Serialize(stream.BaseStream, wc);
        }
    }
    
    //items
    public UnitClassContainer LoadClasses()
    {
        if (File.Exists(Application.persistentDataPath + "/class.xml"))//load from persistent path
        {
            UnitClassContainer classinfo;
            classinfo = LoadClassesFromPersistantPath();
            return classinfo;
        }
        else
        {
            //move from resources to persistent path
            TextAsset text = (TextAsset)Resources.Load("class", typeof(TextAsset));
            File.WriteAllBytes(Application.persistentDataPath + "/class.xml", text.bytes);
            //load from persistantpath
            UnitClassContainer classinfo;
            classinfo = LoadClassesFromPersistantPath();
            return classinfo;
        }
    }
    UnitClassContainer        LoadClassesFromPersistantPath()
    {
        UnitClassContainer iteminfo;
        Debug.Log("Loading From - " + Application.persistentDataPath + "/class.xml");
        string readAllText = File.ReadAllText(Application.persistentDataPath + "/class.xml");
        var serializer = new XmlSerializer(typeof(UnitClassContainer));
        using (var reader = new StringReader(readAllText))
        {
            iteminfo = serializer.Deserialize(reader) as UnitClassContainer;
        }
        return iteminfo;
    }

}
