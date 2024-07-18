using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bed : MonoBehaviour
{
    //UI-UIManager, controller-InputManager에서 가져오도록 수정
    [SerializeField] GameObject popupUI;
    [SerializeField] PlayerController playerController;

    bool isActivePopupUI = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer.Equals(7))
        {   
            if (isActivePopupUI == false)
            {
                popupUI.SetActive(true);
                isActivePopupUI = true;
                playerController.enabled = false ;
            }

        }
    }

}
