using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Utilities.EditorTools;

namespace Utilities.UI
{
    [CustomEditor(typeof(ButtonEvents))]
    public class ButtonEventsEditor : UnityEditor.UI.ButtonEditor
    {
        private bool events = false;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            this.serializedObject.Update();

            SerializedProperty onSelect = this.serializedObject.FindProperty("onSelect");
            SerializedProperty onDeselect = this.serializedObject.FindProperty("onDeselect");
            SerializedProperty onPointerDown = this.serializedObject.FindProperty("onPointerDown");
            SerializedProperty onPointerUp = this.serializedObject.FindProperty("onPointerUp");
            SerializedProperty selectOnHover = this.serializedObject.FindProperty("selectOnHover");

            EditorGUILayout.PropertyField(selectOnHover, new GUIContent("Select on hover"));
            EditorHelper.DrawSection("Events", () =>
            {
                EditorGUILayout.PropertyField(onSelect, new GUIContent("Select"));
                EditorGUILayout.PropertyField(onDeselect, new GUIContent("Deselect"));
                EditorGUILayout.PropertyField(onPointerDown, new GUIContent("Pointer Down"));
                EditorGUILayout.PropertyField(onPointerUp, new GUIContent("Pointer Up"));
            }, ref events, false);

            serializedObject.ApplyModifiedProperties();
        }
    }
}