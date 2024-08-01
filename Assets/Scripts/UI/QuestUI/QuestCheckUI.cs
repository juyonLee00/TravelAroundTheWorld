using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestCheckUI : MonoBehaviour
{
    public GameObject mainQuestBackgroundImg;
    public GameObject subQuestBackgroundImg;
    public GameObject dataSlot;
    public GameObject questUITitle;
    public GameObject subUIBtn;
    public GameObject mainUIBtn;

    private Vector2 defaultPos;
    private Vector2 dataSlotPos;
    private Vector2 questUITitlePos;
    private Vector2 subUIBtnPos;
    private Vector2 mainUIBtnPos;

    private int slotYInterval;
    private int slotMaxNum;

    private Dictionary<string, GameObject> uiInstances = new Dictionary<string, GameObject>();

    private void Awake()
    {
        
    }

    private void Start()
    {
        SetInitData();
        SetComponent();
        
        CreateDataSlots();
    }

    void SetInitData()
    {
        defaultPos = Vector2.zero;
        dataSlotPos = Vector2.zero;
        slotYInterval = -135;
        slotMaxNum = 4;

        questUITitlePos = new Vector2(0, 215);
        subUIBtnPos = new Vector2(-93, 386);
        mainUIBtnPos = new Vector2(-175, 377);
    }

    void SetComponent()
    {
        CreateUIComponent(subQuestBackgroundImg, defaultPos);
        CreateUIComponent(mainQuestBackgroundImg, defaultPos);
        CreateUIComponent(mainUIBtn, mainUIBtnPos);
        CreateUIComponent(subUIBtn, subUIBtnPos);
        CreateUIComponent(questUITitle, questUITitlePos);

        AddBtnEvent();
    }

    void CreateDataSlots()
    {
        for(int i=0; i<slotMaxNum; i++)
        {
            GameObject slot = Instantiate(dataSlot);
            slot.transform.SetParent(gameObject.transform, false);

            Transform slotTransform = slot.GetComponent<Transform>();
            slotTransform.localPosition = dataSlotPos;

            slot.name = "DataSlot" + i;
            dataSlotPos.y += slotYInterval;
        }
    }

    public void CreateUIComponent(GameObject obj, Vector2 pos)
    {
        GameObject placedObj = Instantiate(obj);
        placedObj.transform.SetParent(gameObject.transform, false);

        RectTransform rectTransform = placedObj.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = pos;

        string uiName = placedObj.name;

        uiInstances[uiName] = placedObj;
    }



    void ClickMainBtn()
    {
        uiInstances["MainQuestBackground(Clone)"].SetActive(true);
        uiInstances["Title(Clone)"].GetComponent<TextMeshProUGUI>().text = "메인 퀘스트";
        //현재 진행중인 MainQuestData가져오기
        Debug.Log("Click MainBtn");
    }

    void ClickSubBtn()
    {
        uiInstances["MainQuestBackground(Clone)"].SetActive(false);
        uiInstances["Title(Clone)"].GetComponent<TextMeshProUGUI>().text = "서 퀘스트";
        //현재 진행중인 MainQuestData가져오기
        Debug.Log("Click SubBtn");
    }

    void AddBtnEvent()
    {
        //수정예정
        Button[] buttons = GetComponentsInChildren<Button>();

        foreach(var btn in buttons)
        {
            switch(btn.gameObject.name)
            {
                case "SubUIBtn(Clone)":
                    btn.onClick.AddListener(ClickSubBtn);
                    break;
                case "MainUIBtn(Clone)":
                    btn.onClick.AddListener(ClickMainBtn);
                    break;
            }
        }
    }
}
