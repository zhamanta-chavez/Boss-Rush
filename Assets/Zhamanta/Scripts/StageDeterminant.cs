using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.Splines;

namespace Zhamanta
{
    public class StageDeterminant : MonoBehaviour
    {
        [SerializeField] private Image barFill;
        [SerializeField] private Image patienceBar;
        [SerializeField] private Image patienceBarBg;
        [SerializeField] Animator animator;
        [SerializeField] AnimatorTracker animTracker;
        [SerializeField] GameObject trail;
        [SerializeField] GameObject arrows;
        [SerializeField] Damageable bossDamageable;
        [SerializeField] Retreat patienceSensor;
        [SerializeField] SphereCollider healer;
        [SerializeField] PlayerLogic playerLogic;
        [SerializeField] Rigidbody eyebatRb;
        [SerializeField] float dyingSpeed = 1;
        [SerializeField] Transform player;
        [SerializeField] Collider playerCollider;
        Vector3 originalPlayerPosition;

        private bool canInvokeHalfLife;
        private bool canInvokeNearDeath;
        private bool canSetDying;

        public UnityEvent OnHalfLife;
        public UnityEvent OnNearDeath;
        public UnityEvent OnNextScene;
        public UnityEvent OnLookAtDeath;
        public UnityEvent OnLookAtBabiesComing;
        public UnityEvent OnLookAtHelp;
        public UnityEvent OnLookAtRetreat;

        private void Start()
        {
            animTracker.ResetValues();
            canInvokeHalfLife = true;
            canInvokeNearDeath = true;
            canSetDying = true;
            trail.SetActive(false);
            arrows.SetActive(false);
            healer.enabled = false;
            originalPlayerPosition = player.transform.position;
            animTracker.PatienceSensorState(false);
            StartCoroutine(TurnOnSensor());
        }

        private void Update()
        {
            if (barFill.fillAmount <= .5f && canInvokeHalfLife)
            {
                StartCoroutine(Stage2Margin());
                OnHalfLife.Invoke();
            }

            if (barFill.fillAmount <= .1f && canInvokeNearDeath)
            {
                Debug.Log("Should activate stage 3");
                StartCoroutine(Stage3Margin());
                OnNearDeath.Invoke();
            }

            if (animTracker.GetTrailState() == true)
            {
                trail.SetActive(true);
            }
            else
            {
                trail.SetActive(false);
                Debug.Log("Here");
            }

            if (animTracker.GetPatienceSensorState() == true)
            {
                patienceSensor.enabled = true;
                patienceBar.enabled = true;
                patienceBarBg.enabled = true;
            }
            else
            {
                patienceSensor.enabled = false;
                patienceBar.enabled = false;
                patienceBarBg.enabled = false;
            }

            if (animTracker.GetHealerState() == true)
            {
                healer.enabled = true;
            }
            else
            {
                healer.enabled = false;
            }

            if (animTracker.GetIsDying() == true)
            {
                Vector3 target = new Vector3(0, 0, 0);
                Vector3 newPos = Vector3.MoveTowards(eyebatRb.position, target, dyingSpeed * Time.fixedDeltaTime);
                eyebatRb.MovePosition(newPos);
            }

            Dying();
        }

        IEnumerator Stage2Margin()
        {
            animTracker.PatienceSensorState(false);
            bossDamageable.enabled = false;
            yield return new WaitForSeconds(6f);
            canInvokeHalfLife = false;
            animTracker.FinishActivatingStage2();
            animTracker.PatienceSensorState(true);
            bossDamageable.enabled = true;
        }

        IEnumerator Stage3Margin()
        {
            animTracker.PatienceSensorState(false);
            bossDamageable.enabled = false;
            yield return new WaitForSeconds(6f);
            canInvokeNearDeath = false;
            animTracker.FinishActivatingStage3();
            bossDamageable.enabled = true;
            MakeArrowsFall();
            animTracker.TrailState(true);
            Debug.Log("Done");
        }

        public void MakeArrowsFall()
        {
            arrows.SetActive(true);
        }

        public void Heal()
        {
            if (barFill.fillAmount <= .9)
            {
                animator.ResetTrigger("walk");
                animator.ResetTrigger("attack_01");
                animator.SetTrigger("heal");
                Debug.Log("Heal Set");
            }
        }

        public void Dying()
        {
            if (barFill.fillAmount <= 0 && canSetDying)
            {
                animTracker.SetDying(true);
                canSetDying = false;
                animator.ResetTrigger("heal");
                animator.ResetTrigger("attack_sequence_done");
                animator.ResetTrigger("attack_sequence");
                animator.ResetTrigger("laser");
                animator.ResetTrigger("electric_floor");
                animator.ResetTrigger("revolving_doors");
                animator.ResetTrigger("attack_01");
                animator.ResetTrigger("walk");
                animator.SetTrigger("dying");
                bossDamageable.enabled = false;
                animTracker.TrailState(false);
                arrows.SetActive(false);
                playerLogic.enabled = false;
                playerCollider.enabled = false;
                Debug.Log("Dying set");
                StartCoroutine(StartPostDeathScene());
                //StartCoroutine(WaitForNextScene());
            }
        }

        IEnumerator StartPostDeathScene()
        {
            yield return new WaitForSeconds(5f);
            //animTracker.SetDying(false);
            player.transform.position = originalPlayerPosition;
            OnLookAtDeath.Invoke();
            StartCoroutine(LookAtBabiesComing());
        }

        IEnumerator LookAtBabiesComing()
        {
            yield return new WaitForSeconds(7f);
            animTracker.SetFetchEyebat(true);
            OnLookAtBabiesComing.Invoke();
            StartCoroutine(LookAtHelp());
        }

        IEnumerator LookAtHelp()
        {
            yield return new WaitForSeconds(3f);
            OnLookAtHelp.Invoke();
            StartCoroutine(LookAtRetreat());
        }

        IEnumerator LookAtRetreat()
        {
            yield return new WaitForSeconds(3f);
            animTracker.SetFetchEyebat(false);
            yield return new WaitForSeconds(1.5f);
            OnLookAtRetreat.Invoke();
            StartCoroutine(WaitForNextScene());
        }
        IEnumerator WaitForNextScene()
        {
            yield return new WaitForSeconds(3f);
            OnNextScene.Invoke();
        }

        IEnumerator TurnOnSensor()
        {
            yield return new WaitForSeconds(3f);
            animTracker.PatienceSensorState(true);
        }
    }
}