using UnityEngine;

namespace Zhamanta
{
    public class BRevolvingDoors : StateMachineBehaviour
    {
        Eyebat eyebat;
        Rigidbody rb;
        Transform player;

        [SerializeField] AnimatorTracker animTracker;

        private bool canTrigger;

        //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.ResetTrigger("run");

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
                animator.GetComponent<RevolvingDoors>().ActivateRevolvingDoors();
            }
        }

        //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }
    }
}