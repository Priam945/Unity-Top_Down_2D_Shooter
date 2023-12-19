using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;

    void Update()
    {
        // Check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            // Spawn a bullet at the current mouse position
            SpawnBullet();
        }
    }

    void SpawnBullet()
    {       
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;        
        Instantiate(bulletPrefab, mousePosition, Quaternion.identity);
    }
}
