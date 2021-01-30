using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Structures
{
    [System.Serializable]
    public class Node : IEnumerable<Node>
    {
        public float value;

        public Node parent;
        public List<Node> children = new List<Node>();

        public Node(float value)
        {
            this.value = value;
        }

        public void Add(Node node)
        {
            if (node.parent != null)
            {
                node.parent.children.Remove(node);
            }

            node.parent = this;
            children.Add(node);
        }

        public IEnumerator<Node> GetEnumerator()
        {
            return children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Depth { get { return parent != null ? parent.Depth + 1 : 0; } }
        public int Count { get { return children.Count; } }
        public bool IsALeaf { get { return Count == 0; } }
    }
}