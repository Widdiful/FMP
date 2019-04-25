using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

public class TextLine {

    [XmlAttribute("name")]
    public string name;

    [XmlElement("English")]
    public string english;

    [XmlElement("Japanese")]
    public string japanese;
}
