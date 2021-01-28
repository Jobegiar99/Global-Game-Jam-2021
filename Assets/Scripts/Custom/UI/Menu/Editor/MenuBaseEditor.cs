using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Utilities.EditorTools;

namespace Utilities.UI.Menu
{
    [CanEditMultipleObjects, CustomEditor(typeof(MenuBase))]
    public class MenuBaseEditor : Editor
    {
        private bool showDefaultInspector = false;
        private bool showValues = true;
        private bool showEvents = false;

        public void DrawRequiredSection()
        {
            EditorHelper.DrawSection("VALUES", () =>
            {
                var idProp = serializedObject.FindProperty("menuId");
                var animatorProp = serializedObject.FindProperty("animator");
                var defaultElementProp = serializedObject.FindProperty("defaultUIElement");
                var nameProp = serializedObject.FindProperty("menuName");

                EditorGUILayout.PropertyField(idProp);
                EditorGUILayout.PropertyField(nameProp);
                EditorGUILayout.PropertyField(animatorProp);
                EditorGUILayout.PropertyField(defaultElementProp);
            }, ref showValues, true);
        }

        public void DrawEventsSection()
        {
            var hideProp = serializedObject.FindProperty("onHide");
            var showProp = serializedObject.FindProperty("onShow");

            EditorGUILayout.PropertyField(hideProp);
            EditorGUILayout.PropertyField(showProp);
        }

        public override void OnInspectorGUI()
        {
            showDefaultInspector = EditorGUILayout.Toggle(showDefaultInspector);

            if (showDefaultInspector)
            {
                base.OnInspectorGUI();
            }
            serializedObject.Update();

            DrawRequiredSection();

            var style = new GUIStyle(EditorStyles.foldoutHeader);
            style.fontStyle = EditorHelper.LabelStyle(12).fontStyle;

            showEvents = EditorGUILayout.BeginFoldoutHeaderGroup(showEvents, "EVENTS", style);

            if (showEvents)
            {
                DrawEventsSection();
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.Space();

            serializedObject.ApplyModifiedProperties();
        }
    }
}