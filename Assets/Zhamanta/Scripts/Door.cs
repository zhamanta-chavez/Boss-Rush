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

        [SerializeField] float fadeSpeed = 1;
        [SerializeField] float fadeSpeed2 = 1;
        private bool fadeIn;
        private bool fadeAway;
        private bool canStartCoroutine;

        void OnEnable()
        {
            fadeIn = true;
            fadeAway = false;
            canStartCoroutine = false;
            sp.color = new Color(0, 0, 1, 0);
        }
        void Update()
        {
            if (fadeIn)
            {
                sp.color = Color.Lerp(new Color(0, 0, 1, 0), Color.blue, fadeSpeed * Time.deltaTime);
            }
            if (sp.color == Color.blue && canStartCoroutine)
            {
                fadeIn = false;
                canStartCoroutine = false;
                StartCoroutine(RevolvingDoorsTimer());
            }

            if (fadeAway)
            {
                sp.color = Color.Lerp(Color.blue, new Color(0, 0, 1, 0), fadeSpeed2 * Time.deltaTime);
            }
        }

        IEnumerator RevolvingDoorsTimer()
        {
            sa.Play();
            yield return new WaitForSeconds(5f);
            fadeAway = true;
            yield return new WaitForSeconds(2f);
            sa.Pause();
            fadeAway = false;
            this.gameObject.SetActive(false);
        }
    }
}