using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static UnityEngine.AudioSettings;

public class EdgeDrawer : MonoBehaviour
{
    private static Transform edgesParent;
    private static Transform labelsParent;

    public static void DrawEdge(Node from, Node to)
    {
        if (edgesParent == null)
        {
            GameObject edgesObj = new GameObject("Edges");
            edgesParent = edgesObj.transform;
        }
        if (labelsParent == null)
        {
            GameObject labelsObj = new GameObject("EdgeLabels");
            labelsParent = labelsObj.transform;
        }

        GameObject lineObj = new GameObject($"Edge_{from.name}_to_{to.name}");
        lineObj.transform.SetParent(edgesParent);
        LineRenderer lr = lineObj.AddComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.SetPosition(0, from.transform.position);
        lr.SetPosition(1, to.transform.position);
        lr.startWidth = lr.endWidth = 0.05f;
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = lr.endColor = Color.black;

        GameObject labelObj = new GameObject($"Label_{from.name}_to_{to.name}");
        labelObj.transform.SetParent(labelsParent);
        labelObj.transform.position = (from.transform.position + to.transform.position) / 2f + new Vector3(0.0f,0.0f,-0.1f);

        TextMeshPro tmp = labelObj.AddComponent<TextMeshPro>();
        tmp.fontSize = 4;
        tmp.enableAutoSizing = false;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.color = Color.red;
        tmp.text = Vector3.Distance(from.transform.position, to.transform.position).ToString("F1");
        tmp.outlineColor = Color.white;
        tmp.outlineWidth = 1f;

        labelObj.AddComponent<FaceCamera>();
    }
}