using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class IsPlayerNearShooter : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (context.shooter.GetPlayerScript().GetCurrentHealth() <= 0) {
            return State.Failure;
        }

        bool isCurrentlyInRange = context.shooter.IsInLongRange();

        if (isCurrentlyInRange) {
            return State.Success;
        } else if (!isCurrentlyInRange) {
            return State.Failure;
        }

        return State.Failure;
    }
}
