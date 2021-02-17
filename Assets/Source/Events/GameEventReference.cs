using UnityEngine;
using System.Collections;
using System.Text;
using System.Xml.Serialization;

namespace Assets.Scripts.Events
{
    public class GameEventReference
    {
        [XmlAttribute("load")]
        public string ReferenceName;

        [XmlIgnore]
        public GameEvent ReferredEvent;

        public GameEventReference(string referenceName)
        {
            ReferenceName = referenceName;
        }

        public GameEventReference()
        {
            ReferenceName = "";
        }
    }
}
