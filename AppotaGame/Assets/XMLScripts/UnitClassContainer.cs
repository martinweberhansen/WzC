using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

// checked once
[XmlRoot("classCollection")]
public class UnitClassContainer
{
    [XmlArray("classes")]
    [XmlArrayItem("class")]
    public List<UnitClass> classes = new List<UnitClass>();

}
