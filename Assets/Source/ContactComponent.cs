using UnityEngine;
using System.Collections;

public class ContactComponent : MonoBehaviour 
{
    public float afterTime = 0f;
    private bool destroyed = false;

    public void OnEnable()
    {
        destroyed = false;
    }

    void OnCollisionEnter(Collision collision)
	{
        if (destroyed)
            return;

        destroyed = true;
        if (afterTime > 0f)
        {
            Destroy(gameObject, afterTime);
        } else
        {
            Destroy(gameObject);
        }
	}
}
