using UnityEngine;
using System.Collections;
using RogueEyebrow.Variables;

public class BallisticAimAtTarget : MonoBehaviour
{

    public FloatReference speed;
    public FloatReference gravity;
    public Vector3Variable firingSpeed;
    public FloatReference minArcPeak = new FloatReference(1);
    public FloatReference maxArcPeak = new FloatReference(4);
    public FloatReference flightTime = new FloatReference(3);
    public Transform spawnPosition;
    public TransformVariable target;
    public FloatReference maxAngle;

    public void OnEnable()
    {
    }

    void Update()
    {
        if (target.Value)
        {
            float calculatedGravity;
            Vector3 projPos = spawnPosition.position;
            Vector3 targetPos = target.Value.position;
            Vector3 fireVel, impactPos;

            Vector3 diff = targetPos - projPos;
            Vector3 diffGround = new Vector3(diff.x, 0f, diff.z);
            float maxRange = speed * flightTime;
            float t = diffGround.magnitude / maxRange;
            float arcPeak = Mathf.Lerp(minArcPeak, maxArcPeak, t);
            if (BallisticCalculations.SolveBallisticArcLateral(projPos, speed, targetPos, Vector3.zero, arcPeak, out fireVel, out calculatedGravity, out impactPos))
            {
                float angle = Vector3.Angle(transform.parent.forward, fireVel.normalized);
                angle = Mathf.Clamp(angle, 0f, maxAngle.Value);
                transform.localRotation = Quaternion.Euler(-angle, 0, 0);
                gravity.Variable.SetValue(calculatedGravity);
                if (firingSpeed != null)
                {
                    firingSpeed.Value = fireVel;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        float dt = Time.fixedDeltaTime;
        Vector3 accel = -gravity * Vector3.up;
        Vector3 curPos = transform.position;
        Vector3 impulse = transform.forward * -speed;
        Vector3 lastPos = curPos + impulse * dt + accel * dt * dt;
        Vector3 prevPos = lastPos;

        for (int i = 0; i < 150; ++i)
        {
            Vector3 newPos = curPos + (curPos - prevPos) + accel * dt * dt;
            prevPos = curPos;
            Gizmos.DrawLine(curPos, newPos);
            curPos = newPos;
            if (newPos.y < -1f)
            {
                break;
            }
        }
    }
}