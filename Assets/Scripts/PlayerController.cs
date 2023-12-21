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
    private Boss boss;

    [Header("Player Controls Settings")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotationSpeed = 200f;
    private Rigidbody playerRigidbody;

    void Start()
    {
        //hp bar
        currentHealth = maxHealth;

        // stamina bar
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
        //transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        playerRigidbody.MovePosition(transform.position + moveDirection * moveSpeed * Time.deltaTime);
        RotateTowardsMouseCursor();
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && currentStamina > 0)
        {
            moveSpeed = 15f;
            UseStamina(10);
        } else {
            moveSpeed = 7f;
        }

        if (currentStamina < 100)
        {
            GainStamina(5);
        }

        //Death
        if (currentHealth <= 0)
        {
            Die();
        }

        staminaSlider.value = currentStamina;
    }

    void RotateTowardsMouseCursor() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {
            Vector3 mousePosition = hit.point;
            Vector3 lookDirection = mousePosition - transform.position;
            lookDirection.y = 0f;
            Quaternion rotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Bullet")) {
            float damageAmount = boss.IsAttackSPE() ? boss.GetLongRangeDamageSPE() : boss.GetLongRangeDamage();
            TakeDamage(damageAmount);
            boss.SetIsAttackSPE(false);
        }

        if (collision.gameObject.CompareTag("Enemy")) {
            TakeDamage(10);
        }

        if (currentHealth <= 0) {
            Die();
        }
    }

    public float GetCurrentHealth() => currentHealth;

    void Die() {
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
        healthSlider.value = currentHealth;

        if (currentHealth <= 0) {
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
