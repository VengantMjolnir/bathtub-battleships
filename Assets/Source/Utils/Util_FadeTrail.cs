using UnityEngine;
using System.Collections;

public class Util_FadeTrail : MonoBehaviour
{
	void Update()
	{
		TrailRenderer trail = GetComponentInChildren<TrailRenderer>();
		if (trail && trail.time > 0)
		{
			trail.time -= Time.deltaTime;
		}
		else
		{
			Destroy(this.gameObject);
		}
	}
}
