using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using Utilities.EditorTools;

namespace Utilities.UI.Menu
{
    [CanEditMultipleObjects, CustomEditor(typeof(MenuNavigation))]
    public class MenuNavigationDrawer : CustomEditorBase
    {
        private ReorderableList m_list;
        private bool showList = false;

        public void SetupSpawnHelperList()
        {
            var listProperty = serializedObject.FindProperty("allMenus");
            m_list = new ReorderableList(serializedObject, listProperty, true, true, true, true);

            m_list.drawHeaderCallback += (Rect rect) =>
            {
                Rect lineRect = rect;
                EditorGUI.LabelField(lineRect, "MENUS", EditorHelper.CenteredStyle(10, true));
            };

            //Dibujar el elemento de la lista
            m_list.drawElementCallback += (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                float lineHeight = EditorGUIUtility.singleLineHeight;
                float oldWidth = EditorGUIUtility.labelWidth;
                //Elemento de la lista
                var prop = listProperty.GetArrayElementAtIndex(index);
                var linerect = rect;
                EditorGUI.PropertyField(rect, prop, new GUIContent());

                EditorGUIUtility.labelWidth = oldWidth;
            };

            m_list.elementHeightCallback = (int index) =>
            {
                return EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing * 2f;
            };
        }

        private void OnEnable()
        {
            SetupSpawnHelperList();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            EditorHelper.DrawSection("VALUES", () =>
            {
                var infoProp = serializedObject.FindProperty("GraphData");
                EditorGUILayout.PropertyField(infoProp, new GUIContent("Graph Data"));
            });

            EditorHelper.DrawSection("MENUS", () =>
            {
                var menusProp = serializedObject.FindProperty("allMenus");

                m_list.DoLayoutList();
            }, ref showList, false);

            if (GUILayout.Button("Load all Menus"))
            {
                var menus = FindObjectsOfType<MenuBase>();
                m_list.serializedProperty.ClearArray();
                m_list.serializedProperty.arraySize = menus.Length;
                for (int i = 0; i < menus.Length; i++)
                {
                    m_list.serializedProperty.GetArrayElementAtIndex(i).objectReferenceValue =
                        menus[i];
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}