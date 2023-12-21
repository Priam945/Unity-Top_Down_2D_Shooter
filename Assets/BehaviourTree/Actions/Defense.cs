using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using Unity.VisualScripting;

public class Defense : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        context.boss.Shield();
        return State.Success;
    }
}
