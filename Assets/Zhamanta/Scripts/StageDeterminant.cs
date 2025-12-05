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
        [SerializeField] Animator animator;
        [SerializeField] AnimatorTracker animTracker;
        [SerializeField] GameObject trail;
        [SerializeField] GameObject arrows;
        [SerializeField] Damageable bossDamageable;
        [SerializeField] Retreat patienceSensor;

        private bool canInvokeHalfLife;
        private bool canInvokeNearDeath;
        private bool canSetDying;

        public UnityEvent OnHalfLife;
        public UnityEvent OnNearDeath;
        public UnityEvent OnNextScene;

        private void Start()
        {
            animTracker.ResetValues();
            canInvokeHalfLife = true;
            canInvokeNearDeath = true;
            canSetDying = true;
            trail.SetActive(false);
            arrows.SetActive(false);
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
            }

            if (animTracker.GetPatienceSensorState() == true)
            {
                patienceSensor.enabled = true;
            }
            else
            {
                patienceSensor.enabled = false;
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
                arrows.SetActive(false);
                animTracker.TrailState(false);
                Debug.Log("Dying set");
                StartCoroutine(WaitForNextScene());
            }
        }

        IEnumerator WaitForNextScene()
        {
            yield return new WaitForSeconds(5);
            OnNextScene.Invoke();
        }
    }
}