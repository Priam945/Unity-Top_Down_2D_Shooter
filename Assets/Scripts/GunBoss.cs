using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBoss : MonoBehaviour
{
    [Header("Gun Settings")]
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject particlePrefab;
    [SerializeField] private float bulletSpeed = 20;
    [SerializeField] private Transform playerTransform;

    [Header("Gun Controls Settings")]
    private bool canShoot = true;
    private float shootCooldown = 0.5f;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Shoot() {
        if (canShoot) {
            StartCoroutine(PerformShoot());
        }
    }

    private IEnumerator PerformShoot() {
        canShoot = false;

        Vector3 directionToPlayer = (playerTransform.position - bulletSpawnPoint.position).normalized;
        bulletSpawnPoint.rotation = Quaternion.LookRotation(directionToPlayer);
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = directionToPlayer * bulletSpeed;
        var particle = Instantiate(particlePrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Destroy(particle, 2f);

        yield return new WaitForSeconds(shootCooldown);

        canShoot = true;
    }
}
