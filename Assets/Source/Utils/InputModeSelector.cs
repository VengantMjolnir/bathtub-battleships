using UnityEngine;
using System.Collections;

public class InputModeSelector : MonoBehaviour
{
    public InputManager.InputMode inputMode;

    // Use this for initialization
    void Start()
    {
        InputManager.SetInputMode(inputMode);
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.inputMode != inputMode)
        {
            InputManager.SetInputMode(inputMode);
        }
    }
}
