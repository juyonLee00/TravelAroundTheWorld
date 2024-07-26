using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //animationController? speed ?? ????
    public float speed = 6.0f;
    Vector2 inputVector;
    Rigidbody2D rigid;

    //??? ??
    private UIManager uiManager;
    public PlayerAnimationController playerAnimationController;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        uiManager = FindObjectOfType<UIManager>();
        playerAnimationController = FindObjectOfType<PlayerAnimationController>();
    }

    void Update()
    {
        Move();
        
    }

    void OnMove(InputValue inputValue)
    {
        if (uiManager.IsUIActive())
        {
            //animationController MoveSpeed = 0;?? ??
            return;
        }
        inputVector = inputValue.Get<Vector2>();

        
    }

    void Move()
    {
        Vector2 moveVector = inputVector.normalized * speed * Time.deltaTime;
        rigid.MovePosition(rigid.position + moveVector);

        playerAnimationController.SetMoveDirection(inputVector);
    }

    void OnInventory()
    {
        uiManager.ToggleUI("Inventory");
    }

    void OnSetting()
    {
        uiManager.ToggleUI("Setting");
    }

    void OnMap()
    {
        uiManager.ToggleUI("Map");
    }

    void OnSkipDialogue()
    {
       /*
        *  ?? ?? ?? ???? ?? ?? ??
        */
    }

    void OnMouseMove()
    {
        /*
         * UI???? ?? ?? map ??? ?? ?? ??? ??
?        */
    }
}
