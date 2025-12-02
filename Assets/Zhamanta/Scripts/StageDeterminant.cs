using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Zhamanta
{
    public class StageDeterminant : MonoBehaviour
    {
        [SerializeField] private Image barFill;
        [SerializeField] AnimatorTracker animTracker;

        public UnityEvent OnHalfLife;

        private void Start()
        {
            animTracker.ResetValues();
        }

        private void Update()
        {
            if (barFill.fillAmount <= .5f)
            {
                OnHalfLife.Invoke();
            }
        }
    }
}