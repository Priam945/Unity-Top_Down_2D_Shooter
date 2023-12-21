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

    EndMenu endMenu;
    CanvasGroup endMenuGroup;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
        gunScript = gunBoss.GetComponent<GunBoss>();

        endMenu = GetComponent<EndMenu>();
        endMenuGroup = GameObject.Find("EndCanvas").GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Die()
    {
        Destroy(gameObject);
        endMenuGroup.alpha = 1f;
        endMenuGroup.interactable = true;
        endMenu.EndMenuButton();
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

    private IEnumerator DamageCooldown() {
        canDealDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canDealDamage = true;
    }

    public void Shoot() {
        gunScript.Shoot();
    }
}
