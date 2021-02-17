using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RogueEyebrow.Variables;
using RogueEyebrow.Sets;

public class SelectTargetInView : MonoBehaviour
{
    public ThingRuntimeSet targetSet;
    public Transform target;
    public TransformVariable targetVariable;

    private Transform cachedTransform;

    // Use this for initialization
    void Start()
    {
        cachedTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = cachedTransform.forward;
        if (targetSet.Items.Count == 0)
        {
            target = null;
        } else
        {
            float largestDot = -1f;
            for(int i = 0; i < targetSet.Items.Count; ++i)
            {
                Transform potentialTarget = targetSet.Items[i].transform;
                Vector3 dir = potentialTarget.position - cachedTransform.position;
                float dot = Vector3.Dot(forward, dir.normalized);
                if (dot > largestDot)
                {
                    largestDot = dot;
                    target = potentialTarget;
                }
            }
        }

        if (targetVariable != target)
        {
            targetVariable.SetValue(target);
        }
    }
}
