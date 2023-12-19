using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float life = 3;
    public GameObject sparkPrefab;
    public GameObject bloodPrefab;
    public float damages = 10;

    private void Awake()
    {
        Destroy(gameObject, life);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Untagged"))
        {
            SpawnSpark(collision.contacts[0].point, collision.contacts[0].normal);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SpawnBlood(collision.contacts[0].point, collision.contacts[0].normal);
            Destroy(gameObject);
        }

    }

    void SpawnSpark(Vector3 position, Vector3 normal)
    {               
            GameObject spark = Instantiate(sparkPrefab, position, Quaternion.LookRotation(normal));
            Destroy(spark, 1.0f);                   
    }

    void SpawnBlood(Vector3 position, Vector3 normal)
    {
        GameObject blood = Instantiate(bloodPrefab, position, Quaternion.LookRotation(normal));
        Destroy(blood, 1.0f);
    }
}
