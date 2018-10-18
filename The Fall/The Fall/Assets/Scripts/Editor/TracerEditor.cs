using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Tracer)), CanEditMultipleObjects]
public class TracerEditor : Editor
{
	Tracer tracer;

	SerializedProperty angle;
	SerializedProperty startPointColor;

	SerializedProperty useDirectionHandle;
	SerializedProperty usePositionHandle;
	SerializedProperty handleSize;
	SerializedProperty handleDistance;
	SerializedProperty handleColor;
	SerializedProperty handleDir;

	Vector3 lastHandleDir;
	float lastAngle;

	private void OnEnable()
	{
		tracer = target as Tracer;
		angle = serializedObject.FindProperty("angle");
		startPointColor = serializedObject.FindProperty("startPointColor");

		useDirectionHandle = serializedObject.FindProperty("useDirectionHandle");
		usePositionHandle = serializedObject.FindProperty("usePositionHandle");
		handleSize = serializedObject.FindProperty("handleSize");
		handleDistance = serializedObject.FindProperty("handleDistance");
		handleColor = serializedObject.FindProperty("handleColor");
		handleDir = serializedObject.FindProperty("handleDir");
	}

	private void OnSceneGUI()
	{
		DrawAngleDegree();

		// Draw free move handle
		if (usePositionHandle.boolValue)
			DrawPositionHandle();

		// Draw direction handle
		if (useDirectionHandle.boolValue)
			DrawDirectionHandle();
	}

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		
		if (handleDir.vector3Value != lastHandleDir)
		{
			angle.floatValue = DirToAngle(handleDir.vector3Value);
			lastAngle = angle.floatValue;
		}

		if (angle.floatValue != lastAngle)
		{
			handleDir.vector3Value = AngleToDir(angle.floatValue);
			lastHandleDir = handleDir.vector3Value;
		}

		lastAngle = angle.floatValue;
		lastHandleDir = handleDir.vector3Value;

		serializedObject.ApplyModifiedProperties();
	}

	private void DrawAngleDegree()
	{
		var gui = new GUIStyle();
		gui.fontSize = 16;
		Handles.Label(tracer.transform.position - Vector3.one, angle.floatValue.ToString(), gui);
	}

	private void DrawPositionHandle()
	{
		var origZ = tracer.transform.position.z;
		Handles.color = startPointColor.colorValue;
		var handlePos = tracer.transform.position;
		handlePos = Handles.FreeMoveHandle(handlePos, Quaternion.identity, handleSize.floatValue, Vector3.one * 0.1f, Handles.SphereHandleCap);
		handlePos.z = origZ;
		tracer.transform.position = handlePos;
	}

	private void DrawDirectionHandle()
	{
		Handles.color = handleColor.colorValue;
		var handlePos = tracer.transform.position + tracer.handleDir * handleDistance.floatValue;
		handlePos = Handles.FreeMoveHandle(handlePos, Quaternion.identity, handleSize.floatValue, Vector3.one * 0.1f, Handles.SphereHandleCap);
		var dir = handlePos - tracer.transform.position;
		dir.z = 0;
		tracer.handleDir = dir.normalized;
	}

	float DirToAngle(Vector3 dir)
	{
		var ang = Vector3.SignedAngle(Vector3.right, dir, Vector3.forward);
		return ang < 0 ? ang + 360 : ang;
	}

	Vector3 AngleToDir(float ang)
	{
		var rad = Mathf.Deg2Rad * ang;
		var x = Mathf.Cos(rad);
		var y = Mathf.Sin(rad);
		return new Vector3(x, y, 0);
	}
}
