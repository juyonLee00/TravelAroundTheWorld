using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClickOrderList : MonoBehaviour
{
    private bool test = false;
    // 이동 될 애들
    public GameObject RoomService;
    public GameObject Bat;

    private float speed = 1500.0f; // 이동 속도
    // 

    private Vector3 RSHide = new Vector3 (-467.77f, 257.5f, 0);
    private Vector3 RSShow = new Vector3(313.92f, 257.75f, 0);
    private Vector3 BatHide = new Vector3(815.34f, -588.6f, 0);
    private Vector3 BatShow = new Vector3(815.34f, 144.44f, 0);
    private Vector3 targetPositionRoomService;
    
    private bool shouldMoveRoomService = false; // 이동을 시작할지 여부
    private Vector3 startPositionRoomService; // 시작 위치
    private float startTimeRoomService; // 이동 시작 시간
    private float journeyLengthRoomService; // 시작 위치와 목표 위치 간의 거리

    private Vector3 targetPositionBat;

    private bool shouldMoveBat = false; // 이동을 시작할지 여부
    private Vector3 startPositionBat; // 시작 위치
    private float startTimeBat; // 이동 시작 시간
    private float journeyLengthBat; // 시작 위치와 목표 위치 간의 거리

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldMoveRoomService)
        {
            // 경과된 시간 계산
            float distCovered = (Time.time - startTimeRoomService) * speed;

            // Lerp를 사용하여 오브젝트를 부드럽게 이동
            

            // 목표 위치에 도달했는지 확인
            if (Vector3.Distance(RoomService.transform.position, targetPositionRoomService) < 0.01f)
            {
                RoomService.transform.position = targetPositionRoomService; // 목표 위치에 도달 시 위치 고정
                shouldMoveRoomService = false; // 이동 완료
            }
            else
            {
                float fractionOfJourney = distCovered / journeyLengthRoomService;
                RoomService.transform.position = Vector3.Lerp(startPositionRoomService, targetPositionRoomService, fractionOfJourney);
            }

        }
        if(shouldMoveBat)
        {
            float distCovered1 = (Time.time - startTimeBat) * speed;

            

            if(Vector3.Distance(Bat.transform.position, targetPositionBat) < 0.01f)
            {
                Bat.transform.position = targetPositionBat;
                shouldMoveBat = false;
            }
            else
            {
                float fractionOfJourney1 = distCovered1 / journeyLengthBat;
                Bat.transform.position = Vector3.Lerp(startPositionBat, targetPositionBat, fractionOfJourney1);
            }
        }
    }

    public void ClickCrafting() // 제작하기 버튼 눌렀을때
    {
        SoundManager.Instance.PlaySFX("click sound");
        List<object> list = new List<object> ();
        int c = transform.childCount;
        for (int i = 1; i < transform.childCount-1; i++)
        {
            string menu = transform.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().text;
            list.Add (menu);
            string quantityText = transform.GetChild(i).GetChild(2).GetComponent<TextMeshProUGUI>().text;
            int quantity = int.Parse(quantityText[quantityText.Length - 1].ToString());
            list.Add (quantity);
        }
        // list에 주문정보 있음.
        
    }
    public void ClickOrder()
    {
        SoundManager.Instance.PlaySFX("click sound");
        string roomNum = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text;
        string totalPrice = transform.GetChild(transform.childCount-1).GetChild(0).GetComponent<TextMeshProUGUI>().text;
        Debug.Log(roomNum); Debug.Log(totalPrice);
        Debug.Log("주문서 클릭");
        GameObject.FindWithTag("RS").GetComponent<TextMeshProUGUI>().text = roomNum + "이야~ 정확히 " + totalPrice + "지불하셨어~";


    }
    public void Show()
    {
        SoundManager.Instance.PlaySFX("click sound");
        targetPositionRoomService = RSShow;
        targetPositionBat = BatShow;

        shouldMoveRoomService = true; // 이동 시작
        startPositionRoomService = RoomService.transform.position; // 시작 위치 초기화
        startTimeRoomService = Time.time; // 이동 시작 시간 초기화
        journeyLengthRoomService = Vector3.Distance(startPositionRoomService, targetPositionRoomService); // 거리 계산

        shouldMoveBat = true;
        startPositionBat = Bat.transform.position;
        startTimeBat = Time.time;
        journeyLengthBat = Vector3.Distance(startPositionBat, targetPositionBat);


    }

    public void Hide()
    {
        SoundManager.Instance.PlaySFX("click sound");
        targetPositionRoomService = RSHide;
        targetPositionBat = BatHide;

        shouldMoveRoomService = true; // 이동 시작
        startPositionRoomService = RoomService.transform.position; // 시작 위치 초기화
        startTimeRoomService = Time.time; // 이동 시작 시간 초기화
        journeyLengthRoomService = Vector3.Distance(startPositionRoomService, targetPositionRoomService); // 거리 계산

        shouldMoveBat = true;
        startPositionBat = Bat.transform.position;
        startTimeBat = Time.time;
        journeyLengthBat = Vector3.Distance(startPositionBat, targetPositionBat);
    }
}
