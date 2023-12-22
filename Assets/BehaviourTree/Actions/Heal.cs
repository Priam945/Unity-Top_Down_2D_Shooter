using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class Heal : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        context.boss.Heal();
        return State.Success;
    }
}
