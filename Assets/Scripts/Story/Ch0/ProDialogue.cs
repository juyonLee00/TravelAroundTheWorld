using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProDialogue
{
    public int day; // 일자
    public string location; // 장소
    public string speaker; // 인물
    public string line; // 대사
    public string screenEffect; // 화면 연출
    public string backgroundMusic; // 배경음악
    public string expression; // 표정
    public string note; // 비고
    public string quest; // 퀘스트
    public string questContent; // 퀘스트 내용

    public ProDialogue(int day, string location, string speaker, string line, string screenEffect, string backgroundMusic, string expression, string note, string quest, string questContent)
    {
        this.day = day;
        this.location = location;
        this.speaker = speaker;
        this.line = line;
        this.screenEffect = screenEffect;
        this.backgroundMusic = backgroundMusic;
        this.expression = expression;
        this.note = note;
        this.quest = quest;
        this.questContent = questContent;
    }
}
