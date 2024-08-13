using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ch1NpcScript : MonoBehaviour
{
    public GameObject dialogueButton; // "대화하기" 버튼
    public Ch1TalkManager talkManager; // Ch1TalkManager 참조
    public Transform player; // 플레이어의 위치

    public float interactionRange = 5f; // NPC와 상호작용 가능한 범위

    private bool isPlayerInRange = false; // 플레이어가 범위 내에 있는지 확인

    void Start()
    {
        // 대화 버튼을 처음에는 비활성화
        dialogueButton.SetActive(false);
    }

    void Update()
    {
        // 플레이어가 NPC 근처에 있는지 체크
        isPlayerInRange = Vector3.Distance(player.position, transform.position) <= interactionRange;

        // 플레이어가 범위 내에 있고 currentDialogueIndex가 36이면 버튼 활성화
        if (isPlayerInRange && talkManager.currentDialogueIndex == 36)
        {
            dialogueButton.SetActive(true);
        }
        else
        {
            dialogueButton.SetActive(false);
        }
    }

    // "대화하기" 버튼을 눌렀을 때 호출되는 함수
    public void OnDialogueButtonClicked()
    {
        if (talkManager.currentDialogueIndex == 36)
        {
            talkManager.map.SetActive(false);
            talkManager.player.SetActive(false);
            talkManager.Npc_Violet.SetActive(false);
            talkManager.cafe.SetActive(true);
            talkManager.dialogue.SetActive(true);
            talkManager.isWaitingForPlayer = false;
            talkManager.currentDialogueIndex++;
            talkManager.PrintCh1ProDialogue(talkManager.currentDialogueIndex);
        }
    }
}
