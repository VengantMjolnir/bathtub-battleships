using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RogueEyebrow.Variables;

namespace RogueEyebrow.Utils
{
    public class InputToFloatReference : MonoBehaviour
    {
        public string inputAxis;
        public FloatReference output;
#if UNITY_EDITOR
        [Range(-1.0f, 1.0f)]
        public float test;
#endif

        // Use this for initialization
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (inputAxis == "")
            {
#if UNITY_EDITOR
                output.Variable.SetValue(test);
#endif
                return;
            }
            float value = Input.GetAxis(inputAxis);
            output.Variable.SetValue(value);
#if UNITY_EDITOR
            test = output.Value;
#endif
        }
    }
}