using UnityEngine;

public class Edge
{
    private Node _nodeFrom;
    private Node _nodeTo;

    private bool _visited = false;

    public Edge(Node from, Node to) //CONSTRUCTOR
    {
        _nodeFrom = from;
        _nodeTo = to;
        _visited = false;
    }

    public Edge()
    {
        _visited = false;
    }


    //ENCAPSULAMENTO - GET & SETS
    public void SetNodeTo(Node node) 
    {
        _nodeTo = node;
    }
    public Node GetNodeTo()
    {
        return _nodeTo;
    }

    public void SetNodeFrom(Node node)
    {
        _nodeFrom = node;
    }
    public Node GetNodeFrom()
    {
        return _nodeFrom;
    }

    public void SetVisited(bool visited) 
    {
        _visited = visited;
    }
    public bool GetVisited() 
    {
        return _visited;
    }
}
