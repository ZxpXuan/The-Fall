using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Tracer)), CanEditMultipleObjects]
public class TracerEditor : Editor
{
	Tracer tracer;
	
	SerializedProperty useHandle;

	SerializedProperty radius;
	SerializedProperty angle;
	SerializedProperty maxBounce;
	SerializedProperty wireColor;
	SerializedProperty startPointColor;
	SerializedProperty endPointColor;

	SerializedProperty handleSize;
	SerializedProperty handleDistance;
	SerializedProperty handleColor;

	private void OnEnable()
	{
		tracer = target as Tracer;
		useHandle = serializedObject.FindProperty("useHandle");
		radius = serializedObject.FindProperty("radius");
		angle = serializedObject.FindProperty("angle");
		maxBounce = serializedObject.FindProperty("maxBounce");
		wireColor = serializedObject.FindProperty("wireColor");
		startPointColor = serializedObject.FindProperty("startPointColor");
		endPointColor = serializedObject.FindProperty("endPointColor");

		handleSize = serializedObject.FindProperty("handleSize");
		handleDistance = serializedObject.FindProperty("handleDistance");
		handleColor = serializedObject.FindProperty("handleColor");
	}

	private void OnSceneGUI()
	{
		if (!useHandle.boolValue) return;

		Handles.color = handleColor.colorValue;
		var handlePos = tracer.transform.position + tracer.HandlePos * handleDistance.floatValue;
		handlePos = Handles.FreeMoveHandle(handlePos, Quaternion.identity, handleSize.floatValue, Vector3.one * 0.1f, Handles.SphereHandleCap);
		var dir = handlePos - tracer.transform.position;
		dir.z = 0;
		tracer.HandlePos = dir.normalized;
	}

	public override void OnInspectorGUI()
	{
		EditorGUILayout.Space();
		EditorGUILayout.PropertyField(useHandle);
		EditorGUILayout.PropertyField(radius);
		if (!useHandle.boolValue)
			EditorGUILayout.PropertyField(angle);
		EditorGUILayout.PropertyField(maxBounce);
		EditorGUILayout.PropertyField(wireColor);
		EditorGUILayout.PropertyField(startPointColor);
		EditorGUILayout.PropertyField(endPointColor);
		
		EditorGUILayout.PropertyField(handleSize);
		EditorGUILayout.PropertyField(handleDistance);
		EditorGUILayout.PropertyField(handleColor);
		
		serializedObject.ApplyModifiedProperties();
	}
}
