using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rusher : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private Slider healthSlider;
    private int currentHealth;

    [SerializeField] private float dashDuration = 0.5f; // Réduit pour un dash plus court
    [SerializeField] private float dashSpeed = 20f;     // Augmenté pour une vitesse de dash plus élevée
    [SerializeField] private float dashCooldown = 2f;  // Temps de recharge du dash
    private bool isDashing;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject rangeZone;
    void Start()
    {
        currentHealth = maxHealth;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            print(currentHealth);
            currentHealth -= 10;
        }

        if (currentHealth <= 0)
        {
            Die();
        }

        if (collision.gameObject.CompareTag("Wall"))
        {

            isDashing = false;
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            animator.SetTrigger("death");
            GetComponent<CapsuleCollider>().enabled = false;
        }
        else
        {
            animator.SetTrigger("damage");
        }
    }

    public bool GetRangeZone() => rangeZone.GetComponent<GetTriggerRangeZone>().IsInRange();

    public void Dash()
    {
        if (!isDashing)
        {
            StartCoroutine(PerformDash());
        }
    }

    private IEnumerator PerformDash()
    {
        isDashing = true;

        Vector3 startPosition = transform.position;
        Vector3 dashDestination = player.transform.position;
        Vector3 dashDirection = (dashDestination - startPosition).normalized;
        float elapsed = 0f;

        while (elapsed < dashDuration)
        {
            transform.position += dashDirection * dashSpeed * Time.deltaTime;
            elapsed += Time.deltaTime;

            yield return null;
        }

        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
    }

    public int GetHP() => currentHealth;
    // Update is called once per frame
    void Update()
    {
        Chaselook();
        healthSlider.value = currentHealth;
    }
    public void Chaselook()
    {
        Vector3 playerPosition = player.transform.position;
        transform.LookAt(playerPosition);
    }

}
