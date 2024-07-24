using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 6.0f;
    Vector2 inputVector;
    Rigidbody2D rigid;

    public GameObject inventoryUI;
    private GameObject inventory;
    private Canvas canvas;
    private bool isActivatedInventoryUI = false;
    private int inventoryCount = 0;

    private bool isActivatedMapUI = false;
    private int mapCount = 0;

    private UIManager uiManager;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        uiManager = FindObjectOfType<UIManager>();
    }

    void Update()
    {
        Move();
        
    }

    void OnMove(InputValue inputValue)
    {
        if (uiManager.IsUIActive())
            return;
        inputVector = inputValue.Get<Vector2>();
    }

    void Move()
    {
        Vector2 moveVector = inputVector.normalized * speed * Time.deltaTime;
        rigid.MovePosition(rigid.position + moveVector);
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
