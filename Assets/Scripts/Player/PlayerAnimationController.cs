using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    private Vector2 lastInputVector;

    private IPlayerState currentState;
    public readonly IPlayerState IdleState = new PlayerIdleState();
    public readonly IPlayerState WalkingState = new PlayerWalkingState();
    public readonly IPlayerState FallingAsleepState = new PlayerFallingAsleepState();
    
    public readonly PlayerController playerController;

    public Vector2 InputVector { get; private set; }

    private void Start()
    {
        //TransitionToState(IdleState);
    }

    private void Update()
    {
        //currentState.UpdateState(this);
    }

    public void SetMoveDirection(Vector2 inputVector)
    {
        InputVector = inputVector;

        if (InputVector != Vector2.zero)
        {
            lastInputVector = InputVector;
        }

        Vector2 directionToAnimate = InputVector != Vector2.zero ? InputVector : lastInputVector;

        animator.SetFloat("xDir", directionToAnimate.x);
        animator.SetFloat("yDir", directionToAnimate.y);
        animator.SetBool("isMove", InputVector != Vector2.zero);
        /*
         * if(directionToAnimate == Vector2.zero)
         *      SoundManager.Instance.PlaySFX("WalkSound");
         */
    }

    public void TransitionToState(IPlayerState newState)
    {
        currentState?.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }

}