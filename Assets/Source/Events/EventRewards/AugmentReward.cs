using System.Collections;
using System.Xml.Serialization;

namespace Assets.Scripts.Events
{
    public class AugmentReward 
    {
        [XmlAttribute("name")]
        public string Name;

        public AugmentReward(string name)
        {
            Name = name;
        }

        public AugmentReward()
        {
            Name = "";
        }
    }
}
