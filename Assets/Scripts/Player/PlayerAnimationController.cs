using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    public float moveSpeed = 5f;

    private IPlayerState currentState;
    public readonly IPlayerState IdleState = new PlayerIdleState();
    public readonly IPlayerState WalkingState = new PlayerWalkingState();
    public readonly IPlayerState FallingAsleepState = new PlayerFallingAsleepState();
    
    public readonly PlayerController playerController;

    public Vector2 InputVector { get; private set; }

    private void Start()
    {
        TransitionToState(IdleState);   
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SetMoveDirection(Vector2 inputVector)
    {
        InputVector = inputVector;

        if(InputVector.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        else if(InputVector.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        animator.SetFloat("MoveX", InputVector.x);
        animator.SetFloat("MoveY", InputVector.y);
        animator.SetBool("IsMoving", InputVector != Vector2.zero);
    }

    public void TransitionToState(IPlayerState newState)
    {
        currentState?.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }

}