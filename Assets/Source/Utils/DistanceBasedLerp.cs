using UnityEngine;
using RogueEyebrow.Variables;

public class DistanceBasedLerp : MonoBehaviour
{
    private Transform cachedTransform;
    public TransformVariable target;
    public FloatReference minimumValue;
    public FloatReference maximumValue;
    public FloatReference maximumDistance;
    public FloatReference minimumDistance;

    // Use this for initialization
    void Start()
    {
        cachedTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && target.Value != null)
        {
            float distance = Vector3.Distance(target.Value.position, cachedTransform.position);

        }
    }
}
