using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using System.Threading;

public class Dash : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {

        if (context.enemy.GetRangeZone())
        {
            context.enemy.Dash();
        }
        return State.Success;
    }
}
