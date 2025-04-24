using UnityEngine;

public class Edge : MonoBehaviour
{
    public float value;
    public bool vicited;
    
    public void SetValue(float val)
    {
        value = val;
    }

    public float GetValue()
    {
        return value;
    }

    public void SetVicited(bool vtd)
    {
        vicited = vtd;
    }

    public bool ReturnVicited()
    {
        return vicited;
    }
}
