public interface IPlayerState
{
    void EnterState(PlayerAnimationController playerAnimationController);
    void UpdateState(PlayerAnimationController playerAnimationController);
    void ExitState(PlayerAnimationController playerAnimationController);
}
