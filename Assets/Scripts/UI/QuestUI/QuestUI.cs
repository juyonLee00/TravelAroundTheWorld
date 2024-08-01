using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class QuestUI : MonoBehaviour
{
    public GameObject background;
    public GameObject itemSlot;
    public GameObject title;

    public GameObject remainDay;
    public GameObject descTxt;

    private Vector2 itemSlotPos;
    private Vector2 titlePos;
    private Vector2 remainDayPos;
    private Vector2 descTxtPos;
    private Vector2 defaultPos;

    private Dictionary<string, GameObject> uiInstances = new Dictionary<string, GameObject>();

    private float slotXInterval;
    public int itemNum;

    //Quest관련 Data 자료구조

    private void Start()
    {
        SetInitData();
        CreateUIComponent(background, defaultPos);
        CreateUIComponent(title, titlePos);
        CreateUIComponent(remainDay, remainDayPos);
        CreateUIComponent(descTxt, descTxtPos);

        CreateItemSlots();
        SetQuestionData();
    }

    void SetInitData()
    {
        defaultPos = Vector2.zero;
        itemSlotPos = new Vector2(-121.5f, -148);
        titlePos = new Vector2(0, 134);
        remainDayPos = new Vector2(225, 165);
        descTxtPos = new Vector2(-4, 60);
        //들어오는 퀘스트에 필요한 item 개수에 따라 수정 예정
        itemNum = 3;
        slotXInterval = 129.5f;
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

    void CreateItemSlots()
    {
        for (int i = 0; i < itemNum; i++)
        {
            GameObject slot = Instantiate(itemSlot);
            slot.transform.SetParent(gameObject.transform, false);

            Transform slotTransform = slot.GetComponent<Transform>();
            slotTransform.localPosition = itemSlotPos;

            slot.name = "ItemSlot" + i;
            itemSlotPos.x += slotXInterval;

            TextMeshProUGUI itemCount = slot.GetComponentInChildren<TextMeshProUGUI>();

            int hadItem = 0;
            /*
             * if(inventoryInstances.ContainsKey(itemName))
             *      hadItem = inventoryInstances[itemName];
             *
             * 
             */
            int needItem = 0;
            /*
             * needItem = QuestData.Item[itemName];
             */

            itemCount.text = ("" + hadItem) + "/" + ("" + needItem);

            Image itemImg = slot.GetComponentsInChildren<Image>(true)
                              .FirstOrDefault(img => img.gameObject.name == "ItemIcon");
            //itemImg = needItem.Image;
        }
    }

    void SetQuestionData()
    {
       /*
        * questionData = InteractionNPC.GetQuestionData();
        * 
        * string remainDay = ""+(Mathf.Abs(questionData.day - GameManager.currentDay);
        * bool isClear = questionInstances.ContainsKey(questionData.id);
        * string title = questionData.title;
        * string questionDesc = questionData.desc;
        * string questionNPC = questionData.person;
        * 
        * uiInstances["RemainDay(Clone)"].text = remainDay+"일 남음";
        * uiInstances["Title(Clone)"].text = title;
        * 
        * if(isClear == false)
        *   uiInstances["DescTxt(Clone)"].text = questionDesc;
        *  else
        *   uiInstances["DescTxt(Clone)"].text = "다 모았다~"+\n+"탑승객 "+questionNPC+"에게 가져다 주자.";
        */
    }
}
