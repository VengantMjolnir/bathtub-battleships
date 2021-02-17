using System;
using UnityEngine;
using RogueEyebrow.Variables;

public class FollowTransformVariable : MonoBehaviour
{
    public TransformVariable target;
    public Vector3 offset = new Vector3(0f, 0f, 0f);
    public Vector3 scale = new Vector3(1f, 1f, 1f);
    
    private void LateUpdate()
    {
        if (target != null && target.Value != null)
        {
            Vector3 pos = target.Value.position;
            pos.Scale(scale);
            pos += offset;
            transform.position = pos;
        }
    }
}
