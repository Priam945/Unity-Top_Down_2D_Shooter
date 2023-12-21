using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class AttackLongRange : ActionNode
{
    private Boss boss;
    private float chaseTimer;

    protected override void OnStart() {
        boss = context.boss;
        chaseTimer = 0f;
    }

    protected override void OnStop() {
        chaseTimer = 0f;
    }

    protected override State OnUpdate() {
        float attackSPEpourcentage = 0.15f;
        float attackSPE = Random.Range(0f, 1f);

        boss.ChaseWithDistance(0.8f);

        if (attackSPE <= attackSPEpourcentage) {
            boss.SetIsAttackSPE(true);
            boss.Shoot();
        } else {
            boss.SetIsAttackSPE(false);
            boss.Shoot();
        }

        if (chaseTimer >= 10f) {
            chaseTimer = 0f;
            return State.Failure;
        }

        chaseTimer += Time.deltaTime;

        return State.Running;
    }
}
