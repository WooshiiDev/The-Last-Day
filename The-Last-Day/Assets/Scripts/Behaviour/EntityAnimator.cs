using UnityEngine;

namespace LastDay
{
    public class EntityAnimator : MonoBehaviour
    {
        public Animator Anim { get; private set;}

        public void Awake()
        {
            Anim = this.GetComponentInChildren<Animator>();
        }

        public void SetAnimationBool(string animationName,bool value)
        {
            Anim.SetBool(animationName, value);
        }
    }
}