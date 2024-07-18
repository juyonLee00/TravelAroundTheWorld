using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public string questType; // 퀘스트
    public string questName; // 퀘스트 명  
    public string questDescription; // 퀘스트 설명
    public string questNote; // 비고

    public Quest(string questName, string questDescription, string questNote, string questType)
    {
        this.questName = questName;
        this.questDescription = questDescription;
        this.questNote = questNote;
        this.questType = questType;
    }
}
