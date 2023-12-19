using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalker : MonoBehaviour
{
    public int stalkerHP = 100;

    public Animator animator;

    public void TakeDamage(int damageAmount)
    {
        stalkerHP -= damageAmount;
        if (stalkerHP <= 0)
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
