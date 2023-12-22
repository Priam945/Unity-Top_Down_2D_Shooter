using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenSetting : MonoBehaviour
{
    private bool isPaused = false;

    void Update() {
        // Mettre en pause ou reprendre le jeu lorsque la touche "P" est enfonc�e (vous pouvez changer cette touche selon vos besoins).
        if (Input.GetKeyDown(KeyCode.Escape)) {
            TogglePause();
        }
    }

    void TogglePause() {
        // Inverser l'�tat de pause.
        isPaused = !isPaused;

        // Mettre � l'�chelle le temps en fonction de l'�tat de pause.
        Time.timeScale = isPaused ? 0 : 1;
    }
}
