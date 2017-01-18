using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

[System.Serializable]
public class Level {

    [XmlAttribute("id")]
    public int id;
    [XmlAttribute("name")]
    public string name;
    [XmlAttribute("description")]
    public string Description;
    [XmlAttribute("rewardGold")]
    public int rewardGold;
    [XmlAttribute("rewardCrystal")]
    public int rewardCrystal;
    [XmlAttribute("locked")]
    public bool locked;
    [XmlAttribute("difficultymodifier")]
    public float difficultyModifier;
}
