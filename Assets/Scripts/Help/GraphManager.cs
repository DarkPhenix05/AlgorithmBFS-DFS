using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GraphManager : MonoBehaviour
{
    public static GraphManager Instance;

    public Node[] allNodes;
    public Node startNode;
    public Node goalNode;

    public bool isAlgorithmRunning = false; // Track if the algorithm is running

    public Slider waitTime;
    private TextMeshPro waitText;

    public Slider distance;
    private TextMeshPro distText;


    public float waitfloat = 0.0f;
    public float distancefloat = 0.0f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }


    public void Start()
    {
        allNodes = FindObjectsOfType<Node>();

        distText = distance.GetComponentInChildren<TextMeshPro>();
        waitText = waitTime.GetComponentInChildren<TextMeshPro>();

        SetDistance(5.0f);
        SetWait(0.5f);

        DrawAllEdges();
    }

    public void ResetGraph()
    {
        StopAllCoroutines();
        StopAlgorithm();
        
        foreach (var node in allNodes)
        {
            node.ResetNode();
        }
    }

    public void ClearAllEdges()
    {
        GameObject edgesParent = GameObject.Find("Edges");
        if (edgesParent != null)
            DestroyImmediate(edgesParent);

        GameObject labelsParent = GameObject.Find("EdgeLabels");
        if (labelsParent != null)
            DestroyImmediate(labelsParent);
    }

    public void DrawAllEdges()
    {
        ClearAllEdges();

        foreach (var node in allNodes)
        {
            if (node.neighbors.Count >= 0)
            {
                foreach (var neighbor in node.neighbors)
                {
                    EdgeDrawer.DrawEdge(node, neighbor);
                }
            }
            else
            {

            }
        }
    }

    public void AutoConnectNodes()
    {
        ClearAllEdges();

        if (allNodes == null || allNodes.Length == 0)
            allNodes = FindObjectsOfType<Node>();

        foreach (Node node in allNodes)
        {
            node.neighbors.Clear();
            node.weightedNeighbors.Clear();
        }

        foreach (Node node in allNodes)
        {
            foreach (Node other in allNodes)
            {
                if (node == other)
                    continue;

                float dist = Vector3.Distance(node.transform.position, other.transform.position);
                if (dist <= distancefloat)
                {
                    if (!node.neighbors.Contains(other))
                    {
                        node.neighbors.Add(other);
                        node.weightedNeighbors.Add(new NeighborInfo(other, dist));

                        // Draw the edge visually
                        EdgeDrawer.DrawEdge(node, other);
                    }
                }
            }
        }
    }

    public void StartDFS()
    {
        if (startNode == null)
        {

            return;
        }

        else
        {
            StartAlgorithm();
            ResetGraph();
            StartCoroutine(DFS(startNode));
        }
    }

    public void StartBFS()
    {
        if (startNode == null)
        {

            return;
        }

        else
        {
            StartAlgorithm();
            ResetGraph();
            StartCoroutine(BFS(startNode));
        }
    }

    public void StartDijkstra()
    {
        if (startNode == null)
        {

            return;
        }

        if (goalNode == null)
        {

            return;
        }

        else
        {
            StartAlgorithm();
            ResetGraph();
            StartCoroutine(Dijkstra(startNode, goalNode));

        }
    }

    public void StartAStar()
    {
        if (startNode == null)
        {

            return;
        }

        if (goalNode == null)
        {

            return;
        }

        else
        {
            StartAlgorithm();
            ResetGraph();
            StartCoroutine(AStar(startNode, goalNode));
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
                yield return new WaitForSeconds(waitfloat);

                foreach (var neighbor in current.neighbors)
                {
                    stack.Push(neighbor);
                }
            }
        }

        StopAlgorithm();
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
                yield return new WaitForSeconds(waitfloat);

                foreach (var neighbor in current.neighbors)
                {
                    queue.Enqueue(neighbor);
                }
            }
        }

        StopAlgorithm();
    }

    private IEnumerator Dijkstra(Node start, Node goal)
    {
        Dictionary<Node, float> distances = new Dictionary<Node, float>();
        Dictionary<Node, Node> previous = new Dictionary<Node, Node>();
        List<Node> nodes = new List<Node>(allNodes);

        foreach (Node node in nodes)
        {
            distances[node] = float.MaxValue;
            previous[node] = null;
        }

        distances[start] = 0;

        while (nodes.Count > 0)
        {
            nodes.Sort((a, b) => distances[a].CompareTo(distances[b]));
            Node current = nodes[0];
            nodes.Remove(current);

            if (current == goal)
                break;

            foreach (NeighborInfo neighborInfo in current.weightedNeighbors)
            {
                float alt = distances[current] + neighborInfo.cost;
                if (alt < distances[neighborInfo.node])
                {
                    distances[neighborInfo.node] = alt;
                    previous[neighborInfo.node] = current;
                }
            }

            current.Highlight(Color.yellow);
            yield return new WaitForSeconds(waitfloat);
        }

        Node pathNode = goal;
        while (pathNode != null)
        {
            pathNode.Highlight(Color.green);
            pathNode = previous[pathNode];
            yield return new WaitForSeconds(waitfloat/2.0f);
        }

        
    }

    private IEnumerator AStar(Node start, Node goal)
    {
        Dictionary<Node, float> gScore = new Dictionary<Node, float>();
        Dictionary<Node, float> fScore = new Dictionary<Node, float>();
        Dictionary<Node, Node> previous = new Dictionary<Node, Node>();
        List<Node> openSet = new List<Node>();

        foreach (Node node in allNodes)
        {
            gScore[node] = float.MaxValue;
            fScore[node] = float.MaxValue;
            previous[node] = null;
        }

        gScore[start] = 0;
        fScore[start] = Heuristic(start, goal);
        openSet.Add(start);

        while (openSet.Count > 0)
        {
            openSet.Sort((a, b) => fScore[a].CompareTo(fScore[b]));
            Node current = openSet[0];
            openSet.RemoveAt(0);

            if (current == goal)
                break;

            foreach (NeighborInfo neighborInfo in current.weightedNeighbors)
            {
                float tentativeG = gScore[current] + neighborInfo.cost;
                if (tentativeG < gScore[neighborInfo.node])
                {
                    previous[neighborInfo.node] = current;
                    gScore[neighborInfo.node] = tentativeG;
                    fScore[neighborInfo.node] = tentativeG + Heuristic(neighborInfo.node, goal);
                    if (!openSet.Contains(neighborInfo.node))
                    {
                        openSet.Add(neighborInfo.node);
                    }
                }
            }

            current.Highlight(Color.magenta);
            yield return new WaitForSeconds(waitfloat);
        }

        Node pathNode = goal;
        while (pathNode != null)
        {
            pathNode.Highlight(Color.green);
            pathNode = previous[pathNode];
            yield return new WaitForSeconds(waitfloat/2.0f);
        }

        StopAlgorithm();
    }

    private float Heuristic(Node a, Node b)
    {
        return Vector3.Distance(a.transform.position, b.transform.position);
    }

    public void StartAlgorithm()
    {
        isAlgorithmRunning = true;
    }

    public void StopAlgorithm()
    {
        isAlgorithmRunning = false;
    }

    public void SetStartNode(Node node)
    {
        if (isAlgorithmRunning) return;

        if (startNode != null)
            startNode.Highlight(Color.white);

        startNode = node;
        startNode.Highlight(Color.green);
    }

    public void SetGoalNode(Node node)
    {
        if (isAlgorithmRunning) return;

        if (goalNode != null)
            goalNode.Highlight(Color.white);

        goalNode = node;
        goalNode.Highlight(Color.red);
    }

    public void SetWait()
    {
        waitfloat = waitTime.value;
        waitText.text = waitfloat.ToString("F1") + " s";
    }

    public void SetWait(float val)
    {
        waitfloat = val;
        waitText.text = val.ToString("F1") + " s";
        waitTime.value = val;
    }

    public float GetWaitTime()
    {
        return waitfloat;
    }

    public void SetDistance()
    {
        distancefloat = distance.value;
        distText.text = distancefloat.ToString("F1") + " u";
    }
    public void SetDistance(float val)
    {
        distancefloat = val;
        distText.text = val.ToString("F1") + " u";
        distance.value = val;
    }

    public float GetDistance()
    {
        return distancefloat;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(GraphManager))]
public class GraphManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GraphManager gm = (GraphManager)target;
        if (GUILayout.Button("Clear All Edges and Labels"))
        {
            gm.ClearAllEdges();
        }
    }
}
#endif