using System;
using UnityEngine;
using RogueEyebrow.Variables;

public class RotateTransform : MonoBehaviour
{
    public Vector2 rotationRange = new Vector3(70, 70);
    public FloatReference rotationSpeed;
    public float dampingTime = 0.2f;
    public FloatReference horizontalInput;
    public FloatReference verticalInput;
        
    private Vector3 m_TargetAngles;
    private Vector3 m_FollowAngles;
    private Vector3 m_FollowVelocity;
    private Quaternion m_OriginalRotation;


    private void Start()
    {
        m_OriginalRotation = transform.localRotation;
    }


    private void Update()
    {
        // we make initial calculations from the original local rotation
        transform.localRotation = m_OriginalRotation;

        // read input from mouse or mobile controls
        float inputH = horizontalInput;
        float inputV = verticalInput;

        // wrap values to avoid springing quickly the wrong way from positive to negative
        if (m_TargetAngles.y > 180)
        {
            m_TargetAngles.y -= 360;
            m_FollowAngles.y -= 360;
        }
        if (m_TargetAngles.x > 180)
        {
            m_TargetAngles.x -= 360;
            m_FollowAngles.x -= 360;
        }
        if (m_TargetAngles.y < -180)
        {
            m_TargetAngles.y += 360;
            m_FollowAngles.y += 360;
        }
        if (m_TargetAngles.x < -180)
        {
            m_TargetAngles.x += 360;
            m_FollowAngles.x += 360;
        }
                
        // with mouse input, we have direct control with no springback required.
        m_TargetAngles.y += inputH*rotationSpeed;
        m_TargetAngles.x += inputV*rotationSpeed;

        // clamp values to allowed range
        m_TargetAngles.y = Mathf.Clamp(m_TargetAngles.y, -rotationRange.y*0.5f, rotationRange.y*0.5f);
        m_TargetAngles.x = Mathf.Clamp(m_TargetAngles.x, -rotationRange.x*0.5f, rotationRange.x*0.5f);

        // smoothly interpolate current values to target angles
        m_FollowAngles = Vector3.SmoothDamp(m_FollowAngles, m_TargetAngles, ref m_FollowVelocity, dampingTime);

        // update the actual gameobject's rotation
        transform.localRotation = m_OriginalRotation*Quaternion.Euler(-m_FollowAngles.x, m_FollowAngles.y, 0);
    }
}
