using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

#if UNITY_EDITOR

namespace Utilities.EditorTools
{
    public class EditorHelperRect
    {
        public static void ToggleButton(Rect rect, string text, ref bool toggled, UnityAction<bool> action)
        {
            if (GUI.Button(rect, text, toggled ? EditorHelper.ToggleButtonStyleToggled : EditorHelper.ToggleButtonStyleNormal))
            {
                toggled = !toggled;

                action?.Invoke(toggled);
            }
        }

        public static void Header(Rect rect, string text, int size = 14)
        {
            var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = size };
            EditorGUI.LabelField(rect, text, style);
        }

        public static void Divider(Rect rect)
        {
            var c = GUI.color;
            GUI.color = Color.grey;
            GUI.Box(rect, GUIContent.none, EditorHelper.HorizontalLine);
            GUI.color = c;
        }

        public static void DrawSection(Rect rect, string name, Events.Event draw, ref bool value)
        {
            var line = rect;
            line.height = EditorGUIUtility.singleLineHeight;

            value = EditorGUI.BeginFoldoutHeaderGroup(rect, value, name);
            if (value)
            {
                draw();
            }
            EditorGUI.EndFoldoutHeaderGroup();
        }

        public static void DrawSection(Rect rect, string name, Events.Event draw)
        {
            //if (useBox) EditorGUILayout.BeginVertical("Box");
            var line = rect;
            line.height = EditorGUIUtility.singleLineHeight;
            Header(line, name.ToUpper(), 11);
            draw();
            //if (useBox) EditorGUILayout.EndVertical();
        }
    }
}

#endif