using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

[XmlRoot("GameCollection")]
public class GameContainer {
    [XmlArray("Games")]
    [XmlArrayItem("Game")]
    public List<Microgame> microgames = new List<Microgame>();

    public static GameContainer Load(string path) {
        TextAsset _xml = Resources.Load<TextAsset>(path);

        XmlSerializer serialiser = new XmlSerializer(typeof(GameContainer));

        StringReader reader = new StringReader(_xml.text);

        GameContainer games = serialiser.Deserialize(reader) as GameContainer;

        reader.Close();

        return games;
    }
}
