using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Employ�Base : MonoBehaviour
{
    public int employ�HP = 100;

    public Animator animator;

    public void TakeDamage(int damageAmount)
    {
        employ�HP -= damageAmount;
        if (employ�HP <= 0)
        {
            animator.SetTrigger("death");
            GetComponent<CapsuleCollider>().enabled = false;
        }
        else
        {
            animator.SetTrigger("damage");
        }
    }
}
