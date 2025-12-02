using UnityEngine;
using System;
using System.Collections;

namespace Zhamanta
{
    public class RevolvingDoors : MonoBehaviour
    {
        [SerializeField] GameObject[] doors;

        private void Start()
        {
            for (int i = 0; i < doors.Length; i++)
            {
                doors[i].SetActive(false);
            }
        }

        public void ActivateRevolvingDoors()
        {
            for (int i = 0; i < doors.Length; i++)
            {
                doors[i].SetActive(true);
            }
        }
    }
}