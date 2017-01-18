using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("LevelCollection")]
public class LevelContainer {
    [XmlArray("Levels")]
    [XmlArrayItem("level")]
    public List<Level> levels = new List<Level>();
}
