using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.XR;

namespace Zhamanta
{
    public class BDashToCorner : StateMachineBehaviour
    {
        private bool canChoosePoint;

        Eyebat eyebat;
        Rigidbody rb;

        public float speed = 10f;

        private float timeElapsed = 0f;
        int index = 0;
        int currentIndex = -1;

        //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            eyebat = FindFirstObjectByType<Eyebat>();
            rb = eyebat.Rb;
            animator.ResetTrigger("attack_sequence");
            canChoosePoint = true;
        }

        //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //Choose Point to Move Towards
            if (canChoosePoint)
            {
                Debug.Log(currentIndex);
                canChoosePoint = false;
                do
                {
                    index = Random.Range(0, 4);
                } while (index == currentIndex);

                currentIndex = index;
            }

            //Move Towards Point
            Vector3 target = new Vector3(eyebat.Points[index].position.x, rb.position.y, eyebat.Points[index].position.z);
            Vector3 newPos = Vector3.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
            animator.SetTrigger("laser");
        }

        //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }
    }
}