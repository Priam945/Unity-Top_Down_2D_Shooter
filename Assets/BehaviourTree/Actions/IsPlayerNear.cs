using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class IsPlayerNear : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        bool isCurrentlyInRange = context.boss.IsInLongRange();

        if (isCurrentlyInRange) {
            return State.Success;
        } else if (!isCurrentlyInRange) {
            return State.Failure;
        }

        return State.Failure;
    }
}
