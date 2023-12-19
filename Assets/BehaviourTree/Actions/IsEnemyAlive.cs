using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class IsEnemyAlive : ActionNode
{
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (context.enemy.GetHP() > 0)
        {
            return State.Success;
        }

        return State.Failure;
    }
}
