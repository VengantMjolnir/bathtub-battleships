using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RogueEyebrow.Attributes;

namespace RogueEyebrow.Variables
{
    [CreateAssetMenu]
    public class Vector3Variable : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif

        [Compact]
        public Vector3 Value;

    }
}