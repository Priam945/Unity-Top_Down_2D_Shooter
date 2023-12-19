using System.Collections;
using UnityEngine.AI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FleeBehavior : StateMachineBehaviour
{
    NavMeshAgent agent;

    Transform player;
    float chaseRange = 15;
    float speed = 5;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distance = Vector3.Distance(animator.transform.position, player.position);
        if (distance > chaseRange)
            animator.SetBool("IsFleeing", false);

        // Obtenez la direction opposée en soustrayant la position du joueur de la position de l'IA
        Vector3 directionToFuite = agent.transform.position - player.position;

        // Normalisez le vecteur pour obtenir une direction, puis multipliez-le par une distance pour déterminer la destination de fuite
        Vector3 destinationFuite = agent.transform.position + directionToFuite.normalized * 5f; // Vous pouvez ajuster la distance selon vos besoins
        agent.speed = speed;
        // Définissez la destination pour que l'IA se dirige vers la position de fuite
        agent.SetDestination(destinationFuite);

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that processes and affects root motion
    }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that sets up animation IK (inverse kinematics)
    }
}
