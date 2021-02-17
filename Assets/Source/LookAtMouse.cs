using UnityEngine;
using System.Collections;

public class LookAtMouse : MonoBehaviour
{
	// speed is the rate at which the object will rotate
	public float speed;
	public float minAngle = 0.0f;
	public float maxAngle = 90.0f;
	public float targetVariance = 10.0f;

	private float currentAngle = 0.0f;
	private float targetAngle = 0.0f;
	private float variance = 0.0f;
	private bool onTarget = false;
	
	void FixedUpdate () 
	{
		// Generate a plane that intersects the transform's position with an upwards normal.
		Plane playerPlane = new Plane(Vector3.up, transform.position);
		
		// Generate a ray from the cursor position
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		
		// Determine the point where the cursor ray intersects the plane.
		// This will be the point that the object must look towards to be looking at the mouse.
		// Raycasting to a Plane object only gives us a distance, so we'll have to take the distance,
		//   then find the point along that ray that meets that distance.  This will be the point
		//   to look at.
		float hitdist = 0.0f;
		// If the ray is parallel to the plane, Raycast will return false.
		if (playerPlane.Raycast (ray, out hitdist)) 
		{
			// Get the point along the ray that hits the calculated distance.
			Vector3 targetPoint = ray.GetPoint(hitdist);
			
			// Determine the target rotation.  This is the rotation if the transform looks at the target point.
			Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
			currentAngle = Quaternion.Angle(transform.rotation, transform.parent.transform.rotation);
			targetAngle = Quaternion.Angle(targetRotation, transform.parent.transform.rotation);
			if( (currentAngle < maxAngle || targetAngle < maxAngle) &&
			    ( currentAngle > minAngle || targetAngle > minAngle) 
			   )
			{
				// Smoothly rotate towards the target point.
				transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
			}
			variance = Mathf.Abs(currentAngle - targetAngle);
			if( variance < targetVariance )
			{ 
				if( !onTarget )
				{
					onTarget = true;
					BroadcastMessage("SetOnTarget", onTarget, SendMessageOptions.DontRequireReceiver);
				}
			}
			else if( onTarget )
			{
				onTarget = false;
				BroadcastMessage("SetOnTarget", onTarget, SendMessageOptions.DontRequireReceiver); 
			}
		}
	}
}