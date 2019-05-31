using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using UnityUtility;

namespace UnityUtility.Editor
{
	[CustomEditor(typeof(SequenceEvent))]
	public class SequenceEventEditor : UnityEditor.Editor
	{
		private ReorderableList list;

		private void OnEnable()
		{
			list = new ReorderableList(serializedObject,
				serializedObject.FindProperty("sequences"),
				true, true, true, true);

			list.elementHeightCallback =
				(int index) =>
				{
					var element = list.serializedProperty.GetArrayElementAtIndex(index);

					var seqEvent = element.FindPropertyRelative("Event");

					var height =
						EditorGUI.GetPropertyHeight(seqEvent, new GUIContent(""), true) +
						(EditorGUIUtility.singleLineHeight) * 5;

					return element.isExpanded ? height : EditorGUIUtility.singleLineHeight;
				};

			list.drawElementCallback =
				(Rect rect, int index, bool isActive, bool isFocused) =>
				{
					var element = list.serializedProperty.GetArrayElementAtIndex(index);

					var lineHeight = EditorGUIUtility.singleLineHeight;
					var drawRect = rect;
					drawRect.x += 10;
					drawRect.y += 2;
					drawRect.height = lineHeight;
					EditorGUI.PropertyField(drawRect, element);

					if (!element.isExpanded) return;

					drawRect.x -= 10;
					drawRect.y += EditorGUIUtility.singleLineHeight + 2;
					EditorGUI.PropertyField(drawRect, element.FindPropertyRelative("Label"));

					drawRect.y += EditorGUIUtility.singleLineHeight + 2;
					EditorGUI.PropertyField(drawRect, element.FindPropertyRelative("BeforeTriggerDelay"));

					drawRect.y += EditorGUIUtility.singleLineHeight + 2;
					EditorGUI.PropertyField(drawRect, element.FindPropertyRelative("AfterTriggerDelay"));

					drawRect.y += EditorGUIUtility.singleLineHeight + 2;
					EditorGUI.PropertyField(drawRect, element.FindPropertyRelative("Event"));
				};
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			EditorGUILayout.PropertyField(serializedObject.FindProperty("startAtBeginning"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("manuallyExecute"));
			list.DoLayoutList();
			serializedObject.ApplyModifiedProperties();
		} 
	}
}
