using UnityEngine;

public class BlinkingLight : MonoBehaviour
{
    private Light pointLight;

    // Start is called before the first frame update
    void Start()
    {
        pointLight = GetComponent<Light>();
        InvokeRepeating("ToggleLight", 0.1f, Random.Range(0.5f, 1.5f));
    }

    void ToggleLight()
    {
        pointLight.intensity = (pointLight.intensity == 0.0f) ? Random.Range(0.5f, 1.0f) : 0.0f;
    }
}
