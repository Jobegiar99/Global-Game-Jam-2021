using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using Utilities.EditorTools;

namespace Utilities.UI.Menu
{
    [CanEditMultipleObjects, CustomEditor(typeof(TabMenu))]
    public class TabMenuEditor : Editor
    {
        private ReorderableList m_reorderableList;

        public void SetupSpawnHelperList()
        {
            var listProperty = serializedObject.FindProperty("tabs");
            m_reorderableList = new ReorderableList(serializedObject, listProperty, true, true, true, true);

            m_reorderableList.headerHeight = EditorGUIUtility.singleLineHeight * 2f;

            m_reorderableList.drawHeaderCallback += (Rect rect) =>
            {
                Rect lineRect = rect;
                lineRect.height = rect.height / 2f;
                EditorGUI.LabelField(lineRect, "TABS", EditorHelper.CenteredStyle(10, true));

                lineRect.y += lineRect.height;
                var boxRect = lineRect;
                boxRect.x -= 5f; boxRect.width += 10f;

                EditorGUI.DrawRect(boxRect, ColorHelper.ColorFromHex("#B6B6B6"));

                lineRect.width /= 3f;
                Rect generatorRect = lineRect, backwardsRect = lineRect, areaRect = lineRect;
                backwardsRect.x += generatorRect.width;
                areaRect.x += backwardsRect.x + backwardsRect.width;

                EditorGUI.LabelField(generatorRect, "MENU", EditorHelper.CenteredStyle(10, false));
                EditorGUI.LabelField(backwardsRect, "BUTTON", EditorHelper.CenteredStyle(10, false));
                EditorGUI.LabelField(areaRect, "TAB ANIM", EditorHelper.CenteredStyle(10, false));
            };

            //Dibujar el elemento de la lista
            m_reorderableList.drawElementCallback += (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                float lineHeight = EditorGUIUtility.singleLineHeight;
                float oldWidth = EditorGUIUtility.labelWidth;
                //Elemento de la lista
                var prop = listProperty.GetArrayElementAtIndex(index);
                //Properties
                var menuProp = prop.FindPropertyRelative("menu");
                var butonProp = prop.FindPropertyRelative("button");
                var tabAnimProp = prop.FindPropertyRelative("tabAnim");
                //Rect
                var lineRect = rect;
                lineRect.height = lineHeight;
                lineRect.y += EditorGUIUtility.standardVerticalSpacing;

                Rect menuRect = lineRect,
                buttonRect = lineRect,
                tabAnimRect = lineRect;
                menuRect.width = buttonRect.width = tabAnimRect.width /= 3f;
                buttonRect.x += menuRect.width;
                tabAnimRect.x += menuRect.width * 2f;
                EditorGUI.PropertyField(menuRect, menuProp, new GUIContent());
                EditorGUI.PropertyField(buttonRect, butonProp, new GUIContent());
                EditorGUI.PropertyField(tabAnimRect, tabAnimProp, new GUIContent());

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

        private bool showDefaultInspector = false;
        private bool showSettings = true;

        public override void OnInspectorGUI()
        {
            showDefaultInspector = EditorGUILayout.Toggle(showDefaultInspector);

            if (showDefaultInspector)
            {
                base.OnInspectorGUI();
            }
            serializedObject.Update();

            EditorHelper.DrawSection("Settings", () =>
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("defaultIndex"));
            }, ref showSettings, true);

            m_reorderableList.DoLayoutList();

            serializedObject.ApplyModifiedProperties();
        }
    }
}