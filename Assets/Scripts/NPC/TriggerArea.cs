using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    public GameObject interactionButton; // 상호작용 버튼
    public string locationName; // npc가 있는 장소
    public Transform playerTransform; // 플레이어의 Transform
    public float interactionDistance = 0.3f; // 상호작용 거리

    private void Start()
    {
        interactionButton.SetActive(false); // 처음엔 버튼 비활성화
    }

    private void Update()
    {
        // 플레이어와 NPC 사이 거리를 계산
        float distance = Vector3.Distance(playerTransform.position, transform.position);

        // 거리가 설정된 거리 이내인지 확인
        if (distance <= interactionDistance)
        {
            interactionButton.SetActive(true); // 버튼 활성화
            Managers.Instance.CurrentLocation = locationName; // 현재 위치 설정
        }
        else
        {
            Managers.Instance.CurrentLocation = null; // 버튼 비활성화
        }
    }
}

