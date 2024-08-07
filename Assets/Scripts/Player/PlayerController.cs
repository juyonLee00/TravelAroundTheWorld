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

    public PlayerAnimationController playerAnimationController;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerAnimationController = gameObject.GetComponent<PlayerAnimationController>();
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        if (!UIManager.Instance.IsUIActive())
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
        UIManager.Instance.ToggleUI("Inventory");
    }

    void OnSetting()
    {
        UIManager.Instance.ToggleUI("Setting");
    }

    void OnMap()
    {
        UIManager.Instance.ToggleUI("Map");
    }

    void OnSkipDialogue()
    {
    }

    void OnMouseMove()
    {
        if (!UIManager.Instance.IsUIActive())
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);
            worldPos.z = 0;

            playerAnimationController.MoveToPosition(worldPos);
        }
    }
}
