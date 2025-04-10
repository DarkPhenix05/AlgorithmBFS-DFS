using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    private int _ID;
    private float _value;
    private List<Edge> _edges = new List<Edge>();

    public Node(Edge edge, float value, int id) 
    {
        _ID = id;
        _value = value;
        _edges.Add(edge);
    }

    public Node()
    {

    }

    public void AddEdges(Edge edge)
    {
        _edges.Add(edge);
    }
    public List<Edge> GetEdges() 
    {
        return _edges;
    }

    public void SetValue(float value)
    {
        _value = value;
    }
    public float GetValue()
    {
        return _value;
    }
}
