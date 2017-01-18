using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("MonsterCollection")]
public class UnitContainer
{
    [XmlArray("Units")]
    [XmlArrayItem("Unit")]
    public List<Unit> Units = new List<Unit>();
}
