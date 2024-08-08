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
    private void Awake()
    {
        btnDataList = new List<BtnDataSet>();
        player = GameObject.FindWithTag("Player");
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

        if (DayNightCycleManager.Instance.GetCurrentDay() == 4)
        {
            //HappyEnding부분 작성
        }

        else
        {
            /*
            if (모든 NPC를 전부 확인하지 않았을 때)
            {
                //대화창에 대화를 띄우면서 199번 대사 가져와야 함.
            }

            else
            */
            {
                //대화창에 대화 띄우면서 순차적으로 대사 보여짐
                //천천히 FadeOut
                //씬 전환
                SceneManagerEx.Instance.SceanLoadQueue("Ch1Scene");
            }
        }

    }

    void DeactivateUI()
    {
        if (DayNightCycleManager.Instance.GetCurrentDay() == 4)
        {
            //BadEnding부분 작성
        }

        else
        {
            SoundManager.Instance.PlaySFX("click sound");
            UIManager.Instance.DeactivatedUI("Bed");
            player.GetComponent<PlayerController>().StartMove();
        }
        
    }
}