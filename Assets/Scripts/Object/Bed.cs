using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bed : MonoBehaviour
{
    private GameObject player;
    
    bool isActiveUI = false;
    private float interactionDistance;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Start()
    {
        interactionDistance = 2f;
    }

    private void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance == interactionDistance)
        {
            Debug.Log("II(");
        }
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            UIManager.Instance.ToggleUI("Bed");
            player.GetComponent<PlayerController>().StopMove();
            }
    }
}
