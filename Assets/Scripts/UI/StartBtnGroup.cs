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

    private List<PosData> btnPosList;

    private int yPos;

    private void Awake()
    {
        btnDataList = new List<BtnDataSet>();
        btnPosList = new List<PosData>();
    }

    private void Start()
    {
        SoundManager.Instance.PlayMusic("main theme", loop: true); 

        SetUIData();
        SetBtnData();
        SetBtnPosData();

        CreateStartBtnGroup();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
            UIManager.Instance.DeactivatedUI("SaveData");
    }

    void SetUIData()
    {
        yPos = -200;
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
            xPos = -10,
            width = 306,
            height = 90
        };

        PosData continueBtn = new PosData
        {
            xPos = 330,
            width = 306,
            height = 90
        };

        PosData loadBtn = new PosData
        {
            xPos = 610,
            width = 192,
            height = 90
        };

        PosData exitBtn = new PosData
        {
            xPos = 833,
            width = 192,
            height = 90
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
        int slotIndex;

        SoundManager.Instance.PlaySFX("click sound");
        if (SaveDataManager.Instance.HasSaveData())
        {
            slotIndex = SaveDataManager.Instance.GetAvailableSaveSlots().Count;
        }
        else
            slotIndex = 0;

        PlayerManager.Instance.SetPlayerData(slotIndex);
        SceneManagerEx.Instance.SceanLoadQueue("Ch0Scene");
    }

    void ContinueGameFunc()
    {
        SoundManager.Instance.PlaySFX("click sound");

        if (SaveDataManager.Instance.HasSaveData())
        {
            PlayerData recentData = SaveDataManager.Instance.LoadMostRecentSave();

            if (recentData != null)
            {
                // 불러온 데이터를 PlayerManager에 설정
                PlayerManager.Instance.currentData = recentData;
                PlayerManager.Instance.SetIsLoaded();

                // 게임 시작 씬으로 이동
                StartGameFromLastSave();
            }

        }
            
        
        else
        {
            Debug.LogWarning("No save data found.");
            // 데이터가 없는 경우 처리: 예를 들어, 경고 메시지 출력
        }
    }

    private void StartGameFromLastSave()
    {
        //ChN으로 갈 때에도 적용되도록 수정
        SceneManagerEx.Instance.SceanLoadQueue(PlayerManager.Instance.GetSceneName());

    }

    void LoadGameFunc()
    {
        SoundManager.Instance.PlaySFX("click sound");
        UIManager.Instance.ToggleUI("SaveData");
        //UIManager.Instance.ToggleUI("SaveDataPopup");
        //UIManager.Instance.DeactivatedUI("SaveDataPopup");
        //UIManager.Instance.ToggleUI("SaveData");
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

        btnComponent.onClick.AddListener(() => UIManager.Instance.ToggleUI("Setting"));

    }

}
