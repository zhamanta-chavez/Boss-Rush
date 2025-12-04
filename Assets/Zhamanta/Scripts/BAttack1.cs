using UnityEngine;

namespace Zhamanta
{
    public class BAttack1 : StateMachineBehaviour
    {
        [SerializeField] AnimatorTracker animTracker;
        //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.ResetTrigger("walk");
        }

        //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (animTracker.JustEnteredStage2() == true) //Transition to Stage2
            {
                Debug.Log("Attack1: Just Entered Stage 2");
                animator.SetTrigger("stage2");
            }
            else if (animTracker.JustEnteredStage3() == true) //Transition to Stage3
            {
                Debug.Log("Attack1: Just Entered Stage 3");
                animator.SetTrigger("stage3");
            }
        }

        //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //animTracker.SetFromAttack1(true);
            //Debug.Log(animTracker.GetFromAttack1());
        }
    }
}