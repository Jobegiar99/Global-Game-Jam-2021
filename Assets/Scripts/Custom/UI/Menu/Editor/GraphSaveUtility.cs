using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Utilities.UI.Menu
{
    public class GraphSaveUtility
    {
        private MenuGraphView _targetView;
        private MenuNavigationInfo _assetCache;
        private List<Edge> Edges => _targetView.edges.ToList();
        private List<MenuNode> Nodes => _targetView.nodes.ToList().Cast<MenuNode>().ToList();

        public static GraphSaveUtility GetInstance(MenuGraphView view)
        {
            return new GraphSaveUtility { _targetView = view };
        }

        public void SaveGraph(string filename)
        {
            if (!Edges.Any()) return;

            var asset = ScriptableObject.CreateInstance<MenuNavigationInfo>();

            var connectedPorts = Edges.Where(x => x.input.node != null).ToArray();

            for (int i = 0; i < connectedPorts.Length; i++)
            {
                var outputNode = connectedPorts[i].output.node as MenuNode;
                var inputNode = connectedPorts[i].input.node as MenuNode;

                asset.links.Add(new NodeLinkData()
                {
                    baseNodeId = outputNode.id,
                    targetNodeGuid = inputNode.id,
                    portName = connectedPorts[i].output.portName
                });
            }
            foreach (var node in Nodes.Where(node => !node.EntryPoint))
            {
                asset.nodes.Add(new MenuNodeData
                {
                    id = node.id,
                    menuId = node.menuId,
                    position = node.GetPosition().position
                });
            }

            asset.Graph = CreateGraph(asset, true);

            if (!AssetDatabase.IsValidFolder("Assets/Game")) { AssetDatabase.CreateFolder("Assets", "Game"); }
            if (!AssetDatabase.IsValidFolder("Assets/Game/Menus")) { AssetDatabase.CreateFolder("Assets/Game", "Menus"); }
            AssetDatabase.CreateAsset(asset, $"Assets/Game/Menus/{filename}.asset");
            AssetDatabase.SaveAssets();
        }

        private List<MenuNavigationInfo.Node> CreateGraph(MenuNavigationInfo asset, bool bidirectional)
        {
            var graph = new List<MenuNavigationInfo.Node>();
            Dictionary<string, MenuNavigationInfo.Node> nodes = new Dictionary<string, MenuNavigationInfo.Node>();
            MenuNavigationInfo.Node root = new MenuNavigationInfo.Node()
            {
                index = 0,
                id = asset.links[0].baseNodeId,
                neighbors = new List<int>()
            };

            graph.Add(root);
            nodes.Add(root.id, root);

            //Create nodes
            for (int i = 0; i < asset.nodes.Count; i++)
            {
                var node = asset.nodes[i];
                var n = new MenuNavigationInfo.Node()
                {
                    index = i + 1,
                    id = node.id,
                    menuId = node.menuId,
                    neighbors = new List<int>()
                };

                nodes.Add(n.id, n);
                //graph.Add(n);
            }

            asset.links.ForEach(edge =>
            {
                nodes[edge.baseNodeId].neighbors.Add(nodes[edge.targetNodeGuid].index);//Bi-directional
                if (bidirectional) nodes[edge.targetNodeGuid].neighbors.Add(nodes[edge.baseNodeId].index);//Bi-directional

                var node = nodes[edge.targetNodeGuid];
                node.parentId = nodes[edge.baseNodeId].menuId;
                nodes[edge.targetNodeGuid] = node;
            });

            foreach (var n in nodes.Values)
            {
                if (n.index != 0) graph.Add(n);
            }

            nodes.Clear();

            return graph;
        }

        public void LoadGraph(string filename)
        {
            _assetCache = AssetDatabase.LoadAssetAtPath<MenuNavigationInfo>($"Assets/Game/Menus/{filename}.asset");
            if (_assetCache == null)
            {
                EditorUtility.DisplayDialog("File not found", "Target file does not exists", "ok");
                return;
            }

            ClearGraph();
            GenerateNodes();
            ConnectNodes();
        }

        private void ConnectNodes()
        {
            foreach (var n in Nodes)
            {
                var connections = _assetCache.links.Where(x => x.baseNodeId == n.id).ToList();
                connections.ForEach(c =>
                {
                    var targetNodeGuid = c.targetNodeGuid;
                    var targetNode = Nodes.Find(x => x.id == targetNodeGuid);
                    LinkNodes(n.outputContainer[0].Q<Port>(), targetNode.inputContainer[0].Q<Port>());
                    targetNode.SetPosition(new Rect(
                        _assetCache.nodes.Find(x => x.id == targetNodeGuid).position,
                        _targetView.defaultNodeSize
                        ));
                });
            }
        }

        private void LinkNodes(Port output, Port input)
        {
            var tempEdge = new Edge { output = output, input = input };
            tempEdge?.input.Connect(tempEdge);
            tempEdge?.output.Connect(tempEdge);
            _targetView.Add(tempEdge);
        }

        private void GenerateNodes()
        {
            foreach (var nodeData in _assetCache.nodes)
            {
                var tempNode = _targetView.CreateMenuNode(nodeData.menuId);
                tempNode.id = nodeData.id;
                _targetView.AddElement(tempNode);
                //  _targetView.AddOutputPort(tempNode);
                /*
            var nodePorts = _assetCache.links.Where(x => x.baseNodeId == nodeData.id).ToList();
            nodePorts.ForEach(x => _targetView.AddOutputPort(tempNode));
*/
            }
        }

        private void ClearGraph()
        {
            Nodes.Find(x => x.EntryPoint).id = _assetCache.links[0].baseNodeId;

            foreach (var node in Nodes)
            {
                if (node.EntryPoint) continue;
                Edges.Where(x => x.input.node == node).ToList()
                    .ForEach(edge => { _targetView.RemoveElement(edge); });

                _targetView.RemoveElement(node);
            }
        }
    }
}