using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
 

public class ChoiceBtn : MonoBehaviour
{
    public GameObject choiseBtn;
    private Vector2 btnPos;
    private List<BtnDataSet> btnDataList;
    private GameObject player;

    private string yesData;
    private string noData;

    public GameObject bedNarration;

    private void Awake()
    {
        btnDataList = new List<BtnDataSet>();
        player = GameObject.FindWithTag("Player");
        bedNarration = GameObject.Find("BedNarration");
    }
    void Start()
    {
        SetStringData();
        SetBtnData();
        CreateChoiceBtnGroup();
    }
    void SetBtnData()
    {
        btnPos = new Vector2(50, 0);
        BtnDataSet yesBtn = new BtnDataSet
        {
            btnName = "YesBtn",
            btnTxt = yesData,
            btnEvent = GoToNextDay
        };
        BtnDataSet noBtn = new BtnDataSet
        {
            btnName = "NoBtn",
            btnTxt = noData,
            btnEvent = DeactivateUI
        };
        btnDataList.Add(yesBtn);
        btnDataList.Add(noBtn);
    }

    void SetStringData()
    {
        //언어 상태에 따라 다르게 설정하는 기능 추가

        yesData = "예";
        noData = "아니오";
    }

    void CreateChoiceBtnGroup()
    {
        int btnDataNum = btnDataList.Count;
        for (int i = 0; i < btnDataNum; i++)
        {
            GameObject btn = Instantiate(choiseBtn);
            btn.transform.SetParent(gameObject.transform, false);
            RectTransform rectTransform = btn.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = btnPos;
            TextMeshProUGUI btnTxt = btn.GetComponentInChildren<TextMeshProUGUI>();
            Button btnComponent = btn.GetComponent<Button>();
            btn.name = btnDataList[i].btnName;
            btnTxt.text = btnDataList[i].btnTxt;
            btnComponent.onClick.AddListener(btnDataList[i].btnEvent);
            btnPos.x += 670;
        }
    }
    void GoToNextDay()
    {
        SoundManager.Instance.PlaySFX("click sound");

        //수정 필요
        if (SceneManagerEx.Instance.GetCurrentSceneName() == "Ch0Scene")
        {
            //TalkManager 찾는 코드
            TalkManager talkManager = FindInactiveTalkManager();
            if (talkManager != null)
            {
                Debug.Log("Found TalkManager, even if it was inactive.");
                if (talkManager.isAllNPCActivated)
                {
                    GameObject.Find("MapTutorial").GetComponent<MapTurorial>().isSleeping = true;
                    DayNightCycleManager.Instance.ChangeOnlyDay();
                    PlayerManager.Instance.SetCurrentTimeofDay();
                }

                else
                    return;
            }
            else
            {
                Debug.Log("TalkManager not found.");
            }
        }

        else
        {
            DayNightCycleManager.Instance.ChangeDay();
        }

        UIManager.Instance.DeactivatedUI("Bed");
        bedNarration.SetActive(false);
        //fadeOut

        Debug.Log(PlayerManager.Instance.GetDay());
        
    }

    TalkManager FindInactiveTalkManager()
    {
        // 씬 내의 모든 TalkManager 오브젝트를 포함한 리스트를 찾음
        TalkManager[] allTalkManagers = Resources.FindObjectsOfTypeAll<TalkManager>();

        // TalkManager가 존재하고, 활성화 상태가 아닌 오브젝트 중 하나를 반환
        //1개만 있어서 이렇게 했지만 수정 필
        return allTalkManagers[0];//.Where(tm => !tm.gameObject.activeInHierarchy).FirstOrDefault();
    }

    void DeactivateUI()
    {
        SoundManager.Instance.PlaySFX("click sound");
        UIManager.Instance.DeactivatedUI("Bed");
        bedNarration.SetActive(false);
        player.GetComponent<PlayerController>().StartMove();
     }
        
}