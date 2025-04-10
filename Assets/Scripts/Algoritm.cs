using System;
using UnityEngine;

public class Algoritm : MonoBehaviour
{
    class Node
    {
        public int value;

        public edges[] edges;

        public void show()
        {
            Console.Write("[");
            Console.Write(value);
            Console.Write("]");
        }
    }

    class edges
    {
        public Node node1;
        public Node node2;

        public void SetNodes(Node _node1, Node _node2)
        {
            node1 = _node1;
            node2 = _node2;
        }
    }

    class Graph
    {
        public Node root;

        public Graph()
        {
            root = null;
        }

        public Node ReturnRoot()
        {
            return root;
        }

        public void AddNode(int x)
        {
            Node newNode = new Node();
            newNode.value = x;
            if (root == null)
            {
                root = newNode;
            }
            else
            {

            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
