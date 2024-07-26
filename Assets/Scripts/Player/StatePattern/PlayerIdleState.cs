using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IPlayerState
{
    public void EnterState(PlayerAnimationController playerAnimationController)
    {
        playerAnimationController.animator.SetFloat("Speed", 0);
    }

    public void UpdateState(PlayerAnimationController playerAnimationController)
    {
        //이동할 때
        if(playerAnimationController.InputVector.sqrMagnitude > 0)
        {
            playerAnimationController.TransitionToState(playerAnimationController.WalkingState);
        }

        //잠에 드시겠습니까? 에서 예를 선택한 상태
        //if(다음날로 넘어가는 플래그 == true)
        {
            playerAnimationController.TransitionToState(playerAnimationController.FallingAsleepState);
        }
        
        
    }

    public void ExitState(PlayerAnimationController playerAnimationController)
    {

    }
}
