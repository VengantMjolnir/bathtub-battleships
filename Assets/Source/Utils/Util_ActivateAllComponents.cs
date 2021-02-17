using UnityEngine;
using System.Collections;

public class Util_ActivateAllComponents : MonoBehaviour
{
	public bool ActivateOnEnable = true;

	public void OnEnable()
	{
		if (ActivateOnEnable)
		{
			EnableAll();
		}
	}

	public void EnableAll()
	{
		MonoBehaviour[] behaviours = gameObject.GetComponents<MonoBehaviour>();
		for (int i = 0; i < behaviours.Length; ++i)
		{
			behaviours[i].enabled = true;
		}
	}
}
