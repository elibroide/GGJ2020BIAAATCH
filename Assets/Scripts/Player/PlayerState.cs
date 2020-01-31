using UnityEngine;

namespace Player
{
    public abstract class PlayerState : MonoBehaviour
    {
        public abstract void EnterState(PlayerState previousState);
        public abstract void LeaveState(PlayerState newState);
        public abstract void Tick();
    }
}