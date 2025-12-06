using UnityEngine;

namespace Zhamanta
{
    public class EyebatChildren : MonoBehaviour
    {
        [SerializeField] AnimatorTracker animTracker;
        [SerializeField] Transform batdad;
        [SerializeField] float rescueSpeed = 1.0f;
        [SerializeField] Rigidbody rb;
        Vector3 originalPos;

        private void Start()
        {
            originalPos = rb.position;
        }
        private void Update()
        {
            if (animTracker.GetHealerState() == true)
            {
                Vector3 target = new Vector3(batdad.position.x, batdad.position.y - 1, batdad.position.z);
                Vector3 newPos = Vector3.MoveTowards(rb.position, target, rescueSpeed * Time.fixedDeltaTime);
                rb.MovePosition(newPos);
            }
            else
            {
                Vector3 newPos = Vector3.MoveTowards(rb.position, originalPos, rescueSpeed * Time.fixedDeltaTime);
                rb.MovePosition(newPos);
            }
        }
    }
}