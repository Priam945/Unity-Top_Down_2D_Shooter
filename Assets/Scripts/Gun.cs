using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour {
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public GameObject particlePrefab;
    public float bulletSpeed = 20;
    public float fireInterval = 1.0f;
    public int maxAmmo = 40;
    public float reloadTime = 2.0f;

    private int currentAmmo;
    private float timer;
    private bool isReloading;

    private void Start() {
        currentAmmo = maxAmmo;
        isReloading = false;
    }
    private void Update() {
        timer += Time.deltaTime;

        if (isReloading)
            return;

        if (Input.GetMouseButton(1) && timer >= fireInterval) {
            Shoot();
            timer = 0f;
        }

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo) {
            StartCoroutine(Reload());
        }
    }

    public void Shoot() {
        if (currentAmmo > 0) {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
            var particle = Instantiate(particlePrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            Destroy(particle, 2f);

            currentAmmo--;
        } else {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload() {
        isReloading = true;
        Debug.Log("Reloading...");

        // Optional: Play a reload animation or sound

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
        Debug.Log("Reload complete. Current ammo: " + currentAmmo);
    }
}
