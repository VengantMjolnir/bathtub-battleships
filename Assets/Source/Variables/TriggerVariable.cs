using UnityEngine;
using System.Collections;

namespace RogueEyebrow.Variables
{
    public delegate void TriggerDelegate();

    [CreateAssetMenu]
    public class TriggerVariable : ScriptableObject
    {
        private TriggerDelegate onTrigger;

        public void Trigger()
        {
            if (onTrigger != null)
            {
                onTrigger();
            }
        }

        public void RegisterForTrigger(TriggerDelegate triggerDelegate)
        {
            onTrigger += triggerDelegate;
        }

        public void UnregisterForTrigger(TriggerDelegate triggerDelegate)
        {
            onTrigger -= triggerDelegate;
        }
    }
}