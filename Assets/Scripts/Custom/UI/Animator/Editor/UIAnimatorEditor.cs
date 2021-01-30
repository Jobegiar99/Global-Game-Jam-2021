using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using Utilities.EditorTools;

namespace Utilities.UI.Animation
{
    [CanEditMultipleObjects, CustomEditor(typeof(UIAnimator))]
    public class UIAnimatorEditor : CustomEditorBase
    {
        private ReorderableList m_reorderableList;

        public void SetupSpawnHelperList()
        {
            var listProperty = serializedObject.FindProperty("animations");
            m_reorderableList = new ReorderableList(serializedObject, listProperty, true, true, true, true);

            m_reorderableList.drawHeaderCallback += (Rect rect) =>
            {
                Rect lineRect = rect;
                EditorGUI.LabelField(lineRect, "ANIMATIONS", EditorHelper.CenteredStyle(10, true));
            };

            //Dibujar el elemento de la lista
            m_reorderableList.drawElementCallback += (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                float lineHeight = EditorGUIUtility.singleLineHeight;
                float oldWidth = EditorGUIUtility.labelWidth;
                //Elemento de la lista
                var prop = listProperty.GetArrayElementAtIndex(index);
                var linerect = rect;
                EditorGUI.PropertyField(rect, prop, new GUIContent());

                EditorGUIUtility.labelWidth = oldWidth;
            };

            m_reorderableList.elementHeightCallback = (int index) =>
            {
                return EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing * 2f;
            };
        }

        private void OnEnable()
        {
            SetupSpawnHelperList();
        }

        private bool showAnimations = false;
        private bool showMore = false;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();

            EditorHelper.DrawSection("Values", () =>
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("initialValue"));
            }, true);

            EditorHelper.DrawSection("Animations", () =>
            {
                m_reorderableList.DoLayoutList();
            }, ref showAnimations, false);

            EditorHelper.DrawSection("More", () =>
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("objectToDisable"));
            }, ref showMore, true);

            serializedObject.ApplyModifiedProperties();
        }
    }
}