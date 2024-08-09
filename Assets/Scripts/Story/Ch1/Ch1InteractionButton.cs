using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ch1InteractionButton : MonoBehaviour
{
    private Button button; // 버튼 컴포넌트
    public Ch1TalkManager ch1TalkManager; // 토크 매니저 스크립트
    private string location; // 상호작용할 위치
    private Ch1TriggerArea ch1TriggerArea; // Ch1TriggerArea 컴포넌트
    public PlayerController playerController;

    private void Start()
    {
        button = GetComponent<Button>(); // 버튼 컴포넌트를 가져옴
        button.onClick.AddListener(OnInteract); // 버튼 클릭 시 OnInteract 메서드 호출
        gameObject.SetActive(false); // 시작할 때 버튼 비활성화

        // 현재 버튼 오브젝트의 상위 오브젝트들 중에서 Ch1TriggerArea를 찾음
        ch1TriggerArea = GetComponentInParent<Ch1TriggerArea>(); // 클래스 이름 대문자 C로 수정
    }

    // 위치 설정 메서드
    public void SetLocation(string locationName)
    {
        location = locationName;
    }

    public void OnInteract()
    {
        if (ch1TalkManager != null)
        {
            playerController.StopMove(); // 대화 버튼 클릭할 때 플레이어 움직임 멈춤

            // 대화 활성화 후 버튼 비활성화
            gameObject.SetActive(false); // 버튼 비활성화
                                         // 버튼 비활성화 상태를 TriggerArea에 전달
            if (ch1TriggerArea != null)
            {
                ch1TriggerArea.DisableButton(); // 버튼 비활성화 호출
            }

            // Ch1TalkManager의 메서드를 호출하여 대화 진행
            ch1TalkManager.OnDialogueButtonClicked(33); // 33번 인덱스부터 대화 시작
        }
    }
}
