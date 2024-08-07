using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ch1TalkManager : MonoBehaviour
{
    private List<Ch1ProDialogue> ch1ProDialogue;

    public GameObject narration;
    public GameObject dialogue;

    public GameObject imageObj; // 초상화 이미지 요소
    public GameObject nameObj; // 이름 요소

    public GameObject letter; // 편지지 화면
    public TextMeshProUGUI letterText;

    public GameObject quest; // 퀘스트 화면
    public TextMeshProUGUI questText;

    public GameObject cafe; // 카페 화면
    public GameObject trainRoom; // 객실 화면
    public GameObject trainRoomHallway; // 객실 복도 화면
    public GameObject garden; // 정원 화면
    public GameObject bakery; // 빵집 화면
    public GameObject medicalRoom; // 의무실 화면
    public GameObject jazzBar; // 재즈바 화면

    public ScreenFader screenFader; // 페이드인/아웃 효과 스크립트
    private bool isFadingOut = false; // 페이드 아웃 중인지 여부 (페이드 아웃 중에는 입력 무시하기 위해)

    public Ch0DialogueBar dialogueBar; // 대화창 스크립트 (타이핑 효과 호출을 위해)
    public Ch0DialogueBar narrationBar; // 나레이션창 스크립트 (타이핑 효과 호출을 위해)

    // 문자열 상수 선언
    private const string narrationSpeaker = "나레이션";
    private const string letterSpeaker = "편지지";
    private const string locationCafe = "카페";
    private const string locationEngineRoom = "엔진룸";
    private const string locationOtherRoom1 = "다른 방 1";
    private const string locationOtherRoom2 = "다른 방 2";
    private const string locationGarden = "정원";
    private const string locationBakery = "빵집";
    private const string locationMedicalRoom = "의무실";
    private const string locationTrainRoom = "객실";
    private const string questSpeaker = "퀘스트";
    private const string locationJazzBar = "재즈바";

    public int currentDialogueIndex = 0; // 현재 대사 인덱스
    private bool isActivated = false; // TalkManager가 활성화되었는지 여부

    public QuestManager questManager; // 퀘스트 매니저 참조

    void Awake()
    {
        ch1ProDialogue = new List<Ch1ProDialogue>();
        LoadDialogueFromCSV();
    }

    void Start()
    {
        ActivateTalk("객실");
    }

    void Update()
    {
        if (isActivated && Input.GetKeyDown(KeyCode.Space))
        {
            currentDialogueIndex++;
            PrintCh1ProDialogue(currentDialogueIndex);
        }
    }

    void LoadDialogueFromCSV()
    {
        List<Dictionary<string, object>> data_Dialog = Ch0CSVReader.Read("Travel Around The World - CH1 (1)");

        foreach (var row in data_Dialog)
        {
            string dayString = row["일자"].ToString();
            int day = int.Parse(System.Text.RegularExpressions.Regex.Match(dayString, @"\d+").Value);
            string location = row["장소"].ToString();
            string speaker = row["인물"].ToString();
            string line = row["대사"].ToString();
            string screenEffect = row["화면"].ToString();
            string backgroundMusic = row["배경음악"].ToString();
            string expression = row["표정"].ToString();
            string note = row["비고"].ToString();
            string quest = row["퀘스트"].ToString();
            string questContent = row["퀘스트 내용"].ToString();

            ch1ProDialogue.Add(new Ch1ProDialogue(day, location, speaker, line, screenEffect, backgroundMusic, expression, note, quest, questContent));
        }
    }

    void PrintCh1ProDialogue(int index)
    {
        if (index >= ch1ProDialogue.Count)
        {
            narration.SetActive(false);
            dialogue.SetActive(false);
            return;
        }

        Ch1ProDialogue currentDialogue = ch1ProDialogue[index];

        // 씬 전환을 위한 특별 대사 감지
        /*if (currentDialogue.line.Contains("랜덤 등장인물 룸서비스 주문 3건") ||
            currentDialogue.line.Contains("랜덤 등장인물 주문 2건") ||
            currentDialogue.line.Contains("랜덤 등장인물 주문 1건"))
        {
            // CafeScene으로 전환 시작
            StartCoroutine(TransitionToCafeScene(currentDialogue.line));
            return;
        }*/

        if (currentDialogue.speaker == letterSpeaker)
        {
            narration.SetActive(false);
            dialogue.SetActive(false);
            if (!string.IsNullOrEmpty(letterText.text))
            {
                letterText.text += "\n";
            }
            letterText.text += currentDialogue.line;
        }
        else if (string.IsNullOrEmpty(currentDialogue.speaker) && string.IsNullOrEmpty(currentDialogue.location))
        {
            narration.SetActive(false);
            dialogue.SetActive(false);
        }
        else if ((currentDialogue.speaker == narrationSpeaker) || string.IsNullOrEmpty(currentDialogue.speaker))
        {
            narration.SetActive(true);
            dialogue.SetActive(false);
            narrationBar.SetDialogue(currentDialogue.speaker, currentDialogue.line); // 타이핑 효과 적용
        }
        else
        {
            narration.SetActive(false);
            dialogue.SetActive(true);
            dialogueBar.SetDialogue(currentDialogue.speaker, currentDialogue.line); // 타이핑 효과 적용
        }

        // 현재 대사가 2일차 밤 객실에서 발생했는지 확인하고 비밀 퀘스트 1을 활성화
        /*if (currentDialogue.day == 2 && !DayNightCycleManager.Instance.GetNowDayTime() && currentDialogue.location == locationTrainRoom)
        {
            // 플레이어가 "비밀 퀘스트 1"을 받음
            questManager.ReceiveQuest("비밀 퀘스트 1");
            Debug.Log("플레이어가 '비밀 퀘스트 1'을 받았습니다."); // 디버그 로그 출력
        }*/

        CheckTalk(currentDialogue.location);
    }

    /*private IEnumerator TransitionToCafeScene(string taskDetails)
    {
        // 현재 씬 이름 저장
        string currentSceneName = GetCurrentSceneName();

        // 작업 세부 정보 또는 필요한 데이터 저장
        TaskManager.Instance.SetTaskDetails(taskDetails);

        // CafeScene으로 전환
        yield return StartCoroutine(SceneManagerEx.Instance.LoadSceneWithLoadingScene("CafeScene"));

        // CafeScene에서 작업이 완료될 때까지 대기
        yield return new WaitUntil(() => TaskManager.Instance.AreTasksCompleted());

        // 원래 씬으로 돌아옴
        yield return StartCoroutine(SceneManagerEx.Instance.LoadSceneWithLoadingScene(currentSceneName));

        // 이야기 이어서 진행
        ActivateTalk(currentSceneName);
    }*/

    public void ActivateTalk(string locationName)
    {
        this.gameObject.SetActive(true);
        isActivated = true;

        // locationName에 따라 인덱스 조정하여 특정 대화를 시작할 수 있도록 수정
        currentDialogueIndex = ch1ProDialogue.FindIndex(dialogue => dialogue.location == locationName);

        if (currentDialogueIndex >= 0)
        {
            PrintCh1ProDialogue(currentDialogueIndex);
        }
    }

    void DeactivateTalk()
    {
        this.gameObject.SetActive(false);
        isActivated = false;
    }

    void CheckTalk(string location)
    {
        letter.SetActive(false);
        cafe.SetActive(false);
        trainRoom.SetActive(false);
        trainRoomHallway.SetActive(false);
        garden.SetActive(false);
        bakery.SetActive(false);
        medicalRoom.SetActive(false);
        letter.SetActive(false);
        jazzBar.SetActive(false);
        quest.SetActive(false);

        switch (location)
        {
            case locationTrainRoom:
                trainRoom.SetActive(true);
                if (currentDialogueIndex == 24)
                {
                    StartCoroutine(screenFader.FadeIn(letter));
                }
                else if (currentDialogueIndex >= 25 && currentDialogueIndex <= 29)
                {
                    letter.SetActive(true);
                    if (currentDialogueIndex >= 25 && currentDialogueIndex <= 28)
                    {
                        letterText.gameObject.SetActive(true);
                    }
                    else if (currentDialogueIndex >= 25)
                    {
                        letter.gameObject.SetActive(true);
                    }
                    if (currentDialogueIndex == 29)
                    {
                        StartCoroutine(screenFader.FadeOut(letter));
                    }
                }
                break;

            case locationMedicalRoom:
                medicalRoom.SetActive(true);
                break;

            case locationGarden:
                garden.SetActive(true);
                break;

            case locationBakery:
                bakery.SetActive(true);
                break;

            case locationJazzBar:
                jazzBar.SetActive(true);
                break;

            case locationCafe:
                cafe.SetActive(true);
                break;
        }

        if (currentDialogueIndex > ch1ProDialogue.Count)
        {
            DeactivateTalk();
        }
    }

    private IEnumerator FadeOutAndDeactivateTalk(GameObject obj)
    {
        isFadingOut = true; // 페이드아웃 시작
        yield return StartCoroutine(screenFader.FadeOut(obj)); // FadeOut이 완료될 때까지 기다립니다.
        narration.SetActive(false);
        dialogue.SetActive(false);
        DeactivateTalk(); // FadeOut이 완료된 후 대화 비활성화
        isFadingOut = false; // 페이드아웃 종료
    }
}
