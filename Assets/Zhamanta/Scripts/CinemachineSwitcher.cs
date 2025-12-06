using UnityEngine;
using Cinemachine;
using System.Collections;

namespace Zhamanta
{
    public class CinemachineSwitcher : MonoBehaviour
    {
        [SerializeField] AnimatorTracker animTracker;

        [SerializeField] CinemachineVirtualCamera eyebatCam;
        [SerializeField] CinemachineVirtualCamera playerCam;
        [SerializeField] CinemachineVirtualCamera deathCamera;
        [SerializeField] CinemachineVirtualCamera babiesCamera;
        [SerializeField] CinemachineVirtualCamera helpCamera;

        [SerializeField] float introSwitchTime = 2.5f;
        [SerializeField] float disableEyebatCameraTime = 1f;

        private void Start()
        {
            eyebatCam.Priority = 1;
            playerCam.Priority = 0;
            deathCamera.Priority = 0;
            babiesCamera.Priority = 0;
            helpCamera.Priority = 0;
            StartCoroutine(IntroSwitchTimer());
            deathCamera.gameObject.SetActive(false);
            babiesCamera.gameObject.SetActive(false);
            helpCamera.gameObject.SetActive(false);
        }

        private void IntroSwitch()
        {
            playerCam.Priority = 1;
            eyebatCam.Priority = 0;
        }

        public void LookAtDeathSwitch()
        {
            deathCamera.gameObject.SetActive(true);
            playerCam.Priority = 0;
            deathCamera.Priority = 1;
        }

        public void LookAtBabiesComingSwitch()
        {
            babiesCamera.gameObject.SetActive(true);
            babiesCamera.Priority = 1;
            deathCamera.Priority= 0;
        }

        public void LookAtHelp()
        {
            helpCamera.gameObject.SetActive(true);
            helpCamera.Priority = 1;
            babiesCamera.Priority = 0;
        }

        public void LookAtRetreat()
        {
            babiesCamera.Priority = 1;
            helpCamera.Priority = 0;
        }

        IEnumerator IntroSwitchTimer()
        {
            yield return new WaitForSeconds(introSwitchTime);
            IntroSwitch();
            yield return new WaitForSeconds(disableEyebatCameraTime);
            eyebatCam.gameObject.SetActive(false);
        }


    }
}