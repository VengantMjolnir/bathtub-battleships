using System.Collections.Generic;
using UnityEngine;

namespace RogueEyebrow.Sets
{
    public abstract class RuntimeSet<T> : ScriptableObject
    {
        public bool sorted = false;
        public List<T> Items = new List<T>();

        public void Add(T thing)
        {
            if (!Items.Contains(thing))
            {
                Items.Add(thing);
                Items.Sort();
            }
        }

        public void Remove(T thing)
        {
            if (Items.Contains(thing))
            {
                Items.Remove(thing);
            }
        }
        
    }
}