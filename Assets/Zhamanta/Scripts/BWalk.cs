using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;

namespace Zhamanta
{
    public class BWalk : StateMachineBehaviour
    {
        [SerializeField] AnimatorTracker animTracker;
        Eyebat eyebat;
        Transform player;
        Rigidbody rb;

        public float speed = 2f;

        private float timeElapsed = 0f;

        private bool canIncreaseAttackCount;

        //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.ResetTrigger("attack_sequence_done");
            animator.ResetTrigger("laser");
            animator.ResetTrigger("electric_floor");
            eyebat = FindFirstObjectByType<Eyebat>();
            player = eyebat.Target;
            rb = eyebat.Rb;
            canIncreaseAttackCount = true;

            /*if (animTracker.GetShootCount() >= 5)
            {
                if (animTracker.GetShootCount() % 5 == 0)
                {
                    timeElapsed = 0;
                    Debug.Log("Reset to 0");
                }
            }*/

            if (animTracker.GetFromAttack1() == true)
            {
                //animTracker.SetFromAttack1(false);
            }
            else
            {
                timeElapsed = 0;
            }
            //Debug.Log(timeElapsed);
        }

        //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            timeElapsed += Time.deltaTime;

            //Chase Player
            Vector3 target = new Vector3(player.position.x, -1.5f, player.position.z);
            //Vector3 target = new Vector3(player.position.x, player.position.y, player.position.z);
            Vector3 newPos = Vector3.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);

            //Look at Player
            Vector3 directionToTarget = (player.position - eyebat.transform.position);
            directionToTarget.y = 0;
            eyebat.transform.rotation = Quaternion.Slerp(eyebat.transform.rotation,
                Quaternion.LookRotation(directionToTarget.normalized), 2f * Time.deltaTime);


            //Transition to Stage2
            if (animTracker.JustEnteredStage2() == true)
            {
                Debug.Log("Walk: Just Entered Stage 2");
                animator.SetTrigger("stage2");
            }
            else
            {
                //Transition to Melee Attack
                float distanceToTarget = Vector3.Distance(eyebat.transform.position, eyebat.Target.position);

                if (distanceToTarget <= 5f)
                {
                    animTracker.SetFromAttack1(true);
                    animator.SetTrigger("attack_01");
                }

                //Transition to Attack
                if (timeElapsed >= 5f)
                {
                    animTracker.SetFromAttack1(false);
                    if (canIncreaseAttackCount)
                    {
                        canIncreaseAttackCount = false;
                        animTracker.IncreaseAttackCount();
                    }
                    if (animTracker.GetAttackCount() < 2)
                    {
                        //timeElapsed = 0;
                        animator.SetTrigger("attack_sequence");
                    }
                    else if (animTracker.GetAttackCount() >= 2)
                    {
                        if (animTracker.GetAttackCount() % 2 == 0)
                        {
                            //timeElapsed = 0;
                            animator.SetTrigger("electric_floor");
                        }
                        else
                        {
                            //timeElapsed = 0;
                            animator.SetTrigger("attack_sequence");
                        }
                    }
                }
            }
        }

        //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }
    }
}