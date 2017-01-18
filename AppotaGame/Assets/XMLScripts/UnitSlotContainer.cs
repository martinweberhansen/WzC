using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("partyslotcontainer")]
public class PartySlotContainer {
    [XmlArray("slots")]
    [XmlArrayItem("slot")]
    public List<PartySlot> slots = new List<PartySlot>();

}
