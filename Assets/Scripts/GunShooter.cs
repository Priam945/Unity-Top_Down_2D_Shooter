using UnityEngine;

public class GunShooter : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public GameObject particlePrefab;
    public float bulletSpeed = 20;

    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        RotateTowardsPlayer();
    }

    private void RotateTowardsPlayer()
    {
        if (playerTransform) {
            Vector3 directionToPlayer = (playerTransform.position - bulletSpawnPoint.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);

            Debug.DrawRay(bulletSpawnPoint.position, directionToPlayer * 10f, Color.red);
        }
    }



    public void Shoot()
    {
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
