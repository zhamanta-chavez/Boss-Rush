using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Zhamanta
{
    public class BElectricAttack : StateMachineBehaviour
    {
        Eyebat eyebat;
        Rigidbody rb;
        Transform player;

        [SerializeField] AnimatorTracker animTracker;

        private bool canTrigger;

        //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.ResetTrigger("walk");

            eyebat = FindFirstObjectByType<Eyebat>();
            rb = eyebat.Rb;
            player = eyebat.Target;
            canTrigger = true;
        }

        //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //Look at Player
            Vector3 directionToTarget = (player.position - eyebat.transform.position);
            directionToTarget.y = 0;
            eyebat.transform.rotation = Quaternion.Slerp(eyebat.transform.rotation,
                Quaternion.LookRotation(directionToTarget.normalized), 2f * Time.deltaTime);

            // Move to high center
            rb.MovePosition(new Vector3(0, 7, 0));

            if (canTrigger)
            {
                canTrigger = false;
                animator.GetComponent<ElectricAttack>().ActivateElectricAttack();
                animator.SetTrigger("walk");
            }
        }

        //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }
    }
}