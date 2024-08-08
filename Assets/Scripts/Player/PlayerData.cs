using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct InventoryItemData
{
    public GameObject itemData;
    public int itemNum;
}


public class PlayerData : MonoBehaviour
{
    public int chapterIdx;
    public int mainIdx;
    public List<int> subIdx;

    public Dictionary<string, InventoryItemData> inventoryData;
    public List<int> unlockedCafeItemData;

    public List<int> unlockIllustrationData;
    public List<int> unlockEndingData;

    public Vector2 position;
    public int money;

    public PlayerData()
    {
        chapterIdx = -1;
        mainIdx = 0;
        subIdx = new List<int>();

        inventoryData = new Dictionary<string, InventoryItemData>();
        unlockedCafeItemData = new List<int>();

        unlockIllustrationData = new List<int>();
        unlockEndingData = new List<int>();

        position = Vector2.zero;
        money = 0;
    }


    /*
    public void AddItem(string name, int num)
    {
        if (inventoryData.ContainsKey(name))
            inventoryData[name].itemNum += num;

        else
            inventoryData.Add(name, num);
    }
    */

    /*
    public void useItem(string name, int num)
    {
        if(!inventoryData.ContainsKey(name))
        {
            //오류로그 저장
            return;
        }

        else
        {
            int diffValue = inventoryData[name] - num;

            if(diffValue < 0)
            {
                //팝업창 - 사용하려는 아이템을 사용할 수 없다는 팝업창
                return;
            }    

            else
            {
                inventoryData[name] = diffValue;

                if(diffValue == 0)
                {
                    inventoryData.Remove(idx);
                }
            }
        }
    }
    */



    public void AddEndingData(int idx)
    {
        if(!unlockEndingData.Contains(idx))
            unlockEndingData.Add(idx);
    }

    public void AddIllustrationData(int idx)
    {
        if (!unlockIllustrationData.Contains(idx))
            unlockIllustrationData.Add(idx);
    }

    public void GetCafeItem(int idx)
    {
        if (!unlockedCafeItemData.Contains(idx))
            unlockedCafeItemData.Add(idx);
            
    }

    

}
