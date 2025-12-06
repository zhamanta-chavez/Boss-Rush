using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

namespace Zhamanta
{
    public class Retreat : MonoBehaviour
    {
        [SerializeField] AnimatorTracker animTracker;
        [SerializeField] float patience;
        [SerializeField] float maxPatience = 4;
        [SerializeField] Image patienceBar;
        [SerializeField] Image patienceBarBg;
        private bool playerInSphere;

        public UnityEvent OnMaxPatience;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            patience = 0;
            playerInSphere = false;
            patienceBar.fillAmount = 0;
        }

        private void Update()
        {
            if (playerInSphere)
            {
                patience += Time.deltaTime;
                patienceBar.fillAmount = patience / maxPatience;
            }

            if (patience >= maxPatience)
            {
                OnMaxPatience.Invoke();
                Debug.Log("Invoked");
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                playerInSphere = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                playerInSphere = false;
                patience = 0;
                patienceBar.fillAmount = 0;
            }
        }
    }
}