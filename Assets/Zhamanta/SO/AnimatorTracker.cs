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
        private bool stage3 = false;
        private bool onStage3 = false;
        private bool trailOn = false;
        private bool fromAttack1 = false;
        private int index = 0;

        public void IncreaseIndex()
        {
            index++;

            if (index >= 2)
            {
                index = 0;
            }
        }

        public int GetAttackIndex()
        {
            return index;
        }

        public void IncreaseShootCount()
        {
            shootCount += 1;
        }

        public int GetShootCount()
        {
            return shootCount;
        }

        public void ResetShootCount()
        {
            shootCount = 0;
        }

        public void SetFromAttack1(bool fromAttack1State)
        {
            fromAttack1 = fromAttack1State;
        }

        public bool GetFromAttack1()
        {
            return fromAttack1;
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
            SetFromAttack1(false);
        }

        public bool JustEnteredStage2()
        {
            return stage2;
        }

        public bool OnStage2()
        {
            return onStage2;
        }

        public void ActivateStage3()
        {
            stage3 = true;
            onStage3 = true;
        }

        public void FinishActivatingStage3()
        {
            stage3 = false;
            SetFromAttack1(false);
            Debug.Log("Activated Stage 3");
        }

        public bool JustEnteredStage3()
        {
            return stage3;
        }

        public bool OnStage3()
        {
            return onStage3;
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
            stage3 = false;
            onStage3 = false;
            trailOn = false;
            fromAttack1 = false;
        }
    }
}