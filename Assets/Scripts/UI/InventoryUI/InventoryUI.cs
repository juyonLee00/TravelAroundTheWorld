using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    /*
     * Object들 직접 할당하는 것이 아닌 UIManager에서 데이터 저장해서 해당 데이터 할당하도록 변경
     * InventoryUI Find로 찾는 것이 아닌 다른 방법으로 찾게 변
     */

    public bool isActiveUI = false;
    public GameObject itemSlotPrefab;
    public GameObject itemDescPrefab;
    public GameObject itemImagePrefab;
    public GameObject BlurredImgPrefab;
    public GameObject inventoryBackgroundPrefab;
    public GameObject itemNamePrefab;
    public GameObject inventoryTitlePrefab;

    public GameObject selectedImg;
    public GameObject selectedSlotImg;
    public GameObject selectedItemImg;

    public const int numSlots = 12;
    private const int slotFirstPosX = -405;
    private const int slotFirstPosY = 243;

    private Vector2 itemSlotPos;
    private Vector2 itemImagePos;
    private Vector2 itemDescPos;
    private Vector2 blurredImgPos;
    private Vector2 inventoryBackgroundImgPos;
    private Vector2 itemNamePos;
    private Vector2 inventoryTitlePos;

    private Vector2 selectedImgScale;


    private void Start()
    {
        SetInitData();
        //CreateUIComponent(BlurredImgPrefab, blurredImgPos);
        CreateUIComponent(inventoryBackgroundPrefab, inventoryBackgroundImgPos);

        CreateItemSlots();
        CreateUIComponent(itemNamePrefab, itemNamePos, false);
        CreateUIComponent(itemImagePrefab, itemImagePos, false);
        CreateUIComponent(itemDescPrefab, itemDescPos, false);
        CreateUIComponent(inventoryTitlePrefab, inventoryTitlePos);

        selectedSlotImg = CreateUIComponent(selectedImg, gameObject, selectedImgScale);
        selectedItemImg = CreateUIComponent(selectedImg, gameObject, selectedImgScale);
    }

    void SetInitData()
    {
        blurredImgPos = new Vector2(0, 0);
        itemSlotPos = new Vector2(slotFirstPosY, slotFirstPosX);
        itemImagePos = new Vector2(405, 140);
        itemDescPos = new Vector2(400, -150);
        inventoryBackgroundImgPos = new Vector2(0, 0);
        itemNamePos = new Vector2(410, 0);
        inventoryTitlePos = new Vector2(-430, 480);

        selectedImgScale = new Vector2(1.7f, 1.7f);

    }

    void CreateItemSlots()
    {
        /*
         * ResourceManager 설정 후 가져오기
        if(itemSlotPrefab == null)
        {
        gameobject null일 때 및 데이터 없을 때 처리
        }
        */

        int xNum = 3;
        int yNum = numSlots / xNum;

        for (int i = 0; i < yNum; i++)
        {
            itemSlotPos.y = slotFirstPosY + (i * -177);
            itemSlotPos.x = slotFirstPosX;
            for(int j = 0; j < xNum; j++)
            {
                GameObject slot = Instantiate(itemSlotPrefab);
                slot.transform.SetParent(gameObject.transform, false);

                RectTransform rectTransform = slot.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = itemSlotPos;

                slot.name = "ItemSlot(" + ((i * 3) + j) + ")";
                itemSlotPos.x += 178;
            }



            //슬롯 이미지 받아오기
            //슬롯 텍스쳐 생성하기
        }
    }

    public void CreateUIComponent(GameObject obj, Vector2 pos)
    {
        GameObject placedObj = Instantiate(obj);
        placedObj.transform.SetParent(gameObject.transform, false);

        RectTransform rectTransform = placedObj.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = pos;

    }

    public void CreateUIComponent(GameObject obj, Vector2 pos, bool isActivated)
    {
        GameObject placedObj = Instantiate(obj);
        placedObj.transform.SetParent(gameObject.transform, false);

        RectTransform rectTransform = placedObj.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = pos;
        placedObj.SetActive(isActivated);
    }

    public GameObject CreateUIComponent(GameObject obj, GameObject backgroundObj, Vector2 scaleData)
    {
        GameObject placedObj = Instantiate(obj);
        placedObj.transform.SetParent(backgroundObj.transform, false);

        RectTransform rectTransform = placedObj.GetComponent<RectTransform>();
        rectTransform.localScale = scaleData;
        placedObj.SetActive(false);

        return placedObj;
    }

}
