using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TheKiwiCoder;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;
using UnityEngine.UI;


public class Boss : MonoBehaviour
{
    [Header("Boss Settings")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private float longRangeDamage;
    [SerializeField] private float longRangeDamageSPE;
    [SerializeField] private float shortRangeDamage;
    [SerializeField] private float shortRangeDamageSPE;
    [SerializeField] private GameObject zone1;
    [SerializeField] private GameObject zone2;
    [SerializeField] private GameObject gunBoss;
    private GameObject player;
    private GunBoss gunScript;
    private PlayerController playerScript;
    private bool isAttackSPE = false;

    [Header("Boss Controls Settings")]
    [SerializeField] private float moveSpeed;
    private bool canDealDamage = true;
    private float damageCooldown = 1f;

    [Header("Boss Dash Settings")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown;
    private bool isDashing = false;
    private bool isDashingOut = false;

    [Header("Boss Shield Settings")]
    public GameObject shieldObject;
    [SerializeField] private float shieldDuration;
    [SerializeField] private float shieldCooldown;
    private bool isShielding = false;
    private bool isNotShielding = true;

    [Header("Boss Evade atk Settings")]
    [SerializeField] private float evadeAtkDuration;
    [SerializeField] private float dashOutSpeed;
    [SerializeField] private float dashOutDuration;

    [Header("Boss Heal Settings")]
    [SerializeField] private float healOverTime;
    [SerializeField] private float healCooldown;
    private bool canHeal = true;

    [Header("Boss Zone Damage Settings")]
    [SerializeField] private float damageInZone;
    [SerializeField] private float zoneDamageCooldown;
    private bool canDealZoneDamage = true;

    EndMenu endMenu;
    CanvasGroup endMenuGroup;
    [SerializeField] private Slider healthSlider;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
        gunScript = gunBoss.GetComponent<GunBoss>();
        endMenu = GetComponent<EndMenu>();
        endMenuGroup = GameObject.Find("EndCanvas").GetComponent<CanvasGroup>();
        playerScript = player.GetComponent<PlayerController>();

        if (shieldObject != null) {
            shieldObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = currentHealth;

        Chaselook();
    }

    void Die()
    {
        Destroy(gameObject);
        endMenuGroup.alpha = 1f;
        endMenuGroup.interactable = true;
        endMenu.EndMenuButton();
    }

    public float GetHP() => currentHealth;
    public void AddHP(float amount) { if (currentHealth > 0) { currentHealth += amount; } }
    public float GetMaxHP() => maxHealth;
    public float GetLongRangeDamage() => longRangeDamage;
    public float GetLongRangeDamageSPE() => longRangeDamageSPE;
    public float GetShortRangeDamage() => shortRangeDamage;
    public float GetShortRangeDamageSPE() => shortRangeDamageSPE;
    public bool IsAttackSPE() => isAttackSPE;
    public void SetIsAttackSPE(bool value) => isAttackSPE = value;
    public bool IsInLongRange() => zone2.GetComponent<LongRange>().IsInLongRange();
    public bool IsInShortRange() => zone1.GetComponent<ShortRange>().IsInShortRange();
    public PlayerController GetPlayerScript() => playerScript;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (isNotShielding) {
                currentHealth -= 10;
            }
        }
        if (currentHealth <= 0)
        {
            GetComponent<BehaviourTreeRunner>().tree.Update();
            Die();
        }
    }

    public void DoDamage(float damage) {
        if (canDealDamage) {
            StartCoroutine(DamageCooldown(damage));
        }
    }

    public void Dash() {
        if (!isDashing) {
            StartCoroutine(PerformDash());
        }
    }

    public void DashOut() {
        if (!isDashingOut) {
            StartCoroutine(PerformDashOut());
        }
    }

    public void Shield() {
        if (!isShielding) {
            StartCoroutine(PerformShield());
        }
    }

    public void Heal() {
        if (canHeal) {
            StartCoroutine(HealOverTime());
        }
    }

    public void ZoneDamage() {
        if (canDealZoneDamage) {
            StartCoroutine(PerformZoneDamage());
        }
    }

    public void Chase() {
        Vector3 playerPosition = player.transform.position;
        Vector3 chaseDirection = (playerPosition - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, playerPosition, moveSpeed * Time.deltaTime);
    }

    public void ChaseWithDistance(float distancePercentage) {
        Vector3 playerPosition = player.transform.position;
        Vector3 chaseDirection = (playerPosition - transform.position).normalized;
        float radius = zone2.GetComponent<SphereCollider>().radius * transform.lossyScale.x;
        float effectiveDistance = radius * distancePercentage;
        Vector3 desiredPosition = playerPosition - chaseDirection * effectiveDistance;
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, moveSpeed * Time.deltaTime);
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
    private IEnumerator PerformDashOut() {
        isDashingOut = true;

        Vector3 startPosition = transform.position;
        Vector3 dashDestination = player.transform.position;
        Vector3 dashDirection = (dashDestination - startPosition).normalized;
        Vector3 dashDirectionRight = Quaternion.Euler(0, 90, 0) * dashDirection;
        Vector3 dashDirectionLeft = Quaternion.Euler(0, -90, 0) * dashDirection;
        Vector3 chosenDashDirection = UnityEngine.Random.value < 0.5f ? dashDirectionRight : dashDirectionLeft;
        float elapsed = 0f;

        while (elapsed < dashOutDuration) {
            transform.position -= chosenDashDirection * dashOutSpeed * Time.deltaTime;
            elapsed += Time.deltaTime;

            yield return null;
        }

        yield return new WaitForSeconds(dashCooldown);

        isDashingOut = false;
    }

    private IEnumerator PerformShield() {
        isShielding = true;
        isNotShielding = false;

        if (shieldObject != null) {
            shieldObject.SetActive(true);
        }

        float elapsed = 0f;
        while (elapsed < shieldDuration) {
            elapsed += Time.deltaTime;
            yield return null;
        }

        if (shieldObject != null) {
            shieldObject.SetActive(false);
        }

        isNotShielding = true;

        yield return new WaitForSeconds(shieldCooldown);

        isShielding = false;
    }

    private IEnumerator HealOverTime() {
        canHeal = false;

        AddHP(healOverTime);
        yield return new WaitForSeconds(healCooldown);

        canHeal = true;
    }

    private IEnumerator PerformZoneDamage() {
        canDealZoneDamage = false;
        playerScript.TakeDamage(damageInZone);
        yield return new WaitForSeconds(zoneDamageCooldown);
        canDealZoneDamage = true;
    }

    private IEnumerator DamageCooldown(float damage) {
        canDealDamage = false;
        playerScript.TakeDamage(damage);
        yield return new WaitForSeconds(damageCooldown);
        canDealDamage = true;
    }

    public void Shoot() {
        gunScript.Shoot();
    }
    public void Chaselook()
    {
        Vector3 playerPosition = player.transform.position;
        transform.LookAt(playerPosition);
    }
}
