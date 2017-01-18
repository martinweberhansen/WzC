using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Xml.Serialization;

[System.Serializable]
public class UnitClass
{
    [XmlAttribute("id")]
    public int id;
    [XmlAttribute("attackRange")]
    public float attackRange;
    [XmlAttribute("damage")]
    public float damage;
    [XmlAttribute("movementspeed")]
    public float movementSpeed;
    [XmlAttribute("resistanceAgainstPiercing")]
    public float resistancePiercing;
    [XmlAttribute("resistanceAgainstSlashing")]
    public float resistanceSlashing;
    [XmlAttribute("resistanceAgainstBlunt")]
    public float resistanceBlunt;
}
