using UnityEngine;

namespace Zhamanta
{
    public class EyebatChildren : MonoBehaviour
    {
        [SerializeField] AnimatorTracker animTracker;
        [SerializeField] Transform batdad;
        [SerializeField] float rescueSpeed = 1.0f;
        [SerializeField] float retrieveSpeed = 10.0f;
        [SerializeField] float retreatSpeed = 10.0f;
        [SerializeField] Rigidbody rb;
        [SerializeField] Rigidbody eyebatRb;
        [SerializeField] Transform finalPosition;
        Vector3 originalPos;
        bool canRetreat;

        private void Start()
        {
            originalPos = rb.position;
            canRetreat = false;
        }
        private void Update()
        {
            if (animTracker.GetHealerState() == true)
            {
                Vector3 target = new Vector3(batdad.position.x, batdad.position.y - 1, batdad.position.z);
                Vector3 newPos = Vector3.MoveTowards(rb.position, target, rescueSpeed * Time.fixedDeltaTime);
                rb.MovePosition(newPos);
            }
            else if (animTracker.GetHealerState() == false && !canRetreat)
            {
                Vector3 newPos = Vector3.MoveTowards(rb.position, originalPos, rescueSpeed * Time.fixedDeltaTime);
                rb.MovePosition(newPos);
            }

            if (animTracker.GetFetchEyebat() == true)
            {
                Vector3 target = new Vector3(batdad.position.x, batdad.position.y - 1, batdad.position.z);
                Vector3 newPos = Vector3.MoveTowards(rb.position, target, retrieveSpeed * Time.fixedDeltaTime);
                rb.MovePosition(newPos);
                canRetreat = true;
                Debug.Log("SHOULD GET CLOSER");
            }
            else if (animTracker.GetFetchEyebat() == false && canRetreat)
            {
                Vector3 newPos = Vector3.MoveTowards(rb.position, finalPosition.position, retreatSpeed * Time.fixedDeltaTime);
                rb.MovePosition(newPos);

                eyebatRb.transform.position = rb.position;
            }
        }
    }
}