using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Mover))]
public class MoverEditor : Editor
{
	Mover mover;

	SerializedProperty startingPercentage;

	SerializedProperty freeMove;
	SerializedProperty handleSize;
	SerializedProperty pointA;
	SerializedProperty pointB;
	SerializedProperty lineColor;
	SerializedProperty pointAColor;
	SerializedProperty pointBColor;
	SerializedProperty dragPointColor;

	private void OnEnable()
	{
		mover = target as Mover;

		startingPercentage = serializedObject.FindProperty("startingPercentage");

		freeMove = serializedObject.FindProperty("freeMove");
		handleSize = serializedObject.FindProperty("handleSize");
		pointA = serializedObject.FindProperty("pointA");
		pointB = serializedObject.FindProperty("pointB");
		lineColor = serializedObject.FindProperty("lineColor");
		pointAColor = serializedObject.FindProperty("pointAColor");
		pointBColor = serializedObject.FindProperty("pointBColor");
		dragPointColor = serializedObject.FindProperty("dragPointColor");
	}

	private void OnSceneGUI()
	{
		if (freeMove.boolValue)
		{
			DrawFreeMoveHandle();
			DrawPointA();
			DrawPointB();
			DrawLine();
		}
		else
		{
			DrawPointA();
			DrawPointB();
			DrawDraggingHandle();
			DrawLine();
		}
	}

	private void DrawLine()
	{
		Handles.color = lineColor.colorValue;
		Handles.DrawLine(mover.PointA, mover.PointB);
	}

	private void DrawPointA()
	{
		Handles.color = pointAColor.colorValue;
		var handlePos = mover.PointA;

		EditorGUI.BeginChangeCheck();
		handlePos = Handles.FreeMoveHandle(handlePos, Quaternion.identity, handleSize.floatValue, Vector3.one * 0.1f, Handles.CubeHandleCap);
		if (!EditorGUI.EndChangeCheck()) return;

		Undo.RecordObject(mover, "Change Point A");
		handlePos.z = mover.transform.position.z;
		mover.PointA = handlePos;

		mover.StartingPercentage = GetPercentage();
		mover.transform.position = GetDragPointPos();
	}

	private void DrawPointB()
	{
		Handles.color = pointBColor.colorValue;
		var handlePos = mover.PointB;

		EditorGUI.BeginChangeCheck();
		handlePos = Handles.FreeMoveHandle(handlePos, Quaternion.identity, handleSize.floatValue, Vector3.one * 0.1f, Handles.CubeHandleCap);
		if (!EditorGUI.EndChangeCheck()) return;

		Undo.RecordObject(mover, "Change Point B");
		handlePos.z = mover.transform.position.z;
		mover.PointB = handlePos;

		mover.StartingPercentage = GetPercentage();
		mover.transform.position = GetDragPointPos();
	}

	private void DrawDraggingHandle()
	{
		Handles.color = dragPointColor.colorValue;
		var a2b = mover.PointA - mover.PointB;
		var handlePos = GetDragPointPos();
		
		EditorGUI.BeginChangeCheck();
		handlePos = Handles.FreeMoveHandle(handlePos, Quaternion.identity, handleSize.floatValue, Vector3.one * 0.1f, Handles.CubeHandleCap);
		if (!EditorGUI.EndChangeCheck()) return;

		Undo.RecordObject(mover, "Change Percentage");

		handlePos -= mover.PointA;

		Debug.Log(Vector3.Dot(handlePos, a2b));
		if (Vector3.Dot(handlePos, a2b) > 0)
			handlePos = Vector3.zero;
		else
			handlePos = Vector3.Project(handlePos, a2b);

		mover.StartingPercentage = Mathf.Clamp01(handlePos.magnitude / a2b.magnitude);
		mover.transform.position = GetDragPointPos();
	}

	private void DrawFreeMoveHandle()
	{
		Handles.color = dragPointColor.colorValue;
		var lastPosition = mover.transform.position;

		EditorGUI.BeginChangeCheck();
		mover.transform.position = Handles.FreeMoveHandle(mover.transform.position, Quaternion.identity, handleSize.floatValue, Vector3.one * 0.1f, Handles.CubeHandleCap);
		if (!EditorGUI.EndChangeCheck()) return;

		Undo.RecordObject(mover, "Change Position");

		var offset = mover.transform.position - lastPosition;
		mover.PointA += offset;
		mover.PointB += offset;
	}

	private Vector3 GetDragPointPos()
	{
		return Vector3.Lerp(mover.PointA, mover.PointB, mover.StartingPercentage);
	}

	private float GetPercentage()
	{
		var a2b = mover.PointA - mover.PointB;
		var pos = mover.transform.position - mover.PointA;
		
		if (Vector3.Dot(pos, a2b) > 0)
			return 0;
		else
			pos = Vector3.Project(pos, a2b);

		return Mathf.Clamp01(pos.magnitude / a2b.magnitude);
	}
}
