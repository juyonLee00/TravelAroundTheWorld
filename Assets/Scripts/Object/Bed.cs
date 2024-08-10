
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bed : MonoBehaviour
{
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            UIManager.Instance.ToggleUI("Bed");
            player.GetComponent<PlayerController>().StopMove();
        }
    }

}
