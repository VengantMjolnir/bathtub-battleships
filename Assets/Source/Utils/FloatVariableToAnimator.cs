using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RogueEyebrow.Variables;

namespace RogueEyebrow.Utils
{
    public class FloatVariableToAnimator : MonoBehaviour
    {
        public string animatorKey;
        public Animator controller;
        public FloatReference input;
        public float visualOutput;

        private int keyHash;

        // Use this for initialization
        void Start()
        {
            keyHash = Animator.StringToHash(animatorKey);
        }

        // Update is called once per frame
        void Update()
        {
            controller.SetFloat(keyHash, input.Value);

            visualOutput = input.Value;
        }
    }
}