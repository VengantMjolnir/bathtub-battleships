using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RogueEyebrow
{
    [CreateAssetMenu]
    public class Projectile : ScriptableObject
    {
        public GameObject prefab;
        public GameObject muzzleFlash;
        public GameObject impactObject;
    }
}
