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

public class StartBtnGroup : MonoBehaviour
{
    public GameObject startSceneBtnPrefab;
    public GameObject settingBtnPrefab;

    private Vector2 btnImgScale;
    private Vector2 btnPos;
    private List<BtnDataSet> btnDataList;

    private Vector2 settingBtnPos;
    private Vector2 settingBtnScale;

    private UIManager uIManager;

    private int xInterval;

    private void Awake()
    {
        btnDataList = new List<BtnDataSet>();
        uIManager = FindObjectOfType<UIManager>();
    }

    private void Start()
    {
        SetUIData();
        SetBtnData();

        CreateStartBtnGroup();
    }

    void SetUIData()
    {
        btnPos = new Vector2(-300, -100);
        btnImgScale = new Vector2(1.7f, 1.7f);

        settingBtnPos = new Vector2(300, 120);
        settingBtnScale = new Vector2(1, 1);

        xInterval = 120;
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

    void CreateStartBtnGroup()
    {
        int btnDataNum = btnDataList.Count;

        for(int i=0; i<btnDataNum; i++)
        {
            GameObject btn = Instantiate(startSceneBtnPrefab);
            btn.transform.SetParent(gameObject.transform, false);

            RectTransform rectTransform = btn.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = btnPos;

            TextMeshProUGUI btnTxt = btn.GetComponentInChildren<TextMeshProUGUI>();
            
            Button btnComponent = btn.GetComponent<Button>();

            btn.name = btnDataList[i].btnName;
            btnTxt.text = btnDataList[i].btnTxt;
            btnComponent.onClick.AddListener(btnDataList[i].btnEvent);

            btnPos.x += xInterval;
        }

        CreateSettingBtn();

    }

    void GameStartFunc()
    {
        SceneManagerEx.Instance.SceanLoadQueue("Ch0Scene");
    }

    void ContinueGameFunc()
    {
        //자동저장된 게임파일로 이동
    }

    void LoadGameFunc()
    {
        uIManager.ToggleUI("SaveData");
        uIManager.ToggleUI("SaveDataPopup");
        uIManager.DeactivatedUI("SaveDataPopup");
        uIManager.ActiveUI("SaveData");
    }

    void ExitGameFunc()
    {
        //게임 종료
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
