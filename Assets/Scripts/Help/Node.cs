// Node.cs
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Node> neighbors = new List<Node>();
    public List<Edge> edges = new List<Edge>();
    public Edge correctEdge = null;
    public bool visited = false;

    private Renderer rend;

    public int value;
    public Color TextColor;
    public TMP_Text TextBox;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        TextBox = transform.Find("Canvas/Text (TMP)").GetComponent<TMP_Text>();
    }

    public void AddEdge(Edge edge)
    {
        edges.Add(edge);
    }

    public void SetText()
    {
        TextBox.text = value.ToString();
    }

    public void ResetNode()
    {
        visited = false;
        if (rend != null)
            rend.material.color = Color.white;
        UpdateNodeVal("-");
    }

    public void UpdateNodeVal(string val)
    {
        Int32.TryParse(val, out value);
        TextBox.text = val;
    }    

    public void Highlight(Color color)
    {
        if (rend != null)
            rend.material.color = color;
    }

    public void SetValue(int val)
    {
        value = val;
    }

    public int GetValue()
    {
        return value;
    }

    public void SetColor(Color color)
    {
        TextColor = color;
    }

    public Color GetColor()
    {
        return TextColor;
    }
}
