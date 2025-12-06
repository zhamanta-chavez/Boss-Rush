using UnityEngine;

namespace Zhamanta
{
    public class Sounds : MonoBehaviour
    {
        [SerializeField] AnimatorTracker animTracker;

        [SerializeField] AudioSource audio;
        [SerializeField] AudioClip[] audioClips;

        private void Start()
        {
            IntroLaugh();
        }

        private void Update()
        {
            if (animTracker.GetLaserSoundOn())
            {
                animTracker.SetLaserOn(false);
                LaserSound();
            }
        }

        public void ChangeToSadMusic()
        {
            audio.resource = audioClips[0];
            audio.Play();
        }

        public void LaserSound()
        {
            audio.PlayOneShot(audioClips[1]);
        }

        public void Warning()
        {
            audio.PlayOneShot(audioClips[2]);
        }

        public void Electric()
        {
            audio.PlayOneShot(audioClips[5]);
        }

        public void DoorsOn()
        {
            audio.PlayOneShot(audioClips[6]);
        }

        public void IntroLaugh()
        {
            audio.PlayOneShot(audioClips[4]);
        }

        public void TransitionLaugh()
        {
            audio.PlayOneShot(audioClips[3]);
        }
    }
}