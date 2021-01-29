using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Structures;

namespace Utilities.AI
{
    public class ASNode : Node, IHeapItem<ASNode>
    {
        public ASNode() : base(0)
        {
        }

        public int gCost;//From start
        public int hCost;//From end
        public int fCost { get { return gCost + hCost; } }

        public int HeapIndex { get; set; }

        public int CompareTo(ASNode other)
        {
            int compare = fCost.CompareTo(other.fCost);
            if (compare == 0) compare = hCost.CompareTo(other.hCost);
            return -compare;
        }

        public virtual bool Walkable { get { return true; } }
    }

    public abstract class AStar
    {
        public abstract List<ASNode> GetNeighbors(ASNode node);

        public abstract int GetDistance(ASNode a, ASNode b);

        public abstract int MaxNodes { get; }

        public List<ASNode> FindPath(ASNode start, ASNode target)
        {
            Heap<ASNode> open = new Heap<ASNode>(MaxNodes);//To search
            HashSet<Node> closedSet = new HashSet<Node>();//Already searched

            open.Add(start);

            while (open.Count > 0)
            {
                ASNode currentNode = open.Pop();//Also removes it
                closedSet.Add(currentNode);

                if (currentNode == target)//Found
                {
                    return RetracePath(start, target);
                }

                foreach (ASNode neighbor in GetNeighbors(currentNode))
                {
                    if (!neighbor.Walkable && (neighbor != start && neighbor != target) || closedSet.Contains(neighbor)) continue;

                    int movementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);
                    if (movementCostToNeighbor < neighbor.gCost || !open.Contains(neighbor))
                    {
                        neighbor.gCost = movementCostToNeighbor;
                        neighbor.hCost = GetDistance(neighbor, target);
                        currentNode.Add(neighbor);//Set the parent of neighbor as current node

                        if (!open.Contains(neighbor))
                            open.Add(neighbor);
                    }
                }
            }

            return null;
        }

        private List<ASNode> RetracePath(ASNode start, ASNode end)
        {
            List<ASNode> path = new List<ASNode>();
            ASNode currentNode = end;

            while (currentNode != start)
            {
                path.Add(currentNode);
                currentNode = (ASNode)currentNode.parent;
            }
            path.Add(currentNode);
            path.Reverse();
            return path;
        }
    }
}