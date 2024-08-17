using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapState
{
    Null,
    MechanicalRoom,
    EngineRoom,
    TrainRoom3,
    Hallway,
    Garden,
    Cafe,
    Bakery,
    MedicalRoom,
    Balcony
}

public class Ch0MapManager : MonoBehaviour
{
    public MapState currentState; // 맵의 현재 상태
    public Vector2 playerPosition; // 플레이어의 현재 위치
    public Transform playerTransform; // 플레이어의 Transform 참조

    private Dictionary<string, Bounds> cafeSubZones; // 카페바 구역 지정
    public bool isInCafeBarZone = false; // 카페바 존에 있는지 여부

    public TalkManager talkManager;
    //private string currentMusic = ""; // 현재 재생 중인 음악 이름

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

        InitializeCafeSubZones(); // 카페 내 존 초기화
    }

    void Update()
    {
        // 플레이어 위치 업데이트
        UpdatePlayerPosition();

        // 플레이어 위치에 따른 맵 상태 업데이트
        UpdateMapState();

        // 대화 중이 아닌 경우에만 맵 상태에 따른 음악을 재생
        if (!talkManager.gameObject.activeInHierarchy)
        {
            PlayMapMusic();
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

    void InitializeCafeSubZones()
    {
        cafeSubZones = new Dictionary<string, Bounds>();

        Vector3 cafeBarPosition = new Vector3(6.2f, -4f, 0f); // cafebar의 위치
        Vector3 cafeBarSize = new Vector3(3f, 1.7f, 1f); // cafebar의 스케일

        cafeSubZones["CafeBar"] = new Bounds(cafeBarPosition, cafeBarSize);
    }

    void UpdateMapState()
    {
        if (playerPosition.x >= -89.6 && playerPosition.x <= -69.6 && playerPosition.y >= -5f && playerPosition.y <= 5f)
        {
            currentState = MapState.MechanicalRoom;
        }
        /*
        //임시로 수정/주석처리
        else if (playerPosition.x >= -69.6 && playerPosition.x <= -49.6 && playerPosition.y >= -5f && playerPosition.y <= 5f)
        {
            currentState = MapState.EngineRoom;
        }
        else if (playerPosition.x >= -49.3 && playerPosition.x <= -39.7 && playerPosition.y >= 5f && playerPosition.y <= 13f)
        {
            currentState = MapState.TrainRoom3;
            // 현재 재생 중인 음악이 다른 음악이라면 새 음악을 재생
            if (currentMusic != "a room")
            {
                SoundManager.Instance.PlayMusic("a room", loop: true);
                currentMusic = "a room"; // 현재 재생 중인 음악 이름을 업데이트
            }
        }
        else if (playerPosition.x >= -49.6 && playerPosition.x <= -29.6 && playerPosition.y >= -5f && playerPosition.y <= 5f)
        {
            currentState = MapState.Hallway;
            // 현재 재생 중인 음악이 다른 음악이라면 새 음악을 재생
            if (currentMusic != "a room")
            {
                SoundManager.Instance.PlayMusic("a room", loop: true);
                currentMusic = "a room"; // 현재 재생 중인 음악 이름을 업데이트
            }
        }
        else if (playerPosition.x >= -29.6 && playerPosition.x <= -9.6 && playerPosition.y >= -5f && playerPosition.y <= 5f)
        {
            currentState = MapState.Garden;
            // 현재 재생 중인 음악이 다른 음악이라면 새 음악을 재생
            if (currentMusic != "GARDEN")
            {
                SoundManager.Instance.PlayMusic("GARDEN", loop: true);
                currentMusic = "GARDEN"; // 현재 재생 중인 음악 이름을 업데이트
            }
        }
        */
        else if (playerPosition.x >= -49.6 && playerPosition.x <= -29.6 && playerPosition.y >= -5f && playerPosition.y <= 5f)
        {
            currentState = MapState.EngineRoom;
        }
        else if (playerPosition.x >= -29.3 && playerPosition.x <= -19.7 && playerPosition.y >= 5f && playerPosition.y <= 13f)
        {
            currentState = MapState.TrainRoom3;
        }
        else if (playerPosition.x >= -29.6 && playerPosition.x <= -9.6 && playerPosition.y >= -5f && playerPosition.y <= 5f)
        {
            currentState = MapState.Hallway;
        }
        else if (playerPosition.x >= -29.6 && playerPosition.x <= -9.6 && playerPosition.y >= 20f && playerPosition.y <= 30f)
        {
            currentState = MapState.Garden;
        }
        else if (playerPosition.x >= -9.6 && playerPosition.x <= 9.6 && playerPosition.y >= -5f && playerPosition.y <= 5f)
        {
            currentState = MapState.Cafe;

            // 카페바 존에 있는지 확인
            Bounds cafeBarBounds = cafeSubZones["CafeBar"];
            if (cafeBarBounds.Contains(playerPosition))
            {
                isInCafeBarZone = true;
            }
            else { isInCafeBarZone = false; }
        }
        else if (playerPosition.x >= 9.6 && playerPosition.x <= 29.6 && playerPosition.y >= -5f && playerPosition.y <= 5f)
        {
            currentState = MapState.Bakery;
        }
        else if (playerPosition.x >= 29.6 && playerPosition.x <= 49.6 && playerPosition.y >= -5f && playerPosition.y <= 5f)
        {
            currentState = MapState.MedicalRoom;
        }
        else if (playerPosition.x >= 49.6 && playerPosition.x <= 68.8 && playerPosition.y >= -5f && playerPosition.y <= 5f)
        {
            currentState = MapState.Balcony;
        }
        else
        {
            currentState = MapState.Null;
        }

        //Debug.Log("Current State: " + currentState);
    }

    public void PlayMapMusic()
    {
        string newMusic = "";

        switch (currentState)
        {
            case MapState.TrainRoom3:
                newMusic = "a room";
                break;
            case MapState.Hallway:
                newMusic = "a room";
                break;
            case MapState.Garden:
                newMusic = "GARDEN";
                break;
            case MapState.Cafe:
                newMusic = "CAFE";
                break;
            case MapState.Bakery:
                newMusic = "BAKERY";
                break;
            case MapState.MedicalRoom:
                newMusic = "amedicaloffice_001";
                break;
        }

        // 현재 재생 중인 음악과 다른 음악이라면 새 음악을 재생
        if (talkManager.currentMusic != newMusic)
        {
            SoundManager.Instance.PlayMusic(newMusic, loop: true);
            talkManager.currentMusic = newMusic; // 현재 재생 중인 음악 이름을 업데이트
        }
    }
}