using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.Splines;
using System;

namespace Zhamanta
{
    public class FallingArrows : MonoBehaviour
    {
        [SerializeField] GameObject arrowPrefab;
        [SerializeField] Transform player;
        [SerializeField] AnimatorTracker animTracker;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Debug.Log("Started Coroutine");
            StartCoroutine(MakeArrowFall())
;        }

        IEnumerator MakeArrowFall()
        {
            Debug.Log(animTracker.OnStage3());
            while (animTracker.OnStage3() == true)
            {
                GameObject p = Instantiate(arrowPrefab, new Vector3(player.position.x, transform.position.y, player.position.z), Quaternion.identity);
                p.transform.forward = Vector3.down;
                yield return new WaitForSeconds(1);
            }
        }
    }
}