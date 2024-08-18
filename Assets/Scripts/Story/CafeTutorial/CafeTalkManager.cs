using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CafeTalkManager : MonoBehaviour
{
    private List<ProDialogue> proDialogue;

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
    public GameObject Money; //소지금
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
    public GameObject narrBack;

    private const string narrationSpeaker = "나레이션";
    private const string locationCafe = "카페";

    public int currentDialogueIndex = 0;
    private bool isActivated = false;

    private Dictionary<string, Sprite> characterImages;

    void Awake()
    {
        proDialogue = new List<ProDialogue>();
        LoadDialogueFromCSV();
        InitializeCharacterImages();
    }

    void Start()
    {
        Debug.Log("currentDialogueIndex " + currentDialogueIndex);
        if (CoffeePot == null)
        {
            Debug.LogError("CoffeePot is not assigned");
        }
        ActiveTalk();

        SoundManager.Instance.PlayMusic("CAFE", true);
        if (isActivated && currentDialogueIndex == 0)
        {
            PrintProDialogue(currentDialogueIndex);
        }
        
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
            if (currentDialogueIndex != 0 && currentDialogueIndex != 39 &&
                currentDialogueIndex != 41 && currentDialogueIndex != 45 &&
                currentDialogueIndex != 47 && currentDialogueIndex != 48 &&
                currentDialogueIndex != 49)
            {
                currentDialogueIndex++;
                SceneTransitionManager.Instance.UpdateDialogueIndex(currentDialogueIndex);
                PrintProDialogue(currentDialogueIndex);
            }
        }
    }

    void ProcessClick(GameObject clickedObject)
    {
        if (currentDialogueIndex == 0 || currentDialogueIndex == 39)
        {
            if (clickedObject == CoffeePot)
            {
                Debug.Log("Hit CoffeePot at index " + currentDialogueIndex);
                currentDialogueIndex++;
                SceneTransitionManager.Instance.UpdateDialogueIndex(currentDialogueIndex);
                PrintProDialogue(currentDialogueIndex);
            }
            else
            {
                Debug.Log("Hit something else");
            }
        }
        else if (currentDialogueIndex == 41)
        {
            if (clickedObject == Extract)
            {
                Debug.Log("Hit Extract at index " + currentDialogueIndex);
                StartCoroutine(ActivateObjectAfterDelay(2f, Shot));
                currentDialogueIndex++;
                SceneTransitionManager.Instance.UpdateDialogueIndex(currentDialogueIndex);
                SoundManager.Instance.PlaySFX("grinding coffee");
                PrintProDialogue(currentDialogueIndex);
            }
        }
        else if (currentDialogueIndex == 49)
        {
            Debug.Log("Hit Done at index " + currentDialogueIndex);
            currentDialogueIndex++;
            SceneTransitionManager.Instance.UpdateDialogueIndex(currentDialogueIndex);
            SoundManager.Instance.PlaySFX("mixing with ice");
            PrintProDialogue(currentDialogueIndex);
        }
    }

    IEnumerator ActivateObjectAfterDelay(float delay, GameObject obj)
    {
        Debug.Log("Activate 2f at index " + currentDialogueIndex);
        SoundManager.Instance.PlaySFX("coffee machine (espresso)");
        yield return new WaitForSeconds(delay);
        obj.SetActive(true);
    }

    void LoadDialogueFromCSV()
    {
        List<Dictionary<string, object>> data_Dialog = Ch0CSVReader.Read("Travel Around The World - CafeTutorial");

        foreach (var row in data_Dialog)
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
    
    void InitializeCharacterImages()
    {
        characterImages = new Dictionary<string, Sprite>();
        characterImages["솔"] = Resources.Load<Sprite>("PlayerImage/Sol");
        characterImages["바이올렛"] = Resources.Load<Sprite>("NpcImage/Violet");
        characterImages["파이아"] = Resources.Load<Sprite>("NpcImage/Fire");
        characterImages["???"] = Resources.Load<Sprite>("NpcImage/Fire");
    }

    public void PrintProDialogue(int index)
    {
        if (index >= proDialogue.Count)
        {
            return;
        }

        ProDialogue currentDialogue = proDialogue[index];


        // Explain Bar를 보여주는 경우와 텍스트를 설정하는 부분
        if (index >= 40 && index <= 51)
        {
            dialogue.SetActive(false);
            explainBar.SetActive(true);
            explainText.text = currentDialogue.line;
        }
        else
        {
            explainBar.SetActive(false);
            dialogue.SetActive(true);
            nameText.text = currentDialogue.speaker;
            descriptionText.text = currentDialogue.line;
            Sprite characterSprite = characterImages.ContainsKey(currentDialogue.speaker) ? characterImages[currentDialogue.speaker] : Resources.Load<Sprite>("NpcImage/Default");

            if (imageObj.GetComponent<SpriteRenderer>() != null)
            {
                imageObj.GetComponent<SpriteRenderer>().sprite = characterSprite;
            }
            else if (imageObj.GetComponent<Image>() != null)
            {
                imageObj.GetComponent<Image>().sprite = characterSprite;
            }
        }

        if (index < 1 || (index > 5 && index <= 29) || (index >= 34 && index <= 39) || index > 51)
        {
            Beverage.SetActive(false);
            CafeMap.SetActive(true);
            narrBack.SetActive(false);
            if (index >= 18 && index <= 36)
                cheetah.SetActive(true);
            else
                cheetah.SetActive(false);
            narration.SetActive(false);
        }
        else if (index > 29 && index < 34)
        {
            Beverage.SetActive(false);
            cheetah.SetActive(true);
            CafeMap.SetActive(true);
            narration.SetActive(false);
            narrBack.SetActive(false);
        }
        else if (index > 39 && index < 42)
        { 
            Ingredients.SetActive(true);
            Shot.SetActive(false);
            IceAmericano.SetActive(false);
            CafeMap.SetActive(false);
            Beverage.SetActive(true);
            cheetah.SetActive(false);
            narration.SetActive(false);
            narrBack.SetActive(false);
        }
        else if (index == 42)
        {
            Ingredients.SetActive(true);
            Shot.SetActive(false);
            IceAmericano.SetActive(false);
            CafeMap.SetActive(false);
            Beverage.SetActive(true);
            cheetah.SetActive(false);
            narration.SetActive(false);
            narrBack.SetActive(false);
        }
        else if (index > 42 && index < 51)
        {
            Ingredients.SetActive(true);
            Shot.SetActive(true);
            IceAmericano.SetActive(false);
            CafeMap.SetActive(false);
            Beverage.SetActive(true);
            cheetah.SetActive(false);
            narration.SetActive(false);
            narrBack.SetActive(false);
        }
        else if (index == 51)
        {
            Ingredients.SetActive(true);
            Shot.SetActive(true);
            IceAmericano.SetActive(true);
            IceCup.SetActive(false);
            CafeMap.SetActive(false);
            Beverage.SetActive(true);
            cheetah.SetActive(false);
            narration.SetActive(false);
            narrBack.SetActive(false);
            SoundManager.Instance.PlaySFX("cupsetdown");
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
            narrBack.SetActive(false);
        }
        if (currentDialogue.speaker == narrationSpeaker)
        {
            Beverage.SetActive(false);
            CafeMap.SetActive(false);
            dialogue.SetActive(false);
            narration.SetActive(true);
            narrationText.text = currentDialogue.line;
            narrBack.SetActive(true);
        }
        Debug.Log("current index: " + currentDialogueIndex + " && cheetah active: " + cheetah.activeSelf.ToString());

    }

    void ActiveTalk()
    {
        this.gameObject.SetActive(true);
        isActivated = true;
        Debug.Log("ActivateTalk called, isActivated" + isActivated);
    }

}
