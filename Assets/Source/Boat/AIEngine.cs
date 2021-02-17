using UnityEngine;
using RogueEyebrow.Variables;
using RogueEyebrow.Sets;

public class AIEngine : MonoBehaviour
{
    public TransformVariable target;
    public FloatReference arriveDistance;
    public FloatReference stopDistance;
    public FloatVariable engineOutput;
    public FloatVariable helmOutput;

    private Transform cachedTransform;

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
            float distance = Vector3.Distance(cachedTransform.position, target.Value.position) - stopDistance.Value;
            engineOutput.SetValue(Mathf.Min(distance / arriveDistance.Value, 1f));


            Vector3 direction = target.Value.position - cachedTransform.position;
            direction.Normalize();
            float dot = Vector3.Dot(direction, cachedTransform.right);
            helmOutput.SetValue(dot);
        }

    }
}
