using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Assets.Scripts.Events
{
    public class GameEventChoice
    {
        public const string DefaultChoiceText = "Continue...";

        [XmlAttribute("name")]
        public string Name;

        [System.ComponentModel.DefaultValueAttribute("Continue...")]
        public string Text;

        [XmlIgnore]
        // This property is used to control the serialization of the Text field. Allows mutual exclusion of the two elements
        public bool TextSpecified { get { return TextList == null || TextList.Count == 0; } set { return; } }

        [XmlArray("TextList")]
        [XmlArrayItem("Text")]
        //[XmlElementAttribute(IsNullable = false)]
        //public GameTextList TextList;
        public List<String> TextList = new List<String>();

        [XmlIgnore]
        public bool TextListSpecified { get { return TextList.Count > 0; } set { return; } }

        [XmlElementAttribute(IsNullable = false)]
        public GameEventReference ReferenceEvent;

        [XmlIgnore]
        // This property is used to control the serialization of the ReferenceEvent field. Allows mutual exclusion of the two elements
        public bool ReferenceEventSpecified { get { return InlineEvent == null; } set { return; } }

        [XmlElementAttribute(IsNullable = false)]
        public GameEvent InlineEvent;

        public GameEventChoice()
        {
            Text = "Continue...";
        }

        public GameEventChoice(string text)
        {
            Text = text;
        }
    }
}
