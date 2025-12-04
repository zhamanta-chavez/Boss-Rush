using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.XR;

namespace Zhamanta
{
    public class BDashToCorner : StateMachineBehaviour
    {
        [SerializeField] AnimatorTracker animTracker;

        private bool canChoosePoint;

        Eyebat eyebat;
        Rigidbody rb;

        public float speed = 10f;

        //private float timeElapsed;
        private int index = 0;
        private int currentIndex = -1;
        

        //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            eyebat = FindFirstObjectByType<Eyebat>();
            rb = eyebat.Rb;
            animator.ResetTrigger("attack_sequence");
            animator.ResetTrigger("attack_sequence_done");
            animator.ResetTrigger("walk");
            canChoosePoint = true;
            //timeElapsed = 0f;
        }

        //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //timeElapsed = Time.deltaTime;

            if (animTracker.JustEnteredStage2() == true) //Transition to Stage2
            {
                Debug.Log("Dash: Just Entered Stage 2");
                animator.SetTrigger("stage2");
            }
            else if (animTracker.JustEnteredStage3() == true) //Transition to Stage3
            {
                Debug.Log("Dash: Just Entered Stage 3");
                animator.SetTrigger("stage3");
            }
            else
            {
                if (animTracker.OnStage2() == true)
                {
                    Debug.Log("Verified onStage2: " + animTracker.OnStage2());
                    animTracker.TrailState(true);
                }
                //Choose Point to Move Towards
                if (canChoosePoint)
                {
                    canChoosePoint = false;
                    do
                    {
                        index = Random.Range(0, 4);
                    } while (index == currentIndex);

                    currentIndex = index;
                }

                //Move Towards Point
                Vector3 target = new Vector3(eyebat.Points[index].position.x, eyebat.Points[index].position.y, eyebat.Points[index].position.z);
                Vector3 newPos = Vector3.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
                rb.MovePosition(newPos);

                if (rb.position == newPos)
                {
                    animator.SetTrigger("laser");
                }
            }
        }

        //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }
    }
}