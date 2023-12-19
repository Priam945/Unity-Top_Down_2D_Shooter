using System.Collections;
using UnityEngine;

public class ManagerAnim : MonoBehaviour
{
    // Component
    Animator ManagerAnimator;
    public Transform joueur;  // Référence au transform du joueur
    public GameObject monstrePrefab;  // Préfabriqué du monstre à instancier
    public ParticleSystem invocationParticles;  // Système de particules pour l'invocation
    // Manager
    public bool isInvincible = true;  // Le manager est invincible par défaut
    public float vulnerabilityTime = 1.0f; // Le temps d'invincibilité
    public float rangeManager = 5.0f;  // Portée du manager pour détecter le joueur
    // Zombies
    public float rangeSpawnZombies = 2.0f;  // Portée pour générer des zombies
    public int nbZombies = 1;  // Nombre de zombies à générer
    public int rateSpawn = 7;  // Taux d'apparition des zombies en secondes
    private float lastSpawn = 0f;  // Temps du dernier spawn de zombies

    void Awake()
    {
        ManagerAnimator = GetComponent<Animator>();  // Récupération du composant Animator
    }

    void Start()
    {
        if (joueur == null)
        {
            Debug.LogError("La référence au joueur est nulle. Assurez-vous de l'assigner dans l'inspecteur.");
        }

        if (monstrePrefab == null)
        {
            Debug.LogError("La référence au monstrePrefab est nulle. Assurez-vous de l'assigner dans l'inspecteur.");
        }
        // Désactive le système de particules au démarrage
        if (invocationParticles != null)
        {
            invocationParticles.Stop();
        }
    }

     void Update()
    {
        // Vérification de la distance entre le manager et le joueur
        if (Vector3.Distance(transform.position, joueur.position) < rangeManager)
        {
            ManagerAnimator.SetBool("invocation", true); // Activation de l'animation d'invocation
            isInvincible = false;
            StartInvocation(); // Appel de la méthode pour commencer l'invocation   
        }
        else
        {
            ManagerAnimator.SetBool("invocation", false);  // Désactivation de l'animation d'invocation
        }
        if (!isInvincible) // Si le manager est invincible, commence la vulnérabilité
        {
            StartCoroutine(StartVulnerability());
        }
    }

    // Méthode pour commencer l'invocation de zombies
    void StartInvocation()
    {
        // Vérifiez si le temps écoulé depuis le dernier lancement est supérieur à la cadence d'apparition
        if (Time.time - lastSpawn > rateSpawn)
        {
            if (invocationParticles != null)
            {
                invocationParticles.Play();  // Déclenche la lecture du système de particules
            }
            if (monstrePrefab != null)
            {
                SpawnZombies();  // Appel de la méthode pour générer des zombies
                lastSpawn = Time.time;  // Mise à jour du temps du dernier spawn
            }
        }
    }

    // Méthode pour générer des zombies
    void SpawnZombies()
    {
        for (int i = 0; i < nbZombies; i++)
        {
            // Position devant le manager
            Instantiate(monstrePrefab, transform.position + transform.forward * rangeSpawnZombies, Quaternion.identity);
            // Position à droite du manager
            Instantiate(monstrePrefab, transform.position + transform.right * rangeSpawnZombies, Quaternion.identity);
            // Position à gauche du manager
            Instantiate(monstrePrefab, transform.position - transform.right * rangeSpawnZombies, Quaternion.identity);
        }
    }
    IEnumerator StartVulnerability()
    {
        // Pause
        yield return new WaitForSeconds(vulnerabilityTime);
        // La vulnérabilité est terminée, le manager redevient invincible
        isInvincible = true;
    }
}