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
        [SerializeField] AnimatorTracker animTracker;
        [SerializeField] GameObject trail;
        [SerializeField] GameObject arrows;

        private bool canInvokeHalfLife;
        private bool canInvokeNearDeath;

        public UnityEvent OnHalfLife;
        public UnityEvent OnNearDeath;

        private void Start()
        {
            animTracker.ResetValues();
            canInvokeHalfLife = true;
            canInvokeNearDeath = true;
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
        }

        IEnumerator Stage2Margin()
        {
            yield return new WaitForSeconds(3.5f);
            canInvokeHalfLife = false;
            animTracker.FinishActivatingStage2();
        }

        IEnumerator Stage3Margin()
        {
            yield return new WaitForSeconds(3.5f);
            canInvokeNearDeath = false;
            animTracker.FinishActivatingStage3();
        }

        public void MakeArrowsFall()
        {
            arrows.SetActive(true);
        }
    }
}