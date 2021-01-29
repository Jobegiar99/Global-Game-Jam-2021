using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using Utilities.EditorTools;
using UnityEngine.UIElements;

namespace Gote
{
    public abstract class ListDrawer : PropertyDrawer
    {
        public abstract string Title { get; }
        public abstract string PropertyName { get; }

        private class ViewData
        {
            public bool listOpen = false;
        }

        private Dictionary<string, ViewData> m_PerPropertyViewData = new Dictionary<string, ViewData>();

        private void CreateHeader(ref Rect position, SerializedProperty property)
        {
            var data = GetData(property);
            Rect rect = position;
            rect.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.LabelField(rect, Title.ToUpper(), EditorHelper.LabelStyle(10, false));
            rect.x += rect.width - 20f;
            rect.width = 20f;
            data.listOpen = EditorGUI.Toggle(rect, data.listOpen);
            EditorGUI.EndFoldoutHeaderGroup();
            rect.x -= 30f;
            rect.width = 20f;
            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            if (data.listOpen)
            {
                var prop = property.FindPropertyRelative(PropertyName);
                if (GUI.Button(rect, "+"))
                {
                    prop.arraySize += 1;
                }
            }
        }

        private void CreateElements(ref Rect position, SerializedProperty property)
        {
            var data = GetData(property);
            Rect rect = position;
            rect.height = EditorGUIUtility.singleLineHeight;
            var listProperty = property.FindPropertyRelative(PropertyName);

            for (int i = 0; i < listProperty.arraySize; i++)
            {
                var itemRect = rect;
                itemRect.width -= 30f;

                var prop = listProperty.GetArrayElementAtIndex(i);
                EditorGUI.PropertyField(itemRect, prop, new GUIContent(""));
                itemRect.x += itemRect.width + 10;
                itemRect.width = 20f;

                if (GUI.Button(itemRect, "-"))
                {
                    listProperty.DeleteArrayElementAtIndex(i);
                }
                rect.y += EditorGUIUtility.singleLineHeight;
            }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            CreateHeader(ref position, property);
            if (GetData(property).listOpen) CreateElements(ref position, property);
            //var list = CreateList(property);
            // list.DoList(position);
            EditorGUI.EndProperty();
        }

        private ViewData GetData(SerializedProperty property)
        {
            ViewData viewData;
            if (!m_PerPropertyViewData.TryGetValue(property.propertyPath, out viewData))
            {
                viewData = new ViewData();
                m_PerPropertyViewData[property.propertyPath] = viewData;
            }

            return viewData;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var headerHeight = EditorGUIUtility.standardVerticalSpacing + EditorGUIUtility.singleLineHeight;
            float height = 0;
            if (GetData(property).listOpen)
            {
                height += EditorGUIUtility.standardVerticalSpacing +
                    EditorGUIUtility.singleLineHeight * property.FindPropertyRelative(PropertyName).arraySize;
            }
            return headerHeight + height;
        }
    }
}