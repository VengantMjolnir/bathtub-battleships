using RogueEyebrow.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTorqueFromVariable : MonoBehaviour
{
    public Vector3 angles;

    public FloatReference torqueToApply;
    public FloatReference input;
    public Rigidbody bodyToPush;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (bodyToPush != null)
        {
            bodyToPush.AddRelativeTorque(angles * torqueToApply * input);
        }
    }
}
