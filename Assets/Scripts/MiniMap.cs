using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public Transform player;

    private void LateUpdate()
    {
        // Lock the rotation around the Z-axis
        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);

        // Update the position based on the player's position
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
    }
}
