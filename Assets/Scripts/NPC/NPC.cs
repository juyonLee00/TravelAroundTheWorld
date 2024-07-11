using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public string npcID;
    public float interactionDistance = 3.0f; // 플레이어와 상호작용할 거리

    private GameObject player; // 임시
    private DialogueManager dialogueManager;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        dialogueManager = DialogueManager.Instance;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < interactionDistance)
        {
            StartDialogue();
        }
    }

    void StartDialogue()
    {
        // NPC 대사
        Managers.Instance.Dialogue(npcID, true);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("플레이어와 충돌!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("플레이어와 충돌!");
        }
    }
}
