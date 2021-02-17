using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts.Events
{
    [RequireComponent(typeof(Dropdown))]
    public class GameEventList : MonoBehaviour
    {
        public string EventFilename = String.Empty;

        private static string XML_EXTENSION = ".xml";
        private Dropdown _PopupList = null;
        private GameEventContainer _EventContainer = null;

        public void Start()
        {
            _PopupList = GetComponent<Dropdown>();

            if (String.IsNullOrEmpty(EventFilename) == false)
            {
                try
                {
                    LoadEventFile(EventFilename);
                    _PopupList.ClearOptions();
                    List<String> options = new List<string>();
                    foreach (GameEvent gameEvent in _EventContainer.GameEvents)
                    {
                        options.Add(gameEvent.Name);
                    }
                    _PopupList.AddOptions(options);
                    _PopupList.value = _PopupList.value = 0;
                }
                catch (Exception e)
                {
                    Debug.LogError("Error Deserializing Event Data! \n" + e.Message);
                }

            }
        }

        public void LoadEventFile(string filename)
        {
            FileStream fs = new FileStream(Application.dataPath + "/" + filename + XML_EXTENSION, FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(GameEventContainer));

            _EventContainer = serializer.Deserialize(fs) as GameEventContainer;
            _EventContainer.UpdateChoiceReferences();
            fs.Close();
        }

        private String GetCurrentValue()
        {
            return _PopupList.options[_PopupList.value].text;
        }

        public void SetEventTextToLabel(Text label)
        {
            if (_EventContainer == null)
            {
                return;
            }

            GameEvent gameEvent = _EventContainer.GetEvent(GetCurrentValue());
            if (gameEvent != null)
            {
                string xmlText = string.Empty;

                try
                {
                    StringWriter stringWriter = new StringWriter();
                    XmlSerializer serializer = new XmlSerializer(typeof(GameEvent));

                    XmlWriterSettings xws = new XmlWriterSettings();
                    xws.OmitXmlDeclaration = true;
                    xws.Indent = true;
                    xws.Encoding = Encoding.UTF8;
                    XmlWriter xw = XmlWriter.Create(stringWriter, xws);
                    serializer.Serialize(xw, gameEvent, _EventContainer.Namespaces);

                    xmlText = stringWriter.ToString();
                }
                catch (Exception e)
                {
                    Debug.LogError("Failure to read GameEvent XML! " + e.Message);
                }

                label.text = xmlText;
            }
        }

        public GameEvent GetSelectedEvent()
        {
            return _EventContainer.GetEvent(GetCurrentValue());
        }
    }
}
