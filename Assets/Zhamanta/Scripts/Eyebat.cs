using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Zhamanta
{
    public class Eyebat : MonoBehaviour
    {
        [SerializeField] Animator animator;

        [field: SerializeField] public Transform Target { get; set; }

        [field: SerializeField] public Rigidbody Rb { get; set; }

        [field: SerializeField] public Transform[] Points { get; set; }

    }
}