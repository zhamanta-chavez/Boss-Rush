using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;

namespace Zhamanta
{
    public class BWalk : StateMachineBehaviour
    {
        Eyebat eyebat;
        Transform player;
        Rigidbody rb;

        public float speed = 2f;

        private float timeElapsed = 0f;

        //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            eyebat = FindFirstObjectByType<Eyebat>();
            animator.ResetTrigger("walk");
            player = eyebat.Target;
            rb = eyebat.Rb;
        }

        //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            timeElapsed += Time.deltaTime;

            //Chase Player
            Vector3 target = new Vector3(player.position.x, -1.5f, player.position.z);
            Vector3 newPos = Vector3.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);

            //Look at Player
            Vector3 directionToTarget = (player.position - eyebat.transform.position);
            directionToTarget.y = 0;
            eyebat.transform.rotation = Quaternion.Slerp(eyebat.transform.rotation,
                Quaternion.LookRotation(directionToTarget.normalized), 2f * Time.deltaTime);

            //Transition to Melee Attack
            float distanceToTarget = Vector3.Distance(eyebat.transform.position, eyebat.Target.position);

            if (distanceToTarget <= 5f)
            {
                animator.SetTrigger("attack_01");
            }

            //Transition to Sequence Attack
            if (timeElapsed >= 5f)
            {
                animator.SetTrigger("attack_sequence");
            }
        }

        //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }
    }
}