using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro 네임스페이스 추가


public class TalkManager : MonoBehaviour
{
    // 대사들을 저장할 리스트
    private List<ProDialogue> proDialogue;

    public GameObject opening;
    public GameObject narration;
    public GameObject dialogue;

    public GameObject imageObj; // 초상화 이미지 요소
    public GameObject nameObj; // 이름 요소

    public GameObject invitation; // 초대장 화면
    public TextMeshProUGUI invitationText;

    public GameObject forest; // 숲 화면

    public GameObject trainStation; // 기차역 화면
    public GameObject train; // 기차 화면
    public GameObject trainOutside; // 기차 외부 화면

    public GameObject cafe; // 카페 화면
    public GameObject trainRoom; // 객실 화면
    public GameObject trainRoomHallway; // 객실 복도 화면
    public GameObject garden; // 정원 화면
    public GameObject bakery; // 빵집 화면
    public GameObject medicalRoom; // 의무실 화면

    public ScreenFader screenFader; // 페이드인/아웃 효과 스크립트

    public Ch0DialogueBar dialogueBar; // 대화창 스크립트 (타이핑 효과 호출을 위해)
    public Ch0DialogueBar narrationBar; // 나레이션창 스크립트 (타이핑 효과 호출을 위해)
    public Ch0DialogueBar openingBar; // 오프닝 대사창 스크립트 (타이핑 효과 호출을 위해)

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
        if (isActivated && currentDialogueIndex == 0)
        {
            PrintProDialogue(currentDialogueIndex);
        }
    }

    void Update()
    {
        if (isActivated && Input.GetKeyDown(KeyCode.Space))
        {
            currentDialogueIndex++;
            PrintProDialogue(currentDialogueIndex);
        }
    }

    void LoadDialogueFromCSV()
    {
        List<Dictionary<string, object>> data_Dialog = Ch0CSVReader.Read("Travel Around The World - CH0 (2)");

        foreach(var row in data_Dialog)
        {
            string dayString = row["일자"].ToString();
            int day = int.Parse(System.Text.RegularExpressions.Regex.Match(dayString, @"\d+").Value);
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
            openingBar.SetDialogue(currentDialogue.speaker, currentDialogue.line); // 타이핑 효과 적용
        }
        //오프닝 대사 이후부터 인물에 따라 대사/나레이션/텍스트 창 활성화
        else if (currentDialogue.speaker == "초대장")
        {
            narration.SetActive(false);
            dialogue.SetActive(false);
            opening.SetActive(false);
            if (!string.IsNullOrEmpty(invitationText.text))
            {
                invitationText.text += "\n";
            }
            invitationText.text += currentDialogue.line;
        }
        else if (string.IsNullOrEmpty(currentDialogue.speaker) && string.IsNullOrEmpty(currentDialogue.location))
        {
            narration.SetActive(false);
            dialogue.SetActive(false);
            opening.SetActive(false);
        }
        else if ((currentDialogue.speaker == "나레이션") || string.IsNullOrEmpty(currentDialogue.speaker))
        {
            narration.SetActive(true);
            dialogue.SetActive(false);
            opening.SetActive(false);
            narrationBar.SetDialogue(currentDialogue.speaker, currentDialogue.line); // 타이핑 효과 적용
        }
        else
        {
            narration.SetActive(false);
            dialogue.SetActive(true);
            opening.SetActive(false);
            dialogueBar.SetDialogue(currentDialogue.speaker, currentDialogue.line); // 타이핑 효과 적용
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
        trainOutside.SetActive(false);
        cafe.SetActive(false);
        trainRoom.SetActive(false);
        trainRoomHallway.SetActive(false);
        garden.SetActive(false);
        bakery.SetActive(false);
        medicalRoom.SetActive(false);

        switch (location)
        {
            case "집":
                if (currentDialogueIndex == 2)
                {
                    StartCoroutine(screenFader.FadeIn(invitation));
                }
                else if (currentDialogueIndex >= 3 && currentDialogueIndex <= 23)
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
                    if (currentDialogueIndex == 23)
                    {
                        StartCoroutine(screenFader.FadeOut(invitation));
                    }
                }
                break;
            case "숲":
                if (currentDialogueIndex == 24)
                {
                    StartCoroutine(screenFader.FadeIn(forest));
                }
                else
                {
                    forest.SetActive(true);
                }
                break;
            case "기차역":
                if (currentDialogueIndex == 28)
                {
                    StartCoroutine(screenFader.FadeIn(trainStation));
                }
                else
                {
                    trainStation.SetActive(true);
                    if (currentDialogueIndex >= 32)
                    {
                        train.SetActive(true);
                        if (currentDialogueIndex == 48)
                        {
                            StartCoroutine(screenFader.FadeOut(trainStation));
                            StartCoroutine(screenFader.FadeOut(train));
                        }
                    }
                }
                break;
            case "카페":
                //카페 튜토리얼 이후 ~ 맵 튜토리얼 이전
                if (currentDialogueIndex == 50)
                {
                    StartCoroutine(screenFader.FadeIn(cafe));
                }
                else if (currentDialogueIndex <= 62)
                {
                    cafe.SetActive(true);
                    if (currentDialogueIndex == 62)
                    {
                        StartCoroutine(screenFader.FadeOut(cafe));
                    }
                }
                //맵 튜토리얼
                if (currentDialogueIndex == 64)
                {
                    StartCoroutine(screenFader.FadeIn(cafe));
                }
                else if (currentDialogueIndex <= 68)
                {
                    cafe.SetActive(true);
                    if (currentDialogueIndex == 68)
                    {
                        StartCoroutine(screenFader.FadeOut(cafe));
                    }
                }
                break;
            case "엔진룸":
                StartCoroutine(screenFader.FadeIn(trainRoomHallway));
                //trainRoomHallway.SetActive(true);
                break;
            case "다른 방 1":
                StartCoroutine(screenFader.FadeIn(trainRoomHallway));
                //trainRoomHallway.SetActive(true);
                break;
            case "다른 방 2":
                StartCoroutine(screenFader.FadeIn(trainRoomHallway));
                //trainRoomHallway.SetActive(true);
                break;
            case "정원":
                if (currentDialogueIndex == 73)
                {
                    StartCoroutine(screenFader.FadeIn(garden));
                }
                else
                {
                    garden.SetActive(true);
                    if (currentDialogueIndex == 81)
                    {
                        StartCoroutine(screenFader.FadeOut(garden));
                    }
                }
                break;
            case "빵집":
                if (currentDialogueIndex == 82)
                {
                    StartCoroutine(screenFader.FadeIn(bakery));
                }
                else
                {
                    bakery.SetActive(true);
                    if (currentDialogueIndex == 107)
                    {
                        StartCoroutine(screenFader.FadeOut(bakery));
                    }
                }
                break;
            case "의무실":
                if (currentDialogueIndex == 108)
                {
                    StartCoroutine(screenFader.FadeIn(medicalRoom));
                }
                else
                {
                    medicalRoom.SetActive(true);
                    if (currentDialogueIndex == 131)
                    {
                        StartCoroutine(screenFader.FadeOut(medicalRoom));
                    }
                }
                break;
            case "객실":
                if (currentDialogueIndex == 132)
                {
                    StartCoroutine(screenFader.FadeIn(trainRoom));
                }
                else
                {
                    trainRoom.SetActive(true);
                    if (currentDialogueIndex == 136)
                    {
                        StartCoroutine(screenFader.FadeOut(trainRoom));
                    }
                }
                break;
        }
        if (currentDialogueIndex > proDialogue.Count)
        {
            DeactivateTalk();
        }
    }
}