using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private int chapterIdx;
    private int mainIdx;
    private List<int> subIdx = new List<int>();
    private Dictionary<int, int> inventoryData = new Dictionary<int, int>();
    private List<int> cafeItemData = new List<int>();
    private List<int> unlockIllustrationData = new List<int>();
    private List<int> unlockEndingData = new List<int>();

    public int ChapterIdx
    {
        get { return chapterIdx; }
        set { chapterIdx = value; }
    }

    public int MainIdx
    {
        get { return mainIdx; }
        set { mainIdx = value; }
    }

    public List<int> SubIdx
    {
        get { return subIdx; }
        set { subIdx = value; }
    }

    public Dictionary<int, int> InventoryData
    {
        get { return inventoryData; }
        set { inventoryData = value; }
    }

    public List<int> CafeItemData
    {
        get { return cafeItemData; }
        set { cafeItemData = value; }
    }

    public List<int> UnlockIllustrationData
    {
        get { return unlockIllustrationData; }
        set { unlockIllustrationData = value; }
    }

    public List<int> UnlockEndingData
    {
        get { return unlockEndingData; }
        set { unlockEndingData = value; }
    }

    // subIdx의 인덱서
    // 일단 임시로 작성 - 인덱서로 찾는 부분은 메인퀘스트 데이터 형식에 따라 추후 수정될 예저
    /*
    public int this[int index, bool isSubIdx]
    {
        get
        {
            if (index >= 0 && index < subIdx.Count)
            {
                return subIdx[index];
            }
            else
            {
                throw new IndexOutOfRangeException("Index was outside the bounds of subIdx.");
            }
        }
        set
        {
            if (index >= 0 && index < subIdx.Count)
            {
                subIdx[index] = value;
            }
            else
            {
                throw new IndexOutOfRangeException("Index was outside the bounds of subIdx.");
            }
        }
    }

    // cafeItemData의 인덱서
    public int this[int index, string listName]
    {
        get
        {
            if (listName == "CafeItemData" && index >= 0 && index < cafeItemData.Count)
            {
                return cafeItemData[index];
            }
            else if (listName == "UnlockIllustrationData" && index >= 0 && index < unlockIllustrationData.Count)
            {
                return unlockIllustrationData[index];
            }
            else if (listName == "UnlockEndingData" && index >= 0 && index < unlockEndingData.Count)
            {
                return unlockEndingData[index];
            }
            else
            {
                throw new IndexOutOfRangeException("Index was outside the bounds of the specified list.");
            }
        }
        set
        {
            if (listName == "CafeItemData" && index >= 0 && index < cafeItemData.Count)
            {
                cafeItemData[index] = value;
            }
            else if (listName == "UnlockIllustrationData" && index >= 0 && index < unlockIllustrationData.Count)
            {
                unlockIllustrationData[index] = value;
            }
            else if (listName == "UnlockEndingData" && index >= 0 && index < unlockEndingData.Count)
            {
                unlockEndingData[index] = value;
            }
            else
            {
                throw new IndexOutOfRangeException("Index was outside the bounds of the specified list.");
            }
        }
    
    }
    */

}
