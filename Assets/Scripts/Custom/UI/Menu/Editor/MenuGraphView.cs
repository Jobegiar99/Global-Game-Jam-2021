using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Utilities.UI.Menu
{
    public class MenuGraphView : GraphView
    {
        public readonly Vector2 defaultNodeSize = new Vector2(150, 200);

        public MenuGraphView()
        {
            styleSheets.Add((StyleSheet)AssetDatabase.LoadAssetAtPath("Assets/Scripts/Custom/UI/Menu/Editor/MenuGraphStyles.uss", typeof(StyleSheet)));
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            var grid = new GridBackground();
            Insert(0, grid);
            grid.StretchToParentSize();
            AddElement(GenerateEntryPointNode());
        }

        private Port GeneratePort(MenuNode node, Direction portDirection, Port.Capacity capacity = Port.Capacity.Multi)
        {
            return node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(float));
        }

        private MenuNode GenerateEntryPointNode()
        {
            var node = new MenuNode()
            {
                title = "root",
                id = System.Guid.NewGuid().ToString(),
                menuId = "ENTRYPOINT",
                EntryPoint = true
            };
            var port = GeneratePort(node, Direction.Output);
            port.portName = "Children";
            node.capabilities &= ~Capabilities.Movable;
            node.capabilities &= ~Capabilities.Deletable;
            node.outputContainer.Add(port);
            node.RefreshExpandedState();
            node.RefreshPorts();

            node.SetPosition(new Rect(100, 200, 100, 150));
            return node;
        }

        public void CreateNode(string name)
        {
            this.AddElement(CreateMenuNode(name));
        }

        public void AddOutputPort(MenuNode node)
        {
            var port = GeneratePort(node, Direction.Output);
            // var oldLabel = port.contentContainer.Q<Label>("type");
            // port.contentContainer.Remove(oldLabel);

            port.portName = "Children";
            node.outputContainer.Add(port);
            node.RefreshPorts();
            node.RefreshExpandedState();
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            var compatiblePorts = new List<Port>();
            ports.ForEach(port =>
            {
                if (startPort != port && startPort.node != port.node)
                {
                    compatiblePorts.Add(port);
                }
            });
            return compatiblePorts;
        }

        public MenuNode CreateMenuNode(string nodeName)
        {
            var node = new MenuNode()
            {
                title = nodeName,
                menuId = nodeName,
                id = System.Guid.NewGuid().ToString()
            };

            var textField = new TextField(string.Empty);
            textField.RegisterValueChangedCallback(evt =>
            {
                node.menuId = evt.newValue;
                node.title = evt.newValue;
            });

            textField.SetValueWithoutNotify(nodeName);

            node.mainContainer.Add(textField);

            var inputPort = GeneratePort(node, Direction.Input, Port.Capacity.Single);
            inputPort.portName = "Input";

            var oldLabel = inputPort.contentContainer.Q<Label>("type");
            inputPort.contentContainer.Remove(oldLabel);

            node.inputContainer.Add(inputPort);
            styleSheets.Add((StyleSheet)AssetDatabase.LoadAssetAtPath("Assets/Scripts/Custom/UI/Menu/Editor/NodeStyle.uss", typeof(StyleSheet)));

            AddOutputPort(node);
            node.SetPosition(new Rect(Vector2.zero, defaultNodeSize));
            return node;
        }
    }
}