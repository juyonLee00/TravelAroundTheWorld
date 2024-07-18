using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 6.0f;
    Vector2 inputVector;
    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        
    }

    void OnMove(InputValue inputValue)
    {
        inputVector = inputValue.Get<Vector2>();
    }

    void Move()
    {
        Vector2 moveVector = inputVector.normalized * speed * Time.deltaTime;
        rigid.MovePosition(rigid.position + moveVector);
    }

    void OnInventory()
    {
        /*
         * ????UI ?? ? ??? 
         * 
         * GameObject inventoryUI = UIManager.Instantiate("inventoryUI");
         * 
         * if(inventoryUI != null)
         *      inventoryUI.SetActive(true);
         */
    }

    void OnSetting()
    {
        /*
         * SettingUI ?? ? ??? (Inventory? ??)
         */
    }

    void OnMap()
    {
        /*
         * MapUI ?? ? ??? 
         */
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
         */
    }
}
