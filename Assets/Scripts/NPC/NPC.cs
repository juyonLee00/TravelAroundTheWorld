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
        dialogueManager = DialogueManager.instance;
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
        dialogueManager.StartDialogue(npcID);
    }
}