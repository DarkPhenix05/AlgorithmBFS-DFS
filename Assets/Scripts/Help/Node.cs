// Node.cs
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Node> neighbors = new List<Node>();
    public bool visited = false;

    private Renderer rend;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    public void ResetNode()
    {
        visited = false;
        if (rend != null)
            rend.material.color = Color.white;
    }

    public void Highlight(Color color)
    {
        if (rend != null)
            rend.material.color = color;
    }
}
