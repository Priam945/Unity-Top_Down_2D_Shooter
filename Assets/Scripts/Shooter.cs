using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;


public class Shooter : MonoBehaviour
{

    [Header("Shooter Settings")]
    [SerializeField] private float maxHealth = 500f;
    [SerializeField] private float currentHealth;
    [SerializeField] private float longRangeDamage = 90f;
    [SerializeField] private GameObject zone2;
    [SerializeField] private GameObject gunShooter;
    private GameObject player;
    private GunShooter gunScript;
    private PlayerController playerScript;
    private bool canDealDamage = true;
    private float damageCooldown = 1f;

    [SerializeField] private float autoShootInterval = 3f; 
    private float autoShootTimer = 0f;

    [SerializeField] private Slider healthSlider;
    
    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
        gunScript = gunShooter.GetComponent<GunShooter>();
        playerScript = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = currentHealth;
        autoShootTimer += Time.deltaTime;
        if (autoShootTimer >= autoShootInterval)
        {
            ShootAutomatically();
            autoShootTimer = 0f;
        }
        if (currentHealth <= 0)
        {
            Die();
        }
        Chase();
    }
    public float GetHP() => currentHealth;
    public float GetMaxHP() => maxHealth;
    public float GetLongRangeDamage() => longRangeDamage;
    public bool IsInLongRange() => zone2.GetComponent<LongRange>().IsInLongRange();
    public PlayerController GetPlayerScript() => playerScript;

    private void ShootAutomatically()
    {
        if (IsInLongRange()) 
        {
            Shoot(100);
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
            GetComponent<BehaviourTreeRunner>().tree.Update();
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
        transform.LookAt(playerPosition);
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
