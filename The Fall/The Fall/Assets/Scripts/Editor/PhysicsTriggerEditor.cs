using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace UnityUtility.Editor
{
	[CustomEditor(typeof(PhysicsTrigger))]
	public class PhysicsTriggerEditor : UnityEditor.Editor
	{
		bool triggerEventsExpanded = false;
		bool collisionEventsExpanded = false;
		bool trigger2DEventsExpanded = false;
		bool collision2DEventsExpanded = false;

		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			
			EditorGUILayout.PropertyField(serializedObject.FindProperty("triggerOnlyOnce"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("triggerableTags"), true);
			
			DrawEventProperties();

			serializedObject.ApplyModifiedProperties();
		}

		void DrawEventProperties()
		{
			triggerEventsExpanded = EditorGUILayout.Foldout(triggerEventsExpanded, "Trigger Events");
			if (triggerEventsExpanded)
			{
				EditorGUILayout.PropertyField(serializedObject.FindProperty("OnEnterTrigger"));
				EditorGUILayout.PropertyField(serializedObject.FindProperty("OnStayTrigger"));
				EditorGUILayout.PropertyField(serializedObject.FindProperty("OnExitTrigger"));
			}
			

			collisionEventsExpanded = EditorGUILayout.Foldout(collisionEventsExpanded, "Collision Events");
			if (collisionEventsExpanded)
			{
				EditorGUILayout.PropertyField(serializedObject.FindProperty("OnStartCollision"));
				EditorGUILayout.PropertyField(serializedObject.FindProperty("OnStayCollision"));
				EditorGUILayout.PropertyField(serializedObject.FindProperty("OnExitCollision"));
			}
			

			trigger2DEventsExpanded = EditorGUILayout.Foldout(trigger2DEventsExpanded, "2D Trigger Events");
			if (trigger2DEventsExpanded)
			{
				EditorGUILayout.PropertyField(serializedObject.FindProperty("OnEnterTrigger2D"));
				EditorGUILayout.PropertyField(serializedObject.FindProperty("OnStayTrigger2D"));
				EditorGUILayout.PropertyField(serializedObject.FindProperty("OnExitTrigger2D"));
			}


			collision2DEventsExpanded = EditorGUILayout.Foldout(collision2DEventsExpanded, "2D Collision Events");
			if (collision2DEventsExpanded)
			{
				EditorGUILayout.PropertyField(serializedObject.FindProperty("OnStartCollision2D"));
				EditorGUILayout.PropertyField(serializedObject.FindProperty("OnStayCollision2D"));
				EditorGUILayout.PropertyField(serializedObject.FindProperty("OnExitCollision2D"));
			}
		}
	}
}
