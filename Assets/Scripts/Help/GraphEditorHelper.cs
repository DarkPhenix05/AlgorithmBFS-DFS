#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Node))]
public class NodeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Node node = (Node)target;

        if (GUILayout.Button("Auto Assign Neighbors (within 2.5 units)"))
        {
            node.neighbors.Clear();
            node.weightedNeighbors.Clear();
            Node[] allNodes = FindObjectsOfType<Node>();
            foreach (Node n in allNodes)
            {
                if (n != node && Vector3.Distance(node.transform.position, n.transform.position) < 2.5f)
                {
                    node.neighbors.Add(n);
                    node.weightedNeighbors.Add(new NeighborInfo(n, Vector3.Distance(node.transform.position, n.transform.position)));
                }
            }
            EditorUtility.SetDirty(node);
        }
    }
}
#endif