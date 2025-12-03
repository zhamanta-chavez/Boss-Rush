using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Splines;
using Unity.VisualScripting;

namespace Zhamanta
{
    public class Door : MonoBehaviour
    {
        [SerializeField] SplineAnimate sa;
        [SerializeField] SpriteRenderer sp;
        [SerializeField] BoxCollider box;

        [SerializeField] float fadeSpeed = 1;
        [SerializeField] float fadeSpeed2 = 1;
        private bool fadeIn;
        private bool fadeAway;
        private bool canStartCoroutine;
        private bool canStartNormalRoutine;

        void OnEnable()
        {
            fadeIn = true;
            fadeAway = false;
            canStartCoroutine = true;
            sp.color = new Color(1, 0, 0, 0);
            canStartNormalRoutine = false;
            StartCoroutine(DoorsSeparating());
            box.enabled = false;
        }

        void Update()
        {
            if (canStartNormalRoutine)
            {
                if (fadeIn)
                {
                    sp.color = Color.Lerp(sp.color, new Color(1, 0, 0, 1), fadeSpeed * Time.deltaTime);
                    if (Vector4.Distance(sp.color, new Color(1, 0, 0, 1)) < 0.01f && canStartCoroutine)
                    {
                        fadeIn = false;
                        canStartCoroutine = false;
                        StartCoroutine(RevolvingDoorsTimer());
                    }
                }

                if (fadeAway)
                {
                    sp.color = Color.Lerp(sp.color, new Color(0, 0, 1, 0), fadeSpeed2 * Time.deltaTime);
                }
            }
        }

        IEnumerator RevolvingDoorsTimer()
        {
            box.enabled = true;
            sp.color = Color.blue;
            yield return new WaitForSeconds(.5f);
            sa.Play();
            yield return new WaitForSeconds(5f);
            fadeAway = true;
            yield return new WaitForSeconds(2f);
            sa.Pause();
            fadeAway = false;
            this.gameObject.SetActive(false);
        }

        public void SeparateDoors()
        {
            StartCoroutine(SeparateDoorsTimer());
        }

        IEnumerator SeparateDoorsTimer()
        {
            sa.Play();
            yield return new WaitForSeconds(1f);
            sa.Pause();
            this.gameObject.SetActive(false);
        }

        IEnumerator DoorsSeparating()
        {
            yield return new WaitForSeconds(1f);
            canStartNormalRoutine = true;
        }
    }
}