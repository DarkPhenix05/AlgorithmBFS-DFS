using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SearchService;
using UnityEngine;

public class EdgeDrawer : MonoBehaviour
{
    public static void DrawEdge(Node from, Node to, Color clr)
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
        Vector3 vectDt = to.transform.position - from.transform.position;

        Edge eg =  lineObj.AddComponent<Edge>();
        eg.SetValue(dt);
        to.AddEdge(eg);

        lineObj.transform.parent = to.transform;

        GameObject textMeshPro = new GameObject("Weight (TMP)");
        TextMeshPro txt = textMeshPro.AddComponent<TextMeshPro>();
        RectTransform rct = textMeshPro.GetComponent<RectTransform>();
        
        textMeshPro.transform.SetParent(lineObj.transform);

        txt.text = dt.ToString();
        txt.fontSize = 4.0f;
        txt.alignment = TextAlignmentOptions.Center;
        txt.textWrappingMode = TextWrappingModes.NoWrap;
        txt.outlineWidth = 2.5f;
        textMeshPro.transform.position = (txt.bounds.center - txt.transform.position) + lr.bounds.center + new Vector3 (0.0f,0.0f,-0.1f);
        txt.color = clr;
    }
}

