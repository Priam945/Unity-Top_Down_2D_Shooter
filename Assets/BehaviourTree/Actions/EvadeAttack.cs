using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class Evade : ActionNode
{
    public float evadeRange = 10f;
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
            context.boss.EvadeAtk(); 
            return State.Success;
        }

        return State.Failure;
    }

    private bool IsInRange()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Vector3 directionToPlayer = player.transform.position - context.transform.position;
            float angle = Vector3.Angle(context.transform.forward, directionToPlayer);

            if (angle <= 45f)
            {
                float distance = directionToPlayer.magnitude;
                return distance <= evadeRange;
            }
        }

        return false;
    }
}
