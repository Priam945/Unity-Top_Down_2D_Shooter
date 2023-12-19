using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class LookAtPlayer : ActionNode
{
    private Transform playerTransform;

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
            LookAtPlayerPosition();
            return State.Success;
        }
        else
        {
            return State.Failure;
        }
    }

    private void LookAtPlayerPosition()
    {
        Vector3 directionToPlayer = playerTransform.position - context.transform.position;
        directionToPlayer.y = 0f; 
        Quaternion rotationToPlayer = Quaternion.LookRotation(directionToPlayer);
        context.transform.rotation = rotationToPlayer;
    }
}
