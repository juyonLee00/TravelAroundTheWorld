using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Opening : MonoBehaviour
{
    public TalkManager talkManager; // TalkManager 오브젝트

    private List<string> dialogues; // 대사들을 저장할 리스트
    public TextMeshProUGUI openingText; // TextMeshPro UI 텍스트 요소

    public GameObject invitation; // 비밀 초대장 오브젝트
    public TextMeshProUGUI invitationText; // TextMeshPro UI 텍스트 요소

    private int currentDialogueIndex = 0; // 현재 대사 인덱스

    void Start()
    {
        dialogues = new List<string>();
        GenerateDialogue(); // 대사 생성 함수 호출
        PrintDialogue(); // 첫 번째 대사 출력
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PrintDialogue(); // 스페이스바를 눌렀을 때 다음 대사 출력
        }
    }

    void GenerateDialogue()
    {
        dialogues.Add("모든 것은 이 비밀스러운 초대장으로부터 시작되었다.");
        dialogues.Add("때는 꽃 내음이 물씬 나고 여린 초록 이파리가 차츰 나기 시작하는 새벽이었다.");
    }

    void PrintDialogue()
    {
        if (currentDialogueIndex < dialogues.Count)
        {
            openingText.text = dialogues[currentDialogueIndex];
            currentDialogueIndex++;
        }
        else
        {
            this.gameObject.SetActive(false); // Opening 오브젝트 비활성화
            talkManager.ActivateTalk();
            return;
        }
    }
}
