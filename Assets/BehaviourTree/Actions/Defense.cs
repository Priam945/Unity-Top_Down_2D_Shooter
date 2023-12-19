using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class Defense : ActionNode
{
    public float detectionRange = 10f; 
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (IsInRange())
        {
            context.boss.Shield();
            return State.Success;
        }

        return State.Failure;
    }

    private bool IsInRange()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            float distance = Vector3.Distance(context.transform.position, player.transform.position);
            return distance <= detectionRange;
        }

        return false;
    }
}
