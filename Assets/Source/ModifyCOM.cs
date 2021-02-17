using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyCOM : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidBody;
    [SerializeField]
    private Vector3 _centerOfMass;
    private Vector3 _cachedCOM;

    // Use this for initialization
    void Start()
    {
        if (_centerOfMass != Vector3.zero)
        {
            _cachedCOM = _centerOfMass;
            _rigidBody.centerOfMass = _centerOfMass;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_cachedCOM != _centerOfMass)
        {
            if (_centerOfMass != Vector3.zero)
            {
                _rigidBody.centerOfMass = _centerOfMass;
            }
            else
            {
                _rigidBody.ResetCenterOfMass();
            }
            _cachedCOM = _centerOfMass;
        }
    }

    public void OnDrawGizmos()
    {
        if (_rigidBody != null)
        {
            Vector3 com = _rigidBody.worldCenterOfMass;
            Gizmos.DrawSphere(com, 0.5f);
        }
    }
}
