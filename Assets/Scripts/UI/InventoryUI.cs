using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    /*InventoryUI Active == true
     * 가장 먼저 생성된 ItmSlot 활성화
     * 방향키로 아이템 슬롯 이동 가능
     * Player, 다른 이벤트 비활성화
     */

    public bool isActiveUI = false;
    public GameObject itemSlotPrefab;
    public GameObject itemDescPrefab;
    public GameObject itemImagePrefab;

    public const int numSlots = 12;
    private const int slotFirstPosX = -405;
    private const int slotFirstPosY = 243;

    public Vector2 itemSlotPos;
    public Vector2 itemImagePos;
    public Vector2 itemDescPos;


    private void Start()
    {
        SetPosition();
        CreateItemSlots();
        CreateItemDescUI(itemImagePrefab, itemImagePos);
        CreateItemDescUI(itemDescPrefab, itemDescPos);
    }

    void SetPosition()
    {
        itemSlotPos = new Vector2(slotFirstPosY, slotFirstPosX);
        itemImagePos = new Vector2(405, 140);
        itemDescPos = new Vector2(400, -20);
    }

    void CreateItemSlots()
    {
        /*
         * ResourceManager 설정 후 가져오기
        if(itemSlotPrefab == null)
        {

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
                slot.transform.parent = GameObject.Find("InventoryUI").transform;
                slot.GetComponent<RectTransform>().anchoredPosition = itemSlotPos;
                slot.name = "ItemSlot(" + ((i * 3) + j) + ")";
                itemSlotPos.x += 178;
            }



            //슬롯 이미지 받아오기
            //슬롯 텍스쳐 생성하기
        }
    }

    public void CreateItemDescUI(GameObject obj, Vector2 pos)
    {
        GameObject gameObject = Instantiate(obj);
        gameObject.transform.parent = GameObject.Find("InventoryUI").transform;
        gameObject.GetComponent<RectTransform>().anchoredPosition = pos;

    }

    public void OpenInventory()
    {
        isActiveUI = true;
        gameObject.SetActive(true);
    }

    public void CloseInventory()
    {
        isActiveUI = false;
        gameObject.SetActive(false);
    }
}
