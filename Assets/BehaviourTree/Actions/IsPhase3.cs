using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class IsPhase3 : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        float bossMaxHP = context.boss.GetMaxHP();
        float bossHP = context.boss.GetHP();
        float phase3HPMax = (bossMaxHP * 30) / 100;
        float phase3HPMin = 0f;

        if (phase3HPMax >= bossHP && bossHP > phase3HPMin) {
            return State.Success;
        }

        return State.Failure;
    }
}
