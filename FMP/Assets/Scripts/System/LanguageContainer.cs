using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

[XmlRoot("LanguageLines")]
public class LanguageContainer {
    [XmlArray("Lines")]
    [XmlArrayItem("Line")]
    public List<TextLine> lines = new List<TextLine>();

    public static LanguageContainer Load(string path) {
        TextAsset _xml = Resources.Load<TextAsset>(path);

        XmlSerializer serialiser = new XmlSerializer(typeof(LanguageContainer));

        StringReader reader = new StringReader(_xml.text);

        LanguageContainer lines = serialiser.Deserialize(reader) as LanguageContainer;

        reader.Close();

        return lines;
    }

    public TextLine GetLine(string name) {
        foreach (TextLine line in lines) {
            if (line.name == name) {
                return line;
            }
        }
        return null;
    }
}
