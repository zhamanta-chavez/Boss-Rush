using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

namespace Zhamanta
{
    public class BLaser : StateMachineBehaviour
    {
        [SerializeField] GameObject projectile;
        [SerializeField] AnimatorTracker animTracker;

        Eyebat eyebat;
        Transform player;

        private float elapsedTime;
        private bool canShoot;
   

        //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.ResetTrigger("attack_01");
            animator.ResetTrigger("attack_sequence");
            animator.ResetTrigger("stage2");


            eyebat = FindFirstObjectByType<Eyebat>();
            player = eyebat.Target;

            elapsedTime = 0f;
            canShoot = true;
        }

        //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            elapsedTime += Time.deltaTime;

            if (animTracker.JustEnteredStage2() == true) //Transition to Stage2
            {
                Debug.Log("Laser: Just Entered Stage 2");
                animator.SetTrigger("stage2");
            }
            else if (animTracker.JustEnteredStage3() == true) //Transition to Stage3
            {
                Debug.Log("Laser: Just Entered Stage 3");
                animator.SetTrigger("stage3");
            }
            else if (animTracker.GetIsDying())
            {
                animator.ResetTrigger("attack_sequence_done");
                animator.ResetTrigger("continue_sequence");
                animator.SetTrigger("dying");
            }
            else //Sequence Attack
            {
                //Look at Player
                Vector3 directionToTarget = (player.position - eyebat.transform.position);
                directionToTarget.y = 0;
                eyebat.transform.rotation = Quaternion.Slerp(eyebat.transform.rotation,
                    Quaternion.LookRotation(directionToTarget.normalized), 2f * Time.deltaTime);

                //Shoot Player (Ranged Attack)
                if (elapsedTime >= 1.5f && canShoot)
                {
                    canShoot = false;
                    GameObject p = Instantiate(projectile, eyebat.transform.position + new Vector3(0, 4.5f, 0), Quaternion.identity);
                    //GameObject p = Instantiate(projectile, eyebat.transform.position, Quaternion.identity);
                    animTracker.IncreaseShootCount();
                    p.transform.forward = eyebat.transform.forward;

                    if (animTracker.GetShootCount() == 5)
                    {
                        if (animTracker.OnStage2() == true)
                        {
                            animTracker.TrailState(false);
                        }
                        animTracker.ResetShootCount();
                        Debug.Log("Does this happen once?");
                        animTracker.IncreaseIndex();
                        Debug.Log("index: " + animTracker.GetAttackIndex());
                        animator.SetTrigger("attack_sequence_done");
                    }
                    else
                    {
                        animator.SetTrigger("continue_sequence");
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