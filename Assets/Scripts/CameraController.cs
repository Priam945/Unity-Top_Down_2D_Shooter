using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Assign the capsule's transform to this field
    public Vector3 offset = new Vector3(0f, 2f, -5f); // Adjust the offset as needed

    void Update()
    {
        // Update the position of the empty GameObject to follow the capsule with the specified offset
        transform.position = target.position + offset;
    }
}