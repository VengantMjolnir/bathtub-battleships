using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RogueEyebrow
{
    [CreateAssetMenu]
    public class Part : ScriptableObject
    {
        public PartType Type;
        public GameObject Prefab;
    }
}