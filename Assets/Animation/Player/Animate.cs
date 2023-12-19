using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour
{
    Animator PlayerAnimator;
    void Awake()
    {
        PlayerAnimator = GetComponent<Animator>();
    }
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculer la magnitude du vecteur de direction pour détecter le mouvement dans n'importe quelle direction
        float moveMagnitude = new Vector2(horizontalInput, verticalInput).magnitude;

        // Utiliser la magnitude pour définir le paramètre de l'animation
        PlayerAnimator.SetFloat("walk", moveMagnitude);
        PlayerAnimator.SetBool("fire", Input.GetMouseButton(1));
        PlayerAnimator.SetBool("hurt", Input.GetMouseButton(2));
    }
}
