using UnityEngine;
using System.Collections;
using RogueEyebrow.Variables;

public class AimAtTarget : MonoBehaviour
{
    public TransformVariable target;
    public FloatReference output;
	public float targetVariance = 10.0f;
    public BooleanVariable onTarget;

    public void OnEnable()
    {
        onTarget.SetValue(false);
    }

    void Update () 
	{
        if (target.Value == null)
        {
            output.Variable.SetValue(0f);
            if (onTarget.Value)
            {
                onTarget.SetValue(false);
            }
            return;
        }
        // Get the point along the ray that hits the calculated distance.
        Vector3 targetPoint = target.Value.position;

        Vector3 right = transform.right;
        Vector3 dir = targetPoint - transform.position;
        float dot = Vector3.Dot(dir.normalized, right);
        if (output.Variable != null)
        {
            output.Variable.SetValue(dot);
        }

        dot = Vector3.Dot(dir.normalized, transform.forward);
        float variance = Mathf.Rad2Deg * Mathf.Acos(dot);
		if( variance < targetVariance )
		{ 
			if( !onTarget.Value )
			{
                onTarget.SetValue(true);
			}
		}
		else if( onTarget.Value )
		{
            onTarget.SetValue(false);
		}
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = onTarget.Value ? Color.red : Color.yellow;
        Gizmos.DrawRay(transform.position, transform.forward * 100f);
    }
}