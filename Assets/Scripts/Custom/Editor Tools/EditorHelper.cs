using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

#if UNITY_EDITOR

namespace Utilities.EditorTools
{
    public class EditorHelper
    {
        private static GUIStyle _toggleButtonStyleNormal = null;

        public static GUIStyle ToggleButtonStyleNormal
        {
            get
            {
                if (_toggleButtonStyleNormal == null) _toggleButtonStyleNormal = "Button";
                return _toggleButtonStyleNormal;
            }
        }

        private static GUIStyle _toggleButtonStyleToggled = null;

        public static GUIStyle ToggleButtonStyleToggled
        {
            get
            {
                if (_toggleButtonStyleToggled == null)
                {
                    _toggleButtonStyleToggled = new GUIStyle(EditorStyles.toolbarButton);
                    _toggleButtonStyleToggled.fontStyle = FontStyle.Bold;
                    _toggleButtonStyleToggled.normal.background = EditorStyles.toolbarButton.active.background;
                }
                return _toggleButtonStyleToggled;
            }
        }

        private static GUIStyle _horizontalLine = null;

        public static GUIStyle HorizontalLine
        {
            get
            {
                if (_horizontalLine == null)
                {
                    _horizontalLine = new GUIStyle();
                    _horizontalLine.normal.background = EditorGUIUtility.whiteTexture;
                    _horizontalLine.margin = new RectOffset(0, 0, 4, 4);
                    _horizontalLine.fixedHeight = 1;
                }
                return _horizontalLine;
            }
        }

        public static void ToggleButton(string text, ref bool toggled, UnityAction<bool> action)
        {
            if (GUILayout.Button(text, toggled ? ToggleButtonStyleToggled : ToggleButtonStyleNormal))
            {
                toggled = !toggled;
                if (action != null) action.Invoke(toggled);
            }
        }

        public static void Header(string text, int size = 14)
        {
            var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = size };
            EditorGUILayout.LabelField(text, style, GUILayout.ExpandWidth(true));
        }

        public static GUIStyle CenteredStyle(int size, bool bold = true)
        {
            return new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = bold ? FontStyle.Bold : FontStyle.Normal, fontSize = size };
        }

        public static GUIStyle LabelStyle(int size, bool bold = true)
        {
            return new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleLeft, fontStyle = bold ? FontStyle.Bold : FontStyle.Normal, fontSize = size, margin = new RectOffset(0, 0, 0, 0) };
        }

        public static void Divider()
        {
            EditorGUILayout.Space();
            var c = GUI.color;
            GUI.color = Color.grey;
            GUILayout.Box(GUIContent.none, HorizontalLine);
            GUI.color = c;
            EditorGUILayout.Space();
        }

        public static void DrawSection(string name, Events.Event draw, ref bool value, bool useBox = true)
        {
            value = EditorGUILayout.BeginFoldoutHeaderGroup(value, name);
            if (value)
            {
                if (useBox) EditorGUILayout.BeginVertical("Box");
                draw();
                if (useBox) EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        public static void DrawSection(string name, Events.Event draw, bool useBox = true)
        {
            if (useBox) EditorGUILayout.BeginVertical("Box");
            Header(name.ToUpper(), 11);
            draw();
            if (useBox) EditorGUILayout.EndVertical();
        }
    }
}

#endif