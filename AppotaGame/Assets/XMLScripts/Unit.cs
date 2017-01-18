using UnityEngine;
using System.Collections;
using System;
using System.Xml.Serialization;
using UnityEngine.UI;

[System.Serializable]
public class Unit
{
    [XmlAttribute("id")]
    public int id;
    [XmlAttribute("name")]
    public string name;
    [XmlAttribute("headID")]
    public int headID;
    [XmlAttribute("bodyID")]
    public int bodyID;
    [XmlAttribute("weaponID")]
    public int weaponID;
    [XmlAttribute("classID")]
    public int unitClassID;

    [XmlIgnore]
    Item headItem;
    [XmlIgnore]
    Item bodyItem;
    [XmlIgnore]
    Item weaponItem;
    [XmlIgnore]
    public Sprite texture;

    public Unit()
    {
        this.headID = 0;
        this.bodyID = 0;
        this.weaponID = 0;
    }
    
    public void LoadTextures()
    {
        Sprite tex = (Sprite)Resources.Load("CharacterAssets/helmets/pictures/helmetPicture"+this.headID, typeof(Sprite));
        this.texture = tex;
    }
    public void Initialize()
    {
        headItem = WorldObject.GetworldObject.items.Find(item => item.id == headID);
        bodyItem = WorldObject.GetworldObject.items.Find(item => item.id == bodyID);
        weaponItem = WorldObject.GetworldObject.items.Find(item => item.id == weaponID);
    }
    public void setAssetIDs(int headID,int bodyID,int weaponID)
    {
        this.headID = headID;
        this.bodyID = bodyID;
        this.weaponID = weaponID;
    }
}