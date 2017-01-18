using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

[System.Serializable]
public class PartySlot {

    [XmlAttribute("unitid")]
    public int unitID;
    [XmlAttribute("buttonid")]
    public int buttonID;
}
