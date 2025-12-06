using UnityEngine;
using Zhamanta;

public class BInspire2 : StateMachineBehaviour
{
    [SerializeField] AnimatorTracker animTracker;
    private bool canTriggerLaugh;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("laser");
        animator.ResetTrigger("walk");
        animator.ResetTrigger("attack_sequence");
        animator.ResetTrigger("continue_sequence");
        animator.ResetTrigger("attack_sequence_done");
        animator.ResetTrigger("electric_floor");

        canTriggerLaugh = true;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (canTriggerLaugh)
        {
            canTriggerLaugh = false;
            animTracker.SetLaughOn(true);
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
