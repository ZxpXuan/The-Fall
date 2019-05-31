using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
	[SerializeField] bool towardEnd = true;
	[SerializeField, Range(0, 10)] float speed = 2;
	[SerializeField, Range(0, 1)] float startingPercentage;
	[SerializeField] Vector3 startPoint = Vector3.left;
	[SerializeField, Range(0, 360)] float angle = 0;
	[SerializeField, Range(0, 100)] float distance = 5;

	[Header("Handle Settings")]
	[SerializeField] bool useDirectionHandle = true;
	[SerializeField] float handleSize = 0.5f;
	[SerializeField] float percentageHandleOffset = 1.5f;
	[SerializeField] Color lineColor = Color.white;
	[SerializeField] Color pointAColor = Color.red;
	[SerializeField] Color pointBColor = Color.blue;
	[SerializeField] Color dragPointColor = Color.yellow;

	[Header("Preview")]
	[SerializeField] bool previewMovement = true;
	[SerializeField] bool previewFinalPoint = true;
	[SerializeField] Color movePreviewColor = new Color(1, 1, 0, 0.5f);
	[SerializeField] Color pointAPreviewColor = new Color(1, 0, 0, 0.5f);
	[SerializeField] Color pointBPreviewColor = new Color(0, 0, 1, 0.5f);

	[SerializeField, HideInInspector] bool running;

	Mesh mesh;

	bool wasPreviewMovement;
	bool previewTowardEnd;
	float previewDistFromPoint;
	float previewTimer;

	float distFromPoint;

	public float StartingPercentage
	{
		get { return startingPercentage; }
		set { startingPercentage = value; }
	}

	public Vector3 StartPoint
	{
		get { return startPoint; }
		set { startPoint = value; }
	}

	public Vector3 Direction
	{
		get { return RotateVector(Vector3.right, angle); }
	}

	public Vector3 EndPoint
	{
		get { return startPoint + Direction * distance; }
	}

	public float Angle
	{
		get { return angle; }
		set { angle = value; }
	}

	public float Distance
	{
		get { return distance; }
		set { distance = value; }
	}

	private void OnDrawGizmos()
	{
		if (mesh == null)
		{
			var filter = GetComponent<MeshFilter>();
			if (filter != null)
				mesh = filter.sharedMesh;
		}

		if (wasPreviewMovement != previewMovement)
		{
			var position = Vector3.Lerp(StartPoint, EndPoint, startingPercentage);
			previewDistFromPoint = (towardEnd ? EndPoint - position : StartPoint - position).magnitude;
			wasPreviewMovement = previewMovement;
			previewTowardEnd = towardEnd;
			previewTimer = Time.realtimeSinceStartup;
		}

		var cubeSize = transform.lossyScale;
		var rotationMatrix = GetRotationMatrix();

		if (previewMovement)
		{
			var previewPosition = GetNextPreviewPosition();
			
			Gizmos.color = movePreviewColor;
			if (mesh != null)
			{
				Gizmos.matrix = Matrix4x4.identity;
				Gizmos.DrawMesh(mesh, previewPosition, transform.rotation, transform.lossyScale);
			}
			else
			{
				var previewMatrix = GetPositionMatrix(previewPosition) * rotationMatrix * GetCounterPositionMatrix(previewPosition);
				Gizmos.matrix = previewMatrix;
				Gizmos.DrawCube(previewPosition, cubeSize);
			}
		}

		if (previewFinalPoint)
		{
			var pointAMatrix = GetPositionMatrix(startPoint) * rotationMatrix * GetCounterPositionMatrix(startPoint);
			var pointBMatrix = GetPositionMatrix(EndPoint) * rotationMatrix * GetCounterPositionMatrix(EndPoint);

			Gizmos.color = pointAPreviewColor;
			if (mesh != null)
			{
				Gizmos.matrix = Matrix4x4.identity;
				Gizmos.DrawMesh(mesh, StartPoint, transform.rotation, transform.lossyScale);
			}
			else
			{
				var previewMatrix = GetPositionMatrix(StartPoint) * rotationMatrix * GetCounterPositionMatrix(StartPoint);
				Gizmos.matrix = pointAMatrix;
				Gizmos.DrawCube(StartPoint, cubeSize);
			}

			Gizmos.color = pointBPreviewColor;
			if (mesh != null)
			{
				Gizmos.matrix = Matrix4x4.identity;
				Gizmos.DrawMesh(mesh, EndPoint, transform.rotation, transform.lossyScale);
			}
			else
			{
				var previewMatrix = GetPositionMatrix(EndPoint) * rotationMatrix * GetCounterPositionMatrix(EndPoint);
				Gizmos.matrix = pointBMatrix;
				Gizmos.DrawCube(EndPoint, cubeSize);
			}
		}
	}

	public Matrix4x4 GetPositionMatrix(Vector3 position)
	{
		return new Matrix4x4(
			new Vector4(1, 0, 0, 0),
			new Vector4(0, 1, 0, 0),
			new Vector4(0, 0, 1, 0),
			new Vector4(position.x, position.y, position.z, 1)
			);
	}

	public Matrix4x4 GetCounterPositionMatrix(Vector3 position)
	{
		position *= -1;
		return new Matrix4x4(
			new Vector4(1, 0, 0, 0),
			new Vector4(0, 1, 0, 0),
			new Vector4(0, 0, 1, 0),
			new Vector4(position.x, position.y, position.z, 1)
			);
	}

	public Matrix4x4 GetRotationMatrix()
	{
		var rad = transform.eulerAngles * Mathf.Deg2Rad;
		var sinX = Mathf.Sin(rad.x);
		var sinY = Mathf.Sin(rad.y);
		var sinZ = Mathf.Sin(rad.z);
		var cosX = Mathf.Cos(rad.x);
		var cosY = Mathf.Cos(rad.y);
		var cosZ = Mathf.Cos(rad.z);

		return new Matrix4x4(
			new Vector4(cosY * cosZ - sinX * sinY * sinZ, cosY * sinZ + sinX * sinY * cosX, -cosX * sinY, 0),
			new Vector4(-cosX * sinZ, cosX * cosZ, sinX, 0),
			new Vector4(sinY * cosZ + sinX * cosY * sinZ, sinY * sinZ - sinX * cosY * cosZ, cosX * cosY, 0),
			new Vector4(0,0,0,1)
			);
	}

	private void Start()
	{
		useDirectionHandle = true;
		transform.position = Vector3.Lerp(startPoint, EndPoint, startingPercentage);
		distFromPoint = (towardEnd ? EndPoint - transform.position : startPoint - transform.position).magnitude;
		running = true;
	}

	private void Update()
	{
		transform.position = GetNextPosition();
	}

	private Vector3 GetNextPosition()
	{
		var tDist = speed * Time.deltaTime;

		distFromPoint -= tDist;
		if (distFromPoint < 0)
		{
			towardEnd = !towardEnd;
			distFromPoint += (EndPoint - startPoint).magnitude;
		}

		var dir = !towardEnd ? EndPoint - startPoint : startPoint - EndPoint;
		return (towardEnd ? EndPoint : startPoint) + dir.normalized * distFromPoint;
	}

	private Vector3 GetNextPreviewPosition()
	{
		var tDist = speed * (Time.realtimeSinceStartup - previewTimer);
		previewTimer = Time.realtimeSinceStartup;

		previewDistFromPoint -= tDist;
		if (previewDistFromPoint < 0)
		{
			previewTowardEnd = !previewTowardEnd;
			previewDistFromPoint += (EndPoint - startPoint).magnitude;
		}

		var dir = !previewTowardEnd ? EndPoint - startPoint : startPoint - EndPoint;
		return (previewTowardEnd ? EndPoint : startPoint) + dir.normalized * previewDistFromPoint;
	}
	
	private Vector3 RotateVector(Vector3 vector, float angle)
	{
		var rad = Mathf.Deg2Rad * angle;
		var x = Mathf.Cos(rad) * vector.x - Mathf.Sin(rad) * vector.y;
		var y = Mathf.Sin(rad) * vector.x + Mathf.Cos(rad) * vector.y;
		vector.x = x;
		vector.y = y;
		return vector;
	}
}
