using UnityEngine;

namespace MuziCharacter
{
    public class ChangeClothAnim : MonoBehaviour, IChangeWearableEffect
    {
        private Animator animator;
        private int animIdx;
        private void Awake()
        {
            animator = GetComponent<Animator>();
            animIdx = Animator.StringToHash("ChangeCloth");
        }

        public void DoEffect()
        {
            if (animator != null)
            {
                animator.SetTrigger(animIdx);
            }
        }
    }
}