using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Splines;

namespace Zhamanta
{
    public class RevolvingDoors : MonoBehaviour
    {
        [SerializeField] GameObject splineParent;
        private Door[] doors;

        private void Start()
        {
            doors = splineParent.GetComponentsInChildren<Door>();

            for (int i = 0; i < doors.Length; i++)
            {
                doors[i].SeparateDoors();
            }
        }

        public void ActivateRevolvingDoors()
        {
            for (int i = 0; i < doors.Length; i++)
            {
                doors[i].gameObject.SetActive(true);
            }
        }
    }
}