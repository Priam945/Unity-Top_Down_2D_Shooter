using System.Collections;
using UnityEngine;

public class HideRoof : MonoBehaviour
{
    private Renderer cubeRenderer;
    private bool isFading = false;

    // Duration of the fade in seconds
    public float fadeDuration = 2f;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Renderer component of the cube
        cubeRenderer = GetComponent<Renderer>();
    }

    // OnTriggerEnter is called when another collider enters the trigger collider
    void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider has the "player" tag
        if (other.CompareTag("Player"))
        {
            // Start the fading coroutine
            StartCoroutine(FadeOut());
        }
    }

    // OnTriggerExit is called when another collider exits the trigger collider
    void OnTriggerExit(Collider other)
    {
        // Check if the exiting collider has the "player" tag
        if (other.CompareTag("Player"))
        {
            // Stop the fading coroutine (if it's running)
            StopCoroutine(FadeOut());

            // Reset the alpha value to fully opaque
            Color currentColor = cubeRenderer.material.color;
            currentColor.a = 1f;
            cubeRenderer.material.color = currentColor;

            // Ensure the cube is visible
            cubeRenderer.enabled = true;

            // Reset the flag
            isFading = false;
        }
    }

    // Coroutine to fade out the cube over time
    IEnumerator FadeOut()
    {
        // Prevent multiple coroutines from running simultaneously
        if (isFading)
            yield break;

        isFading = true;

        // Get the initial color of the cube
        Color startColor = cubeRenderer.material.color;

        // Gradually decrease the alpha value over time
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            Color newColor = startColor;
            newColor.a = alpha;
            cubeRenderer.material.color = newColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Set the alpha value to fully transparent
        Color finalColor = startColor;
        finalColor.a = 0f;
        cubeRenderer.material.color = finalColor;

        // Disable rendering of the cube
        cubeRenderer.enabled = false;

        // Reset the flag
        isFading = false;
    }
}
