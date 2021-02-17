using UnityEngine;
using RogueEyebrow.Sets;
using RogueEyebrow.Variables;

public class AITargetSelector : MonoBehaviour
{
    public ThingRuntimeSet pathNavSet;
    public TransformVariable pathTarget;
    public FloatReference arriveDistance;

    private Transform cachedTransform;
    private int index;

    private void Start()
    {
        cachedTransform = transform;
        index = 0;
    }

    private void Update()
    {
        if (pathNavSet != null && pathNavSet.Items.Count > 0)
        {
            index = VerifyIndex(pathNavSet, index);
            RuntimeThing target = pathNavSet.Items[index];
            float distance = Vector3.Distance(target.transform.position, cachedTransform.position);
            if (distance <= arriveDistance)
            {
                index = VerifyIndex(pathNavSet, index + 1);
            }
            if (target.transform != pathTarget)
            {
                pathTarget.SetValue(target.transform);
            }
        }
    }

    private int VerifyIndex(ThingRuntimeSet set, int index)
    {
        if (index >= pathNavSet.Items.Count)
        {
            return 0;
        }
        return index;
    }
}
