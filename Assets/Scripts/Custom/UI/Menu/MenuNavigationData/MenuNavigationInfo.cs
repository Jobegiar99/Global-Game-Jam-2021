using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace Utilities.UI.Menu
{
    [System.Serializable]
    public class MenuNavigationInfo : ScriptableObject
    {
        public List<NodeLinkData> links = new List<NodeLinkData>();
        public List<MenuNodeData> nodes = new List<MenuNodeData>();

        [SerializeField] public List<Node> Graph;

        [System.Serializable]
        public struct Node
        {
            public int index;
            public string id;
            public string menuId;
            public string parentId;
            public List<int> neighbors;
        };

        public Node GetNode(MenuBase menu)
        {
            return Graph.Find((n) => n.menuId == menu.menuId);
        }

        public List<Node> GetPath(MenuBase start, MenuBase end)
        {
            var startIndex = GetNode(start).index;
            var endIndex = GetNode(end).index;
            return GetPath(startIndex, endIndex);
        }

        public List<Node> GetPath(int startIndex, int endIndex)
        {
            var prev = Solve(startIndex);
            return ReconstructPath(startIndex, endIndex, prev);
        }

        private List<Node> ReconstructPath(int startIndex, int endIndex, int[] prev)
        {
            var path = new List<Node>();
            for (int i = endIndex; i >= 0; i = prev[i])
            {
                path.Add(Graph[i]);
            }
            path.Reverse();

            if (path[0].index == startIndex)
                return path;
            return null;
        }

        public int[] Solve(int startIndex)
        {
            var q = new Queue<int>();
            q.Enqueue(startIndex);
            var visited = new bool[Graph.Count];
            visited[startIndex] = true;
            var prev = new int[Graph.Count];
            for (int i = 0; i < prev.Length; i++)
            {
                prev[i] = -1;
            }

            while (q.Count != 0)
            {
                var node = q.Dequeue();
                var neighbours = Graph[node].neighbors;

                foreach (var next in neighbours)
                {
                    if (!visited[next])
                    {
                        q.Enqueue(next);
                        visited[next] = true;
                        prev[next] = node;
                    }
                }
            }
            return prev;
        }
    }
}