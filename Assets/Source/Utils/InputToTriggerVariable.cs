using UnityEngine;
using System.Collections;
using RogueEyebrow.Variables;

public class InputToTriggerVariable : MonoBehaviour
{
    public string input;
    public TriggerVariable output;
    public InputEdge inputEdge = InputEdge.OnPress;

    public enum InputEdge
    {
        OnPress,
        OnRelease
    }

    void LateUpdate()
    {
        if (input == "")
        {
            return;
        }
        bool value = false;
        if (inputEdge == InputEdge.OnPress)
        {
            value = Input.GetButtonDown(input);
        } else if (inputEdge == InputEdge.OnRelease)
        {
            value = Input.GetButtonUp(input);
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
