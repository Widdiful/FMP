using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Microgame {
    [XmlAttribute("name")]
    public string name;

    [XmlElement("Landscape")]
    public bool isLandscape;

    [XmlElement("Tap")]
    public bool useTap;

    [XmlElement("Motion")]
    public bool useMotion;

    [XmlElement("Mic")]
    public bool useMic;

    [XmlElement("Proximity")]
    public bool useProximity;

    [XmlElement("Extra")]
    public bool hasExtra;

    [XmlElement("Hint")]
    public string hint;
}
