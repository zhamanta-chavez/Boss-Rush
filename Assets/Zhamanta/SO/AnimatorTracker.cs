using UnityEngine;

namespace Zhamanta
{
    [CreateAssetMenu(menuName = "Animator Tracker")]
    public class AnimatorTracker : ScriptableObject
    {
        [SerializeField] private int shootCount = 0;
        [SerializeField] private int attackCount = 0;

        private bool stage2 = false;
        private bool onStage2 = false;
        private bool trailOn = false;

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

        public void ActivateStage2()
        {
            stage2 = true;
            onStage2 = true;
        }

        public void FinishActivatingStage2()
        {
            stage2 = false;
        }

        public bool JustEnteredStage2()
        {
            return stage2;
        }

        public bool OnStage2()
        {
            return onStage2;
        }

        public void TrailState(bool trailState)
        {
            trailOn = trailState;
        }

        public bool GetTrailState()
        {
            return trailOn;
        }

        public void ResetValues()
        {
            shootCount = 0;
            attackCount = 0;
            stage2 = false;
            onStage2 = false;
            trailOn = false;
        }
    }
}