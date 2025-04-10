using System;
using UnityEngine;

public class Algoritm : MonoBehaviour
{
    class Node
    {
        public int value;

        public Node left;
        public bool leftFree = true;

        public Node right;
        public bool rightFree = true;

        public bool edge = true;

        public void show()
        {
            Console.Write("[");
            Console.Write(value);
            Console.Write("]");
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
