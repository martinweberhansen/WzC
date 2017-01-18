using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using UnityEngine.UI;
using System;

[System.Serializable]
public class Item {

    [XmlAttribute("id")]
    public int id;
    [XmlIgnore]
    public Location location { get; set; }

    [XmlAttribute("location")]
    public string locationString
    {
        get { return location.ToString(); }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                location = default(Location);
            }
            else
            {
                location = (Location)Enum.Parse(typeof(Location), value);
            }
        }
    }

    [XmlAttribute("name")] //!!! assetName
    public string assetName;
    [XmlAttribute("damage")]
    public int damage;
    
    [XmlAttribute("movementspeed")]
    public float movementSpeed;
    [XmlAttribute("health")]
    public int health;

    [XmlAttribute("price")]
    public int price;
    //[XmlAttribute("resistanceAgainstPiercing")]
    //public int resistancePiercing;
    //[XmlAttribute("resistanceAgainstSlashing")]
    //public int resistanceSlashing;
    //[XmlAttribute("resistanceAgainstBlunt")]
    //public int resistanceBlunt;
    [XmlIgnore]
    public GameObject _gameObject;
    [XmlIgnore]
    public Texture _texture;

    public void LoadAsset()
    {
        _gameObject = (GameObject)Resources.Load(id.ToString(), typeof(GameObject));
        _texture = (Texture)Resources.Load(id.ToString(), typeof(Texture));
    }

    public int GetDamage()
    {
        return damage;
    }
}
