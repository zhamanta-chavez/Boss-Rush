using UnityEngine;

namespace Zhamanta
{
    public class BLaser : StateMachineBehaviour
    {
        [SerializeField] GameObject projectile;

        Eyebat eyebat;
        Transform player;
        Rigidbody rb;

        private float elapsedTime;
        private bool canShoot;

        //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.ResetTrigger("laser");

            eyebat = FindFirstObjectByType<Eyebat>();
            rb = eyebat.Rb;
            player = eyebat.Target;

            elapsedTime = 0f;
            canShoot = true;
        }

        //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            elapsedTime += Time.deltaTime;

            //Look at Player
            Vector3 directionToTarget = (player.position - eyebat.transform.position);
            directionToTarget.y = 0;
            eyebat.transform.rotation = Quaternion.Slerp(eyebat.transform.rotation,
                Quaternion.LookRotation(directionToTarget.normalized), 2f * Time.deltaTime);

            //Shoot Player (Ranged Attack)
            if (elapsedTime >= 1.5f && canShoot)
            {
                canShoot = false;
                GameObject p = Instantiate(projectile, eyebat.transform.position + new Vector3(0, 5f, 0), Quaternion.identity);
                p.transform.forward = eyebat.transform.forward;
                animator.SetTrigger("continue_sequence");
            }

            //Transition Back to Sequence Point Determinant
            /*if (elapsedTime >= 2f)
            {
                animator.SetTrigger("continue_sequence");
            }*/
        }

        //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }
    }
}