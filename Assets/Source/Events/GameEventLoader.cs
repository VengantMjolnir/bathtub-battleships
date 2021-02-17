using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Assets.Scripts.Events;
using System.Xml;
using System.Text;

[ExecuteInEditMode]
public class GameEventLoader : MonoBehaviour 
{
    private static string XML_EXTENSION = ".xml";

    public string filename = "";
    public bool Save = false;
    public bool Load = false;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Load && filename != "")
        {
            LoadEventFile(filename);
        }
        Load = false;
        if (Save && filename != "")
        {
            SaveEventFile(filename);
        }
        Save = false;
	}

    public void SaveEventFile(string filename)
    {
        FileStream fs = new FileStream(Application.dataPath + "/" + filename + XML_EXTENSION, FileMode.Create);


        XmlSerializer serializer = new XmlSerializer(typeof(GameEventContainer));

        XmlWriterSettings xws = new XmlWriterSettings();
        xws.OmitXmlDeclaration = true;
        xws.Encoding = Encoding.UTF8;
        XmlTextWriter xtw = (XmlTextWriter)XmlTextWriter.Create(fs, xws);
        xtw.Formatting = Formatting.Indented;

        GameEventContainer container = new GameEventContainer();
        GameEvent xmlEvent = new GameEvent();
        xmlEvent.Name = "TEST";
        xmlEvent.Text = "This is a Test!";
        xmlEvent.Augment = new AugmentReward("TEST_REWARD");
        container.GameEvents.Add(xmlEvent);
        
        xmlEvent = new GameEvent();
        xmlEvent.Name = "TEST2";
        xmlEvent.Text = "This is also a Test!";
        xmlEvent.Choice = new GameEventChoice[1];
        xmlEvent.Choice[0] = new GameEventChoice("Ok...");
        xmlEvent.Choice[0].InlineEvent = new GameEvent("Its crazy up in here!");
        container.GameEvents.Add(xmlEvent);

        xmlEvent = new GameEvent();
        xmlEvent.Name = "TEST3";
        xmlEvent.Text = "This is a further Test!";
        xmlEvent.Choice = new GameEventChoice[2];
        xmlEvent.Choice[0] = new GameEventChoice();
        xmlEvent.Choice[0].ReferenceEvent = new GameEventReference(container.GameEvents[0].Name);
        List<string> textList = new List<string>();
        textList.Add("Blah");
        textList.Add("Blather");
        textList.Add("Blatherest");
        xmlEvent.Choice[0].TextList = textList;
        xmlEvent.Choice[1] = new GameEventChoice("Not what I thought it would be...");
        xmlEvent.Choice[1].ReferenceEvent = new GameEventReference(container.GameEvents[1].Name);
        container.GameEvents.Add(xmlEvent);

        serializer.Serialize(xtw, container, container.Namespaces);
        fs.Close();
    }

    public void LoadEventFile(string filename)
    {
        FileStream fs = new FileStream(Application.dataPath + "/" + filename + XML_EXTENSION, FileMode.Open);
        XmlSerializer serializer = new XmlSerializer(typeof(GameEventContainer));

        GameEventContainer container = serializer.Deserialize(fs) as GameEventContainer;
        container.UpdateChoiceReferences();
        fs.Close();
    }
}
