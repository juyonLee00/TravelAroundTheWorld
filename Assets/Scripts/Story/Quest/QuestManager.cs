using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; } // 싱글톤 인스턴스

    public Dictionary<string, Quest> questDictionary = new Dictionary<string, Quest>(); // 퀘스트 저장
    public TextMeshProUGUI questUI; // UI 업데이트용

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Initialize(); // 초기화 메서드 호출
        }
        else
        {
            Destroy(gameObject); // 기존 인스턴스가 있을 경우 새 인스턴스 파괴
        }
    }

    private void Initialize()
    {
        // 초기화 로직: 필요한 초기 설정을 여기에 추가
        LoadQuestsFromCSV(); // CSV에서 퀘스트 데이터 로드
    }

    private void LoadQuestsFromCSV()
    {
        // CSV 파일로부터 데이터 읽어오기
        List<Dictionary<string, object>> data_Quest = Ch0CSVReader.Read("Travel Around The World - 퀘스트");

        // CSV 파일에서 각 줄을 순회하며 퀘스트 데이터 추출
        foreach (var row in data_Quest)
        {
            // CSV 데이터에서 값 추출
            string questType = row["퀘스트"].ToString();
            string questDescription = row["퀘스트 설명"].ToString();

            // 퀘스트 객체 생성
            Quest quest = new Quest(questType, questDescription);

            // 퀘스트 사전에 추가
            if (!questDictionary.ContainsKey(questType))
            {
                questDictionary.Add(questType, quest);
            }
        }
    }

    public Quest GetQuest(string questType)
    {
        questDictionary.TryGetValue(questType, out Quest quest);
        return quest;
    }

    public void CompleteQuest(string questType)
    {
        if (questDictionary.TryGetValue(questType, out Quest quest))
        {
            if (!quest.isCompleted)
            {
                quest.CompleteQuest();
                Debug.Log(questType + " 퀘스트가 완료되었습니다.");
            }
        }
    }

    public void ReceiveQuest(string questType)
    {
        if (questDictionary.TryGetValue(questType, out Quest quest))
        {
            if (!quest.isReceived)
            {
                quest.isReceived = true;
                Debug.Log(questType + " 퀘스트를 받았습니다.");
            }
        }
    }
}
