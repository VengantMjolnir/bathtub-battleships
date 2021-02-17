using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RogueEyebrow.Variables;

namespace RogueEyebrow.Utils
{
    public class InputManagerToFloat : MonoBehaviour
    {
        public InputManager.Button negativeButton;
        public InputManager.Button positiveButton;
        public PlayerID player;
        public FloatReference output;
        
        // Update is called once per frame
        void Update()
        {
            float value = 0f;
            if (InputManager.GetButton(positiveButton, player))
            {
                value += 1f;
            }
            if (InputManager.GetButton(negativeButton, player))
            {
                value -= 1f;
            }
            output.Variable.SetValue(value);
        }
    }
}