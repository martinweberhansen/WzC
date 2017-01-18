using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

[System.Serializable]
public class World {

    [XmlAttribute("gold")]
    public int gold;
    [XmlAttribute("crystal")]
    public int crystal;
    [XmlAttribute("slotsopen")]
    public int slotsOpen;
}
