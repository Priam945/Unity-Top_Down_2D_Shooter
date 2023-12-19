using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class ChasePlayer : ActionNode
{
    private Transform playerTransform;
    public float moveSpeed = 4f;
    public float desiredDistance = 20f;
    public float retreatDistance = 10f;
    protected override void OnStart()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (playerTransform != null)
        {
            Chase();
            return State.Running;
        }
        else
        {
            return State.Failure;
        }
    }

    private void Chase()
    {
        Vector3 playerPosition = playerTransform.position;
        Vector3 chaseDirection = (playerPosition - context.transform.position).normalized;

        float distanceToPlayer = Vector3.Distance(context.transform.position, playerPosition);

        if (distanceToPlayer > desiredDistance)
        {
            // Chase
            context.transform.position += chaseDirection * moveSpeed * Time.deltaTime;
        }
        else if (distanceToPlayer <= retreatDistance)
        {
            // Retreat (ignorer la composante Y)
            chaseDirection.y = 0f;
            context.transform.position -= chaseDirection * moveSpeed * Time.deltaTime;
        }
    }

}
