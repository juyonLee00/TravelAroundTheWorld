using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ch0InteractionButton : MonoBehaviour
{
    private Button button; // 버튼 컴포넌트
    public TalkManager talkManager; // 토크 매니저 스크립트
    private string location; // 상호작용할 위치
    private Ch0TriggerArea triggerArea; // Ch0TriggerArea 컴포넌트
    public PlayerController playerController;

    private void Start()
    {
        button = GetComponent<Button>(); // 버튼 컴포넌트를 가져옴
        button.onClick.AddListener(OnInteract); // 버튼 클릭 시 OnInteract 메서드 호출
        gameObject.SetActive(false); // 시작할 때 버튼 비활성화

        // 현재 버튼 오브젝트의 상위 오브젝트들 중에서 Ch0TriggerArea를 찾음
        triggerArea = GetComponentInParent<Ch0TriggerArea>();
    }

    // 위치 설정 메서드
    public void SetLocation(string locationName)
    {
        location = locationName;
    }

    // 버튼 클릭 시 호출되는 메서드
    public void OnInteract()
    {
        if (talkManager != null)
        {
            playerController.StopMove(); //대화 버튼 클릭할 때 플레이어 움직임 멈춤
            talkManager.ActivateTalk(location); // 토크매니저에 npc 위치 정보 전달

            //대화 활성화 후 버튼 비활성화
            gameObject.SetActive(false); // 버튼 비활성화
            // 버튼 비활성화 상태를 TriggerArea에 전달
            if (triggerArea != null)
            {
                triggerArea.DisableButton(); // 버튼 비활성화 호출
            }
        }
    }
}

