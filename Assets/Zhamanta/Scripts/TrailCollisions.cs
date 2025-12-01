using UnityEngine;
using System.Collections.Generic;
using UnityEngine;

namespace Zhamanta
{
    public class TrailCollisions : MonoBehaviour
    {
        public TrailRenderer trailRenderer;
        public float detectionRange = 5.0f;
        public float damagePerSecond = 20.0f;

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
                    //player has been hit, we can do damage to it
                    Debug.Log("Player detected");
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