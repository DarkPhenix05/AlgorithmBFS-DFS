#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Node))]
public class NodeEditor : Editor
{
    public float _DistanceToNeighbours = 2.5f;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Node node = (Node)target;

        if (GUILayout.Button("Auto Assign Neighbors (within " + _DistanceToNeighbours + " units)"))
        {
            node.neighbors.Clear();
            Node[] allNodes = FindObjectsOfType<Node>();
            foreach (Node n in allNodes)
            {
                if (n != node && Vector3.Distance(node.transform.position, n.transform.position) < _DistanceToNeighbours)
                {
                    node.neighbors.Add(n);
                }
            }
            EditorUtility.SetDirty(node);
        }
    }

    public void SetDistance(int distance)
    {
        _DistanceToNeighbours = distance;
    }
}
#endif