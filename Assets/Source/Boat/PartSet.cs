using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RogueEyebrow
{
    [CreateAssetMenu]
    public class PartSet : ScriptableObject
    {
        List<Part> Choices = new List<Part>();
    }
}