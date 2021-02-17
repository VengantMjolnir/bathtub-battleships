using UnityEngine;
using System.Collections;

public class BallisticMotion : MonoBehaviour
{
    // Private fields
    private Vector3 lastPos;
    private Vector3 impulse;
    private float gravity;
    
    public void Initialize(Vector3 pos, float gravity)
    {
        transform.position = pos;
        lastPos = transform.position;
        this.gravity = gravity;
    }

    void FixedUpdate()
    {
        // Simple verlet integration
        float dt = Time.fixedDeltaTime;
        Vector3 accel = -gravity * Vector3.up;

        Vector3 curPos = transform.position;
        Vector3 newPos = curPos + (curPos - lastPos) + impulse * dt + accel * dt * dt;
        lastPos = curPos;
        transform.position = newPos;
        transform.forward = newPos - lastPos;

        impulse = Vector3.zero;

        // Z-kill
        if (transform.position.y < -5f)
            Destroy(gameObject);
    }

    public void AddImpulse(Vector3 impulse)
    {
        this.impulse += impulse;
    }

    public void OnDrawGizmos()
    {
        Vector3 curPos = transform.position;
        Vector3 prevPos = lastPos;
        for (int i = 0; i < 150; ++i)
        {
            float dt = Time.fixedDeltaTime;
            Vector3 accel = -gravity * Vector3.up;

            Vector3 newPos = curPos + (curPos - prevPos) + impulse * dt + accel * dt * dt;
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