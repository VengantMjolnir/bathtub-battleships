using UnityEngine;
using System.Collections;
using RogueEyebrow.Variables;

public class InputManagerToTrigger : MonoBehaviour
{
    public InputManager.Button inputButton;
    public PlayerID player;
    public TriggerVariable output;
    public InputEdge inputEdge = InputEdge.OnPress;

    public enum InputEdge
    {
        OnPress,
        OnRelease
    }

    void LateUpdate()
    {
        if (inputButton == InputManager.Button.UNASSIGNED)
        {
            return;
        }
        bool value = false;
        if (inputEdge == InputEdge.OnPress)
        {
            value = InputManager.GetButtonDown(inputButton, player);
        } else if (inputEdge == InputEdge.OnRelease)
        {
            value = InputManager.GetButtonUp(inputButton, player);
        }

        if (value && output != null)
        {
            output.Trigger();
        }
    }

    [ContextMenu("Trigger")]
    public void TestTrigger()
    {
        if (output != null)
        {
            output.Trigger();
        }
    }
}
