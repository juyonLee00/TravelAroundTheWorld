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

public struct StartSceneImgData
{
    public Sprite backgroundImgData;
    public Sprite btnGroupImgData;
    public Sprite settingBtnImgData;
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

    public Image backgroundImg;
    public Image btnGroupImg;

    public List<StartSceneImgData> startSceneImgDatas;

    public Sprite backgroundImgNoon;
    public Sprite backgroundImgNight;
    public Sprite btnGroupImgNoon;
    public Sprite btnGroupImgNight;
    public Sprite settingBtnImgNoon;
    public Sprite settingBtnImgNight;

    private int yPos;

    private void Awake()
    {
        btnDataList = new List<BtnDataSet>();
        btnPosList = new List<PosData>();
        startSceneImgDatas = new List<StartSceneImgData>();
    }

    private void Start()
    {
        //처음에 더미데이터 생성되는 문제 해결해야 함
        SaveDataManager.Instance.DeleteSave(0);
        SoundManager.Instance.PlayMusic("main theme", loop: true);

        SetImgData();
        SetUIData();
        SetBtnData();
        SetBtnPosData();

        CreateStartBtnGroup();
    }

    void SetImgData()
    {
        StartSceneImgData noonImg = new StartSceneImgData
        {
            backgroundImgData = backgroundImgNoon,
            btnGroupImgData = btnGroupImgNoon,
            settingBtnImgData = settingBtnImgNoon
        };

        StartSceneImgData nightImg = new StartSceneImgData
        {
            backgroundImgData = backgroundImgNight,
            btnGroupImgData = btnGroupImgNight,
            settingBtnImgData = settingBtnImgNight
        };

        startSceneImgDatas.Add(noonImg);
        startSceneImgDatas.Add(nightImg);

        int imgIdx = 0;

        if(SaveDataManager.Instance.HasSaveData())
        {
            //가장 최근에 저장한 데이터값 불러오기
            bool isDayTime = SaveDataManager.Instance.LoadMostRecentSave().currentTimeofDay;

            //낮
            if(isDayTime)
            {
                UpdateImgData(imgIdx);
            }

            //밤
            else
            {
                imgIdx = 1;
                UpdateImgData(imgIdx);
            }
        }

        else
        {
            UpdateImgData(imgIdx);
        }
    }

    void UpdateImgData(int idx)
    {
        backgroundImg.sprite = startSceneImgDatas[idx].backgroundImgData;
        btnGroupImg.sprite = startSceneImgDatas[idx].btnGroupImgData;
        settingBtnPrefab.GetComponent<Image>().sprite = startSceneImgDatas[idx].settingBtnImgData;
    }

    void SetUIData()
    {
        yPos = -200;
        btnPos = new Vector2(-300, -100);

        settingBtnPos = new Vector2(844, 425);
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

        CreateSettingBtn();

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
        {
            slotIndex = 0;
        }

        SaveDataManager.Instance.SetActiveSlot(slotIndex);
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
            // 데이터가 없는 경우 처리
            Debug.LogWarning("No save data found.");
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
        btnComponent.onClick.AddListener(() => SoundManager.Instance.PlaySFX("click sound"));

    }

}
