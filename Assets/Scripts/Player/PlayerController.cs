using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 6.0f;
    Vector2 inputVector;
    Rigidbody2D rigid;

    public Camera mainCamera;

    //??? ??
    private UIManager uiManager;
    public PlayerAnimationController playerAnimationController;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        uiManager = FindObjectOfType<UIManager>();
        playerAnimationController = gameObject.GetComponent<PlayerAnimationController>();
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        if (!uiManager.IsUIActive())
        {
            Move();
        }

    }

    void OnMove(InputValue inputValue)
    {
        inputVector = inputValue.Get<Vector2>();
        playerAnimationController.SetMoveDirection(inputVector);
    }

    void Move()
    {
        if (inputVector != Vector2.zero)
        {
            Vector2 moveVector = inputVector.normalized * speed * Time.deltaTime;
            rigid.MovePosition(rigid.position + moveVector);
        }
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
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);
        worldPos.z = 0; // 2D 게임의 경우 Z 값을 0으로 설정

        playerAnimationController.MoveToPosition(worldPos);
    }
}
