using System;
using UnityEngine;

namespace Zhamanta
{
    public class BRun : StateMachineBehaviour
    {
        [SerializeField] AnimatorTracker animTracker;
        Eyebat eyebat;
        Transform player;
        Rigidbody rb;

        public float speed = 3f;

        private float timeElapsed = 0f;

        private bool canIncreaseAttackCount;

        private bool canChooseAttack;
        private int index = 0;
        private int currentIndex = -1;

        //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.ResetTrigger("attack_sequence_done");
            animator.ResetTrigger("laser");
            animator.ResetTrigger("electric_floor");
            animator.ResetTrigger("revolving_doors");

            eyebat = FindFirstObjectByType<Eyebat>();
            player = eyebat.Target;
            rb = eyebat.Rb;
            canIncreaseAttackCount = true;
            canChooseAttack = true;

            // Works even when coming from electric floor attack
            if (animTracker.GetShootCount() >= 5)
            {
                if (animTracker.GetShootCount() % 5 == 0)
                {
                    timeElapsed = 0;
                }
            }
        }

        //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            timeElapsed += Time.deltaTime;

            //Chase Player
            Vector3 target = new Vector3(player.position.x, player.position.y, player.position.z);
            //Vector3 target = new Vector3(player.position.x, player.position.y, player.position.z);
            Vector3 newPos = Vector3.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);

            //Look at Player
            Vector3 directionToTarget = player.position - eyebat.transform.position;
            directionToTarget.y = 0;
            eyebat.transform.rotation = Quaternion.Slerp(eyebat.transform.rotation,
                Quaternion.LookRotation(directionToTarget.normalized), 2f * Time.deltaTime);

            //Transition to Melee Attack
            float distanceToTarget = Vector3.Distance(eyebat.transform.position, eyebat.Target.position);

            if (distanceToTarget <= 5f)
            {
                animator.SetTrigger("attack_01");
            }

            //Choose Attack
            if (canChooseAttack)
            {
                canChooseAttack = false;
                do
                {
                    index = UnityEngine.Random.Range(0, 3);
                } while (index == currentIndex);

                currentIndex = index;
            }

            //Transition to Attack
            if (timeElapsed >= 5f)
            {
                switch (currentIndex)
                {
                    case 0:
                        animator.SetTrigger("attack_sequence");
                        break;
                    case 1:
                        animator.SetTrigger("electric_floor");
                        break;
                    case 2:
                        animator.SetTrigger("revolving_doors");
                        break;
                }
            }

            //Transition to Stage3
            /*if (animTracker.Stage2())
            {
                animator.SetTrigger("stage2");
            }*/
        }

        //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            
        }

        public void ResetTimer()
        {
            timeElapsed = 0f;
        }

    }
}