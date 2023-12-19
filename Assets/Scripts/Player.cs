using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerHP = 100;

    public Animator animator;

    public void TakeDamage(int damageAmount)
    {
        playerHP -= damageAmount;
        if (playerHP <= 0)
        {
            animator.SetTrigger("death");
        }
        else
        {
            animator.SetTrigger("damage");
        }
    }
}
