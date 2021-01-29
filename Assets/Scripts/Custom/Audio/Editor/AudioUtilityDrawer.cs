using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace Utilities.Audio
{
    [CustomEditor(typeof(AudioUtility), true)]
    [CanEditMultipleObjects]
    public class AudioUtilityDrawer : UnityEditor.Editor
    {
        private ReorderableList m_reordenableList;

        private void OnEnable()
        {
            //Obtener la lista original
            var listProperty = serializedObject.FindProperty("soundsToUse");

            //Crear la lista reordenable
            m_reordenableList = new ReorderableList(serializedObject, listProperty, true, true, true, true);

            m_reordenableList.drawHeaderCallback += (Rect rect) =>
            {
                EditorGUI.LabelField(rect, new GUIContent("Sonidos"));
            };

            //Dibujar el elemento de la lista
            m_reordenableList.drawElementCallback += (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                float lineHeight = EditorGUIUtility.singleLineHeight;
                float currentY = 0;
                //Elemento de la lista
                var prop = listProperty.GetArrayElementAtIndex(index);

                //Properties
                var nameProp = prop.FindPropertyRelative("name");
                var audioProp = prop.FindPropertyRelative("audio");
                var volumeProp = prop.FindPropertyRelative("volume");
                var pitchProp = prop.FindPropertyRelative("pitch");
                var isFoldedProp = prop.FindPropertyRelative("isFolded");

                rect.height = EditorGUIUtility.singleLineHeight * 8;
                Rect itemRect = new Rect(rect.x + 20, rect.y + lineHeight, rect.width - 20, rect.height - lineHeight); currentY = rect.y + lineHeight + 5;

                var nameRect = new Rect(itemRect.x, currentY, itemRect.width, lineHeight); currentY = nameRect.y + lineHeight + 5;
                var header1Rect = new Rect(itemRect.x, currentY, itemRect.width, lineHeight); currentY = header1Rect.y + lineHeight;
                var audioRect = new Rect(itemRect.x, currentY, itemRect.width * 0.2f, lineHeight); currentY = audioRect.y;
                var volumeRect = new Rect(itemRect.x + itemRect.width * 0.2f, currentY, itemRect.width * 0.4f, lineHeight); currentY = audioRect.y;
                var pitchRect = new Rect(itemRect.x + itemRect.width * 0.6f, currentY, itemRect.width * 0.4f, lineHeight); currentY = audioRect.y + lineHeight;

                bool isExpanded = EditorGUI.Foldout(new Rect(rect.x + 10, rect.y + 3, rect.width - 10, lineHeight), isFoldedProp.boolValue, nameProp.stringValue);
                //Update vars
                isFoldedProp.boolValue = isExpanded;

                if (!isExpanded)
                    return;

                //Dibujar
                //Fondos
                EditorGUI.DrawRect(new Rect(audioRect.x - 0.125f * lineHeight, header1Rect.y, audioRect.width + lineHeight * 0.125f, lineHeight * 2 + lineHeight * 0.25f), Color.black);
                EditorGUI.DrawRect(new Rect(volumeRect.x - 0.125f * lineHeight, header1Rect.y, volumeRect.width + lineHeight * 0.125f, lineHeight * 2 + lineHeight * 0.25f), Color.black);
                EditorGUI.DrawRect(new Rect(pitchRect.x - 0.125f * lineHeight, header1Rect.y, pitchRect.width + lineHeight * 0.125f, lineHeight * 2 + lineHeight * 0.25f), Color.black);

                EditorGUI.DrawRect(new Rect(audioRect.x, header1Rect.y + lineHeight * 0.125f, audioRect.width - lineHeight * 0.125f, lineHeight * 2), Color.white);
                EditorGUI.DrawRect(new Rect(volumeRect.x, header1Rect.y + lineHeight * 0.125f, volumeRect.width - lineHeight * 0.125f, lineHeight * 2), Color.white);
                EditorGUI.DrawRect(new Rect(pitchRect.x, header1Rect.y + lineHeight * 0.125f, pitchRect.width - lineHeight * 0.125f, lineHeight * 2), Color.white);

                //EditorGUI.DrawRect(new Rect(itemRect.x, useJoystickRect.y - lineHeight * 0.25f / 2, itemRect.width, lineHeight * 2 + lineHeight * 0.25f), new Color(82, 188, 198));
                //EditorGUI.DrawRect(itemRect, Color.black);

                EditorGUI.PropertyField(nameRect, nameProp, new GUIContent());
                var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };

                EditorGUI.LabelField(new Rect(header1Rect.x, header1Rect.y, itemRect.width * (0.2f), lineHeight), "Audio", style);
                EditorGUI.LabelField(new Rect(header1Rect.x + header1Rect.width * (0.2f), header1Rect.y, itemRect.width * (0.4f), lineHeight), "Volumen", style);
                EditorGUI.LabelField(new Rect(header1Rect.x + header1Rect.width * (0.6f), header1Rect.y, itemRect.width * (0.4f), lineHeight), "Tono", style);

                EditorGUI.PropertyField(new Rect(audioRect.x + lineHeight * 0.0625f, audioRect.y, audioRect.width - lineHeight * 0.25f, audioRect.height), audioProp, new GUIContent());

                float volumeVal = EditorGUI.Slider(new Rect(volumeRect.x + lineHeight * 0.0625f, volumeRect.y, volumeRect.width - lineHeight * 0.25f, volumeRect.height), new GUIContent(), volumeProp.floatValue, 0, 1);
                volumeProp.floatValue = volumeVal;

                float pitchVal = EditorGUI.Slider(new Rect(pitchRect.x + lineHeight * 0.0625f, pitchRect.y, pitchRect.width - lineHeight * 0.25f, pitchRect.height), new GUIContent(), pitchProp.floatValue, -3, 3);
                pitchProp.floatValue = pitchVal;
            };

            m_reordenableList.elementHeightCallback += (int id) =>
            {
                return listProperty.GetArrayElementAtIndex(id).FindPropertyRelative("isFolded").boolValue ? EditorGUIUtility.singleLineHeight * 3 + 30 : EditorGUIUtility.singleLineHeight + 3;
            };
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            serializedObject.Update();

            GUILayout.Space(10);
            m_reordenableList.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }
    }
}