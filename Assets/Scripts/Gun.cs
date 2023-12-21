using System.Collections;
using UnityEngine;
using TMPro;

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

    public TMP_Text ammoText;

    private void Start() {
        currentAmmo = maxAmmo;
        isReloading = false;

        if (ammoText == null) {
            Debug.LogError("TextMeshPro Text component is not assigned!");
        } else {
            UpdateAmmoUI();
        }
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

    private void UpdateAmmoUI() {
        ammoText.text = "Ammo: " + currentAmmo + " / " + maxAmmo;
    }

    public void Shoot() {
        if (currentAmmo > 0) {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
            var particle = Instantiate(particlePrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            Destroy(particle, 2f);

            currentAmmo--;
            UpdateAmmoUI();
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
        UpdateAmmoUI();
        Debug.Log("Reload complete. Current ammo: " + currentAmmo);
    }
}
