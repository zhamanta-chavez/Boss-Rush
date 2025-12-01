using UnityEngine;

namespace Zhamanta
{
    [CreateAssetMenu(menuName = "Animator Tracker")]
    public class AnimatorTracker : ScriptableObject
    {
        [SerializeField] private int shootCount = 0;
        [SerializeField] private int attackCount = 0;

        public void IncreaseShootCount()
        {
            shootCount += 1;
        }

        public int GetShootCount()
        {
            return shootCount;
        }

        public void IncreaseAttackCount()
        {
            attackCount += 1;  
        }

        public int GetAttackCount()
        {
            return attackCount;
        }
    }
}