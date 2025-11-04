using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;
public class Ch1TalkManager : MonoBehaviour
{
    public static Ch1TalkManager Instance { get; private set; }
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

    public GameObject destPointObject;

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

    private bool isActivated = false; // TalkManager가 활성화되었는지 여부

    public PlayerController playerController; // 플레이어 컨트롤러 참조
    public Ch1MapManager mapManager; // 맵 매니저 참조

    public string currentMusic = ""; // 현재 재생 중인 음악의 이름을 저장

    private Dictionary<string, Sprite> characterImages; // 캐릭터 이름과 이미지를 매핑하는 사전
    private Dictionary<string, Sprite> characterBigImages; // 캐릭터 이름과 큰 이미지를 매핑하는 사전
    private Sprite characterSprite;

    public bool isWaitingForPlayer = false; // 플레이어가 특정 위치에 도달할 때까지 기다리는 상태인지 여부

    public bool isTransition = false;

    public string speakerKey;

    public List<DialogueNode> dialogueNodes;
    public Dictionary<int, DialogueNode> nodeById;
    public DialogueNode currentNode;

    void Awake()
    {
        Instance = this;
        LoadDialogueFromCSV();
        InitializeCharacterImages();
        
        mapManager = map.GetComponent<Ch1MapManager>();
        playerController = player.GetComponent<PlayerController>(); // 플레이어 컨트롤러 참조 설정
        player.SetActive(false);
    }

    void Start()
    {
        if (isTransition)
        {
            int returnNodeId = SceneTransitionManager.Instance.returnNodeId; 
            if (nodeById.TryGetValue(returnNodeId, out var n))
                ActivateTalk("카페", returnNodeId);
            else
                ActivateTalk("카페", FindFirstNodeIdByLocation("카페"));
            return;
        }

        // 세이브 복구일 경우
        if (PlayerManager.Instance.GetIsLoaded())
        {
            int savedId = PlayerManager.Instance.GetDialogueIdx();
            if (nodeById.TryGetValue(savedId, out var node))
            {
                ActivateTalk(node.data.location, savedId);
                return;
            }
        }

        ActivateTalk("객실", FindFirstNodeIdByLocation("객실"));
    }

    private int FindFirstNodeIdByLocation(string location)
    {
        if (dialogueNodes != null)
        {
            var node = dialogueNodes.Find(n => n.data.location == location);
            if (node != null)
                return node.nodeId;
        }

        // 만약 해당 location이 없으면 전체 대사 중 첫 노드를 반환 (안전 폴백)
        return (dialogueNodes != null && dialogueNodes.Count > 0)
            ? dialogueNodes[0].nodeId
            : 0;
    }


    public void SetDialogueIndex(int nodeId, bool isTransitionValue)
    {
        isTransition = isTransitionValue;

        if (nodeById == null)
        {
            Debug.LogError("[TalkManager] nodeById is NULL.");
            return;
        }
        if (!nodeById.TryGetValue(nodeId, out DialogueNode targetNode))
        {
            Debug.LogError($"[TalkManager] Node ID {nodeId} not found in nodeById.");
            return;
        }

        currentNode = targetNode;
    }


    void Update()
    {
        if (isShopActive || SceneTransitionManager.Instance.isTransitioning) // 씬 전환 중에는 입력 무시
        {
            return;
        }

        // 인덱스가 462인 경우 499로 이동
        if (currentNode != null && currentNode.nodeId == 462)
        {
            if (nodeById.TryGetValue(499, out var jump))
            {
                currentNode = jump;
                PrintNode(currentNode);
            }
        }

        if (isActivated && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && !isWaitingForPlayer)
        {
            if (isQuestActive)
            {
                // 퀘스트 UI를 비활성화
                questObject.SetActive(false);
                narration.SetActive(false);
                dialogue.SetActive(false);
                isQuestActive = false;
            }

            bool anyTyping = false;

            // 순서대로 확인
            if (narration != null && narration.GetComponentInChildren<Ch0DialogueBar>().IsTyping())
            {
                narration.GetComponentInChildren<Ch0DialogueBar>().CompleteTypingEffect();
                anyTyping = true;
            }

            if (dialogue != null && dialogue.GetComponentInChildren<Ch0DialogueBar>().IsTyping())
            {
                dialogue.GetComponentInChildren<Ch0DialogueBar>().CompleteTypingEffect();
                anyTyping = true;
            }

            if (currentNode.nodeId == 5)
                destPointObject.SetActive(true);
            else
                destPointObject.SetActive(false);

            if (!anyTyping && currentNode != null)
            {
                // 현재 노드에서 주문/씬 전환/특수 이벤트 처리
                HandleDialogueProgression(currentNode.nodeId);

                //씬 전환중일 경우 즉시 리턴
                if (SceneTransitionManager.Instance.isTransitioning)
                    return;

                // 다음 노드 진행 
                if (currentNode.nextNodes != null && currentNode.nextNodes.Count > 0)
                {
                    currentNode = currentNode.nextNodes[0];
                    PrintNode(currentNode);
                }
                else
                {
                    // 다음 노드가 없음
                    DeactivateTalk();
                }
            }
        }

        // 플레이어가 특정 위치에 도달했는지 확인하는 부분
        if (isWaitingForPlayer && mapManager != null && currentNode != null)
        {
            int id = currentNode.nodeId;
            // 카페바에 도착하면 스토리 다시 진행
            if (mapManager.currentState == MapState.Cafe
                && mapManager.isInCafeBarZone
                && (id == 5 || id == 76 || id == 152 || id == 280 || id == 371 || id == 416 || id == 456 || id == 518))
            {
                destPointObject.SetActive(false);
                isWaitingForPlayer = false;
                player.SetActive(false);
                map.SetActive(false);
                cafe.SetActive(true);

                if (currentNode.nextNodes.Count > 0)
                    currentNode = currentNode.nextNodes[0];
                PrintNode(currentNode);

                Npc_Rayviyak.SetActive(false);
                Npc_MrHam.SetActive(false);
                Npc_Rusk.SetActive(false);
                Npc_Violet.SetActive(false);
            }
            // 객실에 도착하면 스토리 다시 진행
            /*else if (mapManager.currentState == MapState.TrainRoom3 
                        && (id == 29 || id == 101))
            {
                isWaitingForPlayer = false;
                player.SetActive(false);
                map.SetActive(false);
                trainRoom.SetActive(true);

                if (currentNode.nextNodes.Count > 0)
                    currentNode = currentNode.nextNodes[0];
                PrintNode(currentNode);
            }*/
            else if (mapManager.currentState == MapState.Balcony
                    && id == 205) // 발코니 도착하면 스토리 다시 진행
            {
                isWaitingForPlayer = false;
                player.SetActive(false);
                map.SetActive(false);
                balcony.SetActive(true);

                if (nodeById.TryGetValue(215, out var node209))
                    currentNode = node209;
                PrintNode(currentNode);
            }
            else if (mapManager.currentState == MapState.Balcony
                    && id == 452) // 발코니 도착하면 스토리 다시 진행
            {
                isWaitingForPlayer = false;
                player.SetActive(false);
                map.SetActive(false);
                balcony.SetActive(true);

                if (nodeById.TryGetValue(453, out var node209))
                    currentNode = node209;
                PrintNode(currentNode);
                PrintNode(currentNode);
            }

            // 카페에서 일해야 되는데 다른 곳으로 가려고 하면 다시 카페로 플레이어 강제 이동
            if (mapManager.currentState != MapState.Cafe 
                && (id == 5 || id == 76 || id == 152 || id == 274 || id == 364 || id == 409 || id == 456))
            {
                player.transform.position = new Vector3(0, 0, 0);
                narration.SetActive(true);
                dialogue.SetActive(false);
                narrationBar.SetDialogue("나레이션", "지금은 일할 시간이야.");
            }
        }
    }

    private void HandleDialogueProgression(int id)
    {
        List<CafeOrder> orders;
        switch (id)
        {
            case 6: // 룸서비스 랜덤 3건
                Debug.Log("배달 랜덤 룸서비스 주문 3건");
                SceneTransitionManager.Instance.HandleDialogueTransition("ch1Scene", "CafeScene", 8, 3);
                break;

            case 11: // 에스프레소 1잔 직접 주문
                orders = new List<CafeOrder>();
                orders.Add(new CafeOrder("Espresso"));
                SceneTransitionManager.Instance.HandleDialogueTransition("ch1Scene", "CafeScene", 13, orders);
                break;

            case 14: // 랜덤 주문 2건 14
                SceneTransitionManager.Instance.HandleRandomMenuTransition("ch1Scene", "CafeScene", 16, 2);
                break;

            case 19: // 따아 1잔 직접 주문 
                orders = new List<CafeOrder>();
                orders.Add(new CafeOrder("HotAmericano"));
                SceneTransitionManager.Instance.HandleDialogueTransition("ch1Scene", "CafeScene", 21, orders);
                break;

            case 22: // 랜덤 주문 1건
                SceneTransitionManager.Instance.HandleRandomMenuTransition("ch1Scene", "CafeScene", 24, 1);
                break;

            case 26: // 아아 1잔 직접 주문, 원래 25
                orders = new List<CafeOrder>();
                orders.Add(new CafeOrder("IceAmericano"));
                SceneTransitionManager.Instance.HandleDialogueTransition("ch1Scene", "CafeScene", 28, orders);
                break;

            case 29: // 랜덤 주문 1건//원래26
                SceneTransitionManager.Instance.HandleRandomMenuTransition("ch1Scene", "CafeScene", 31, 1);
                Debug.Log(currentNode.nodeId);
                break;

            case 77: // 룸서비스 랜덤 4건(3건 수정)
                Debug.Log("배달 랜덤 룸서비스 주문 4건");
                SceneTransitionManager.Instance.HandleDialogueTransition("ch1Scene", "CafeScene", 80, 3);
                break;

            case 80: // 랜덤 주문 1건
                SceneTransitionManager.Instance.HandleRandomMenuTransition("ch1Scene", "CafeScene", 82, 1);
                break;

            case 82: // 따아 1잔 직접 주문
                orders = new List<CafeOrder>();
                orders.Add(new CafeOrder("HotAmericano"));
                SceneTransitionManager.Instance.HandleDialogueTransition("ch1Scene", "CafeScene", 84, orders);
                break;

            case 96:  // 랜덤 주문 1건
                SceneTransitionManager.Instance.HandleRandomMenuTransition("ch1Scene", "CafeScene", 98, 1);
                break;

            case 98: // 에스프레소 1잔 직접 주문
                orders = new List<CafeOrder>();
                orders.Add(new CafeOrder("Espresso"));
                SceneTransitionManager.Instance.HandleDialogueTransition("ch1Scene", "CafeScene", 100, orders);
                break;

            case 114: // 랜덤 주문 2건
                SceneTransitionManager.Instance.HandleRandomMenuTransition("ch1Scene", "CafeScene", 116, 2);
                break;

            case 153: // 룸서비스 랜덤 3건
                Debug.Log("배달 랜덤 룸서비스 주문 3건");
                SceneTransitionManager.Instance.HandleDialogueTransition("ch1Scene", "CafeScene", 156, 3);
                break;

            case 156: // 랜덤 주문 2건
                SceneTransitionManager.Instance.HandleRandomMenuTransition("ch1Scene", "CafeScene", 158, 2);
                break;

            case 158: // 에스프레소 1잔 직접 주문
                orders = new List<CafeOrder>();
                orders.Add(new CafeOrder("Espresso"));
                SceneTransitionManager.Instance.HandleDialogueTransition("ch1Scene", "CafeScene", 160, orders);
                break;

            case 167: // 랜덤 주문 3건
                SceneTransitionManager.Instance.HandleRandomMenuTransition("ch1Scene", "CafeScene", 169, 3);
                break;

            case 169: // 아아 1잔 직접 주문
                orders = new List<CafeOrder>();
                orders.Add(new CafeOrder("IceAmericano"));
                SceneTransitionManager.Instance.HandleDialogueTransition("ch1Scene", "CafeScene", 171, orders);
                break;

            case 281: // 룸서비스 랜덤 3건
                Debug.Log("배달 랜덤 룸서비스 주문 3건");
                SceneTransitionManager.Instance.HandleDialogueTransition("ch1Scene", "CafeScene", 284, 3);
                break;

            case 284: // 아아 1잔 직접 주문
                orders = new List<CafeOrder>();
                orders.Add(new CafeOrder("IceAmericano"));
                SceneTransitionManager.Instance.HandleDialogueTransition("ch1Scene", "CafeScene", 286, orders);
                break;

            case 310: // 랜덤 주문 4건
                SceneTransitionManager.Instance.HandleRandomMenuTransition("ch1Scene", "CafeScene", 312, 4);
                break;

            case 312: // 에스프레소 1잔 직접 주문
                orders = new List<CafeOrder>();
                orders.Add(new CafeOrder("Espresso"));
                SceneTransitionManager.Instance.HandleDialogueTransition("ch1Scene", "CafeScene", 314, orders);
                break;

            case 338: // 랜덤 주문 1건
                SceneTransitionManager.Instance.HandleRandomMenuTransition("ch1Scene", "CafeScene", 340, 1);
                break;

            case 372: // 룸서비스 랜덤 3건
                Debug.Log("배달 랜덤 룸서비스 주문 3건");
                SceneTransitionManager.Instance.HandleDialogueTransition("ch1Scene", "CafeScene", 375, 3);
                break;

            case 375: // 랜덤 주문 5건
                SceneTransitionManager.Instance.HandleRandomMenuTransition("ch1Scene", "CafeScene", 377, 5);
                break;

            case 377: // 따아 1잔 직접 주문
                orders = new List<CafeOrder>();
                orders.Add(new CafeOrder("HotAmericano"));
                SceneTransitionManager.Instance.HandleDialogueTransition("ch1Scene", "CafeScene", 379, orders);
                break;

            case 408: // 아아 1잔 직접 주문            
                orders = new List<CafeOrder>();
                orders.Add(new CafeOrder("IceAmericano"));
                SceneTransitionManager.Instance.HandleDialogueTransition("ch1Scene", "CafeScene", 410, orders);
                break;

            case 411: // 룸서비스 랜덤 5건
                Debug.Log("배달 랜덤 룸서비스 주문 5건");
                SceneTransitionManager.Instance.HandleDialogueTransition("ch1Scene", "CafeScene", 413, 5);
                break;

            case 417: // 랜덤 주문 4건
                SceneTransitionManager.Instance.HandleRandomMenuTransition("ch1Scene", "CafeScene", 420, 4);
                break;

            case 422: // 에스프레소 1잔 직접 주문
                orders = new List<CafeOrder>();
                orders.Add(new CafeOrder("Espresso"));
                SceneTransitionManager.Instance.HandleDialogueTransition("ch1Scene", "CafeScene", 424, orders);
                break;

            default:
                // 기본 대화 진행
                PrintNode(currentNode);
                break;
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
        JumpToNode(currentNode.nodeId + 2);
        PrintNode(currentNode);
    }

    private bool TryJumpToNode(int nodeId)
    {
        if (nodeById != null && nodeById.TryGetValue(nodeId, out var n))
        {
            currentNode = n;
            PrintNode(currentNode);
            return true;
        }
        return false;
    }
    
    private void JumpToNode(int nodeId)
    {
        if (nodeById != null && nodeById.TryGetValue(nodeId, out var n))
        {
            currentNode = n;
            return;
        }
    }

    // csv 읽어오기
    void LoadDialogueFromCSV()
    {
        nodeById = new Dictionary<int, DialogueNode>();
        dialogueNodes = new List<DialogueNode>();
        List<Dictionary<string, object>> datas = Ch0CSVReader.Read("Travel_Around_The_World_CH1");

        for (int i = 0; i < datas.Count; i++)
        {
            var data = datas[i];
            string dayString = data["일자"].ToString();
            int day = int.Parse(System.Text.RegularExpressions.Regex.Match(dayString, @"\d+").Value);
            string location = data["장소"].ToString();
            string speaker = data["인물"].ToString();
            string line = data["대사"].ToString();
            string screenEffect = data["화면"].ToString();
            string backgroundMusic = data["배경음악"].ToString();
            string expression = data["표정"].ToString();
            string note = data["비고"].ToString();
            string quest = data["퀘스트"].ToString();
            string questContent = data["퀘스트 내용"].ToString();

            ProDialogue pro = new ProDialogue(day, location, speaker, line, screenEffect, backgroundMusic, expression, note, quest, questContent);
            DialogueNode dialogueNode = new DialogueNode(pro, i);

            dialogueNodes.Add(dialogueNode);
            nodeById.Add(i, dialogueNode);
        }

        //튜토리얼-선형 연결
        for (int i = 0; i < dialogueNodes.Count - 1; i++)
        {
            dialogueNodes[i].AddNext(dialogueNodes[i + 1]);
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
            ["루카스_찡그림"] = Resources.Load<Sprite>("NpcImage/Lucas_찡그림"),

            // 슬로우 표정 이미지
            ["슬로우_일반"] = Resources.Load<Sprite>("NpcImage/Slow"),
            ["슬로우_당황"] = Resources.Load<Sprite>("NpcImage/Slow_당황"),
            ["슬로우_화남"] = Resources.Load<Sprite>("NpcImage/Slow_화남"),

            // 가이 표정 이미지
            ["가이_일반"] = Resources.Load<Sprite>("NpcImage/Gai"),
            ["가이_당황"] = Resources.Load<Sprite>("NpcImage/Gai_당황"),

            // 파이아 표정 이미지
            ["파이아_일반"] = Resources.Load<Sprite>("NpcImage/Fire"),
            ["파이아_웃음"] = Resources.Load<Sprite>("NpcImage/Fire_웃음"),

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
            ["파이아"] = Resources.Load<Sprite>("NpcImage/Fire_full"),
            ["Default"] = Resources.Load<Sprite>("NpcImage/Default")
        };
    }

    public void PrintNode(DialogueNode node)
    {
        if (node == null)
        {
            narration.SetActive(false);
            dialogue.SetActive(false);
            return;
        }

        ProDialogue currentDialogue = node.data;//ch1ProDialogue[index];
        
        string expressionKey = !string.IsNullOrEmpty(currentDialogue.expression) ? $"_{currentDialogue.expression}" : "";
        speakerKey = currentDialogue.speaker;
        
        // 인물과 표정을 포함한 최종 키 생성
        string finalKey = speakerKey + expressionKey;

        if (node.nodeId == 533)
        {
            // Transition to 'Ch2Scene'
            SceneManager.LoadScene("Ch2Scene");
            return; // Exit the method to prevent further processing
        }

        Debug.Log($"PrintCh1ProDialogue called with index: {node.nodeId}");
        if (node.nodeId >= dialogueNodes.Count)
        {
            narration.SetActive(false);
            dialogue.SetActive(false);
            bigImageObj.SetActive(false); // 대화가 끝날 때 bigImageObj를 비활성화
            return;
        }

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
        playerImageObj.SetActive(currentNode.nodeId <= 5);

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

        if (node.nodeId == 5 || node.nodeId == 76 || node.nodeId == 152 || node.nodeId == 280 || node.nodeId == 371 || node.nodeId == 416 || node.nodeId == 463 )//|| node.nodeId == 518) // 카페로 강제 이동 후 이동 가능하게 전환
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
        else if (node.nodeId == 32 || node.nodeId == 117 || node.nodeId == 201 || node.nodeId == 341 || node.nodeId == 411 || node.nodeId == 446) // 카페 일 끝나고 이동 가능하게 전환
        {
            player.transform.position = new Vector3(2, -3.5f, 0);
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
        else if (node.nodeId == 512)
        {
            mapManager.currentState = MapState.TrainRoom3;
            cafe.SetActive(false);
            trainRoom.SetActive(true);
            currentNode = node;
            PrintNode(currentNode);
        }
        else if (currentNode.nodeId == 518)
        {
            mapManager.currentState = MapState.Cafe;
            trainRoom.SetActive(true);
            cafe.SetActive(true);
            currentNode = node;
            PrintNode(currentNode);
        }
        /*
        else if (node.nodeId == 32) // 퀘스트 활성화
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
        else if (node.nodeId == 33 && mapManager.currentState == MapState.TrainRoom3) // 퀘스트 받은 후 이동 가능하게 전환
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
        else if (node.nodeId == 37 && mapManager.currentState == MapState.Cafe) // 정원 npc와 대화 이후 이동 가능하게 전환
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

        else if (node.nodeId == 64) // 퀘스트 UI 띄우고 비밀퀘스트2 활성화
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

        else if (node.nodeId == 104 && mapManager.currentState == MapState.TrainRoom3) // npc와 대화를 위해 이동 가능하게 전환
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
        else if (node.nodeId == 133) // 정원 npc와 대화 후 객실 자동 이동 및 맵상태 변경
        {
            player.transform.position = new Vector3(-44.5f, 9f, 0f);
            mapManager.currentState = MapState.TrainRoom3;
        }
        else if (node.nodeId == 191 && mapManager.currentState == MapState.Cafe) // 정원 npc와 대화 이후 이동 가능하게 전환
        {
            isWaitingForPlayer = true;
            playerController.StartMove();
            map.SetActive(true);
            player.SetActive(true);
            garden.SetActive(false);
            narration.SetActive(false);
            dialogue.SetActive(false);
            Npc_Violet.SetActive(true);
        } */
        else if (node.nodeId == 214 || node.nodeId == 453) // 치타샵 ui 활성화
        {
            // Shop UI를 표시
            cheetahShopCh0.SetActive(true);
            OnShopOpened(); // Shop UI가 열렸음을 기록

            // 대화를 임시로 숨기기
            balcony.SetActive(false);
            narration.SetActive(false);
            dialogue.SetActive(false);
        }
        else if ((node.nodeId == 220 || node.nodeId == 453) && mapManager.currentState == MapState.Balcony) // 이동 가능하게 전환
        {
            player.transform.position = new Vector3(52, -1, 0);
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
        /*
        else if (node.nodeId == 257) // 빵집 npc와 대화 후 객실 자동 이동 및 맵상태 변경
        {
            player.transform.position = new Vector3(-44.5f, 9f, 0f);
            mapManager.currentState = MapState.TrainRoom3;
        }
        else if (node.nodeId == 322 && mapManager.currentState == MapState.Cafe) // 정원 npc와 대화 이후 이동 가능하게 전환
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
        else if (node.nodeId == 326 && mapManager.currentState == MapState.Cafe) // 바 npc와 대화 이후 이동 가능하게 전환
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
        else if (node.nodeId == 330 && mapManager.currentState == MapState.Bakery) // 빵집 npc와 대화 이후 이동 가능하게 전환
        {
            isWaitingForPlayer = true;
            playerController.StartMove();
            map.SetActive(true);
            player.SetActive(true);
            bakery.SetActive(false);
            narration.SetActive(false);
            dialogue.SetActive(false);
            Npc_MrHam.SetActive(true);
        } */
        else
        {
            CheckTalk(currentDialogue.location, node.nodeId);
        }
    }

    public void OnDialogueButtonClicked(int nodeId)
    {
        if (!nodeById.TryGetValue(nodeId, out var node))
        {
            Debug.LogWarning($"[Ch1TalkManager] Invalid nodeId {nodeId} in OnDialogueButtonClicked");
            return;
        }
        currentNode = node;

        switch (nodeId)
        {/*
            case 33:
            case 104:
            case 187:
            case 318:
                map.SetActive(false);
                player.SetActive(false);
                Npc_Rayviyak.SetActive(false);
                garden.SetActive(true);
                isWaitingForPlayer = false;
                PrintNode(currentNode);
                break;

            case 37:
            case 322:
            case 191:
                map.SetActive(false);
                player.SetActive(false);
                Npc_Violet.SetActive(false);
                cafe.SetActive(true);
                dialogue.SetActive(true);
                isWaitingForPlayer = false;
                PrintNode(currentNode);
                break;

            case 207:
            case 326:
                map.SetActive(false);
                player.SetActive(false);
                Npc_Rusk.SetActive(false);
                bakery.SetActive(true);
                dialogue.SetActive(true);
                isWaitingForPlayer = false;
                PrintNode(currentNode);
                break;

            case 330:
                map.SetActive(false);
                player.SetActive(false);
                Npc_MrHam.SetActive(false);
                medicalRoom.SetActive(true);
                dialogue.SetActive(true);
                isWaitingForPlayer = false;
                PrintNode(currentNode);
                break;
*/
            default:
                PrintNode(currentNode);
                break;
        }
    }

    public void ActivateTalk(string locationName, int curNodeId)
    {
        this.gameObject.SetActive(true);
        isActivated = true;

        //저장된 curNodeId가 유효할 경우 해당 노드로 바로 이동
        if (nodeById != null && nodeById.TryGetValue(curNodeId, out var SavedNode))
        {
            currentNode = SavedNode;
            PrintNode(currentNode);
            //player.SetActive(false);
            return;
        }

        //nodeId가 유효하지 않으면 locationName의 첫 노드로 시작.
        DialogueNode startNode = null;
        if (!string.IsNullOrEmpty(locationName))
        {
            startNode = dialogueNodes.Find(n => n.data.location == locationName);
        }
        // locationName에 따라 인덱스 조정하여 특정 대화를 시작할 수 있도록 수정
        
        if (startNode == null && dialogueNodes.Count > 0)
        {
            startNode = dialogueNodes[0];
        }
        currentNode = startNode;
        PrintNode(currentNode);
        //player.SetActive(false);
    }

    public void DeactivateTalk()
    {
        this.gameObject.SetActive(false);
        isActivated = false;
    }

    public void CheckTalk(string location, int nodeId)
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
                if (nodeId == 23)
                {
                    StartCoroutine(screenFader.FadeIn(letter));
                }
                else if (nodeId >= 24 && nodeId <= 31) 
                {
                    letter.SetActive(true);
                    if (nodeId >= 24 && nodeId <= 26)
                    {
                        letterText.gameObject.SetActive(true);
                    }
                    else if (nodeId >= 24)
                    {
                        letter.gameObject.SetActive(true);
                    }
                    if (nodeId == 30)
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

        if (nodeId > dialogueNodes.Count)
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