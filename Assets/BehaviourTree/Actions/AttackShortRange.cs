using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class AttackShortRange : ActionNode {
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


        if (attackSPE <= attackSPEpourcentage) {
            boss.Dash();
        } else {
            boss.Chase();
        }

        if (boss.IsInShortRange()) {
            if (attackSPE <= attackSPEpourcentage) {
                boss.DoDamage(boss.GetShortRangeDamageSPE());
            } else {
                boss.DoDamage(boss.GetShortRangeDamage());
            }

            return State.Success;
        }

        if (chaseTimer >= 5f) {
            chaseTimer = 0f;
            return State.Failure;
        }

        chaseTimer += Time.deltaTime;

        return State.Running;
    }
}