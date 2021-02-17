using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Assets.Scripts.Events
{
    [XmlRoot("EventCollection")]
    public class GameEventContainer
    {
        [XmlArray("Events")]
        [XmlArrayItem("Event")]
        public List<GameEvent> GameEvents = new List<GameEvent>();

        private Dictionary<string, GameEvent> _EventDictionary = new Dictionary<string, GameEvent>();

        public GameEventContainer()
        {
            this._Namespaces = new XmlSerializerNamespaces(new XmlQualifiedName[] {
                new XmlQualifiedName(string.Empty, "evt:Tides")
            });
        }

        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces Namespaces
        {
            get { return this._Namespaces; }
        }
        private XmlSerializerNamespaces _Namespaces;

        public void UpdateChoiceReferences()
        {
            if (GameEvents.Count != 0)
            {
                foreach (GameEvent ge in GameEvents)
                {
                    ge.Container = this;
                    if (_EventDictionary.ContainsKey(ge.Name))
                    {
                        System.Diagnostics.Debug.WriteLine("Error! Event being added already exists in event table: " + ge.Name);
                        continue;
                    }
                    _EventDictionary.Add(ge.Name, ge);
                }
            }

            foreach (GameEvent ge in GameEvents)
            {
                ge.UpdateChoiceReferences();
            }
        }

        public GameEvent GetEvent(String name)
        {
            GameEvent gameEvent = _EventDictionary[name];
            return gameEvent;
        }
    }
}
