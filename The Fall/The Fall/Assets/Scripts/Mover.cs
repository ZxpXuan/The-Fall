using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
	[SerializeField] bool towardB;
	[SerializeField, Range(0, 10)] float speed = 2;
	[SerializeField, Range(0, 1)] float startingPercentage;
	[SerializeField] Vector3 pointA = Vector3.left;
	[SerializeField] Vector3 pointB = Vector3.right;

	[Header("Handle Settings")]
	[SerializeField] bool freeMove;
	[SerializeField] float handleSize = 1;
	[SerializeField] Color lineColor = Color.white;
	[SerializeField] Color pointAColor = Color.red;
	[SerializeField] Color pointBColor = Color.blue;
	[SerializeField] Color dragPointColor = Color.yellow;

	float distFromPoint;

	public float StartingPercentage
	{
		get { return startingPercentage; }
		set { startingPercentage = value; }
	}

	public Vector3 PointA
	{
		get { return pointA; }
		set { pointA = value; }
	}

	public Vector3 PointB
	{
		get { return pointB; }
		set { pointB = value; }
	}

	private void Start()
	{
		freeMove = true;
		transform.position = Vector3.Lerp(pointA, pointB, startingPercentage);
		distFromPoint = (towardB ? pointB - transform.position : pointA - transform.position).magnitude;
	}

	private void Update()
	{
		var tDist = speed * Time.deltaTime;

		distFromPoint -= tDist;
		if (distFromPoint < 0)
		{
			towardB = !towardB;
			distFromPoint += (pointB - pointA).magnitude;
		}

		var dir = !towardB ? pointB - pointA : pointA - pointB;
		transform.position = (towardB ? pointB : pointA) + dir.normalized * distFromPoint;
	}
}
