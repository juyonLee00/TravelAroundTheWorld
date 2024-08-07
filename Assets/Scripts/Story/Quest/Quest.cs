using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public string questType; // 퀘스트 종류
    public string description; // 퀘스트 설명
    public bool isCompleted; // 완료 여부
    public bool isReceived; // 퀘스트를 받았는지 여부

    public Quest(string type, string desc)
    {
        questType = type;
        description = desc;
        isCompleted = false;
        isReceived = false; // 초기값 설정
    }

    // 퀘스트 수락 메서드
    public void ReceiveQuest()
    {
        isReceived = true;
    }

    // 퀘스트 완료 메서드
    public void CompleteQuest()
    {
        isCompleted = true;
    }
}
