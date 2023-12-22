using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ManagerAnim : MonoBehaviour
{
    // Component
    Animator ManagerAnimator;
    public Transform joueur;  // R�f�rence au transform du joueur
    public GameObject monstrePrefab;  // Pr�fabriqu� du monstre � instancier
    public ParticleSystem invocationParticles;  // Syst�me de particules pour l'invocation
    // Manager
    public bool isInvincible = true;  // Le manager est invincible par d�faut
    public float vulnerabilityTime = 1.0f; // Le temps d'invincibilit�
    public float rangeManager = 5.0f;  // Port�e du manager pour d�tecter le joueur
    // Zombies
    public float rangeSpawnZombies = 2.0f;  // Port�e pour g�n�rer des zombies
    public int nbZombies = 1;  // Nombre de zombies � g�n�rer
    public int rateSpawn = 7;  // Taux d'apparition des zombies en secondes
    private float lastSpawn = 0f;  // Temps du dernier spawn de zombies
    public int maxHealth = 100;
    private int currentHealth;
    [SerializeField] private Slider healthSlider;

    void Awake()
    {
        ManagerAnimator = GetComponent<Animator>();  // R�cup�ration du composant Animator
    }

    void Start()
    {
        currentHealth = maxHealth;
        if (joueur == null)
        {
            Debug.LogError("La r�f�rence au joueur est nulle. Assurez-vous de l'assigner dans l'inspecteur.");
        }

        if (monstrePrefab == null)
        {
            Debug.LogError("La r�f�rence au monstrePrefab est nulle. Assurez-vous de l'assigner dans l'inspecteur.");
        }
        // D�sactive le syst�me de particules au d�marrage
        if (invocationParticles != null)
        {
            invocationParticles.Stop();
        }
    }

     void Update()
    {
        healthSlider.value = currentHealth;

        // V�rification de la distance entre le manager et le joueur
        if (Vector3.Distance(transform.position, joueur.position) < rangeManager)
        {
            ManagerAnimator.SetBool("invocation", true); // Activation de l'animation d'invocation
            isInvincible = false;
            StartInvocation(); // Appel de la m�thode pour commencer l'invocation   
        }
        else
        {
            ManagerAnimator.SetBool("invocation", false);  // D�sactivation de l'animation d'invocation
        }
        if (!isInvincible) // Si le manager est invincible, commence la vuln�rabilit�
        {
            StartCoroutine(StartVulnerability());
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            print(currentHealth);
            TakeDamage(10);
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    // M�thode pour commencer l'invocation de zombies
    void StartInvocation()
    {
        // V�rifiez si le temps �coul� depuis le dernier lancement est sup�rieur � la cadence d'apparition
        if (Time.time - lastSpawn > rateSpawn)
        {
            if (invocationParticles != null)
            {
                invocationParticles.Play();  // D�clenche la lecture du syst�me de particules
            }
            if (monstrePrefab != null)
            {
                SpawnZombies();  // Appel de la m�thode pour g�n�rer des zombies
                lastSpawn = Time.time;  // Mise � jour du temps du dernier spawn
            }
        }
    }
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            GetComponent<CapsuleCollider>().enabled = false;
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
    // M�thode pour g�n�rer des zombies
    void SpawnZombies()
    {
        for (int i = 0; i < nbZombies; i++)
        {
            // Position devant le manager
            Instantiate(monstrePrefab, transform.position + transform.forward * rangeSpawnZombies, Quaternion.identity);
            // Position � droite du manager
            Instantiate(monstrePrefab, transform.position + transform.right * rangeSpawnZombies, Quaternion.identity);
            // Position � gauche du manager
            Instantiate(monstrePrefab, transform.position - transform.right * rangeSpawnZombies, Quaternion.identity);
        }
    }
    IEnumerator StartVulnerability()
    {
        // Pause
        yield return new WaitForSeconds(vulnerabilityTime);
        // La vuln�rabilit� est termin�e, le manager redevient invincible
        isInvincible = true;
    }
}