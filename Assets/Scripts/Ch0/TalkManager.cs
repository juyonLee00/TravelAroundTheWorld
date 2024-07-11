using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro 네임스페이스 추가

// ProDialogue 클래스 정의 (프롤로그 대사 저장)
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

public class TalkManager : MonoBehaviour
{
    // 대사들을 저장할 리스트
    private List<ProDialogue> proDialogue;

    public GameObject opening;
    public TextMeshProUGUI openingText; // TextMeshPro UI 텍스트 요소

    public GameObject narration;
    public TextMeshProUGUI narrationText; // TextMeshPro UI 텍스트 요소

    public GameObject dialogue;
    public GameObject imageObj; // 초상화 이미지 요소
    public GameObject nameObj; // 이름 요소
    public TextMeshProUGUI nameText; // TextMeshPro UI 텍스트 요소
    public TextMeshProUGUI descriptionText; // TextMeshPro UI 텍스트 요소

    public GameObject invitation; // 초대장 화면
    public TextMeshProUGUI invitationText; // TextMeshPro UI 텍스트 요소

    public GameObject forest; // 숲 화면

    public GameObject trainStation; // 기차역 화면
    public GameObject train; // 기차 화면

    private int currentDialogueIndex = 0; // 현재 대사 인덱스
    private bool isActivated = false; // TalkManager가 활성화되었는지 여부

    void Awake()
    {
        proDialogue = new List<ProDialogue>();
        LoadDialogueFromCSV(); // CSV에서 데이터를 로드하는 함수 호출
    }

    void Start()
    {
        ActivateTalk(); // 오브젝트 활성화
    }

    void Update()
    {
        if (isActivated && currentDialogueIndex == 0)
        {
            PrintProDialogue(currentDialogueIndex);
        }
        if (isActivated && Input.GetKeyDown(KeyCode.Space))
        {
            currentDialogueIndex++;
            PrintProDialogue(currentDialogueIndex);
        }
    }

    void LoadDialogueFromCSV()
    {
        List<Dictionary<string, object>> data_Dialog = Ch0CSVReader.Read("Travel Around The World - CH0");

        foreach(var row in data_Dialog)
        {
            int day = int.Parse(row["일자"].ToString().Replace("일차", "").Trim());
            string location = row["장소"].ToString();
            string speaker = row["인물"].ToString();
            string line = row["대사"].ToString();
            string screenEffect = row["화면 연출"].ToString();
            string backgroundMusic = row["배경음악"].ToString();
            string expression = row["표정"].ToString();
            string note = row["비고"].ToString();
            string quest = row["퀘스트"].ToString();
            string questContent = row["퀘스트 내용"].ToString();

            proDialogue.Add(new ProDialogue(day, location, speaker, line, screenEffect, backgroundMusic, expression, note, quest, questContent));
        }
    }

    void PrintProDialogue(int index)
    {
        if (index >= proDialogue.Count)
        {
            narration.SetActive(false);
            dialogue.SetActive(false);
            return; // 대사 리스트를 벗어나면 오브젝트 비활성화 후 리턴
        }

        ProDialogue currentDialogue = proDialogue[index];

        if (index < 2)
        {
            narration.SetActive(false);
            dialogue.SetActive(false);
            opening.SetActive(true);
            openingText.text = currentDialogue.line;
        }
        //오프닝 대사 이후부터 인물에 따라 대사/나레이션/텍스트 창 활성화
        else if (currentDialogue.speaker == "초대장")
        {
            narration.SetActive(false);
            dialogue.SetActive(false);
            opening.SetActive(false);
            if (!string.IsNullOrEmpty(invitationText.text))
            {
                invitationText.text += "\n"; // 기존 내용이 있으면 한 줄 띄우고 추가
            }
            invitationText.text += currentDialogue.line; // 새로운 내용 추가
        }
        else if (currentDialogue.speaker == "나레이션")
        {
            narration.SetActive(true);
            dialogue.SetActive(false);
            opening.SetActive(false);
            narrationText.text = currentDialogue.line;
        }
        else
        {
            narration.SetActive(false);
            dialogue.SetActive(true);
            opening.SetActive(false);
            nameText.text = currentDialogue.speaker;
            descriptionText.text = currentDialogue.line;
        }

        CheckTalk(currentDialogue.location);
    }

    public void ActivateTalk()
    {
        this.gameObject.SetActive(true);
        isActivated = true;
    }

    void DeactivateTalk()
    {
        this.gameObject.SetActive(false);
        isActivated = false;
    }

    void CheckTalk(string location)
    {
        invitation.SetActive(false);
        forest.SetActive(false);
        trainStation.SetActive(false);
        train.SetActive(false);

        switch (location)
        {
            case "집":
                if (currentDialogueIndex >= 3 && currentDialogueIndex <= 22)
                {
                    invitation.SetActive(true);
                    if (currentDialogueIndex >= 3 && currentDialogueIndex <= 5)
                    {
                        invitationText.gameObject.SetActive(false);
                    }
                    else if (currentDialogueIndex >= 6)
                    {
                        invitationText.gameObject.SetActive(true);
                    }
                }
                break;
            case "숲":
                forest.SetActive(true);
                break;
            case "기차역":
                trainStation.SetActive(true);
                if (currentDialogueIndex >= 32)
                {
                    train.SetActive(true);
                }
                break;
        }
        if (currentDialogueIndex > proDialogue.Count)
        {
            DeactivateTalk();
        }
    }
}
