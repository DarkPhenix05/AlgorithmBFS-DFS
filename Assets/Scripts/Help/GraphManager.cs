using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphManager : MonoBehaviour
{
    public List<Node> allNodes = new List<Node>();
    public Node startNode; // assign this in inspector for UI buttons

    public void Start()
    {
        DrawAllEdges();
    }

    public void ResetGraph()
    {
        foreach (var node in allNodes)
        {
            node.ResetNode();
        }
    }

    public void DrawAllEdges()
    {
        foreach (var node in allNodes)
        {
            foreach (var neighbor in node.neighbors)
            {
                EdgeDrawer.DrawEdge(node, neighbor);
            }
        }
    }

    public void StartDFS()
    {
        if (startNode != null)
        {
            ResetGraph();
            StartCoroutine(DFS(startNode));
        }
    }

    public void StartBFS()
    {
        if (startNode != null)
        {
            ResetGraph();
            StartCoroutine(BFS(startNode));
        }
    }

    private IEnumerator DFS(Node start)
    {
        Stack<Node> stack = new Stack<Node>();
        stack.Push(start);

        while (stack.Count > 0)
        {
            Node current = stack.Pop();
            if (!current.visited)
            {
                current.visited = true;
                current.Highlight(Color.red);
                yield return new WaitForSeconds(0.5f);

                foreach (var neighbor in current.neighbors)
                {
                    stack.Push(neighbor);
                }
            }
        }
    }

    private IEnumerator BFS(Node start)
    {
        Queue<Node> queue = new Queue<Node>();
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            Node current = queue.Dequeue();
            if (!current.visited)
            {
                current.visited = true;
                current.Highlight(Color.blue);
                yield return new WaitForSeconds(0.5f);

                foreach (var neighbor in current.neighbors)
                {
                    queue.Enqueue(neighbor);
                }
            }
        }
    }
}