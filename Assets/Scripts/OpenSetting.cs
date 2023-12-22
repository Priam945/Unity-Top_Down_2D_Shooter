using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenSetting : MonoBehaviour
{
    private bool isPaused = false;

    void Update() {
        // Mettre en pause ou reprendre le jeu lorsque la touche "P" est enfoncée (vous pouvez changer cette touche selon vos besoins).
        if (Input.GetKeyDown(KeyCode.Escape)) {
            TogglePause();
        }
    }

    void TogglePause() {
        // Inverser l'état de pause.
        isPaused = !isPaused;

        // Mettre à l'échelle le temps en fonction de l'état de pause.
        Time.timeScale = isPaused ? 0 : 1;
    }
}
