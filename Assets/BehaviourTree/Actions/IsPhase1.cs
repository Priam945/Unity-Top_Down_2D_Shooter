using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class IsPhase1 : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        float bossMaxHP = context.boss.GetMaxHP();
        float bossHP = context.boss.GetHP();
        float phase1HPMax = context.boss.GetMaxHP();
        float phase1HPMin = (bossMaxHP * 75) / 100;

        if (phase1HPMax >= bossHP && bossHP > phase1HPMin) {
            return State.Success;
        }

        return State.Failure;
    }
}
