using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingAsleepState : IPlayerState
{
    public void EnterState(PlayerAnimationController playerAnimationController)
    {
        //잠에 드는 상태의 애니메이션 재생
    }

    public void UpdateState(PlayerAnimationController playerAnimationController)
    {
        //다음날로 이동했을 때, 기본 상태로 연결
        //if(isBrightDay == true)
         {
            playerAnimationController.TransitionToState(playerAnimationController.IdleState);
         }
        

    }

    public void ExitState(PlayerAnimationController playerAnimationController)
    {

    }
}
