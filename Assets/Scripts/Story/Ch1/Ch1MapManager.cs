using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ch1MapManager : MonoBehaviour
{
    public MapState currentState; // 맵의 현재 상태
    public Vector2 playerPosition; // 플레이어의 현재 위치
    public Transform playerTransform; // 플레이어의 Transform 참조
    private MapState previousState = MapState.Null; // 이전 상태를 저장하는 변수

    void Start()
    {
        currentState = MapState.Null; // 초기 상태 설정
        if (playerTransform != null)
        {
            playerPosition = playerTransform.position; // 초기 플레이어 위치 설정
        }
        else
        {
            Debug.LogError("Player Transform is not assigned!");
        }
    }

    void Update()
    {
        UpdatePlayerPosition(); // 플레이어 위치 업데이트
        UpdateMapState(); // 맵 상태 업데이트

        // 플레이어가 새로운 위치에 들어갔는지 확인
        if (currentState != previousState)
        {
            previousState = currentState;
            Ch1TalkManager talkManager = FindObjectOfType<Ch1TalkManager>();
            if (talkManager != null)
            {
                talkManager.OnPlayerEnterLocation(currentState); // 플레이어가 새로운 위치에 들어갔을 때 대화 재개
            }
        }
    }

    void UpdatePlayerPosition()
    {
        if (playerTransform != null)
        {
            playerPosition = playerTransform.position;
        }
        else
        {
            Debug.LogError("Player Transform is not assigned!");
        }
    }

    void UpdateMapState()
    {
        if (playerPosition.x >= -255 && playerPosition.x <= -225 && playerPosition.y >= -7.5f && playerPosition.y <= 7.5f)
        {
            currentState = MapState.MechanicalRoom;
        }
        else if (playerPosition.x >= -225 && playerPosition.x <= -195 && playerPosition.y >= -7.5f && playerPosition.y <= 7.5f)
        {
            currentState = MapState.EngineRoom;
        }
        else if (playerPosition.x >= -195 && playerPosition.x <= -165 && playerPosition.y >= -7.5f && playerPosition.y <= 7.5f)
        {
            currentState = MapState.SpecialRoom;
        }
        else if (playerPosition.x >= -165 && playerPosition.x <= -135 && playerPosition.y >= -7.5f && playerPosition.y <= 7.5f)
        {
            currentState = MapState.TrainRoom1;
        }
        else if (playerPosition.x >= -135 && playerPosition.x <= -105 && playerPosition.y >= -7.5f && playerPosition.y <= 7.5f)
        {
            currentState = MapState.TrainRoom2;
        }
        else if (playerPosition.x >= -105 && playerPosition.x <= -75 && playerPosition.y >= -7.5f && playerPosition.y <= 7.5f)
        {
            currentState = MapState.TrainRoom3;
        }
        else if (playerPosition.x >= -75 && playerPosition.x <= -45 && playerPosition.y >= -7.5f && playerPosition.y <= 7.5f)
        {
            currentState = MapState.Hallway1;
        }
        else if (playerPosition.x >= -45 && playerPosition.x <= -15 && playerPosition.y >= -7.5f && playerPosition.y <= 7.5f)
        {
            currentState = MapState.Garden;
        }
        else if (playerPosition.x >= -15 && playerPosition.x <= 15 && playerPosition.y >= -7.5f && playerPosition.y <= 7.5f)
        {
            currentState = MapState.Cafe;
        }
        else if (playerPosition.x >= 15 && playerPosition.x <= 45 && playerPosition.y >= -7.5f && playerPosition.y <= 7.5f)
        {
            currentState = MapState.Bakery;
        }
        else if (playerPosition.x >= 45 && playerPosition.x <= 75 && playerPosition.y >= -7.5f && playerPosition.y <= 7.5f)
        {
            currentState = MapState.MedicalRoom;
        }
        else if (playerPosition.x >= 75 && playerPosition.x <= 105 && playerPosition.y >= -7.5f && playerPosition.y <= 7.5f)
        {
            currentState = MapState.Hallway2;
        }
        else if (playerPosition.x >= 105 && playerPosition.x <= 135 && playerPosition.y >= -7.5f && playerPosition.y <= 7.5f)
        {
            currentState = MapState.Balcony;
        }
        else
        {
            currentState = MapState.Null;
        }

        //Debug.Log("Current State: " + currentState);
    }
}