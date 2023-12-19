using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployéBase : MonoBehaviour
{
    public int employéHP = 100;

    public Animator animator;

    public void TakeDamage(int damageAmount)
    {
        employéHP -= damageAmount;
        if (employéHP <= 0)
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
