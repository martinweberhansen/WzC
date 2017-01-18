using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("ItemCollection")]
public class ItemContainer
{
    [XmlArray("items")]
    [XmlArrayItem("item")]
    public List<Item> Items = new List<Item>();
}
