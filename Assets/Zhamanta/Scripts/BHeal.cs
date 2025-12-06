using UnityEngine;

namespace Zhamanta
{
    public class BHeal : StateMachineBehaviour
    {
        Eyebat eyebat;
        Rigidbody rb;

        [SerializeField] AnimatorTracker animTracker;

        [SerializeField] float speedToCenter = 1;
        private float timeElapsed;

        //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.ResetTrigger("attack_01");
            animator.ResetTrigger("walk");

            eyebat = FindFirstObjectByType<Eyebat>();
            rb = eyebat.Rb;

            timeElapsed = 0;
            animTracker.SetHealerState(true);
        }

        //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            timeElapsed += Time.deltaTime;

            // Move to high center
            Vector3 newPos = Vector3.MoveTowards(rb.position, new Vector3(0, 6, 0), speedToCenter * Time.fixedDeltaTime);
            rb.MovePosition(newPos);

            if (timeElapsed >= 3)
            {
                animator.SetTrigger("walk");
            }
        }

        //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animTracker.SetHealerState(false);
        }
    }
}