using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Structures;

public class TreeTest : MonoBehaviour
{
    public Node tree = new Node(0)
    {
        new Node(0)
        {
            new Node(0){
                new Node(-1f),
                new Node(3f)
            },
            new Node(5)
            {
                new Node(5),
                new Node(1)
            }
        },
        new Node(0)
        {
            new Node(0)
            {
                new Node(-6),
                new Node(-4)
            },
            new Node(0)
            {
                new Node(0),
                new Node(9)
            }

        }
    };


    private void Awake()
    {
        //Node t1 = TreeAlgorithms.BuildTree(9);
        // TreeAlgorithms.AsignUtilityFunctionValues(t1);
        //Node n = TreeAlgorithms.Minimax(tree, 100, true);
        //if (n != null) Debug.Log(n.value + " " + (n.parent == tree));
        //Preorder(tree);


    }

    public void Preorder(Node node)
    {
        string spacing = "";

        for (int i = 0; i < node.Depth; i++)
        {
            spacing += ">";
        }


        Debug.Log(spacing + node.value);
        foreach (var item in node.children)
        {
            Preorder(item);
        }


    }
}
