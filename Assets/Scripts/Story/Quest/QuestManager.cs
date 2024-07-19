using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public Dictionary<string, Quest> questDictionary = new Dictionary<string, Quest>();
    public TextMeshProUGUI questUI;

    void Start()
    {
        LoadQuestsFromCSV();
    }

    void LoadQuestsFromCSV()
    {
        List<Dictionary<string, object>> data_Quest = Ch0CSVReader.Read("Travel Around The World - 퀘스트");

        foreach (var row in data_Quest)
        {
            string questType = row["퀘스트"].ToString();
            string questName = row["퀘스트 명"].ToString();
            string questDescription = row["퀘스트 설명"].ToString();
            string questNote = row["비고"].ToString();

            Quest quest = new Quest(questName, questDescription, questNote, questType);
            if (!questDictionary.ContainsKey(questName))
            {
                questDictionary.Add(questName, quest);
            }
        }
    }

    public Quest GetQuest(string questName)
    {
        questDictionary.TryGetValue(questName, out Quest quest);
        return quest;
    }
}
