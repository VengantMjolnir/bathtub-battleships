using UnityEngine;
using System.Collections;

public class FaceTarget : MonoBehaviour
{
    [SerializeField]
    private Transform m_Target;
    [SerializeField]
    private bool m_FlipFacing = false;
    private Transform m_CachedTransform;

    // Use this for initialization
    void Start()
    {
        m_CachedTransform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Target != null)
        {
            //Vector3 dir = m_CachedTransform.position - m_Target.position;
            //dir.Normalize();

            m_CachedTransform.LookAt(m_Target.position);
            if (m_FlipFacing)
            {
                Vector3 forward = m_CachedTransform.forward;
                forward *= -1f;
                m_CachedTransform.forward = forward;
            }
        }
    }
}
