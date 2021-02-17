using UnityEngine;

namespace RogueEyebrow.Variables
{
    public delegate void BoolVariableChangeDelegate(bool value);
    [CreateAssetMenu]
    public class BooleanVariable : ScriptableObject
    {
        private BoolVariableChangeDelegate onChange;

#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif
        public bool Value;

        public void SetValue(bool value)
        {
            Value = value;
            if (onChange != null)
            {
                onChange(Value);
            }
        }

        public void SetValue(BooleanVariable value)
        {
            Value = value.Value;
            if (onChange != null)
            {
                onChange(Value);
            }
        }

        public void RegisterForChange(BoolVariableChangeDelegate changeDelegate)
        {
            onChange += changeDelegate;
        }

        public void UnregisterForChange(BoolVariableChangeDelegate changeDelegate)
        {
            onChange -= changeDelegate;
        }

        public static implicit operator bool(BooleanVariable reference)
        {
            return reference.Value;
        }
    }
}