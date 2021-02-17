using RogueEyebrow.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyForceFromVariable : MonoBehaviour
{
    public Vector3 direction;

    public FloatReference forceToApply;
    public FloatReference input;
    public BooleanVariable controlValue;
    public Rigidbody bodyToPush;
    public bool local = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (controlValue != null && !controlValue.Value)
        {
            return;
        }

        if (bodyToPush != null)
        {
            if (local)
            {
                Vector3 dir = transform.TransformDirection(direction);
                bodyToPush.AddForceAtPosition(dir * forceToApply * input, transform.position);
            } else
            {
                bodyToPush.AddRelativeForce(direction * forceToApply * input);
            }
        }
    }
}
