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

    public GameObject imageObj; // 초상화 이미지
    public GameObject nameObj; // 이름
    public GameObject bigImageObj; // 큰 이미지
    public GameObject playerImageObj; // 플레이어 이미지

    public GameObject letter; // 편지지 화면
    public TextMeshProUGUI letterText;

    public GameObject player; // 플레이어 캐릭터
    public GameObject map; // 맵

    public GameObject questObject; // 퀘스트 오브젝트
    public TextMeshProUGUI questText; // 퀘스트 내용 텍스트
    private bool isQuestActive = false; // 퀘스트 오브젝트가 활성화되었는지 여부

    public GameObject cafe; // 카페 화면
    public GameObject trainRoom; // 객실 화면
    public GameObject trainRoomHallway; // 객실 복도 화면
    public GameObject garden; // 정원 화면
    public GameObject bakery; // 빵집 화면
    public GameObject medicalRoom; // 의무실 화면
    public GameObject jazzBar; // 재즈바 화면
    public GameObject balcony; // 발코니 화면

    public GameObject Npc_Rayviyak; // 정원 npc
    public GameObject Npc_MrHam; // 병원 npc
    public GameObject Npc_Rusk; // 빵집 npc
    public GameObject Npc_Violet; // 바 npc

    public GameObject cheetahShopCh0; // 치타샵 UI

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
    private Dictionary<string, Sprite> characterBigImages; // 캐릭터 이름과 큰 이미지를 매핑하는 사전

    public bool isWaitingForPlayer = false; // 플레이어가 특정 위치에 도달할 때까지 기다리는 상태인지 여부

    void Awake()
    {
        ch1ProDialogue = new List<Ch1ProDialogue>();
        LoadDialogueFromCSV();
        InitializeCharacterImages(); 
        mapManager = map.GetComponent<Ch0MapManager>();
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
            // 퀘스트 ui 띄우는 코드
            if (isQuestActive)
            {
                questObject.SetActive(false);
                narration.SetActive(false);
                dialogue.SetActive(false);
                isQuestActive = false;
            }
            else
            {
                currentDialogueIndex++;
                PrintCh1ProDialogue(currentDialogueIndex);
            }
        }

        // 플레이어가 특정 위치에 도달했는지 확인하는 부분
        if (isWaitingForPlayer && mapManager != null)
        {
            if (mapManager.currentState == MapState.Cafe && mapManager.isInCafeBarZone && currentDialogueIndex == 5) // 인덱스 5일 때 카페바에 도착하면 스토리 다시 진행
            {
                isWaitingForPlayer = false;
                player.SetActive(false);
                map.SetActive(false);
                cafe.SetActive(true);
                currentDialogueIndex++;
                PrintCh1ProDialogue(currentDialogueIndex);
            }
            else if (mapManager.currentState == MapState.TrainRoom3 && currentDialogueIndex == 23) // 인덱스 23일 때 객실에 도착하면 편지지 띄우기
            {
                isWaitingForPlayer = false;
                player.SetActive(false);
                map.SetActive(false);
                trainRoom.SetActive(true);
                currentDialogueIndex++;
                PrintCh1ProDialogue(currentDialogueIndex);
            }
            else if (mapManager.currentState == MapState.Cafe && mapManager.isInCafeBarZone && currentDialogueIndex == 67) // 인덱스 67일 때 카페바에 도착하면 스토리 다시 진행
            {
                isWaitingForPlayer = false;
                player.SetActive(false);
                map.SetActive(false);
                cafe.SetActive(true);
                currentDialogueIndex++;
                PrintCh1ProDialogue(currentDialogueIndex);
            }
            else if (mapManager.currentState == MapState.TrainRoom3 && currentDialogueIndex == 101) // 인덱스 101 때 객실 도착하면 다시 스토리 진행
            {
                isWaitingForPlayer = false;
                player.SetActive(false);
                map.SetActive(false);
                trainRoom.SetActive(true);
                currentDialogueIndex++;
                PrintCh1ProDialogue(currentDialogueIndex);
            }
            else if (mapManager.currentState == MapState.Cafe && mapManager.isInCafeBarZone && currentDialogueIndex == 136) // 인덱스 136 때 카페바에 도착하면 스토리 다시 진행
            {
                isWaitingForPlayer = false;
                player.SetActive(false);
                map.SetActive(false);
                cafe.SetActive(true);
                currentDialogueIndex++;
                PrintCh1ProDialogue(currentDialogueIndex);
            }
            else if (mapManager.currentState == MapState.Balcony && currentDialogueIndex == 195) // 인덱스 195 때 발코니 도착하면 스토리 다시 진행
            {
                isWaitingForPlayer = false;
                player.SetActive(false);
                map.SetActive(false);
                balcony.SetActive(true);
                currentDialogueIndex++;
                PrintCh1ProDialogue(currentDialogueIndex);
            }
            else if (mapManager.currentState == MapState.Cafe && mapManager.isInCafeBarZone && currentDialogueIndex == 261) // 인덱스 261 때 카페바에 도착하면 스토리 다시 진행
            {
                isWaitingForPlayer = false;
                player.SetActive(false);
                map.SetActive(false);
                cafe.SetActive(true);
                currentDialogueIndex++;
                PrintCh1ProDialogue(currentDialogueIndex);
            }

            // 카페에서 일해야 되는데 다른 곳으로 가려고 하면 다시 카페로 플레이어 강제 이동
            if (mapManager.currentState != MapState.Cafe && (currentDialogueIndex == 5 || currentDialogueIndex == 67 || currentDialogueIndex == 136 || currentDialogueIndex == 261))
            {
                player.transform.position = new Vector3(0, 0, 0);
                narration.SetActive(true);
                dialogue.SetActive(false);
                narrationBar.SetDialogue("나레이션", "지금은 일할 시간이야.");
            }
        }
    }

    // csv 읽어오기
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

    // 이미지 가져오는 코드
    void InitializeCharacterImages()
    {
        characterImages = new Dictionary<string, Sprite>();
        characterBigImages = new Dictionary<string, Sprite>();

        // 초상화 이미지
        characterImages["솔"] = Resources.Load<Sprite>("PlayerImage/Sol");
        characterImages["루카스"] = Resources.Load<Sprite>("NpcImage/Lucas");
        characterImages["슬로우"] = Resources.Load<Sprite>("NpcImage/Slow");
        characterImages["가이"] = Resources.Load<Sprite>("NpcImage/Gai");
        characterImages["레이비야크"] = Resources.Load<Sprite>("NpcImage/Leviac");
        characterImages["바이올렛"] = Resources.Load<Sprite>("NpcImage/Violet");
        characterImages["파이아"] = Resources.Load<Sprite>("NpcImage/Fire");
        characterImages["러스크"] = Resources.Load<Sprite>("NpcImage/Rusk");

        // 전신 이미지
        characterBigImages["루카스"] = Resources.Load<Sprite>("NpcImage/Lucas_big");
        characterBigImages["슬로우"] = Resources.Load<Sprite>("NpcImage/Slow_big");
        characterBigImages["가이"] = Resources.Load<Sprite>("NpcImage/Gai_big");
        characterBigImages["레이비야크"] = Resources.Load<Sprite>("NpcImage/Leviac_full");
        characterBigImages["바이올렛"] = Resources.Load<Sprite>("NpcImage/Violet_full");
        characterBigImages["러스크"] = Resources.Load<Sprite>("NpcImage/Rusk_full");
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

        // 초상화 이미지
        if (imageObj.GetComponent<SpriteRenderer>() != null)
        {
            imageObj.GetComponent<SpriteRenderer>().sprite = characterSprite;
        }
        else if (imageObj.GetComponent<Image>() != null)
        {
            imageObj.GetComponent<Image>().sprite = characterSprite;
        }

        // 전신 이미지
        Sprite bigCharacterSprite = characterBigImages.ContainsKey(currentDialogue.speaker) ? characterBigImages[currentDialogue.speaker] : null;
        if (bigImageObj != null && bigCharacterSprite != null)
        {
            bigImageObj.GetComponent<Image>().sprite = bigCharacterSprite;
            bigImageObj.SetActive(true);
        }
        else
        {
            bigImageObj.SetActive(false);
        }

        // 플레이어 이미지
        if (currentDialogueIndex <= 5)
        {
            playerImageObj.SetActive(true);
        }
        else
        {
            playerImageObj.SetActive(false);
        }

        // 편지 띄우기
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
        
        if (index == 5) // 카페로 강제 이동 후 이동 가능하게 전환
        {
            player.transform.position = new Vector3(0, 0, 0);
            mapManager.currentState = MapState.Cafe;
            isWaitingForPlayer = true;
            player.SetActive(true);
            map.SetActive(true);
            EnablePlayerMovement();
            trainRoom.SetActive(false);
            narration.SetActive(false);
            dialogue.SetActive(false);
        }
        
        else if (index == 23 && mapManager.currentState == MapState.Cafe) // 카페 일 끝나고 이동 가능하게 전환
        {
            isWaitingForPlayer = true; // 플레이어가 특정 위치에 도달할 때까지 대기
            EnablePlayerMovement();
            map.SetActive(true);
            player.SetActive(true);
            cafe.SetActive(false);
            narration.SetActive(false);
            dialogue.SetActive(false);
        }
        else if (index == 32) // 퀘스트 활성화
        {
            string quest = currentDialogue.quest;
            string questContent = currentDialogue.questContent;

            questText.text = $"{quest}\n\n{questContent}";
            questObject.SetActive(true);
            map.SetActive(false);
            player.SetActive(false);
            Npc_Rayviyak.SetActive(false);
            isQuestActive = true;
        }
        else if (index == 33 && mapManager.currentState == MapState.TrainRoom3) // 퀘스트 받은 후 이동 가능하게 전환
        {
            isWaitingForPlayer = true;
            EnablePlayerMovement();
            map.SetActive(true);
            player.SetActive(true);
            trainRoom.SetActive(false);
            narration.SetActive(false);
            dialogue.SetActive(false);
            Npc_Rayviyak.SetActive(true);
        }
        else if (index == 37 && mapManager.currentState == MapState.Cafe) // 정원 npc와 대화 이후 이동 가능하게 전환
        {
            isWaitingForPlayer = true;
            EnablePlayerMovement();
            map.SetActive(true);
            player.SetActive(true);
            garden.SetActive(false);
            narration.SetActive(false);
            dialogue.SetActive(false);
            Npc_Violet.SetActive(true);
        }
        else if (index == 64) // 재즈바 npc와 대화 후 객실 자동 이동 및 맵상태 변경
        {
            player.transform.position = new Vector3(-44.5f, 9f, 0f);
            mapManager.currentState = MapState.TrainRoom3;
        }
        else if (index == 67) // 기상 후 플레이어 카페로 자동 이동 및 맵상태 변경
        {
            player.transform.position = new Vector3(0, 0, 0);
            mapManager.currentState = MapState.Cafe;
            isWaitingForPlayer = true;
            player.SetActive(true);
            map.SetActive(true);
            EnablePlayerMovement();
            trainRoom.SetActive(false); 
            narration.SetActive(false);
            dialogue.SetActive(false);
        }
        else if (index == 101 && mapManager.currentState == MapState.Cafe) // 카페 일 끝나고 이동 가능하게 전환
        {
            isWaitingForPlayer = true;
            EnablePlayerMovement();
            map.SetActive(true);
            player.SetActive(true);
            cafe.SetActive(false);
            narration.SetActive(false);
            dialogue.SetActive(false);
        }
        else if (index == 104 && mapManager.currentState == MapState.TrainRoom3) // npc와 대화를 위해 이동 가능하게 전환
        {
            isWaitingForPlayer = true;
            EnablePlayerMovement();
            map.SetActive(true);
            player.SetActive(true);
            trainRoom.SetActive(false);
            narration.SetActive(false);
            dialogue.SetActive(false);
            Npc_Rayviyak.SetActive(true);
        }
        else if (index == 133) // 정원 npc와 대화 후 객실 자동 이동 및 맵상태 변경
        {
            player.transform.position = new Vector3(-44.5f, 9f, 0f);
            mapManager.currentState = MapState.TrainRoom3;
        }
        else if (index == 136) // 기상 후 플레이어 카페로 자동 이동 및 맵상태 변경
        {
            player.transform.position = new Vector3(0, 0, 0);
            mapManager.currentState = MapState.Cafe;
            isWaitingForPlayer = true;
            player.SetActive(true);
            map.SetActive(true);
            EnablePlayerMovement();
            trainRoom.SetActive(false);
            narration.SetActive(false);
            dialogue.SetActive(false);
        }
        else if (index == 187 && mapManager.currentState == MapState.Cafe) // 카페 일 끝나고 이동 가능하게 전환
        {
            isWaitingForPlayer = true;
            EnablePlayerMovement();
            map.SetActive(true);
            player.SetActive(true);
            cafe.SetActive(false);
            narration.SetActive(false);
            dialogue.SetActive(false);
            Npc_Rayviyak.SetActive(true);
        }
        else if (index == 191 && mapManager.currentState == MapState.Cafe) // 정원 npc와 대화 이후 이동 가능하게 전환
        {
            isWaitingForPlayer = true;
            EnablePlayerMovement();
            map.SetActive(true);
            player.SetActive(true);
            garden.SetActive(false);
            narration.SetActive(false);
            dialogue.SetActive(false);
            Npc_Violet.SetActive(true);
        }
        else if (index == 195) // 특별상점까지 이동 가능하게 전환
        {
            player.transform.position = new Vector3(0, 0, 0);
            mapManager.currentState = MapState.Cafe;
            isWaitingForPlayer = true;
            EnablePlayerMovement();
            map.SetActive(true);
            player.SetActive(true);
            cafe.SetActive(false);
            narration.SetActive(false);
            dialogue.SetActive(false);
        }
        else if (index == 203) // 치타샵 ui 활성화
        {
            balcony.SetActive(false);
            narration.SetActive(false);
            dialogue.SetActive(false);
            cheetahShopCh0.SetActive(true);
        }
        else if (index == 204) // 치타샵 ui 비활성화
        {
            balcony.SetActive(true);
            narration.SetActive(true);
            dialogue.SetActive(true);
            cheetahShopCh0.SetActive(false);
        }
        else if (index == 207 && mapManager.currentState == MapState.Balcony) // 빵집까지 이동 가능하게 전환
        {            
            isWaitingForPlayer = true;
            EnablePlayerMovement();
            map.SetActive(true);
            player.SetActive(true);
            balcony.SetActive(false);
            narration.SetActive(false);
            dialogue.SetActive(false);
            Npc_Rusk.SetActive(true);
        }
        else if (index == 257) // 빵집 npc와 대화 후 객실 자동 이동 및 맵상태 변경
        {
            player.transform.position = new Vector3(-44.5f, 9f, 0f);
            mapManager.currentState = MapState.TrainRoom3;
        }
        else if (index == 261) // 기상 후 플레이어 카페로 자동 이동 및 맵상태 변경
        {
            player.transform.position = new Vector3(0, 0, 0);
            mapManager.currentState = MapState.Cafe;
            isWaitingForPlayer = true;
            player.SetActive(true);
            map.SetActive(true);
            EnablePlayerMovement();
            trainRoom.SetActive(false);
            narration.SetActive(false);
            dialogue.SetActive(false);
        }
        else if (index == 318 && mapManager.currentState == MapState.Cafe) // 카페 일 끝나고 이동 가능하게 전환
        {
            isWaitingForPlayer = true;
            EnablePlayerMovement();
            map.SetActive(true);
            player.SetActive(true);
            cafe.SetActive(false);
            narration.SetActive(false);
            dialogue.SetActive(false);
        }
        else
        {
            CheckTalk(currentDialogue.location);
        }
    }

    public void OnDialogueButtonClicked(int index)
    {
        currentDialogueIndex = index;

        if (currentDialogueIndex == 33)
        {
            map.SetActive(false);
            player.SetActive(false);
            Npc_Rayviyak.SetActive(false);
            garden.SetActive(true);
            isWaitingForPlayer = false;
            PrintCh1ProDialogue(currentDialogueIndex);
        }
        else if (currentDialogueIndex == 37)
        {
            map.SetActive(false);
            player.SetActive(false);
            Npc_Violet.SetActive(false);
            cafe.SetActive(true);
            dialogue.SetActive(true);
            isWaitingForPlayer = false;
            PrintCh1ProDialogue(currentDialogueIndex);
        }
        else if (currentDialogueIndex == 104)
        {
            map.SetActive(false);
            player.SetActive(false);
            Npc_Rayviyak.SetActive(false);
            garden.SetActive(true);
            isWaitingForPlayer = false;
            PrintCh1ProDialogue(currentDialogueIndex);
        }
        else if (currentDialogueIndex == 187)
        {
            map.SetActive(false);
            player.SetActive(false);
            Npc_Rayviyak.SetActive(false);
            garden.SetActive(true);
            isWaitingForPlayer = false;
            PrintCh1ProDialogue(currentDialogueIndex);
        }
        else if (currentDialogueIndex == 191)
        {
            map.SetActive(false);
            player.SetActive(false);
            Npc_Violet.SetActive(false);
            cafe.SetActive(true);
            dialogue.SetActive(true);
            isWaitingForPlayer = false;
            PrintCh1ProDialogue(currentDialogueIndex);
        }
        else if (currentDialogueIndex == 207)
        {
            map.SetActive(false);
            player.SetActive(false);
            Npc_Rusk.SetActive(false);
            bakery.SetActive(true);
            dialogue.SetActive(true);
            isWaitingForPlayer = false;
            PrintCh1ProDialogue(currentDialogueIndex);
        }
        else
        {
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
