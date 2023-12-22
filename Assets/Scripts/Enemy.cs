using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;
    [SerializeField] private Slider healthSlider;
    public float GetHP() => currentHealth;
    public float GetMaxHP() => maxHealth;
    private GameObject player;
    [SerializeField] private float moveSpeed = 4f;

    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");

    }

    void Update()
    {
        Chaselook();
        if (currentHealth <= 0) {
            Die();
        }
        if (gameObject) {
            healthSlider.value = currentHealth;
        }
    }
    public void Chaselook()
    {
        if (gameObject) {
            Vector3 playerPosition = player.transform.position;
            transform.LookAt(playerPosition);
        }
    }


    void Die()
    {
        StartCoroutine(AttendrePuisFaireQuelqueChose());
        Destroy(gameObject);
    }

    IEnumerator AttendrePuisFaireQuelqueChose()
    {
        yield return new WaitForSeconds(5f);      
    }

    void OnCollisionEnter(Collision collision)
    {     
        if (collision.gameObject.CompareTag("Bullet"))
        {
            print(currentHealth);
            TakeDamage(10);
            healthSlider.value = currentHealth;
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Chase()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 chaseDirection = (playerPosition - transform.position).normalized;
        transform.position += chaseDirection * moveSpeed * Time.deltaTime;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
    }
}
