using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;

public class Boss : MonoBehaviour
{
    [Header("Boss Settings")]
    [SerializeField] private float maxHealth = 500f;
    [SerializeField] private float currentHealth;
    [SerializeField] private float longRangeDamage = 5f;
    [SerializeField] private float longRangeDamageSPE = 15f;
    [SerializeField] private float shortRangeDamage = 6f;
    [SerializeField] private float shortRangeDamageSPE = 10f;
    [SerializeField] private GameObject zone1;
    [SerializeField] private GameObject zone2;
    [SerializeField] private GameObject gunBoss;
    private GameObject player;
    private GunBoss gunScript;
    private bool isAttackSPE = false;

    [Header("Boss Controls Settings")]
    [SerializeField] private float moveSpeed = 4f;
    private bool canDealDamage = true;
    private float damageCooldown = 1f;

    [Header("Boss Dash Settings")]
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float dashDuration = 1f;
    [SerializeField] private float dashCooldown = 3f;
    private bool isDashing = false;

    [Header("Boss Shield Settings")]
    public GameObject shieldObject;
    [SerializeField] private float shieldDuration = 20f;
    [SerializeField] private float shieldCooldown = 5f;
    private bool isShielding = false;

    [Header("Boss Evade atk Settings")]
    [SerializeField] private float evadeAtkDuration = 0.5f;
    [SerializeField] private float evadeAtkCooldown = 10f;
    public float evadeDistance = 5f;
    private bool isEvadingAtk = false;

    private Vector3 evadeDirection = Vector3.right;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
        gunScript = gunBoss.GetComponent<GunBoss>();

        if (shieldObject != null)
        {
            shieldObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Die()
    {
        Destroy(gameObject);
    }

    public float GetHP() => currentHealth;
    public float GetMaxHP() => maxHealth;
    public float GetLongRangeDamage() => longRangeDamage;
    public float GetLongRangeDamageSPE() => longRangeDamageSPE;
    public float GetShortRangeDamage() => shortRangeDamage;
    public float GetShortRangeDamageSPE() => shortRangeDamageSPE;
    public bool IsAttackSPE() => isAttackSPE;
    public void SetIsAttackSPE(bool value) => isAttackSPE = value;
    public bool IsInLongRange() => zone2.GetComponent<LongRange>().IsInLongRange();
    public bool IsInShortRange() => zone1.GetComponent<ShortRange>().IsInShortRange();

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            currentHealth -= 10;
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void DoDamage(float damage) {
        if (canDealDamage) {
            player.GetComponent<PlayerController>().TakeDamage(damage);
            StartCoroutine(DamageCooldown());
        }
    }

    public void Dash() {
        if (!isDashing) {
            StartCoroutine(PerformDash());
        }
    }
    public void Shield()
    {
        if (!isShielding)
        {
            StartCoroutine(PerformShield());
        }
    }

    public void EvadeAtk()
    {
        if (!isEvadingAtk)
        {
            StartCoroutine(PerformEvadeAtk());
        }
    }

    public void Chase() {
        Vector3 playerPosition = player.transform.position;
        Vector3 chaseDirection = (playerPosition - transform.position).normalized;
        transform.position += chaseDirection * moveSpeed * Time.deltaTime;
    }

    private IEnumerator PerformDash() {
        isDashing = true;

        Vector3 startPosition = transform.position;
        Vector3 dashDestination = player.transform.position;
        Vector3 dashDirection = (dashDestination - startPosition).normalized;
        float elapsed = 0f;

        while (elapsed < dashDuration) {
            transform.position += dashDirection * dashSpeed * Time.deltaTime;
            elapsed += Time.deltaTime;

            yield return null;
        }

        yield return new WaitForSeconds(dashCooldown);

        isDashing = false;
    }

    private IEnumerator PerformShield()
    {
        if (!isShielding)
        {
            isShielding = true;

            if (shieldObject != null)
            {
                shieldObject.SetActive(true);
            }

            // Active le bouclier pendant la durée spécifiée
            float elapsed = 0f;
            while (elapsed < shieldDuration)
            {
                elapsed += Time.deltaTime;
                yield return null;
            }

            // Désactive le bouclier visuel
            if (shieldObject != null)
            {
                shieldObject.SetActive(false);
            }

            // Désactive le bouclier
            isShielding = false;

            // Attend le temps de recharge avant de pouvoir réactiver le bouclier
            yield return new WaitForSeconds(shieldCooldown);
        }
        else
        {
            // Le bouclier est déjà en cours d'utilisation
            Debug.Log("Le bouclier est déjà actif.");
        }
    }
    public void ActivateShield()
    {
        StartCoroutine(PerformShield());
    }
    private IEnumerator PerformEvadeAtk()
    {
        if (!isEvadingAtk)
        {
            isEvadingAtk = true;

            // Stocke la position de départ du boss
            Vector3 startPosition = transform.position;

            // Calcule la direction du joueur par rapport à la position actuelle du boss
            Vector3 playerDirection = player.transform.position - transform.position;
            playerDirection.y = 0f; // Ignore la composante verticale (peut être ajusté selon votre besoin)
            playerDirection.Normalize();

            // Calcule la direction d'évasion
            Vector3 evadeDirection = Quaternion.Euler(0, 90, 0) * playerDirection; // Tourne de 90 degrés pour obtenir une direction perpendiculaire

            // Calcule la position cible pour l'évasion
            Vector3 evadeDestination = startPosition + evadeDirection * evadeDistance;

            // Initialise le temps écoulé
            float elapsed = 0f;

            while (elapsed < evadeAtkDuration)
            {
                // Déplace le boss le long de la direction d'évasion
                transform.position += evadeDirection * moveSpeed * Time.deltaTime;

                // Incrémente le temps écoulé
                elapsed += Time.deltaTime;

                // Attend la prochaine frame
                yield return null;
            }

            // Attend le temps de recharge avant de pouvoir réutiliser l'évasion
            yield return new WaitForSeconds(evadeAtkCooldown);

            // Termine l'évasion
            isEvadingAtk = false;
        }
        else
        {
            // L'évasion est déjà en cours d'utilisation
            Debug.Log("L'évasion est déjà en cours.");
        }
    }




    private IEnumerator DamageCooldown() {
        canDealDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canDealDamage = true;
    }

    public void Shoot() {
        gunScript.Shoot();
    }
}
