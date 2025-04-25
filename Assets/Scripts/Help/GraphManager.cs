using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//SEPARAR LOGICA DE DIBUJADO DE CAMINO Y ALGORITMO

public class GraphManager : MonoBehaviour
{
    public List<Node> allNodes = new List<Node>();
    public Node startNode; // assign this in inspector for UI buttons
    public Color _DFSColor;
    public Color _BFSColor;

    [Range(0.0f, 1.0f)]
    public float WhaitTime;

    public Color LineTextColor;

    public Slider slider;

    public void Start()
    {
        DrawAllEdges();
        ResetGraph();
        slider = FindObjectOfType<Slider>();
    }

    public void ResetGraph()
    {
        StopAllCoroutines();
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
                EdgeDrawer.DrawEdge(node, neighbor, LineTextColor);
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

    public void Djkstras()
    {
        if(startNode != null)
        {
            ResetGraph();
            StartCoroutine(Djkstras(startNode));
        }
    }

    private IEnumerator DFS(Node start)
    {
        Stack<Node> stack = new Stack<Node>();
        stack.Push(start);
        int Number = 0;


        while (stack.Count > 0)
        {
            Node current = stack.Pop();
            if (!current.visited)
            {                
                current.visited = true;
                current.Highlight(_DFSColor);
                current.UpdateNodeVal(Number.ToString());
                Number++;
                yield return new WaitForSeconds(WhaitTime);

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
        int Number = 0;

        while (queue.Count > 0)
        {
            Node current = queue.Dequeue();
            if (!current.visited)
            {
                current.visited = true;
                current.Highlight(_BFSColor);
                current.UpdateNodeVal(Number.ToString());
                Number++;
                yield return new WaitForSeconds(WhaitTime);

                foreach (var neighbor in current.neighbors)
                {
                    queue.Enqueue(neighbor);
                }
            }
        }
    }

    private IEnumerator Djkstras(Node start)
    {
        Queue<Node> queue = new Queue<Node>();
        queue.Enqueue(start);
        int Number = 0;

        while (queue.Count > 0)
        {
            Node current = queue.Dequeue();
            if (!current.visited)
            {
                current.visited = true;
                current.Highlight(_BFSColor);
                current.UpdateNodeVal(Number.ToString());
                Number++;
                yield return new WaitForSeconds(WhaitTime);

                foreach (var neighbor in current.neighbors)
                {
                    queue.Enqueue(neighbor);
                }
            }
        }
    }

    public void SetWait()
    {
        WhaitTime = slider.value;
    }

    public float GetWhaitTime()
    {
        return WhaitTime;
    }
}