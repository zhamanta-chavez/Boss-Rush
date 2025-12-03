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

        private bool canInvokeHalfLife;

        public UnityEvent OnHalfLife;

        private void Start()
        {
            animTracker.ResetValues();
            canInvokeHalfLife = true;
        }

        private void Update()
        {
            if (barFill.fillAmount <= .5f && canInvokeHalfLife)
            {
                StartCoroutine(Stage2Margin());
                OnHalfLife.Invoke();
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