using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    /*InventoryUI Active == true
     * 가장 먼저 생성된 ItmSlot 활성화
     * 방향키로 아이템 슬롯 이동 가능
     * Player, 다른 이벤트 비활성화
     *  
     */

    public GameObject itemSlotPrefab;
    public const int numSlots = 12;

    private void Start()
    {
        CreateItemSlots();
    }

    void CreateItemSlots()
    {
        /*
         * ResourceManager 설정 후 가져오기
        if(itemSlotPrefab == null)
        {

        }
        */

        for(int i=0; i<numSlots; i++)
        {
            GameObject slot = Instantiate(itemSlotPrefab);
            slot.name = "ItemSlot(" + i + ")";

            //슬롯 이미지 받아오기
            //슬롯 텍스쳐 생성하기
        }
    }
}
