using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Shooter : MonoBehaviour
{

    [Header("Shooter Settings")]
    [SerializeField] private float maxHealth = 500f;
    [SerializeField] private float currentHealth;
    [SerializeField] private float longRangeDamage = 5f;
    [SerializeField] private GameObject zone2;
    [SerializeField] private GameObject gunShooter;
    private GameObject player;
    private GunShooter gunScript;

    [Header("Shooter Controls Settings")]
    [SerializeField] private float moveSpeed = 4f;
    private bool canDealDamage = true;
    private float damageCooldown = 1f;

    [SerializeField] private float autoShootInterval = 5f; 
    private float autoShootTimer = 0f;


    public float GetHP() => currentHealth;
    public float GetMaxHP() => maxHealth;
    public float GetLongRangeDamage() => longRangeDamage;
    public bool IsInLongRange() => zone2.GetComponent<LongRange>().IsInLongRange();
    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
        gunScript = gunShooter.GetComponent<GunShooter>();
    }

    // Update is called once per frame
    void Update()
    {
        autoShootTimer += Time.deltaTime;
        if (autoShootTimer >= autoShootInterval)
        {
            ShootAutomatically();
            autoShootTimer = 0f;
        }
    }

    private void ShootAutomatically()
    {
        if (IsInLongRange()) 
        {
            Shoot(30);
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

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

    public void DoDamage(float damage)
    {
        if (canDealDamage)
        {
            Debug.Log("Deal damage : " + damage);
            player.GetComponent<PlayerController>().TakeDamage(damage);
            StartCoroutine(DamageCooldown());
        }
    }

    public void Chase()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 chaseDirection = (playerPosition - transform.position).normalized;
        transform.position += chaseDirection * moveSpeed * Time.deltaTime;
    }

    private IEnumerator DamageCooldown()
    {
        canDealDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canDealDamage = true;
    }

    public void Shoot(float v)
    {
        gunScript.Shoot();
    }
}