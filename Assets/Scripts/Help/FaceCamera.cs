using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    void Awake()
    {
        FaceTowardsCam();
    }

    void FaceTowardsCam()
    {
        if (Camera.main != null)
        {
            transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
        }
    }
}
