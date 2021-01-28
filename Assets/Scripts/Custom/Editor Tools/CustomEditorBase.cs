using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR

namespace Utilities.EditorTools
{
    public class CustomEditorBase : Editor
    {
        private bool showDefaultInspector = false;

        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            var oldwidth = EditorGUIUtility.labelWidth;
            showDefaultInspector = EditorGUILayout.Toggle(" ", showDefaultInspector);
            EditorGUIUtility.labelWidth = oldwidth;
            EditorGUILayout.EndHorizontal();

            if (showDefaultInspector)
            {
                base.OnInspectorGUI();
            }
        }
    }
}

#endif