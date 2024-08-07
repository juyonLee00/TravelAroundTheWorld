using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; } // 싱글톤 인스턴스

    public TextMeshProUGUI questUI; // UI 업데이트용

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

    public void ShowQuest(string questType, string questDescription)
    {
        if (questUI != null)
        {
            questUI.text = questDescription;
            questUI.gameObject.SetActive(true);

            // Quest dictionary에 퀘스트가 없다면 추가하고 수락 상태로 설정
            if (!questDictionary.TryGetValue(questType, out Quest quest))
            {
                quest = new Quest(questType, questDescription);
                quest.ReceiveQuest(); // 퀘스트를 수락 상태로 설정
                questDictionary.Add(questType, quest);
            }
            else if (!quest.isReceived)
            {
                quest.ReceiveQuest(); // 퀘스트를 수락 상태로 설정
            }
        }
        else
        {
            Debug.LogError("Quest UI is not assigned in the QuestManager.");
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
                questUI.gameObject.SetActive(false); // 완료되면 UI 비활성화
            }
        }
    }

    public void HideQuest()
    {
        if (questUI != null)
        {
            questUI.gameObject.SetActive(false);
        }
    }
}
