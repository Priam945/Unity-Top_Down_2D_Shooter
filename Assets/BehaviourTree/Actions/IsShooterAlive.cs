using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using Unity.VisualScripting;

public class IsShooterAlive : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (context.shooter.GetHP() > 0) {
            return State.Success;
        }

        return State.Failure;
    }
}
