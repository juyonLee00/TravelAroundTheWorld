using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Ch1TalkManager : MonoBehaviour
{
    private List<Ch1ProDialogue> ch1ProDialogue;

    public GameObject narration;
    public GameObject dialogue;

    public GameObject imageObj; // 초상화 이미지 요소
    public GameObject nameObj; // 이름 요소

    public GameObject letter; // 편지지 화면
    public TextMeshProUGUI letterText;

    public GameObject player; // 플레이어 캐릭터
    public GameObject map; // 맵

    public GameObject cafe; // 카페 화면
    public GameObject trainRoom; // 객실 화면
    public GameObject trainRoomHallway; // 객실 복도 화면
    public GameObject garden; // 정원 화면
    public GameObject bakery; // 빵집 화면
    public GameObject medicalRoom; // 의무실 화면
    public GameObject jazzBar; // 재즈바 화면

    public GameObject Npc_Rayviyak; // 정원 npc

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
    private const string locationJazzBar = "재즈바";

    public int currentDialogueIndex = 0; // 현재 대사 인덱스
    private bool isActivated = false; // TalkManager가 활성화되었는지 여부

    public QuestManager questManager; // 퀘스트 매니저 참조
    public PlayerController playerController; // 플레이어 컨트롤러 참조
    public Ch0MapManager mapManager; // 맵 매니저 참조

    private Dictionary<string, Sprite> characterImages; // 캐릭터 이름과 이미지를 매핑하는 사전

    public bool isWaitingForPlayer = false; // 플레이어가 특정 위치에 도달할 때까지 기다리는 상태인지 여부

    void Awake()
    {
        ch1ProDialogue = new List<Ch1ProDialogue>();
        LoadDialogueFromCSV();
        InitializeCharacterImages();

        // mapManager 초기화
        if (map != null)
        {
            mapManager = map.GetComponent<Ch0MapManager>();
        }

        playerController = player.GetComponent<PlayerController>(); // 플레이어 컨트롤러 참조 설정
    }


    void Start()
    {
        playerController = player.GetComponent<PlayerController>(); // 플레이어 컨트롤러 참조 설정
        ActivateTalk("객실");
    }

    void Update()
    {
        if (isActivated && Input.GetKeyDown(KeyCode.Space) && !isWaitingForPlayer)
        {
            currentDialogueIndex++;
            PrintCh1ProDialogue(currentDialogueIndex);
        }

        // 플레이어가 특정 위치에 도달했는지 확인하는 부분
        if (isWaitingForPlayer && mapManager != null)
        {
            if (mapManager.currentState == MapState.Cafe && currentDialogueIndex == 5)
            {
                isWaitingForPlayer = false; // 대기 상태 해제
                player.SetActive(false);
                map.SetActive(false);
                cafe.SetActive(true);
                currentDialogueIndex++; // 다음 대사로 넘어가기
                PrintCh1ProDialogue(currentDialogueIndex); // 다음 대사 출력
            }
            else if (mapManager.currentState == MapState.TrainRoom3 && currentDialogueIndex == 23) // 인덱스 23 이후에만 실행
            {
                isWaitingForPlayer = false; // 대기 상태 해제
                player.SetActive(false);
                map.SetActive(false);
                trainRoom.SetActive(true);
                currentDialogueIndex++;
                PrintCh1ProDialogue(currentDialogueIndex); // 대사 출력
            }
        }
    }

    void LoadDialogueFromCSV()
    {
        List<Dictionary<string, object>> data_Dialog = Ch0CSVReader.Read("Travel Around The World - CH1");

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

            // 퀘스트가 존재하면 QuestManager를 통해 퀘스트 저장
            if (!string.IsNullOrEmpty(quest))
            {
                QuestManager.Instance.AddQuest(quest, questContent);
            }
        }
    }

    void InitializeCharacterImages()
    {
        characterImages = new Dictionary<string, Sprite>();
        characterImages["솔"] = Resources.Load<Sprite>("PlayerImage/Sol");
        characterImages["루카스"] = Resources.Load<Sprite>("NpcImage/Lucas");
        characterImages["슬로우"] = Resources.Load<Sprite>("NpcImage/Slow");
        characterImages["가이"] = Resources.Load<Sprite>("NpcImage/Gai");
        characterImages["레이비야크"] = Resources.Load<Sprite>("NpcImage/Leviac");
        characterImages["바이올렛"] = Resources.Load<Sprite>("NpcImage/Violet");
    }

    public void PrintCh1ProDialogue(int index)
    {
        if (index >= ch1ProDialogue.Count)
        {
            narration.SetActive(false);
            dialogue.SetActive(false);
            return;
        }

        Ch1ProDialogue currentDialogue = ch1ProDialogue[index];

        Sprite characterSprite = characterImages.ContainsKey(currentDialogue.speaker) ? characterImages[currentDialogue.speaker] : Resources.Load<Sprite>("NpcImage/Default");

        if (imageObj.GetComponent<SpriteRenderer>() != null)
        {
            imageObj.GetComponent<SpriteRenderer>().sprite = characterSprite;
        }
        else if (imageObj.GetComponent<Image>() != null)
        {
            imageObj.GetComponent<Image>().sprite = characterSprite;
        }

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

        // 특정 대화 인덱스에서 플레이어 이동을 활성화하는 로직
        if (index == 5) // 예시: "카페로 가자" 대사 이후
        {
            isWaitingForPlayer = true; // 플레이어가 특정 위치에 도달할 때까지 대기
            EnablePlayerMovement();
            map.SetActive(true);
            player.SetActive(true);
            narration.SetActive(false);
            dialogue.SetActive(false);
        }
        else if (index == 23 && mapManager.currentState == MapState.Cafe) // 인덱스 23 이후의 로직
        {
            isWaitingForPlayer = true; // 플레이어가 특정 위치에 도달할 때까지 대기
            EnablePlayerMovement();
            map.SetActive(true);
            player.SetActive(true);
            cafe.SetActive(false);
            narration.SetActive(false);
            dialogue.SetActive(false);
        }
        else if (index == 33 && mapManager.currentState == MapState.TrainRoom3)
        {
            isWaitingForPlayer = true; // 플레이어가 특정 위치에 도달할 때까지 대기
            EnablePlayerMovement();
            map.SetActive(true);
            player.SetActive(true);
            trainRoom.SetActive(false);
            narration.SetActive(false);
            dialogue.SetActive(false);
            // NPC 오브젝트 활성화
            if (Npc_Rayviyak != null)
            {
                Npc_Rayviyak.SetActive(true);
            }
        }

        else
        {
            CheckTalk(currentDialogue.location);
        }
    }

    public void OnDialogueButtonClicked(int index)
    {
        // 전달된 인덱스를 사용하여 대화 시작
        currentDialogueIndex = index;

        if (currentDialogueIndex == 33)
        {
            map.SetActive(false);
            player.SetActive(false);
            Npc_Rayviyak.SetActive(false);
            garden.SetActive(true);
            isWaitingForPlayer = false; // 대기 상태 해제
            PrintCh1ProDialogue(currentDialogueIndex);
        }
    }

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

    public void DeactivateTalk()
    {
        this.gameObject.SetActive(false);
        isActivated = false;
    }

    public void CheckTalk(string location)
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

    public void EnablePlayerMovement()
    {
        playerController.StartMove(); // 플레이어 이동 활성화
    }

    public void DisablePlayerMovement()
    {
        playerController.StopMove(); // 플레이어 이동 비활성화
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