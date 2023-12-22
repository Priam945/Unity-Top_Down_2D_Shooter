using UnityEngine;

public class GunShooter : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public GameObject particlePrefab;
    public float bulletSpeed = 20;
    public LineRenderer aimLineRenderer;
    private float maxAimRange = 20f;
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        RotateTowardsPlayer();
        UpdateAimLineRenderer();
    }

    private void RotateTowardsPlayer()
    {
        if (playerTransform) {
            Vector3 directionToPlayer = (playerTransform.position - bulletSpawnPoint.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }

    private void UpdateAimLineRenderer()
    {
        float distanceToPlayer = Vector3.Distance(playerTransform.position, bulletSpawnPoint.position);

        // Vérifiez si le joueur est dans la portée
        if (distanceToPlayer > maxAimRange)
        {
            // Si le joueur n'est pas dans la portée, désactive le Line Renderer
            aimLineRenderer.enabled = false;
            return;
        }

        Vector3 directionToPlayer = (playerTransform.position - bulletSpawnPoint.position).normalized;

        // Utilisez un raycast pour détecter les obstacles
        RaycastHit hit;
        if (Physics.Raycast(bulletSpawnPoint.position, directionToPlayer, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                aimLineRenderer.enabled = false;
                return;
            }
        }

        aimLineRenderer.enabled = true;
        aimLineRenderer.SetPosition(0, bulletSpawnPoint.position);
        aimLineRenderer.SetPosition(1, bulletSpawnPoint.position + directionToPlayer * 50f);
    }

    public void Shoot()
    {
        // Vérifiez si le Line Renderer est actif

        if (!aimLineRenderer.enabled)
        {
            // Si le Line Renderer n'est pas actif, annule le tir
            return;
        }

        if (playerTransform) {
            Vector3 directionToPlayer = (playerTransform.position - bulletSpawnPoint.position).normalized;
            bulletSpawnPoint.rotation = Quaternion.LookRotation(directionToPlayer);
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = directionToPlayer * bulletSpeed;
            var particle = Instantiate(particlePrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            Destroy(particle, 2f);
        }
    }
}
