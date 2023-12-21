using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Assign the capsule's transform to this field

    void Update()
    {
        // Update the position of the empty GameObject to follow the capsule with the specified offset
        if (target) {
            transform.position = target.position;
        }
    }
}