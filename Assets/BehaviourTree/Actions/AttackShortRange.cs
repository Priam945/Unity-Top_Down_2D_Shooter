using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class AttackShortRange : ActionNode
{
    private float chaseDuration = 5f;
    private float elapsedTime = 0f;

    protected override void OnStart() {
        chaseDuration = 5f;
        elapsedTime = 0f;
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        float attackSPEpourcentage = 0.15f;
        float attackSPE = Random.Range(0f, 1f);


        if (context.boss.IsInShortRange()) {
            if (attackSPE <= attackSPEpourcentage) {
                context.boss.DoDamage(context.boss.GetShortRangeDamageSPE());
            } else {
                context.boss.DoDamage(context.boss.GetShortRangeDamage());
            }

            return State.Success;
        } else  {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= chaseDuration) {
                return State.Failure;
            }

            if (attackSPE <= attackSPEpourcentage) {
                context.boss.Dash();
            } else {
                context.boss.Chase();
            }

            return State.Running;
        }
    }
}


// on chase le player jusqu'� arriver dans zone 1
// tant que on est pas dans le range, on chase le player (utilisation d'un dash)
// d�s que on est dans le range (zone 1), on met des d�gats au player (degats augment�)

// on chase le player jusqu'� arriver dans zone 1
// tant que on est pas dans le range, on chase le player
// si au bout de X secondes on est toujours pas dans le range, alors on fait autre chose
// d�s que on est dans le range (zone 1), on met des d�gats au player


/*
if (in short range)
    if (attackSPe <= attackSPEpourcentage)
        on fait plus de d�gats
    else
        on fait des d�gats normal

else
    on va chase le player (avec ou sans dash) pendant un temps limit�
    si on est pas dans le range quand le temps est �coul�, alors on arr�te l'action attack (donc on retourne failure)


    on est en train de chase le joueur, donc il faut que le code continue de tourn�

    if (attackSPe <= attackSPEpourcentage)
        on fait un dash d'une certaine dur�e
    else
        on poursuit le joueur normalement (sans le dash)
*/
