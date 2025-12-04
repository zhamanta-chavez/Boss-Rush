using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.Events;

namespace Zhamanta
{
    public class TrailCollisions : MonoBehaviour
    {
        [SerializeField] int damageAmount;
        [SerializeField] float knockbackForce = 1;
        [SerializeField] GameObject hitEffectPrefab;
        [SerializeField] AudioClipCollection hitSounds;

        public UnityEvent OnContact;
        public UnityEvent OnSuccessfulHit;

        public TrailRenderer trailRenderer;
        public float detectionRange = 5.0f;
        public float damagePerSecond = 20.0f;

        private void Start()
        {
            this.gameObject.SetActive(false);
        }

        public void SetDamageAmount(int amount)
        {
            damageAmount = amount;
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < trailRenderer.positionCount; i++)
            {
                if (i == trailRenderer.positionCount - 1)
                    continue;

                float t = i / (float)trailRenderer.positionCount;

                //get the approximate width of the line segment
                float width = trailRenderer.widthCurve.Evaluate(t);

                Vector3 startPosition = trailRenderer.GetPosition(i);
                Vector3 endPosition = trailRenderer.GetPosition(i + 1);
                Vector3 direction = endPosition - startPosition;
                float distance = Vector3.Distance(endPosition, startPosition);

                RaycastHit hit;

                if (Physics.SphereCast(startPosition, width, direction, out hit, distance, LayerMask.GetMask("Player")))
                {
                    if (hit.transform.gameObject.GetComponent<Damageable>())
                    {
                        Vector3 dir = hit.transform.position - transform.position;
                        dir.Normalize();

                        Damage damage = new Damage();
                        damage.amount = damageAmount;
                        damage.direction = dir;
                        damage.knockbackForce = knockbackForce;

                        if (hit.transform.gameObject.GetComponent<Damageable>().Hit(damage))
                        {
                            OnSuccessfulHit?.Invoke();

                            if (hitEffectPrefab != null)
                            {
                                Instantiate(hitEffectPrefab, hit.transform.position, Quaternion.identity);
                            }

                            if (hitSounds != null)
                                SoundEffectsManager.instance.PlayRandomClip(hitSounds.clips, true);
                        }
                    }

                    return;
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detectionRange);
        }
    }
}