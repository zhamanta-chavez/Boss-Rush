using UnityEngine;
using Cinemachine;
using System.Collections;

namespace Zhamanta
{
    public class CinemachineSwitcher : MonoBehaviour
    {
        [SerializeField] CinemachineVirtualCamera eyebatCam;
        [SerializeField] CinemachineVirtualCamera playerCam;

        [SerializeField] float introSwitchTime = 2.5f;
        [SerializeField] float disableEyebatCameraTime = 1f;

        private void Start()
        {
            eyebatCam.Priority = 1;
            playerCam.Priority = 0;
            StartCoroutine(IntroSwitchTimer());
        }

        private void IntroSwitch()
        {
            playerCam.Priority = 1;
            eyebatCam.Priority = 0;
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