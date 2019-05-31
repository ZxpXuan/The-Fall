using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Tracer : MonoBehaviour
{
	[SerializeField, Range(0.1f, 3f)] float radius;
	[SerializeField, Range(0, 360)] float angle;
	[SerializeField, Range(0, 10)] int maxBounce;
	[SerializeField] Color wireColor = Color.white;
	[SerializeField] Color startPointColor = Color.green;
	[SerializeField] Color endPointColor = Color.blue;

	[Header("Handle Settings")]
	[SerializeField] bool useDirectionHandle;
	[SerializeField] bool usePositionHandle;
	[SerializeField, Range(1f, 5)] float handleSize = 1;
	[SerializeField, Range(1f, 10)] float handleDistance = 5;
	[SerializeField] Color handleColor = Color.blue;
	[HideInInspector] public Vector3 handleDir = Vector3.right;

	private void OnDrawGizmos()
	{
		var bouncedTimes = 0;
		var dir = Vector3.one;
		var pos = transform.position;
		RaycastHit hit = new RaycastHit();

        Physics.queriesHitTriggers = true;
		
		if (useDirectionHandle)
		{
			dir = handleDir;
			angle = Vector3.SignedAngle(Vector3.right, dir, Vector3.forward);
			if (angle < 0) angle += 360;
		}
		else
		{
			dir = RotateVector(Vector3.right, angle);
		}
		
		// Draw starting point
		Gizmos.color = startPointColor;
		Gizmos.DrawSphere(pos, radius);

		// Start drawing traces
		Gizmos.color = wireColor;
		while (bouncedTimes <= maxBounce)
		{
			// If the ball collided with something
			if (Physics.SphereCast(pos, radius, dir, out hit))
			{
				// Draw line in editor
				Gizmos.DrawRay(pos, dir * hit.distance);
                
				// Calcuate the new position and direction
				pos += dir * hit.distance;
				dir = Vector3.Reflect(dir, hit.normal);
				bouncedTimes++;
			}
			else
			{
				break;
			}
		}

		// Draw ending point, if had a end
		if (hit.collider != null)
		{
			Gizmos.color = endPointColor;
			Gizmos.DrawSphere(pos, radius);
		}
	}

	/// <summary>
	/// Rotate a vector along z axis by a degree
	/// </summary>
	/// <param name="vector"></param>
	/// <param name="angle"></param>
	/// <returns></returns>
	Vector3 RotateVector(Vector3 vector, float angle)
	{
		var rad = Mathf.Deg2Rad * angle;
		var x = Mathf.Cos(rad) * vector.x - Mathf.Sin(rad) * vector.y;
		var y = Mathf.Sin(rad) * vector.x + Mathf.Cos(rad) * vector.y;
		vector.x = x;
		vector.y = y;
		return vector;
	}
}
