using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RogueEyebrow.Variables
{
    [CreateAssetMenu]
    public class TransformVariable : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif
        public Transform Value;

        public void SetValue(Transform value)
        {
            Value = value;
        }

        public void SetValue(TransformVariable value)
        {
            Value = value.Value;
        }

        public static implicit operator Transform(TransformVariable variable)
        {
            return variable.Value;
        }
    }
}