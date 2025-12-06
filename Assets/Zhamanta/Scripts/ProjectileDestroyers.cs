using UnityEngine;

namespace Zhamanta
{
    public class ProjectileDestroyers : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Something Entered");
            if (other.GetComponent<Projectile>())
            {
                Debug.Log("It's projectile");
                other.GetComponent<Projectile>().DestroyProjectile();   
            }
        }
    }
}