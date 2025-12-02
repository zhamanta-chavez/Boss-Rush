using UnityEngine;
using System;
using System.Collections;

namespace Zhamanta
{
    public class ElectricAttack : MonoBehaviour
    {
        [SerializeField] GameObject electricSign;
        [SerializeField] GameObject electricFloor;

        private int signCount = 0;

        private void Start()
        {
            electricSign.SetActive(false);
            electricFloor.SetActive(false);
        }

        public void ActivateElectricAttack()
        {
            signCount = 0;
            StartCoroutine(ElectricAttackTimer());
        }

        public IEnumerator ElectricAttackTimer()
        {
            while (signCount <= 2)
            {
                Debug.Log(signCount);
                electricSign.SetActive(true);
                yield return new WaitForSeconds(.2f);
                electricSign.SetActive(false);
                yield return new WaitForSeconds(.2f);
                signCount += 1;
            }

            electricFloor.SetActive(true);
            yield return new WaitForSeconds(.3f);
            electricFloor.SetActive(false);
        }
    }
}