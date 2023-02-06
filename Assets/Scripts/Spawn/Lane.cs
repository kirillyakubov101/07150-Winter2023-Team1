using UnityEngine;

namespace OurGame.Spawn
{
    public class Lane : MonoBehaviour
    {
        [SerializeField] private Animator m_animator;

        readonly int animateLaneHash = Animator.StringToHash("Show");

        public void AnimateLane()
        {
            m_animator.SetTrigger(animateLaneHash);
        }

    } 

}
