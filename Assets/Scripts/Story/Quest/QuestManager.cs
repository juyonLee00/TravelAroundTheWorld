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

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; } // 싱글톤 인스턴스

    private Dictionary<string, Quest> questDictionary = new Dictionary<string, Quest>(); // 퀘스트 저장

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 게임 오브젝트가 씬 전환 시 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject); // 기존 인스턴스가 있을 경우 새 인스턴스 파괴
        }
    }

    public void AddQuest(string questType, string questDescription)
    {
        if (!questDictionary.TryGetValue(questType, out Quest quest))
        {
            quest = new Quest(questType, questDescription);
            quest.ReceiveQuest(); // 퀘스트를 수락 상태로 설정
            questDictionary.Add(questType, quest);
        }
    }

    public void CompleteQuest(string questType)
    {
        if (questDictionary.TryGetValue(questType, out Quest quest))
        {
            if (!quest.isCompleted)
            {
                quest.CompleteQuest();
                Debug.Log($"{questType} 퀘스트가 완료되었습니다.");
            }
        }
    }

    public Dictionary<string, Quest> GetQuests()
    {
        return new Dictionary<string, Quest>(questDictionary);
    }

}
