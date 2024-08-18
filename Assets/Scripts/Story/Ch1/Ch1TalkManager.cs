using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Ch1TalkManager : MonoBehaviour
{
    public static Ch1TalkManager Instance { get; private set; }

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
    public bool isShopActive = false;

    public ScreenFader screenFader; // 페이드인/아웃 효과 스크립트
    private bool isFadingOut = false; // 페이드 아웃 중인지 여부 (페이드 아웃 중에는 입력 무시하기 위해)

    public Ch0DialogueBar dialogueBar; // 대화창 스크립트 (타이핑 효과 호출을 위해)
    public Ch0DialogueBar narrationBar; // 나레이션창 스크립트 (타이핑 효과 호출을 위해)

    public bool bedUsed = false; // 침대를 사용했는지 여부

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
    public Ch1MapManager mapManager; // 맵 매니저 참조

    public string currentMusic = ""; // 현재 재생 중인 음악의 이름을 저장

    private Dictionary<string, Sprite> characterImages; // 캐릭터 이름과 이미지를 매핑하는 사전
    private Dictionary<string, Sprite> characterBigImages; // 캐릭터 이름과 큰 이미지를 매핑하는 사전
    private Sprite characterSprite;

    public bool isWaitingForPlayer = false; // 플레이어가 특정 위치에 도달할 때까지 기다리는 상태인지 여부

    public bool isTransition = false;

    void Awake()
    {
        ch1ProDialogue = new List<Ch1ProDialogue>();
        LoadDialogueFromCSV();
        InitializeCharacterImages(); 
        mapManager = map.GetComponent<Ch1MapManager>();
        playerController = player.GetComponent<PlayerController>(); // 플레이어 컨트롤러 참조 설정
        player.SetActive(false);
    }

    void Start()
    {
        if (PlayerManager.Instance.GetIsLoaded())
        {
            currentDialogueIndex = PlayerManager.Instance.GetDialogueIdx();
            //Index만 설정하면 되는지는 잘 모르겠어서 이렇게 남겨둡니다
            //PlayerManager.Instance.SetIsLoaded();
            //DayNightCycleManager.Instance.LoadSaveData();
            //로드되었을 경우 플레이어가 리젠될 위치 정보를 선언하는 변수가 필요합니다
            //
        }
        else
        {
            ActivateTalk("객실");
        }
    }

    public void SetDialogueIndex(int index, bool isTransitionValue)
    {
        isTransition = isTransitionValue;
        currentDialogueIndex = index;
    }


    void Update()
    {
        if (isShopActive)
        {
            // Shop UI가 활성화된 동안 Update 함수의 나머지 처리 무시
            return;
        }

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
            // 카페바에 도착하면 스토리 다시 진행
            if (mapManager.currentState == MapState.Cafe && mapManager.isInCafeBarZone && (currentDialogueIndex == 5 || currentDialogueIndex == 67 || currentDialogueIndex == 136 || currentDialogueIndex == 261 || currentDialogueIndex == 348 || currentDialogueIndex == 391 || currentDialogueIndex == 431 || currentDialogueIndex == 496))
            {
                isWaitingForPlayer = false;
                player.SetActive(false);
                map.SetActive(false);
                cafe.SetActive(true);
                currentDialogueIndex++;
                PrintCh1ProDialogue(currentDialogueIndex);
                Npc_Rayviyak.SetActive(false);
                Npc_MrHam.SetActive(false);
                Npc_Rusk.SetActive(false);
                Npc_Violet.SetActive(false);
            }
            else if (mapManager.currentState == MapState.TrainRoom3 && currentDialogueIndex == 23)
            {
                isWaitingForPlayer = false;
                player.SetActive(false);
                map.SetActive(false);
                trainRoom.SetActive(true);
                currentDialogueIndex = 65;
                PrintCh1ProDialogue(currentDialogueIndex);
            }
            else if (mapManager.currentState == MapState.TrainRoom3 && currentDialogueIndex == 101)
            {
                isWaitingForPlayer = false;
                player.SetActive(false);
                map.SetActive(false);
                trainRoom.SetActive(true);
                currentDialogueIndex = 132;
                PrintCh1ProDialogue(currentDialogueIndex);
            }
            else if (mapManager.currentState == MapState.TrainRoom3 && currentDialogueIndex == 207)
            {
                isWaitingForPlayer = false;
                player.SetActive(false);
                map.SetActive(false);
                trainRoom.SetActive(true);
                currentDialogueIndex = 258;
                PrintCh1ProDialogue(currentDialogueIndex);
            }
            else if (mapManager.currentState == MapState.TrainRoom3 && currentDialogueIndex == 318)
            {
                isWaitingForPlayer = false;
                player.SetActive(false);
                map.SetActive(false);
                trainRoom.SetActive(true);
                currentDialogueIndex = 345;
                PrintCh1ProDialogue(currentDialogueIndex);
            }
            else if (mapManager.currentState == MapState.TrainRoom3 && currentDialogueIndex == 386)
            {
                isWaitingForPlayer = false;
                player.SetActive(false);
                map.SetActive(false);
                trainRoom.SetActive(true);
                currentDialogueIndex = 388;
                PrintCh1ProDialogue(currentDialogueIndex);
            }
            else if (mapManager.currentState == MapState.TrainRoom3 && currentDialogueIndex == 428)
            {
                isWaitingForPlayer = false;
                player.SetActive(false);
                map.SetActive(false);
                trainRoom.SetActive(true);
                currentDialogueIndex++;
                PrintCh1ProDialogue(currentDialogueIndex);
            }
            else if (currentDialogueIndex == 437)
            {
                currentDialogueIndex = 476;
                PrintCh1ProDialogue(currentDialogueIndex);
            }
            else if (mapManager.currentState == MapState.TrainRoom3 && currentDialogueIndex == 489)
            {
                isWaitingForPlayer = false;
                player.SetActive(false);
                map.SetActive(false);
                trainRoom.SetActive(true);
                currentDialogueIndex++;
                PrintCh1ProDialogue(currentDialogueIndex);
            }
            else if (mapManager.currentState == MapState.TrainRoom3 && currentDialogueIndex == 510)
            {
                isWaitingForPlayer = false;
                player.SetActive(false);
                map.SetActive(false);
                trainRoom.SetActive(true);
                currentDialogueIndex++;
                PrintCh1ProDialogue(currentDialogueIndex);
            }
            // 객실에 도착하면 스토리 다시 진행
            /*else if (mapManager.currentState == MapState.TrainRoom3 && (currentDialogueIndex == 23 || currentDialogueIndex == 101))
            {
                isWaitingForPlayer = false;
                player.SetActive(false);
                map.SetActive(false);
                trainRoom.SetActive(true);
                currentDialogueIndex++;
                PrintCh1ProDialogue(currentDialogueIndex);
            }*/
            else if (mapManager.currentState == MapState.Balcony && currentDialogueIndex == 187) // 발코니 도착하면 스토리 다시 진행
            {
                isWaitingForPlayer = false;
                player.SetActive(false);
                map.SetActive(false);
                balcony.SetActive(true);
                currentDialogueIndex = 196;
                PrintCh1ProDialogue(currentDialogueIndex);
            }
            else if (mapManager.currentState == MapState.Balcony && currentDialogueIndex == 420) // 발코니 도착하면 스토리 다시 진행
            {
                isWaitingForPlayer = false;
                player.SetActive(false);
                map.SetActive(false);
                balcony.SetActive(true);
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

    // Shop UI를 여는 부분에서 호출
    public void OnShopOpened()
    {
        isShopActive = true;
    }

    // Shop UI가 닫혔음을 알리는 메서드
    public void OnShopClosed()
    {
        isShopActive = false;
        balcony.SetActive(true);
        cheetahShopCh0.SetActive(false);
        currentDialogueIndex += 2;
        PrintCh1ProDialogue(currentDialogueIndex);
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
        characterImages = new Dictionary<string, Sprite>
        {
            // 기본 캐릭터 이미지
            ["솔"] = Resources.Load<Sprite>("PlayerImage/Sol"),
            ["레이비야크"] = Resources.Load<Sprite>("NpcImage/Leviac"),
            ["바이올렛"] = Resources.Load<Sprite>("NpcImage/Violet"),
            ["러스크"] = Resources.Load<Sprite>("NpcImage/Rusk"),
            ["Mr. Ham"] = Resources.Load<Sprite>("NpcImage/MrHam"),

            // 솔 표정 이미지
            ["솔_일반"] = Resources.Load<Sprite>("PlayerImage/Sol"),
            ["솔_놀람"] = Resources.Load<Sprite>("PlayerImage/놀람"),
            ["솔_슬픔"] = Resources.Load<Sprite>("PlayerImage/눈물"),
            ["솔_당황"] = Resources.Load<Sprite>("PlayerImage/당황"),
            ["솔_웃음"] = Resources.Load<Sprite>("PlayerImage/웃음"),
            ["솔_화남"] = Resources.Load<Sprite>("PlayerImage/화남"),

            // 레이비야크 표정 이미지
            ["레이비야크_일반"] = Resources.Load<Sprite>("NpcImage/Leviac"),
            ["레이비야크_웃음"] = Resources.Load<Sprite>("NpcImage/Leviac_웃음"),

            // 바이올렛 표정 이미지
            ["바이올렛_일반"] = Resources.Load<Sprite>("NpcImage/Violet"),
            ["바이올렛_웃음"] = Resources.Load<Sprite>("NpcImage/Violet_웃음"),
            ["바이올렛_윙크"] = Resources.Load<Sprite>("NpcImage/Violet_윙크"),

            // 러스크 표정 이미지
            ["러스크_일반"] = Resources.Load<Sprite>("NpcImage/Rusk"),
            ["러스크_웃음"] = Resources.Load<Sprite>("NpcImage/Rusk_웃음"),

            // Mr. Ham 표정 이미지
            ["Mr. Ham_일반"] = Resources.Load<Sprite>("NpcImage/MrHam"),
            ["Mr. Ham_웃음"] = Resources.Load<Sprite>("NpcImage/MrHam_웃음"),
            ["Mr. Ham_화남"] = Resources.Load<Sprite>("NpcImage/MrHam_화남"),
            ["Mr. Ham_아쉬움"] = Resources.Load<Sprite>("NpcImage/MrHam_아쉬움"),

            // 루카스 표정 이미지
            ["루카스_일반"] = Resources.Load<Sprite>("NpcImage/Lucas"),
            ["루카스_곤란"] = Resources.Load<Sprite>("NpcImage/Lucas_곤란"),

            // 슬로우 표정 이미지
            ["슬로우_일반"] = Resources.Load<Sprite>("NpcImage/Slow"),

            // 가이 표정 이미지
            ["가이_일반"] = Resources.Load<Sprite>("NpcImage/Gai"),

            // 기본 NPC 이미지
            ["Default"] = Resources.Load<Sprite>("NpcImage/Default")
        };

        characterBigImages = new Dictionary<string, Sprite>
        {
            ["솔"] = Resources.Load<Sprite>("NpcImage/Sol"),
            ["레이비야크"] = Resources.Load<Sprite>("NpcImage/Leviac_full"),
            ["바이올렛"] = Resources.Load<Sprite>("NpcImage/Violet_full"),
            ["러스크"] = Resources.Load<Sprite>("NpcImage/Rusk_full"),
            ["Mr. Ham"] = Resources.Load<Sprite>("NpcImage/MrHam_full"),
            ["루카스"] = Resources.Load<Sprite>("NpcImage/Lucas_big"),
            ["슬로우"] = Resources.Load<Sprite>("NpcImage/Slow_big"),
            ["가이"] = Resources.Load<Sprite>("NpcImage/Gai_big"),
            ["Default"] = Resources.Load<Sprite>("NpcImage/Default")
        };
    }

    public void PrintCh1ProDialogue(int index)
    {
        if (index >= ch1ProDialogue.Count)
        {
            narration.SetActive(false);
            dialogue.SetActive(false);
            bigImageObj.SetActive(false); // 대화가 끝날 때 bigImageObj를 비활성화
            return;
        }

        Ch1ProDialogue currentDialogue = ch1ProDialogue[index];

        string expressionKey = !string.IsNullOrEmpty(currentDialogue.expression) ? $"_{currentDialogue.expression}" : "";
        string speakerKey = currentDialogue.speaker;

        // 인물과 표정을 포함한 최종 키 생성
        string finalKey = speakerKey + expressionKey;

        if (characterImages.ContainsKey(finalKey))
        {
            characterSprite = characterImages[finalKey];
        }
        else
        {
            // 해당사항 없는 경우 기본 이미지 사용
            characterSprite = characterImages.ContainsKey(speakerKey)
                ? characterImages[speakerKey]
                : characterImages["Default"];
        }

        // Set regular image
        if (imageObj.GetComponent<SpriteRenderer>() != null)
        {
            imageObj.GetComponent<SpriteRenderer>().sprite = characterSprite;
        }
        else if (imageObj.GetComponent<Image>() != null)
        {
            imageObj.GetComponent<Image>().sprite = characterSprite;
        }

        // Set big image (화자가 '솔'이 아닐 때만 활성화)
        if (speakerKey != "솔")
        {
            if (characterBigImages.ContainsKey(speakerKey))
            {
                bigImageObj.GetComponent<Image>().sprite = characterBigImages[speakerKey];
            }
            else
            {
                bigImageObj.GetComponent<Image>().sprite = characterBigImages["Default"];
            }
            bigImageObj.SetActive(true); // 화자가 '솔'이 아닐 때 bigImageObj를 활성화
        }
        else
        {
            bigImageObj.SetActive(false); // 화자가 '솔'일 때 bigImageObj를 비활성화
        }

        // 플레이어 이미지 처리
        playerImageObj.SetActive(currentDialogueIndex <= 5);

        // 편지 띄우기
        if (currentDialogue.speaker == letterSpeaker)
        {
            narration.SetActive(false);
            dialogue.SetActive(false);
            bigImageObj.SetActive(false); // 편지 화면에서는 bigImageObj를 비활성화
            letter.SetActive(true);
            letterText.text += string.IsNullOrEmpty(letterText.text) ? currentDialogue.line : "\n" + currentDialogue.line;
        }
        else if (string.IsNullOrEmpty(currentDialogue.speaker) && string.IsNullOrEmpty(currentDialogue.location))
        {
            narration.SetActive(false);
            dialogue.SetActive(false);
            bigImageObj.SetActive(false); // 대화가 없을 때 bigImageObj를 비활성화
        }
        else if (currentDialogue.speaker == narrationSpeaker || string.IsNullOrEmpty(currentDialogue.speaker))
        {
            narration.SetActive(true);
            dialogue.SetActive(false);
            bigImageObj.SetActive(false); // 나레이션에서는 bigImageObj를 비활성화
            narrationBar.SetDialogue(currentDialogue.speaker, currentDialogue.line); // 타이핑 효과 적용
        }
        else
        {
            narration.SetActive(false);
            dialogue.SetActive(true);
            dialogueBar.SetDialogue(currentDialogue.speaker, currentDialogue.line); // 타이핑 효과 적용
        }

        if (index == 5 || index == 67 || index == 136 || index == 261 || index == 348 || index == 391 || index == 431 || index == 496) // 카페로 강제 이동 후 이동 가능하게 전환
        {
            player.transform.position = new Vector3(0, 0, 0);
            mapManager.currentState = MapState.Cafe;
            isWaitingForPlayer = true;
            player.SetActive(true);
            map.SetActive(true);
            playerController.StartMove();
            trainRoom.SetActive(false);
            narration.SetActive(false);
            dialogue.SetActive(false);
        }        
        else if ((index == 23 || index == 101 || index == 187 || index == 318 || index == 386 || index == 420 || index == 489 | index == 510) && mapManager.currentState == MapState.Cafe) // 카페 일 끝나고 이동 가능하게 전환
        {
            isWaitingForPlayer = true;
            playerController.StartMove();
            map.SetActive(true);
            player.SetActive(true);
            cafe.SetActive(false);
            narration.SetActive(false);
            dialogue.SetActive(false);
            Npc_Rayviyak.SetActive(true);
            Npc_MrHam.SetActive(true);
            Npc_Rusk.SetActive(true);
            Npc_Violet.SetActive(true);
        }
        /*else if (index == 32) // 퀘스트 활성화
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
            playerController.StartMove();
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
            playerController.StartMove();
            map.SetActive(true);
            player.SetActive(true);
            garden.SetActive(false);
            narration.SetActive(false);
            dialogue.SetActive(false);
            Npc_Violet.SetActive(true);
        }
        else if (index == 64) // 퀘스트 UI 띄우고 비밀퀘스트2 활성화
        {
            player.transform.position = new Vector3(-44.5f, 9f, 0f);
            mapManager.currentState = MapState.TrainRoom3;

            string quest = currentDialogue.quest;
            string questContent = currentDialogue.questContent;

            QuestManager.Instance.CompleteQuest("비밀퀘스트1"); // 기존 퀘스트 완료 처리
            QuestManager.Instance.AddQuest(quest, questContent); // 새로운 퀘스트 추가

            questText.text = $"비밀 퀘스트 2\n\n편지를 남긴 사람은 누구일까요? 정원사는 그에 대해 알지도 모릅니다. 정원에서 정보를 얻어봅시다.";
            questObject.SetActive(true);
            map.SetActive(false);
            player.SetActive(false);
            isQuestActive = true;
        }
        else if (index == 67 || index == 136) // 기상 후 플레이어 카페로 자동 이동 및 맵상태 변경
        {
            player.transform.position = new Vector3(0, 0, 0);
            mapManager.currentState = MapState.Cafe;
            isWaitingForPlayer = true;
            player.SetActive(true);
            map.SetActive(true);
            playerController.StartMove();
            trainRoom.SetActive(false); 
            narration.SetActive(false);
            dialogue.SetActive(false);
        }
        else if (index == 101 && mapManager.currentState == MapState.Cafe) // 카페 일 끝나고 이동 가능하게 전환
        {
            isWaitingForPlayer = true;
            playerController.StartMove();
            map.SetActive(true);
            player.SetActive(true);
            cafe.SetActive(false);
            narration.SetActive(false);
            dialogue.SetActive(false);
        }
        else if (index == 104 && mapManager.currentState == MapState.TrainRoom3) // npc와 대화를 위해 이동 가능하게 전환
        {
            isWaitingForPlayer = true;
            playerController.StartMove();
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
            playerController.StartMove();
            trainRoom.SetActive(false);
            narration.SetActive(false);
            dialogue.SetActive(false);
        }
        else if (index == 187 && mapManager.currentState == MapState.Cafe) // 카페 일 끝나고 이동 가능하게 전환
        {
            isWaitingForPlayer = true;
            playerController.StartMove();
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
            playerController.StartMove();
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
            playerController.StartMove();
            map.SetActive(true);
            player.SetActive(true);
            cafe.SetActive(false);
            narration.SetActive(false);
            dialogue.SetActive(false);
        }*/
        else if (index == 203 || index == 426) // 치타샵 ui 활성화
        {
            // Shop UI를 표시
            cheetahShopCh0.SetActive(true);
            OnShopOpened(); // Shop UI가 열렸음을 기록

            // 대화를 임시로 숨기기
            balcony.SetActive(false);
            narration.SetActive(false);
            dialogue.SetActive(false);
        }
        else if ((index == 207 || index == 428) && mapManager.currentState == MapState.Balcony) // 이동 가능하게 전환
        {            
            isWaitingForPlayer = true;
            playerController.StartMove();
            map.SetActive(true);
            player.SetActive(true);
            balcony.SetActive(false);
            narration.SetActive(false);
            dialogue.SetActive(false);
            Npc_Rayviyak.SetActive(true);
            Npc_MrHam.SetActive(true);
            Npc_Rusk.SetActive(true);
            Npc_Violet.SetActive(true);
        }
        /*else if (index == 257) // 빵집 npc와 대화 후 객실 자동 이동 및 맵상태 변경
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
            playerController.StartMove();
            trainRoom.SetActive(false);
            narration.SetActive(false);
            dialogue.SetActive(false);
        }
        else if (index == 318 && mapManager.currentState == MapState.Cafe) // 카페 일 끝나고 이동 가능하게 전환
        {
            isWaitingForPlayer = true;
            playerController.StartMove();
            map.SetActive(true);
            player.SetActive(true);
            cafe.SetActive(false);
            narration.SetActive(false);
            dialogue.SetActive(false);
            Npc_Rayviyak.SetActive(true);
        }
        else if (index == 322 && mapManager.currentState == MapState.Cafe) // 정원 npc와 대화 이후 이동 가능하게 전환
        {
            isWaitingForPlayer = true;
            playerController.StartMove();
            map.SetActive(true);
            player.SetActive(true);
            garden.SetActive(false);
            narration.SetActive(false);
            dialogue.SetActive(false);
            Npc_Violet.SetActive(true);
        }
        else if (index == 326 && mapManager.currentState == MapState.Cafe) // 바 npc와 대화 이후 이동 가능하게 전환
        {
            isWaitingForPlayer = true;
            playerController.StartMove();
            map.SetActive(true);
            player.SetActive(true);
            cafe.SetActive(false);
            narration.SetActive(false);
            dialogue.SetActive(false);
            Npc_Rusk.SetActive(true);
        }
        else if (index == 330 && mapManager.currentState == MapState.Bakery) // 빵집 npc와 대화 이후 이동 가능하게 전환
        {
            isWaitingForPlayer = true;
            playerController.StartMove();
            map.SetActive(true);
            player.SetActive(true);
            bakery.SetActive(false);
            narration.SetActive(false);
            dialogue.SetActive(false);
            Npc_MrHam.SetActive(true);
        }*/
        else
        {
            CheckTalk(currentDialogue.location);
        }
    }

    public void OnDialogueButtonClicked(int index)
    {
        /*currentDialogueIndex = index;

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
        else if (currentDialogueIndex == 318)
        {
            map.SetActive(false);
            player.SetActive(false);
            Npc_Rayviyak.SetActive(false);
            garden.SetActive(true);
            isWaitingForPlayer = false;
            PrintCh1ProDialogue(currentDialogueIndex);
        }
        else if (currentDialogueIndex == 322)
        {
            map.SetActive(false);
            player.SetActive(false);
            Npc_Violet.SetActive(false);
            cafe.SetActive(true);
            dialogue.SetActive(true);
            isWaitingForPlayer = false;
            PrintCh1ProDialogue(currentDialogueIndex);
        }
        else if (currentDialogueIndex == 326)
        {
            map.SetActive(false);
            player.SetActive(false);
            Npc_Rusk.SetActive(false);
            bakery.SetActive(true);
            dialogue.SetActive(true);
            isWaitingForPlayer = false;
            PrintCh1ProDialogue(currentDialogueIndex);
        }
        else if (currentDialogueIndex == 330)
        {
            map.SetActive(false);
            player.SetActive(false);
            Npc_MrHam.SetActive(false);
            medicalRoom.SetActive(true);
            dialogue.SetActive(true);
            isWaitingForPlayer = false;
            PrintCh1ProDialogue(currentDialogueIndex);
        }
        else
        {
            PrintCh1ProDialogue(currentDialogueIndex);
        }*/
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
                PlayMusic(locationMedicalRoom);
                medicalRoom.SetActive(true);
                break;

            case locationGarden:
                PlayMusic(locationGarden);
                garden.SetActive(true);
                break;

            case locationBakery:
                PlayMusic(locationBakery);
                bakery.SetActive(true);
                break;

            case locationJazzBar:
                jazzBar.SetActive(true);
                break;

            case locationCafe:
                PlayMusic(locationCafe);
                cafe.SetActive(true);
                break;
        }

        if (currentDialogueIndex > ch1ProDialogue.Count)
        {
            DeactivateTalk();
        }
    }

    public void PlayMusic(string location = null)
    {
        string newMusic = ""; // 재생할 음악 이름

        // 대사 상의 location에 따른 음악 설정
        switch (location)
        {
            case locationCafe:
                newMusic = "CAFE";
                break;
            case locationGarden:
                newMusic = "GARDEN";
                break;
            case locationBakery:
                newMusic = "BAKERY";
                break;
            case locationMedicalRoom:
                newMusic = "amedicaloffice_001";
                break;
            case locationTrainRoom:
                newMusic = "a room";
                break;
            default:
                newMusic = "CAFE";
                break;
        }

        // 새로운 음악이 현재 음악과 다를 경우에만 음악 재생
        if (currentMusic != newMusic)
        {
            SoundManager.Instance.PlayMusic(newMusic, loop: true);
            currentMusic = newMusic;
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
