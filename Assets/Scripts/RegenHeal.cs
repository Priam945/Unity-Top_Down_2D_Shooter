using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenHeal : MonoBehaviour
{
    public int heal;
    public float rot;
    public float speed;

    public float rotxspeed = 0.0f;
    public float rotyspeed = 0.0f;
    public float rotzspeed = 0.0f;

    void Update()
    {
        transform.Rotate(Vector3.right * Time.deltaTime * speed);
        transform.Rotate(
            rotxspeed * Time.deltaTime,
            rotyspeed * Time.deltaTime,
            rotzspeed * Time.deltaTime
       );
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();

            if (player != null)
            {
                AudioManager.instance.PlaySFX("rubis");
                AudioManager.instance.ChangeVolume(20);
                player.Heal(heal);
                Debug.Log("Regenheal");
                Debug.Log(player.GetCurrentHealth());
                Destroy(gameObject);
            }
        }
    }
}
