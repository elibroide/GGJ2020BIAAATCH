namespace Player
{
    public interface IPlayerState
    {
        void EnterState(IPlayerState previousState);
        void LeaveState(IPlayerState newState);
        void Tick();
    }
}