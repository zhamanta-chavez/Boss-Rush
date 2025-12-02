using UnityEngine;

namespace Zhamanta
{
    [CreateAssetMenu(menuName = "Animator Tracker")]
    public class AnimatorTracker : ScriptableObject
    {
        [SerializeField] private int shootCount = 0;
        [SerializeField] private int attackCount = 0;

        private bool stage2 = false;

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

        public void SwicthStage2()
        {
            stage2 = !stage2;
            Debug.Log(stage2);
        }

        public bool JustEnteredStage2()
        {
            return stage2;
        }

        public void ResetValues()
        {
            shootCount = 0;
            attackCount = 0;
            stage2 = false;
        }
    }
}