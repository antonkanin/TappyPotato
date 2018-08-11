using UnityEngine;

namespace DefaultNamespace
{
    public static class PotatoState
    {
        public static readonly int IsAliveId = Animator.StringToHash("isAlive");
        public static readonly int PausedId = Animator.StringToHash("paused");
        public static readonly int IsDiveId = Animator.StringToHash("isDive");
    }
}