using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        // Déplacement horizontal
        float horizontalInput = Input.GetAxis("Horizontal");
        // Déplacement vertical
        float verticalInput = Input.GetAxis("Vertical");

        // Calcul du mouvement
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * speed * Time.deltaTime;

        // Application du mouvement
        transform.Translate(movement);
    }
}