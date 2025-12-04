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
            animator.ResetTrigger("heal");
            animator.ResetTrigger("attack_sequence_done");
            animator.ResetTrigger("laser");
            animator.ResetTrigger("electric_floor");
            animator.ResetTrigger("revolving_doors");

            eyebat = FindFirstObjectByType<Eyebat>();
            player = eyebat.Target;
            rb = eyebat.Rb;
            //canIncreaseAttackCount = true;
            //canChooseAttack = true;

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

            float distanceToTarget = Vector3.Distance(eyebat.transform.position, eyebat.Target.position);

            //Look at Player
            Vector3 directionToTarget = player.position - eyebat.transform.position;
            directionToTarget.y = 0;
            eyebat.transform.rotation = Quaternion.Slerp(eyebat.transform.rotation,
                Quaternion.LookRotation(directionToTarget.normalized), 2f * Time.deltaTime);

            if (distanceToTarget >= 3f)
            {
                //Chase Player
                Vector3 target = new Vector3(player.position.x, 0, player.position.z);
                //Vector3 target = new Vector3(player.position.x, player.position.y, player.position.z);
                Vector3 newPos = Vector3.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
                rb.MovePosition(newPos);
            }

            //Transition to Stage3
            if (animTracker.JustEnteredStage3() == true)
            {
                Debug.Log("Run: Just Entered Stage 3");
                animTracker.TrailState(true);
                animator.SetTrigger("stage3");
            }
            else
            {
                //Transition to Melee Attack
                //float distanceToTarget = Vector3.Distance(eyebat.transform.position, eyebat.Target.position);

                if (distanceToTarget <= 5f)
                {
                    animTracker.SetFromAttack1(true);
                    animator.SetTrigger("attack_01");
                }

                //Choose Attack
                /*if (canChooseAttack)
                {
                    canChooseAttack = false;
                    do
                    {
                        index = UnityEngine.Random.Range(0, 3);
                    } while (index == currentIndex);

                    currentIndex = index;
                    Debug.Log(currentIndex);
                }*/

                //Transition to Attack
                if (timeElapsed >= 6f)
                {
                    animTracker.SetFromAttack1(false);
                    switch (animTracker.GetAttackIndex())
                    {
                        case 0:
                            //timeElapsed = 0;
                            //Debug.Log("index: " + animTracker.GetAttackIndex());
                            animator.ResetTrigger("attack_01");
                            animator.SetTrigger("attack_sequence");
                            break;
                        case 1:
                            //timeElapsed = 0;
                            //Debug.Log("index: " + animTracker.GetAttackIndex());
                            animator.ResetTrigger("attack_01");
                            animator.SetTrigger("electric_floor");
                            break;
                        case 2:
                            //timeElapsed = 0;
                            //Debug.Log("index: " + animTracker.GetAttackIndex());
                            animator.ResetTrigger("attack_01");
                            animator.SetTrigger("revolving_doors");
                            break;
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