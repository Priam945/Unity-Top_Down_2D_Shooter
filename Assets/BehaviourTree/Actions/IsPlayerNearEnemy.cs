using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class IsPlayerNearEnemy : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (context.enemy.GetRangeZone())
        {
            Debug.Log("Je suis dans la range neuille");
            return State.Success;
        }
        return State.Failure;
        
    }
}
