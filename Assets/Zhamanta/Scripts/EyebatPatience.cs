using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Zhamanta
{
    public class EyebatPatience : MonoBehaviour
    {
        private bool playerInRange;

        private void Start()
        {
            playerInRange = false;
        }

        public void PlayerInRange()
        {
            playerInRange = true;
        }

        public void PlayerNotInRange()
        {
            playerInRange = false;
        }

        public void StartPatienceTimer()
        {
            StartCoroutine(EyebatPatienceTimer());
        }

        IEnumerator EyebatPatienceTimer()
        {
            yield return new WaitForSeconds(10);
            if (playerInRange)
            {
                //Punishment
            }
        }
    }
}