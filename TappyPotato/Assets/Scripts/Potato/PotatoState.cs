using UnityEngine;

namespace DefaultNamespace
{
    public static class PotatoState
    {
        public static readonly int IsAliveId = Animator.StringToHash("isAlive");
        public static readonly int PausedId = Animator.StringToHash("paused");
        public static readonly int IsDiveId = Animator.StringToHash("isDive");
        public static readonly int DeathTypeId = Animator.StringToHash("deathType");
    }

    public static class DeathType
    {
        public static readonly int Bowell = 0;
        public static readonly int LooseEye = 1;
        public static readonly int Ground = 2;
    }
}