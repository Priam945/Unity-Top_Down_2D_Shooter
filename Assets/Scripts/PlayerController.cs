using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxStamina = 100;
    [SerializeField] private float currentStamina;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider staminaSlider;
    public float regenRate = 0f;
    private Boss boss;

    [Header("Player Controls Settings")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotationSpeed = 200f;
    private Rigidbody playerRigidbody;

    void Start()
    {
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        Cursor.visible = false;
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        RotateTowardsMouseCursor();
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && currentStamina > 0)
        {
            moveSpeed = 15f;
            UseStamina(10);
        }
        else
        {
            moveSpeed = 7f;
        }

        if (currentStamina < 100)
        {
            GainStamina(5);
        }

        if (currentHealth <= 0)
        {
            Die();
        }

        staminaSlider.value = currentStamina;

        if (Input.GetKeyDown(KeyCode.V))
        {
            TakeDamage(50);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Heal(50);
        }
        healthSlider.value = currentHealth;
        //Debug.Log(currentHealth);


    }

    void RotateTowardsMouseCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 mousePosition = hit.point;
            Vector3 lookDirection = mousePosition - transform.position;
            lookDirection.y = 0f;
            Quaternion rotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (boss.IsAttackSPE())
            {
                TakeDamage(boss.GetLongRangeDamageSPE());
                boss.SetIsAttackSPE(false);
            }
            else
            {
                TakeDamage(boss.GetLongRangeDamage());
            }
            healthSlider.value = currentHealth;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(10);
            healthSlider.value = currentHealth;
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Companion"))
        {
            currentHealth += regenRate * (Time.deltaTime / 50);
            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
            Debug.Log("Heal");
        }
    }

    public float GetCurrentHealth() => currentHealth;

    void Die()
    {
        Destroy(gameObject);
    }

    void RotateWithMouse(float mouseX)
    {
        float rotationAmount = mouseX * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, rotationAmount);
    }

    void OnApplicationFocus(bool hasFocus)
    {
        Cursor.visible = !hasFocus;
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void UseStamina(float stamina)
    {
        currentStamina -= stamina * Time.deltaTime;
        staminaSlider.value = currentStamina;

    }

    public void GainStamina(float stamina)
    {
        currentStamina += stamina * Time.deltaTime;
        staminaSlider.value = currentStamina;

    }

    public void Heal(float heal)
    {
        if (currentHealth < 100)
        {
            currentHealth += heal;
            healthSlider.value = currentHealth;
            Debug.Log("heal");
        }
        else if (currentHealth > 100)
        {
            currentHealth = 100;
        }

    }



}
