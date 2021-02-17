using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RogueEyebrow
{
    [CreateAssetMenu]
    public class Weapon : ScriptableObject
    {
        public GameObject prefab;
        public float damage;
        public Projectile projectile;
    }
}
