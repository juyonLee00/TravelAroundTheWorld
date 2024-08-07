using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public struct BtnDataSet
{
    public string btnName;
    public string btnTxt;
    public UnityEngine.Events.UnityAction btnEvent;
}

public struct PosData
{
    public int xPos;
    public int width;
    public int height;
}

public class StartBtnGroup : MonoBehaviour
{
    public GameObject startSceneBtnPrefab;
    public GameObject settingBtnPrefab;

    private Vector2 btnSize;
    private Vector2 btnPos;
    private List<BtnDataSet> btnDataList;

    private Vector2 settingBtnPos;
    private Vector2 settingBtnScale;

    private UIManager uIManager;

    private List<PosData> btnPosList;

    private int yPos;

    private void Awake()
    {
        btnDataList = new List<BtnDataSet>();
        btnPosList = new List<PosData>();
        uIManager = FindObjectOfType<UIManager>();
    }

    private void Start()
    {
        SetUIData();
        SetBtnData();
        SetBtnPosData();

        CreateStartBtnGroup();
    }

    void SetUIData()
    {
        yPos = -188;
        btnPos = new Vector2(-300, -100);

        settingBtnPos = new Vector2(300, 120);
        settingBtnScale = new Vector2(1, 1);
    }

    void SetBtnData()
    {
        BtnDataSet startBtn = new BtnDataSet
        {
            btnName = "StartBtn",
            btnTxt = "처음부터",
            btnEvent = GameStartFunc
        };

        BtnDataSet continueBtn = new BtnDataSet
        {
            btnName = "ContinueBtn",
            btnTxt = "이어하기",
            btnEvent = ContinueGameFunc
        };

        BtnDataSet loadBtn = new BtnDataSet
        {
            btnName = "LoadBtn",
            btnTxt = "불러오기",
            btnEvent = LoadGameFunc
        };

        BtnDataSet exitBtn = new BtnDataSet
        {
            btnName = "ExitBtn",
            btnTxt = "나가기",
            btnEvent = ExitGameFunc
        };

        btnDataList.Add(startBtn);
        btnDataList.Add(continueBtn);
        btnDataList.Add(loadBtn);
        btnDataList.Add(exitBtn);
    }

    void SetBtnPosData()
    {
        PosData startBtn = new PosData
        {
            xPos = 37,
            width = 280,
            height = 75
        };

        PosData continueBtn = new PosData
        {
            xPos = 355,
            width = 280,
            height = 75
        };

        PosData loadBtn = new PosData
        {
            xPos = 620,
            width = 180,
            height = 75
        };

        PosData exitBtn = new PosData
        {
            xPos = 833,
            width = 180,
            height = 75
        };
        btnPosList.Add(startBtn);
        btnPosList.Add(continueBtn);
        btnPosList.Add(loadBtn);
        btnPosList.Add(exitBtn);
    }

    void CreateStartBtnGroup()
    {
        int btnDataNum = btnDataList.Count;

        for(int i=0; i<btnDataNum; i++)
        {
            GameObject btn = Instantiate(startSceneBtnPrefab);
            btn.transform.SetParent(gameObject.transform, false);

            btnPos = new Vector2(btnPosList[i].xPos, yPos);
            btnSize = new Vector2(btnPosList[i].width, btnPosList[i].height);

            RectTransform rectTransform = btn.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = btnPos;
            rectTransform.sizeDelta = btnSize;

            TextMeshProUGUI btnTxt = btn.GetComponentInChildren<TextMeshProUGUI>();
            
            Button btnComponent = btn.GetComponent<Button>();

            btn.name = btnDataList[i].btnName;
            btnTxt.text = btnDataList[i].btnTxt;
            btnComponent.onClick.AddListener(btnDataList[i].btnEvent);

        }

        //CreateSettingBtn();

    }

    void GameStartFunc()
    {
        SoundManager.Instance.PlaySFX("click sound");
        SceneManagerEx.Instance.SceanLoadQueue("Ch0Scene");
    }

    void ContinueGameFunc()
    {
        SoundManager.Instance.PlaySFX("click sound");
        //자동저장된 게임파일로 이동
    }

    void LoadGameFunc()
    {
        SoundManager.Instance.PlaySFX("click sound");
        uIManager.ToggleUI("SaveData");
        uIManager.ToggleUI("SaveDataPopup");
        uIManager.DeactivatedUI("SaveDataPopup");
        uIManager.ActiveUI("SaveData");
    }

    void ExitGameFunc()
    {
        //게임 종료
        SoundManager.Instance.PlaySFX("click sound");
        Application.Quit();
    }

    void CreateSettingBtn()
    {
        GameObject btn = Instantiate(settingBtnPrefab);
        btn.transform.SetParent(gameObject.transform, false);

        RectTransform rectTransform = btn.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = settingBtnPos;

        Button btnComponent = btn.GetComponent<Button>();

        btnComponent.onClick.AddListener(() => uIManager.ToggleUI("Setting"));

    }

}
