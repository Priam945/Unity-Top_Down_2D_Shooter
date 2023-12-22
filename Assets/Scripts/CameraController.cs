using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        if (target) {
            transform.position = target.position;
        }
    }
}