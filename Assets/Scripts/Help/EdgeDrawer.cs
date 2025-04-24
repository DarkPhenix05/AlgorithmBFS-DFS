using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EdgeDrawer : MonoBehaviour
{
    public static void DrawEdge(Node from, Node to)
    {
        GameObject lineObj = new GameObject("Edge");
        LineRenderer lr = lineObj.AddComponent<LineRenderer>();

        lr.positionCount = 2;
        lr.SetPosition(0, from.transform.position);
        lr.SetPosition(1, to.transform.position);
        lr.startWidth = lr.endWidth = 0.05f;
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = lr.endColor = Color.black;

        float dtX = (to.transform.position.x - from.transform.position.x);
        float dtY = (to.transform.position.y - from.transform.position.y);
        float dtZ = (to.transform.position.z - from.transform.position.z);

        float dt = Mathf.Sqrt( (dtX * dtX)+(dtY * dtY)+(dtZ * dtZ) );
        Vector3 vectDt = new Vector3 (dtX, dtY, dtZ);

        Edge eg =  lineObj.AddComponent<Edge>();
        eg.SetValue(dt);
        to.AddEdge(eg);

        lineObj.transform.parent = to.transform;

        GameObject canvas = new GameObject("LineCanvas");
        canvas.AddComponent<Canvas>();
        canvas.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
        canvas.transform.SetParent(lineObj.transform);
        canvas.transform.position = (from.transform.position + vectDt/2);

        GameObject textMeshPro = new GameObject("Text (TMP)");
        textMeshPro.AddComponent<TextMeshPro>();
        textMeshPro.GetComponent<TextMeshPro>().text = dt.ToString();
        textMeshPro.GetComponent<TextMeshPro>().fontSize = 1.0f;
        textMeshPro.GetComponent<TextMeshPro>().outlineWidth = 2.5f;
        textMeshPro.transform.SetParent(canvas.transform);
        //textMeshPro.transform.position = (from.transform.position + vectDt/2);

    }
}

