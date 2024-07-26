using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkingState : IPlayerState
{
    public void EnterState(PlayerAnimationController playerAnimationController)
    {
        playerAnimationController.animator.SetFloat("Speed", 5);
    }

    public void UpdateState(PlayerAnimationController playerAnimationController)
    {
        if(playerAnimationController.InputVector.sqrMagnitude == 0)
        {
            playerAnimationController.TransitionToState(playerAnimationController.IdleState);
        }
    }

    public void ExitState(PlayerAnimationController playerAnimationController)
    {

    }
}
