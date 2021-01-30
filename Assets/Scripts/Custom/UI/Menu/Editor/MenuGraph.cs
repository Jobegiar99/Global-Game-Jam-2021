using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Utilities.UI.Menu
{
    public class MenuGraph : EditorWindow
    {
        private MenuGraphView _graphView;
        private string _filename = "New Menu Navigation";

        [MenuItem("Utilities/Menu Graph")]
        public static void OpenGraphWindow()
        {
            var window = GetWindow<MenuGraph>();
            window.titleContent = new GUIContent("Menu");
        }

        private void OnEnable()
        {
            ConstructGraph();
            GenerateToolbar();
        }

        private void OnDisable()
        {
            rootVisualElement.Remove(_graphView);
        }

        private void ConstructGraph()
        {
            _graphView = new MenuGraphView()
            {
                name = "Menu Graph"
            };
            _graphView.StretchToParentSize();
            rootVisualElement.Add(_graphView);
        }

        private void GenerateToolbar()
        {
            var toolbar = new Toolbar();

            var fileNameTextField = new TextField("File name: ");
            fileNameTextField.SetValueWithoutNotify(_filename);
            fileNameTextField.MarkDirtyRepaint();
            fileNameTextField.RegisterValueChangedCallback(val => _filename = val.newValue);
            toolbar.Add(fileNameTextField);

            toolbar.Add(new Button(Save) { text = "Save" });
            toolbar.Add(new Button(Load) { text = "Load" });

            var nodeCreateButton = new Button(() => { _graphView.CreateNode("Node"); });
            nodeCreateButton.text = "Create Node";
            toolbar.Add(nodeCreateButton);
            rootVisualElement.Add(toolbar);
        }

        private void RequestDataOperation(bool save)
        {
            if (string.IsNullOrEmpty(_filename))
            {
                EditorUtility.DisplayDialog("Invalid file name!", "Please enter a valid filename", "ok");
                return;
            }

            var saveUtility = GraphSaveUtility.GetInstance(_graphView);
            if (save) { saveUtility.SaveGraph(_filename); }
            else { saveUtility.LoadGraph(_filename); }
        }

        private void Save()
        {
            RequestDataOperation(true);
        }

        private void Load()
        {
            RequestDataOperation(false);
        }
    }
}