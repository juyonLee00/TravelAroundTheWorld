using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ch1NpcScript : MonoBehaviour
{
    public GameObject dialogueButton; // "대화하기" 버튼
    //public Ch1TalkManager talkManager; // Ch1TalkManager 참조
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
        // NPC가 활성화되어 있을 때만 상호작용 가능 여부를 확인
        if (!gameObject.activeInHierarchy)
        {
            dialogueButton.SetActive(false);
            return;
        }

        // 플레이어가 NPC 근처에 있는지 체크
        isPlayerInRange = Vector3.Distance(player.position, transform.position) <= interactionRange;

        // currentDialogueIndex가 33인 경우 정원 NPC와 상호작용
        /*if (isPlayerInRange && Ch1TalkManager.Instance.currentDialogueIndex == 33 && gameObject.name == "Npc_Rayviyak")
        {
            dialogueButton.SetActive(true);
        }
        // currentDialogueIndex가 37인 경우 바 NPC와 상호작용
        else if (isPlayerInRange && Ch1TalkManager.Instance.currentDialogueIndex == 37 && gameObject.name == "Npc_Violet")
        {
            dialogueButton.SetActive(true);
        }
        // currentDialogueIndex가 104인 경우 정원 NPC와 상호작용
        else if (isPlayerInRange && Ch1TalkManager.Instance.currentDialogueIndex == 104 && gameObject.name == "Npc_Rayviyak")
        {
            dialogueButton.SetActive(true);
        }
        // currentDialogueIndex가 187인 경우 정원 NPC와 상호작용
        else if (isPlayerInRange && Ch1TalkManager.Instance.currentDialogueIndex == 187 && gameObject.name == "Npc_Rayviyak")
        {
            dialogueButton.SetActive(true);
        }
        // currentDialogueIndex가 191인 경우 바 NPC와 상호작용
        else if (isPlayerInRange && Ch1TalkManager.Instance.currentDialogueIndex == 191 && gameObject.name == "Npc_Violet")
        {
            dialogueButton.SetActive(true);
        }
        // currentDialogueIndex가 207인 경우 빵집 NPC와 상호작용
        else if (isPlayerInRange && Ch1TalkManager.Instance.currentDialogueIndex == 207 && gameObject.name == "Npc_Rusk")
        {
            dialogueButton.SetActive(true);
        }
        // currentDialogueIndex가 318 경우 정원 NPC와 상호작용
        else if (isPlayerInRange && Ch1TalkManager.Instance.currentDialogueIndex == 318 && gameObject.name == "Npc_Rayviyak")
        {
            dialogueButton.SetActive(true);
        }
        // currentDialogueIndex가 322 경우 바 NPC와 상호작용
        else if (isPlayerInRange && Ch1TalkManager.Instance.currentDialogueIndex == 322 && gameObject.name == "Npc_Violet")
        {
            dialogueButton.SetActive(true);
        }
        // currentDialogueIndex가 326 경우 빵집 NPC와 상호작용
        else if (isPlayerInRange && Ch1TalkManager.Instance.currentDialogueIndex == 326 && gameObject.name == "Npc_Rusk")
        {
            dialogueButton.SetActive(true);
        }
        // currentDialogueIndex가 330 경우 의무실 NPC와 상호작용
        else if (isPlayerInRange && Ch1TalkManager.Instance.currentDialogueIndex == 330 && gameObject.name == "Npc_MrHam")
        {
            dialogueButton.SetActive(true);
        }
        else
        {
            dialogueButton.SetActive(false);
        }*/
    }


    public void EnterDialogueMode()
    {
        Ch1TalkManager.Instance.map.SetActive(false);
        Ch1TalkManager.Instance.player.SetActive(false);
        Ch1TalkManager.Instance.isWaitingForPlayer = false;
    }

    public void ChangeDayMode()
    {
        DayNightCycleManager.Instance.ChangeDay();
        PlayerManager.Instance.SetCurrentTimeofDay();
    }

    public void AdvanceToNextNode()
    {
        Ch1TalkManager.Instance.currentNode = Ch1TalkManager.Instance.currentNode.nextNodes[0];
        Ch1TalkManager.Instance.PrintNode(Ch1TalkManager.Instance.currentNode);
    }

    public void ChangedayInTrainRoom()
    {
        EnterDialogueMode();
        Ch1TalkManager.Instance.trainRoom.SetActive(true);
        ChangeDayMode();
    }

    public DialogueNode JumpToAnotherNode(int getNodeId)
    {
        if (Ch1TalkManager.Instance.nodeById != null && Ch1TalkManager.Instance.nodeById.TryGetValue(getNodeId, out var n))
        {
            Ch1TalkManager.Instance.currentNode = n;
            return n;
        }
        return null;
    }

    public void ResumeNodeAfterMovement()
    {
        Ch1TalkManager.Instance.dialogue.SetActive(true);
        EnterDialogueMode();
        AdvanceToNextNode();
    }

    public void SleepToNextDay()
    {
        Ch1TalkManager.Instance.trainRoom.SetActive(true);
        EnterDialogueMode();
        AdvanceToNextNode();
        ChangeDayMode();
    }

    public void SleepToNextDay(int getNodeId)
    {
        Ch1TalkManager.Instance.trainRoom.SetActive(true);
        EnterDialogueMode();
        AdvanceToNextNode();
        ChangeDayMode();
    }

    // "대화하기" 버튼을 눌렀을 때 호출되는 함수
    public void OnDialogueButtonClicked()
    {
        // currentDialogueIndex가 33인 경우 정원 NPC와 대화 진행
        if (Ch1TalkManager.Instance.currentNode.nodeId == 33 && gameObject.name == "Npc_Rayviyak")//(Ch1TalkManager.Instance.currentDialogueIndex == 33 && gameObject.name == "Npc_Rayviyak")
        {
            Ch1TalkManager.Instance.Npc_Rayviyak.SetActive(false);
            Ch1TalkManager.Instance.garden.SetActive(true);
            ResumeNodeAfterMovement();
            //Ch1TalkManager.Instance.currentDialogueIndex++;
            //Ch1TalkManager.Instance.PrintProDialogue(Ch1TalkManager.Instance.currentDialogueIndex);
        }
        // currentDialogueIndex가 37인 경우 바 NPC와 대화 진행
        else if (Ch1TalkManager.Instance.currentNode.nodeId == 37 && gameObject.name == "Npc_Violet")//(Ch1TalkManager.Instance.currentDialogueIndex == 37 && gameObject.name == "Npc_Violet")
        {
            Ch1TalkManager.Instance.Npc_Violet.SetActive(false);
            Ch1TalkManager.Instance.cafe.SetActive(true);
            ResumeNodeAfterMovement();
            //Ch1TalkManager.Instance.currentDialogueIndex++;
            //Ch1TalkManager.Instance.PrintProDialogue(Ch1TalkManager.Instance.currentDialogueIndex);
        }
        // currentDialogueIndex가 104인 경우 정원 NPC와 대화 진행
        else if (Ch1TalkManager.Instance.currentNode.nodeId == 104 && gameObject.name == "Npc_Rayviyak")//(Ch1TalkManager.Instance.currentDialogueIndex == 104 && gameObject.name == "Npc_Rayviyak")
        {
            Ch1TalkManager.Instance.Npc_Rayviyak.SetActive(false);
            Ch1TalkManager.Instance.garden.SetActive(true);
            ResumeNodeAfterMovement();
            //Ch1TalkManager.Instance.currentDialogueIndex++;
            //Ch1TalkManager.Instance.PrintProDialogue(Ch1TalkManager.Instance.currentDialogueIndex);
        }
        // currentDialogueIndex가 187인 경우 정원 NPC와 대화 진행
        else if (Ch1TalkManager.Instance.currentNode.nodeId == 187 && gameObject.name == "Npc_Rayviyak")//(Ch1TalkManager.Instance.currentDialogueIndex == 187 && gameObject.name == "Npc_Rayviyak")
        {
            Ch1TalkManager.Instance.Npc_Rayviyak.SetActive(false);
            Ch1TalkManager.Instance.garden.SetActive(true);
            ResumeNodeAfterMovement();
            //Ch1TalkManager.Instance.currentDialogueIndex++;
            //Ch1TalkManager.Instance.PrintProDialogue(Ch1TalkManager.Instance.currentDialogueIndex);
        }
        // currentDialogueIndex가 191 경우 바 NPC와 대화 진행
        else if (Ch1TalkManager.Instance.currentNode.nodeId == 191 && gameObject.name == "Npc_Violet")//(Ch1TalkManager.Instance.currentDialogueIndex == 191 && gameObject.name == "Npc_Violet")
        {
            Ch1TalkManager.Instance.Npc_Violet.SetActive(false);
            Ch1TalkManager.Instance.cafe.SetActive(true);
            ResumeNodeAfterMovement();
            //Ch1TalkManager.Instance.currentDialogueIndex++;
            //Ch1TalkManager.Instance.PrintProDialogue(Ch1TalkManager.Instance.currentDialogueIndex);
        }
        // currentDialogueIndex가 207 경우 빵집 NPC와 대화 진행
        else if (Ch1TalkManager.Instance.currentNode.nodeId == 207 && gameObject.name == "Npc_Rusk")//(Ch1TalkManager.Instance.currentDialogueIndex == 207 && gameObject.name == "Npc_Rusk")
        {
            Ch1TalkManager.Instance.Npc_Rusk.SetActive(false);
            Ch1TalkManager.Instance.bakery.SetActive(true);
            ResumeNodeAfterMovement();
            //Ch1TalkManager.Instance.currentDialogueIndex++;
            //Ch1TalkManager.Instance.PrintProDialogue(Ch1TalkManager.Instance.currentDialogueIndex);
        }
        // currentDialogueIndex가 318 경우 정원 NPC와 대화 진행
        else if (Ch1TalkManager.Instance.currentNode.nodeId == 318 && gameObject.name == "Npc_Rayviyak")//(Ch1TalkManager.Instance.currentDialogueIndex == 318 && gameObject.name == "Npc_Rayviyak")
        {
            Ch1TalkManager.Instance.Npc_Rayviyak.SetActive(false);
            Ch1TalkManager.Instance.garden.SetActive(true);
            ResumeNodeAfterMovement();
            //Ch1TalkManager.Instance.currentDialogueIndex++;
            //Ch1TalkManager.Instance.PrintProDialogue(Ch1TalkManager.Instance.currentDialogueIndex);
        }
        // currentDialogueIndex가 322 경우 바 NPC와 대화 진행
        else if (Ch1TalkManager.Instance.currentNode.nodeId == 322 && gameObject.name == "Npc_Violet")//(Ch1TalkManager.Instance.currentDialogueIndex == 322 && gameObject.name == "Npc_Violet")
        {
            Ch1TalkManager.Instance.Npc_Violet.SetActive(false);
            Ch1TalkManager.Instance.cafe.SetActive(true);
            ResumeNodeAfterMovement();
            //Ch1TalkManager.Instance.currentDialogueIndex++;
            //Ch1TalkManager.Instance.PrintProDialogue(Ch1TalkManager.Instance.currentDialogueIndex);
        }
        // currentDialogueIndex가 326 경우 빵집 NPC와 대화 진행
        else if (Ch1TalkManager.Instance.currentNode.nodeId == 326 && gameObject.name == "Npc_Rusk")//(Ch1TalkManager.Instance.currentDialogueIndex == 326 && gameObject.name == "Npc_Rusk")
        {
            Ch1TalkManager.Instance.Npc_Rusk.SetActive(false);
            Ch1TalkManager.Instance.bakery.SetActive(true);
            ResumeNodeAfterMovement();
            //Ch1TalkManager.Instance.currentDialogueIndex++;
            //Ch1TalkManager.Instance.PrintProDialogue(Ch1TalkManager.Instance.currentDialogueIndex);
        }
        // currentDialogueIndex가 330 경우 의무실 NPC와 대화 진행
        else if (Ch1TalkManager.Instance.currentNode.nodeId == 330 && gameObject.name == "Npc_MrHam")//(Ch1TalkManager.Instance.currentDialogueIndex == 330 && gameObject.name == "Npc_MrHam")
        {
            Ch1TalkManager.Instance.Npc_MrHam.SetActive(false);
            Ch1TalkManager.Instance.medicalRoom.SetActive(true);
            ResumeNodeAfterMovement();
            //Ch1TalkManager.Instance.currentDialogueIndex++;
            //Ch1TalkManager.Instance.PrintProDialogue(talkManager.currentDialogueIndex);
        }
    }
}
