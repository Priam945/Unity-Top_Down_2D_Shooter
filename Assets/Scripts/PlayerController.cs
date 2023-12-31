using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxStamina;
    [SerializeField] private float currentStamina;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider staminaSlider;
    public float regenRate;
    private Boss boss;

    [Header("Player Controls Settings")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotationSpeed;
    private Rigidbody playerRigidbody;
    EndMenu endMenu;
    CanvasGroup endMenuGroup;
    public GameObject potoObject;
    private GameObject HealLight;


    void Start()
    {
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>();
        playerRigidbody = GetComponent<Rigidbody>();

        endMenu = GetComponent<EndMenu>();
        endMenuGroup = GameObject.Find("EndCanvas").GetComponent<CanvasGroup>();
       HealLight = GameObject.FindGameObjectWithTag("HealLight");
        

    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        playerRigidbody.MovePosition(transform.position + moveDirection * moveSpeed * Time.deltaTime);
        RotateTowardsMouseCursor();
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && currentStamina > 0)
        {
            moveSpeed = 10f;
            UseStamina(7);
        }

        if (currentStamina < 100)
        {
            GainStamina(4);
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
            AudioManager.instance.PlayMusic("Death");
        }

        staminaSlider.value = currentStamina;

        if (Input.GetKeyDown(KeyCode.V))
        {
            TakeDamage(10);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Heal(50);
        }
        healthSlider.value = currentHealth;
        //Debug.Log(currentHealth);

        Debug.Log(moveSpeed);

      
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
            float damageAmount = boss.IsAttackSPE() ? boss.GetLongRangeDamageSPE() : boss.GetLongRangeDamage();
            TakeDamage(damageAmount);
            boss.SetIsAttackSPE(false);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(10);
        }
        if (collision.gameObject.CompareTag("RUSH"))
        {
            TakeDamage(50);
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void OnTriggerStay(Collider other)
        {
        if (other.gameObject.CompareTag("Companion")) {
           
            Debug.Log("Heal");
            Debug.Log("currentHealth");
            currentHealth += regenRate * (Time.deltaTime / 50);
            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
            HealLight.GetComponent<Light>().intensity = 10;
           
            


        } else
        {
            HealLight.GetComponent<Light>().intensity = 0;
        }

    }
    

    public float GetCurrentHealth() => currentHealth;

    void Die()
    {
        Destroy(gameObject);
        AudioManager.instance.PlayMusic("death");
        endMenuGroup.interactable = true;
        endMenuGroup.alpha = 1f;
        endMenu.EndMenuButton();
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
            AudioManager.instance.PlayMusic("death");
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
