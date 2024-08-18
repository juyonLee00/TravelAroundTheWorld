using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diary : MonoBehaviour
{
    private GameObject player;
    private PlayerAnimationController playerAnimationController;
    
    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        playerAnimationController = player.GetComponent<PlayerAnimationController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            UIManager.Instance.ToggleUI("Diary");
            player.GetComponent<PlayerController>().StopMove();

            playerAnimationController.StopAllCoroutines();
            playerAnimationController.SetMoveDirection(Vector2.zero);
        }
    }
}
