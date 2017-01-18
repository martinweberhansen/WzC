using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.Collections.Generic;

[XmlRoot("WorldCollection")]
public class WorldContainer
{
    [XmlArray("World")]
    [XmlArrayItem("Worldinfo")]
    public List<World> worldInfo = new List<World>();

}
