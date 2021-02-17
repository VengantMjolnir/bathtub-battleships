using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RogueEyebrow.Variables;

namespace RogueEyebrow.Utils
{
    public class InputToAnimator : MonoBehaviour
    {
        public string inputAxis;
        public string animatorKey;
        public Animator controller;

        private int keyHash;

        // Use this for initialization
        void Start()
        {
            keyHash = Animator.StringToHash(animatorKey);
        }

        // Update is called once per frame
        void Update()
        {
            float value = Input.GetAxis(inputAxis);
            controller.SetFloat(keyHash, value);
        }
    }
}