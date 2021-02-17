using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Assets.Scripts.Events
{
    public class GameEvent
    {
        private Dictionary<string, GameEvent> _EventDictionary = new Dictionary<string, GameEvent>();

        [XmlAttribute("name")]
        public string Name;

        [XmlElementAttribute(IsNullable = false)]
        public string Text;

        [XmlIgnore]
        // This property is used to control the serialization of the Text field. Allows mutual exclusion of the two elements
        public bool TextSpecified { get { return TextList == null || TextList.Count == 0; } set { return; } }

        [XmlArray("TextList")]
        [XmlArrayItem("Text")]
        public List<String> TextList = new List<String>();

        [XmlIgnore]
        public bool TextListSpecified { get { return TextList.Count > 0; } set { return; } }

        [XmlElementAttribute(IsNullable = false)]
        public AugmentReward Augment;

        [XmlElement(ElementName = "Choice", IsNullable = false)]
        public GameEventChoice[] Choice;

        [XmlIgnore]
        public GameEventContainer Container = null;

        public GameEvent()
        {
        }

        public GameEvent(string text)
        {
            Text = text;
        }

        public void UpdateChoiceReferences()
        {
            if (Choice != null)
            {
                for (int i = 0; i < Choice.Length; ++i)
                {
                    if (Choice[i].InlineEvent != null)
                    {
                        Choice[i].InlineEvent.UpdateChoiceReferences();
                    }
                    else if (Choice[i].ReferenceEvent != null)
                    {
                        if (Container == null)
                        {
                            // Log error and return
                            return;
                        }
                        if (Choice[i].ReferenceEvent.ReferredEvent == null && !String.IsNullOrEmpty(Choice[i].ReferenceEvent.ReferenceName))
                        {
                            Choice[i].ReferenceEvent.ReferredEvent = Container.GetEvent(Choice[i].ReferenceEvent.ReferenceName);
                        }
                    }
                }
            }
        }
    }
}
