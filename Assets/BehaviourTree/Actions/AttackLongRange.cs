using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class AttackLongRange : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        float attackSPEpourcentage = 0.15f;
        float attackSPE = Random.Range(0f, 1f);


        if (context.boss.IsInLongRange()) {
            if (attackSPE <= attackSPEpourcentage) {
                context.boss.SetIsAttackSPE(true);
                context.boss.Shoot();
            } else {
                context.boss.Shoot();
            }
        }

        return State.Success;
    }
}
