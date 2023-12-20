using UnityEngine;

public class Companion : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public GameObject particlePrefab;
    public Transform playerTransform;
    public float followingRate = 2f; // Adjust this value to set the following rate
    public float minDistanceToPlayer = 2f;
    public bool canShoot = false;
    public float bulletSpeed = 20;


    private void Start()
    {
        InvokeRepeating("Shoot", 0f, 0.2f);
    }
    void Update()
    {
        if (playerTransform != null)
        {
            FollowPlayer();
        }
        else
        {
            Debug.LogWarning("Player Transform non assigné");
        }
        
    }

    void FollowPlayer()
    {        
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer > minDistanceToPlayer)
        {           
            Vector3 targetPosition = playerTransform.position;
            targetPosition.y = transform.position.y; 
            float step = followingRate * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);          
            RotateTowardsNearestEnemy();
        }
    }

    void RotateTowardsNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length > 0)
        {
            Transform nearestEnemy = GetNearestEnemy(enemies);
            if (nearestEnemy != null)
            {
                Vector3 lookDirection = nearestEnemy.position - transform.position;
                lookDirection.y = 0f;
                Quaternion rotation = Quaternion.LookRotation(lookDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, followingRate * Time.deltaTime);               
            }          
        }
    }

    Transform GetNearestEnemy(GameObject[] enemies)
    {
        Transform nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);            
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = enemy.transform;
                if (nearestDistance < 6)
                {
                    canShoot = true;
                }
                else
                {
                    canShoot = false;
                } 
                    
            }
            
        }

        return nearestEnemy;
    }
    public void Shoot()
    {
        if(canShoot == true)
        {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
            var particle = Instantiate(particlePrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            Destroy(particle, 2f);
        }
        
                 
    }
}
