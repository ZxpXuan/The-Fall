using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Mover)), CanEditMultipleObjects]
public class MoverEditor : Editor
{
	Mover mover;

	SerializedProperty startingPercentage;

	SerializedProperty useDirectionHandle;
	SerializedProperty handleSize;
	SerializedProperty lineColor;
	SerializedProperty pointAColor;
	SerializedProperty pointBColor;
	SerializedProperty dragPointColor;
	SerializedProperty percentageHandleOffset;

	SerializedProperty running;

	Dictionary<Object, Vector3> lastWorldPositionMap = new Dictionary<Object, Vector3>();

	private void OnEnable()
	{
		mover = target as Mover;

		startingPercentage = serializedObject.FindProperty("startingPercentage");

		useDirectionHandle = serializedObject.FindProperty("useDirectionHandle");
		handleSize = serializedObject.FindProperty("handleSize");
		lineColor = serializedObject.FindProperty("lineColor");
		pointAColor = serializedObject.FindProperty("pointAColor");
		pointBColor = serializedObject.FindProperty("pointBColor");
		dragPointColor = serializedObject.FindProperty("dragPointColor");
		percentageHandleOffset = serializedObject.FindProperty("percentageHandleOffset");

		running = serializedObject.FindProperty("running");

		lastWorldPositionMap.Clear();
		lastWorldPositionMap.Add(target, mover.transform.position);
	}

	private void OnSceneGUI()
	{
		mover = target as Mover;
		if (!lastWorldPositionMap.ContainsKey(target))
			lastWorldPositionMap.Add(target, mover.transform.position);
		
		if (running.boolValue) return;

		CheckLastWorldPosition();
		DrawStartPoint();
		DrawDraggingHandle();

		if (useDirectionHandle.boolValue)
		{
			DrawDirectionHandle();
		}
		DrawLine();

		lastWorldPositionMap[target] = mover.transform.position;
	}

	private void DrawLine()
	{
		Handles.color = lineColor.colorValue;
		Handles.DrawLine(mover.StartPoint, mover.EndPoint);
	}

	private void CheckLastWorldPosition()
	{
		if (mover.transform.position == lastWorldPositionMap[target]) return;
		
		var diff = mover.transform.position - lastWorldPositionMap[target];
		mover.StartPoint += diff;
	}

	private void DrawStartPoint()
	{
		Handles.color = pointAColor.colorValue;
		var handlePos = mover.StartPoint;

		EditorGUI.BeginChangeCheck();
		handlePos = Handles.FreeMoveHandle(handlePos, Quaternion.identity, handleSize.floatValue, Vector3.one, Handles.DotHandleCap);
		if (!EditorGUI.EndChangeCheck()) return;

		Undo.RecordObject(mover, "Change Start Point");
		handlePos.z = mover.transform.position.z;
		mover.StartPoint = handlePos;

		mover.StartingPercentage = GetPercentage();
		mover.transform.position = GetDragPointPos();
	}

	private void DrawDirectionHandle()
	{
		Handles.color = pointBColor.colorValue;
		var handlePos = mover.EndPoint;

		EditorGUI.BeginChangeCheck();
		handlePos = Handles.FreeMoveHandle(handlePos, Quaternion.identity, handleSize.floatValue, Vector3.one, Handles.DotHandleCap);
		if (!EditorGUI.EndChangeCheck()) return;

		Undo.RecordObject(mover, "Change Direction and Distance");
		handlePos.z = mover.transform.position.z;
		var dir = handlePos - mover.StartPoint;
		mover.Distance = dir.magnitude;
		mover.Angle = Vector3.SignedAngle(Vector3.right, dir, Vector3.forward);
		mover.Angle += mover.Angle < 0 ? 360 : 0;

		mover.StartingPercentage = GetPercentage();
		mover.transform.position = GetDragPointPos();
	}

	private void DrawDraggingHandle()
	{
		var offset = RotateVector(Vector3.up, mover.Angle) * percentageHandleOffset.floatValue;
		var handlePos = GetDragPointPos() + offset;

		Handles.color = dragPointColor.colorValue;
		Handles.DrawLine(mover.StartPoint + offset, mover.EndPoint + offset);

		EditorGUI.BeginChangeCheck();
		handlePos = Handles.FreeMoveHandle(handlePos, Quaternion.identity, handleSize.floatValue, Vector3.one, Handles.DotHandleCap);
		if (!EditorGUI.EndChangeCheck()) return;

		var a2b = mover.Direction * mover.Distance;
		Undo.RecordObject(mover, "Change Percentage");

		handlePos -= mover.StartPoint + offset;
		
		if (Vector3.Dot(handlePos, a2b) < 0)
			handlePos = Vector3.zero;
		else
			handlePos = Vector3.Project(handlePos, a2b);

		mover.StartingPercentage = Mathf.Clamp01(handlePos.magnitude / a2b.magnitude);
		mover.transform.position = GetDragPointPos();
	}

	private Vector3 GetDragPointPos()
	{
		return Vector3.Lerp(mover.StartPoint, mover.EndPoint, mover.StartingPercentage);
	}

	private float GetPercentage()
	{
		var a2b = mover.Direction * mover.Distance;
		var pos = mover.transform.position - mover.StartPoint;
		
		if (Vector3.Dot(pos, a2b) < 0)
			return 0;
		else
			pos = Vector3.Project(pos, a2b);

		return Mathf.Clamp01(pos.magnitude / a2b.magnitude);
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
