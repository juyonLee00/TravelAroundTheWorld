using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌이 발생하면 플레이어 움직임을 멈춤
        playerController.ColliderStart();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 충돌이 끝나면 플레이어 움직임을 재개
        playerController.ColliderEnd();
    }

}
