using UnityEngine;
using System.Collections;

public class LifetimeComponent : MonoBehaviour 
{
	private float elapsedTime = 0.0f;
	public float lifetime = 3.0f;

	// Use this for initialization
	void Start () 
	{
		elapsedTime = 0.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		elapsedTime += Time.deltaTime;
		if( elapsedTime >= lifetime )
		{
			gameObject.SetActive(false);
			Destroy(gameObject);
		}
	}
}
