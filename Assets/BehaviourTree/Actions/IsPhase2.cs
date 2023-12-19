using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class IsPhase2 : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        float bossMaxHP = context.boss.GetMaxHP();
        float bossHP = context.boss.GetHP();
        float phase2HPMax = (bossMaxHP * 75) / 100;
        float phase2HPMin = (bossMaxHP * 30) / 100;

        if (phase2HPMax >= bossHP && bossHP > phase2HPMin) {
            return State.Success;
        }

        return State.Failure;
    }
}
