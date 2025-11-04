using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CafeTalkManager : MonoBehaviour
{
    public List<DialogueNode> dialogueNodes;
    private Dictionary<int, DialogueNode> nodeById;
    public DialogueNode currentNode;

    public GameObject narration;
    public TextMeshProUGUI narrationText;

    public GameObject dialogue;
    public GameObject imageObj;
    public GameObject nameObj;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;

    public GameObject explainBar;
    public TextMeshProUGUI explainText;

    public GameObject CafeMap; //카페 기본 화면
    public GameObject MainCharacter; //주인공 초상화
    public GameObject CoffeePot; //커피머신
    public GameObject RecipeBook; // 레시피북

    public GameObject Beverage; //음료 제작창
    public GameObject BackSpace; // 뒤로가기
    public GameObject EspressoBar; // 커피 추출
    public GameObject Extract; // 추출하기
    public GameObject HotCup;
    public GameObject IceCup;
    public GameObject Ingredients;
    public GameObject Water;
    public GameObject Shot;
    public GameObject IceAmericano;
    public GameObject Done;

    public GameObject train;
    public GameObject cheetah;

    private const string narrationSpeaker = "나레이션";
    private const string locationCafe = "카페";
    private bool isActivated = false;

    private Dictionary<string, Sprite> characterImages;

    public Ch0DialogueBar dialogueBar;
    public Ch0DialogueBar narrationBar;
    public Ch0DialogueBar openingBar;


    void Awake()
    {
        LoadDialogueFromCSV();
        InitializeCharacterImages();
    }

    void Start()
    {
        ActiveTalk();

        SoundManager.Instance.PlayMusic("CAFE", true);
        currentNode = dialogueNodes[0];
            PrintNode(currentNode);
    }

    void Update()
    {
        if (isActivated)
        {
            HandleInput();
        }
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero);

            if (hit.collider != null)
            {
                GameObject clickedObject = hit.collider.gameObject;
                ProcessClick(clickedObject);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {

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

            if (!anyTyping)
            {
                if (currentNode.nodeId != 0 && currentNode.nodeId != 39 &&
                currentNode.nodeId != 41 && currentNode.nodeId != 45 &&
                currentNode.nodeId != 47 && currentNode.nodeId != 48 &&
                currentNode.nodeId != 49)
                {
                    if (currentNode.nextNodes == null || currentNode.nextNodes.Count == 0)
                    {
                        Debug.LogWarning($"Node {currentNode.nodeId} has no nextNodes. Dialogue ends.");
                        return;
                    }
                    currentNode = currentNode.nextNodes[0];
                    SceneTransitionManager.Instance.UpdateDialogueIndex(currentNode.nodeId);
                    PrintNode(currentNode);
                }
            }
                
        }
    }

    void ProcessClick(GameObject clickedObject)
    {
        if (currentNode.nodeId == 0 || currentNode.nodeId == 39)
        {
            if (clickedObject == CoffeePot)
            {
                Debug.Log("Hit CoffeePot at index " + currentNode.nodeId);
                SoundManager.Instance.PlaySFX("click sound");
                currentNode = currentNode.nextNodes[0];
                SceneTransitionManager.Instance.UpdateDialogueIndex(currentNode.nodeId);
                PrintNode(currentNode);
            }
        }
        else if (currentNode.nodeId == 41)
        {
            if (clickedObject == Extract)
            {
                Debug.Log("Hit Extract at index " + currentNode.nodeId);
                StartCoroutine(ActivateObjectAfterDelay(2f, Shot));
                currentNode = currentNode.nextNodes[0];
                SceneTransitionManager.Instance.UpdateDialogueIndex(currentNode.nodeId);
                SoundManager.Instance.PlaySFX("grinding coffee");
                PrintNode(currentNode);
            }
        }
        else if (currentNode.nodeId == 49)
        {
            Debug.Log("Hit Done at index " + currentNode.nodeId);
            currentNode = currentNode.nextNodes[0];
            SceneTransitionManager.Instance.UpdateDialogueIndex(currentNode.nodeId);
            SoundManager.Instance.PlaySFX("mixing with ice");
            PrintNode(currentNode);
        }
    }

    IEnumerator ActivateObjectAfterDelay(float delay, GameObject obj)
    {
        SoundManager.Instance.PlaySFX("coffee machine (espresso)");
        yield return new WaitForSeconds(delay);
        obj.SetActive(true);
    }

    void LoadDialogueFromCSV()
    {
        dialogueNodes = new List<DialogueNode>();
        nodeById = new Dictionary<int, DialogueNode>();

        List<Dictionary<string, object>> datas = Ch0CSVReader.Read("Travel Around The World - CafeTutorial");

        for (int i = 0; i < datas.Count; i++)
        {
            var data = datas[i];
            string dayString = data["일자"].ToString();
            int day = int.Parse(System.Text.RegularExpressions.Regex.Match(dayString, @"\d+").Value);
            string location = data["장소"].ToString();
            string speaker = data["인물"].ToString();
            string line = data["대사"].ToString();
            string screenEffect = data["화면 연출"].ToString();
            string backgroundMusic = data["배경음악"].ToString();
            string expression = data["표정"].ToString();
            string note = data["비고"].ToString();
            string quest = data["퀘스트"].ToString();
            string questContent = data["퀘스트 내용"].ToString();

            ProDialogue pro = new ProDialogue(day, location, speaker, line, screenEffect, backgroundMusic, expression, note, quest, questContent);

            dialogueNodes.Add(new DialogueNode(pro, i));
        }
        for (int i = 0; i < dialogueNodes.Count - 1; i++)
            dialogueNodes[i].AddNext(dialogueNodes[i + 1]);
    }
    
    void InitializeCharacterImages()
    {
        characterImages = new Dictionary<string, Sprite>();
        characterImages["솔"] = Resources.Load<Sprite>("PlayerImage/Sol");
        characterImages["바이올렛"] = Resources.Load<Sprite>("NpcImage/Violet");
        characterImages["파이아"] = Resources.Load<Sprite>("NpcImage/Fire");
        characterImages["???"] = Resources.Load<Sprite>("NpcImage/Fire");
    }

    public void PrintNode(DialogueNode node)
    {
        if (node == null) return;

        currentNode = node;
        ProDialogue data = node.data;

        int id = node.nodeId;

        // Explain Bar를 보여주는 경우와 텍스트를 설정하는 부분
        if (id >= 40 && id <= 50)
        {
            if (id == 50)
                Debug.Log("current dialogue = " + data.line);
            dialogue.SetActive(false);
            explainBar.SetActive(true);
            explainText.text = data.line;
        }
        else
        {
            explainBar.SetActive(false);
            dialogue.SetActive(true);
            dialogueBar.SetDialogue(data.speaker, data.line);
            Sprite characterSprite = characterImages.ContainsKey(data.speaker) ? characterImages[data.speaker] : Resources.Load<Sprite>("NpcImage/Default");

            if (imageObj.GetComponent<SpriteRenderer>() != null)
            {
                imageObj.GetComponent<SpriteRenderer>().sprite = characterSprite;
            }
            else if (imageObj.GetComponent<Image>() != null)
            {
                imageObj.GetComponent<Image>().sprite = characterSprite;
            }
        }

        if (id < 1 || (id > 5 && id <= 29) || (id >= 34 && id <= 39) || id > 50)
        {
            Beverage.SetActive(false);
            CafeMap.SetActive(true);
            if (id >= 18 && id <= 36)
                cheetah.SetActive(true);
            else
                cheetah.SetActive(false);
            if(id == 34)
            {
                PlayerManager.Instance.PayMoney(500);
            }
            if (id == 13 || id == 37)
                SoundManager.Instance.PlaySFX("window open");
            else if (id == 15 || id == 30)
                SoundManager.Instance.PlaySFX("motorcycle");
            else if (id == 14)
                SoundManager.Instance.PlaySFX("wind");
            narration.SetActive(false);
        }
        else if (id > 29 && id < 34)
        {
            if (id == 32)
            {
                PlayerManager.Instance.EarnMoney(500);
            }
            Beverage.SetActive(false);
            cheetah.SetActive(true);
            CafeMap.SetActive(true);
            narration.SetActive(false);
        }
        else if (id > 39 && id < 42)
        { 
            Ingredients.SetActive(true);
            Shot.SetActive(false);
            IceAmericano.SetActive(false);
            CafeMap.SetActive(false);
            Beverage.SetActive(true);
            cheetah.SetActive(false);
            narration.SetActive(false);
        }
        else if (id == 42)
        {
            Ingredients.SetActive(true);
            Shot.SetActive(false);
            IceAmericano.SetActive(false);
            CafeMap.SetActive(false);
            Beverage.SetActive(true);
            cheetah.SetActive(false);
            narration.SetActive(false);
        }
        else if (id > 42 && id < 50)
        {
            Ingredients.SetActive(true);
            Shot.SetActive(true);
            IceAmericano.SetActive(false);
            CafeMap.SetActive(false);
            Beverage.SetActive(true);
            cheetah.SetActive(false);
            narration.SetActive(false);
        }
        else if (id == 50)
        {
            Ingredients.SetActive(true);
            Shot.SetActive(true);
            IceAmericano.SetActive(true);
            IceCup.SetActive(false);
            CafeMap.SetActive(false);
            Beverage.SetActive(true);
            cheetah.SetActive(false);
            narration.SetActive(false);
            SoundManager.Instance.PlaySFX("complete bell");
        }
        else
        {
            explainBar.SetActive(false);
            Ingredients.SetActive(false);
            Shot.SetActive(false);
            Water.SetActive(true);
            IceAmericano.SetActive(false);
            CafeMap.SetActive(false);
            Beverage.SetActive(true);
            cheetah.SetActive(false);
            narration.SetActive(false);
        }
        if (data.speaker == narrationSpeaker)
        {
            Beverage.SetActive(false);
            CafeMap.SetActive(true);
            dialogue.SetActive(false);
            narration.SetActive(true);
            narrationBar.SetDialogue(data.speaker, data.line);
        }

    }

    void ActiveTalk()
    {
        this.gameObject.SetActive(true);
        isActivated = true;
        Debug.Log("ActivateTalk called, isActivated" + isActivated);
    }

}
