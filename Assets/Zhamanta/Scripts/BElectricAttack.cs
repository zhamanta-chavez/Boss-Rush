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
        [SerializeField] float speedToCenter = 1;

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

            if (animTracker.JustEnteredStage2() == true) //Transition to Stage2
            {
                Debug.Log("Electric: Just Entered Stage 2");
                animator.SetTrigger("stage2");
            }
            else if (animTracker.JustEnteredStage3() == true) //Transition to Stage3
            {
                Debug.Log("Electric: Just Entered Stage 3");
                animator.SetTrigger("stage3");
            }
            else
            {
                // Move to high center
                Vector3 newPos = Vector3.MoveTowards(rb.position, new Vector3(0, 7, 0), speedToCenter * Time.fixedDeltaTime);
                rb.MovePosition(newPos);

                if (canTrigger)
                {
                    canTrigger = false;
                    animTracker.IncreaseIndex();
                    Debug.Log("index: " + animTracker.GetAttackIndex());
                    animator.GetComponent<ElectricAttack>().ActivateElectricAttack();
                    animator.SetTrigger("walk");
                }
            }
        }

        //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }
    }
}