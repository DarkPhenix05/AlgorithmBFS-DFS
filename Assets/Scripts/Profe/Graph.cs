using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Graph
{
    private List<Node> nodes = new List<Node>();
    private Queue<Node> _nodesToCheck;
    private Node _root;

    public Graph()
    {

    }

    public void AddNode(Node from, Node newNode)
    {
        Edge edge = new Edge(from, newNode);

        from.AddEdges(edge);
        newNode.AddEdges(edge);

        nodes.Add(newNode);
    }

    public void SetNode (Node node)
    {

    }

    public void SetRoot(Node root)
    {
        _root = root;
    }

    public Node GetRoot()
    {
        return _root;
    }

    public void BFS()
    {
        foreach(Edge e in _root.GetEdges())
        {
            e.SetVisited(true);
            if(e.GetNodeTo()!= _root)
            {
                _nodesToCheck.Enqueue(e.GetNodeTo());
            }
            if(e.GetNodeFrom()!= _root)
            {
                _nodesToCheck.Enqueue(e.GetNodeFrom());
            }
        }
        while(_nodesToCheck.Count > 0);
        {
            BFS2(_nodesToCheck.First());
        }
    }


    public void DFS()
    {
        foreach(Edge e in _root.GetEdges())
        {
            e.SetVisited(true);
            if (e.GetNodeTo() != _root)
            {
                _nodesToCheck.Enqueue(e.GetNodeTo());
            }
            if (e.GetNodeFrom() != _root)
            {
                _nodesToCheck.Enqueue(e.GetNodeFrom());
            }
        }

        while (_nodesToCheck.Count > 0)
        {
            DFS2(_nodesToCheck.First());
        }

    }

    private void BFS2(Node node)
    {
        foreach (Edge e in _root.GetEdges())
        {
            e.SetVisited(true);
            if (e.GetNodeTo() != node)
            {
                _nodesToCheck.Enqueue(e.GetNodeTo());
            }
            if (e.GetNodeFrom() != node)
            {
                _nodesToCheck.Enqueue(e.GetNodeFrom());
            }
        }

        _nodesToCheck.Dequeue();
    }

    private void DFS2(Node node)
    {
        foreach (Edge e in _root.GetEdges())
        {
            e.SetVisited(true);
            if (e.GetNodeTo() != node)
            {
                _nodesToCheck.Enqueue(e.GetNodeTo());
            }
            if (e.GetNodeFrom() != node)
            {
                _nodesToCheck.Enqueue(e.GetNodeFrom());
            }

        }

        DFS2(_nodesToCheck.Dequeue());
    }

    //Algoritmo DEHIKSTRA (Current+Peso)

    //Algorimo A* (Current+Peso+DistanciaManjatan)
    //  Es una buscqueda informada, dependeindo de la DM, seteada dependiendo del comportamiento que se busca.
}
