using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class NeighborInfo
{
    public Node node;
    public float cost;

    public NeighborInfo(Node node, float cost)
    {
        this.node = node;
        this.cost = cost;
    }
}

public class Node : MonoBehaviour
{
    public List<Node> neighbors = new List<Node>();
    public List<NeighborInfo> weightedNeighbors = new List<NeighborInfo>();
    public bool visited = false;

    private Renderer rend;
    private TextMeshPro textLabel;

    private void Awake()
    {
        rend = GetComponent<Renderer>();

        // Create a label
        GameObject labelObj = new GameObject("NodeLabel");
        labelObj.transform.SetParent(transform);
        labelObj.transform.localPosition = Vector3.up * 1.2f;
        textLabel = labelObj.AddComponent<TextMeshPro>();
        textLabel.fontSize = 2;
        textLabel.enableAutoSizing = false;
        textLabel.alignment = TextAlignmentOptions.Center;
        textLabel.color = Color.black;
        textLabel.text = gameObject.name;
        textLabel.outlineWidth = 0.2f;
        textLabel.outlineColor = Color.white;

        labelObj.AddComponent<FaceCamera>();
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

    private void OnMouseOver()
    {
        if (GraphManager.Instance != null && !GraphManager.Instance.isAlgorithmRunning) // Check if algorithm is running
        {
            if (Input.GetMouseButtonDown(0)) // Left click
            {
                GraphManager.Instance.SetStartNode(this);
            }
            else if (Input.GetMouseButtonDown(1)) // Right click
            {
                GraphManager.Instance.SetGoalNode(this);
            }
        }
    }

}