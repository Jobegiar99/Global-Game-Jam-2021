using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Structures;

namespace Utilities.AI
{
    public class TreeAlgorithms
    {

        public class Node : Utilities.Structures.Node
        {
            public Node(float value) : base(value) { }
            public int movements;

        }

        public static Node Minimax(Node node, int maxEvaluationDepth, bool isMax, float alpha = -int.MaxValue, float beta = int.MaxValue)
        {
            if (maxEvaluationDepth == 0 || node.IsALeaf) return node;

            if (isMax)
            {
                float value = -int.MaxValue;
                Node n = null;

                foreach (var child in node.children)
                {
                    float eval = Minimax((Node)child, maxEvaluationDepth - 1, false, alpha, beta).value;
                    if (eval > value) n = (Node)child;

                    value = Mathf.Max(value, eval);
                    alpha = Mathf.Max(alpha, eval);
                    if (beta <= alpha) break;
                }
                n.value = value;
                return n;
            }
            else
            {
                float value = int.MaxValue;
                Node n = null;
                foreach (var child in node.children)
                {
                    float eval = Minimax((Node)child, maxEvaluationDepth - 1, true, alpha, beta).value;
                    if (eval < value) n = (Node)child;
                    value = Mathf.Min(value, eval);
                    beta = Mathf.Min(beta, eval);
                    if (beta <= alpha)
                        break;
                }
                n.value = value;
                return n;
            }

        }



        public static Node BuildTree(int nTokens, int movements = 0)
        {
            Node n = new Node(nTokens);
            n.movements = movements;

            if (n.value >= 1)
            {
                n.Add(BuildTree(nTokens - 1, 1));
            }
            if (n.value >= 2)
            {
                n.Add(BuildTree(nTokens - 2, 2));
            }
            if (n.value >= 3)
            {
                n.Add(BuildTree(nTokens - 3, 3));
            }
            return n;

        }


        public static void AsignUtilityFunctionValues(Node node, bool max = false)
        {
            if (node.IsALeaf)
            {
                node.value = max ? 1 : -1f;
            }

            foreach (var item in node.children)
            {
                AsignUtilityFunctionValues((Node)item, !max);
            }

        }

    }
}