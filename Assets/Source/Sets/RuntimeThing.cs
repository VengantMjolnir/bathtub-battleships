using UnityEngine;
using System;

namespace RogueEyebrow.Sets
{
    public class RuntimeThing : MonoBehaviour, IComparable<RuntimeThing>
    {
        public ThingRuntimeSet RuntimeSet;
        public int SortValue;

        private void OnEnable()
        {
            RuntimeSet.Add(this);
        }

        private void OnDisable()
        {
            RuntimeSet.Remove(this);
        }

        public int CompareTo(RuntimeThing thing)
        {
            if (thing == null)
            {
                return 1;
            } else
            {
                return this.SortValue.CompareTo(thing.SortValue);
            }
        }
    }
}