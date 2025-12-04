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

        private bool canInvokeHalfLife;

        public UnityEvent OnHalfLife;

        private void Start()
        {
            animTracker.ResetValues();
            canInvokeHalfLife = true;
            trail.SetActive(false);
        }

        private void Update()
        {
            if (barFill.fillAmount <= .5f && canInvokeHalfLife)
            {
                StartCoroutine(Stage2Margin());
                OnHalfLife.Invoke();
            }

            if (animTracker.GetTrailState() == true)
            {
                trail.SetActive(true);
                Debug.Log("Trail should be active");
            }
            else
            {
                trail.SetActive(false);
            }
        }

        IEnumerator Stage2Margin()
        {
            yield return new WaitForSeconds(3f);
            canInvokeHalfLife = false;
            animTracker.FinishActivatingStage2();
        }
    }
}