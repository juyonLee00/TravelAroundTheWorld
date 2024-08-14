using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    public PlayerData currentData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            List<int> availableSlots = SaveDataManager.Instance.GetAvailableSaveSlots();

            //현재 저장된 데이터가 없으면 새 데이터 선언.
            if (availableSlots.Count == 0)
            {
                currentData = new PlayerData();
                InitializePlayerData();
            }

            else
            {
                currentData = SaveDataManager.Instance.LoadGame(slotIndex: 1);
            }

        }
        else
        {
            Destroy(gameObject);
        }
    }

    public DateTime GetDayTime()
    {
        return currentData.saveTime;
    }

    public void SetDateTime()
    {
        currentData.saveTime = DateTime.Now;
    }

    public bool GetIsAutoSave()
    {
        return currentData.isAutoSave;
    }

    public void SetIsAutoSave()
    {
        //카페에 다녀왔을 경우/침대와 상호작용했을 경우 ++
    }

    public MapState GetMapLocation()
    {
        return currentData.mapLocation;
    }

    public void SetMapLocation(MapState map)
    {
        currentData.mapLocation = map;
    }

    public Vector3 GetPlayerPosition()
    {
        return currentData.playerPosition;
    }

    public void SetPlayerPosition()
    {
        currentData.playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    public string GetSceneName()
    {
        return currentData.currentSceneName;
    }

    public void SetSceneName(string curScene)
    {
        currentData.currentSceneName = curScene;
    }

    public int GetDay()
    {
        return currentData.currentDay;
    }

    public void SetDay()
    {
        currentData.currentDay = DayNightCycleManager.Instance.GetCurrentDay();
    }

    public bool GetCurrentTimeofDay()
    {
        return currentData.currentTimeofDay;
    }

    public int GetMoney()
    {
        return currentData.money;
    }

    public int GetDialogueIdx()
    {
        return currentData.dialogueIdx;
    }

    public void SetDialogueIdx(int idx)
    {
        currentData.dialogueIdx = idx;
    }

    public void SetCurrentTimeofDay()
    {
        currentData.currentTimeofDay = DayNightCycleManager.Instance.GetNowDayTime();
    }

    public void AddCafeItem(string name)
    {
        currentData.unlockedCafeItems.Add(name);
    }

    public bool IsBoughtCafeItem(string name)
    {
        return currentData.unlockedCafeItems.Contains(name);
    }

    //inventory형식 수정 예정-아이템 형식 미정
    public void AddInventoryItem(GameObject inventoryItem)
    {
        currentData.inventoryItem.Add(inventoryItem);
    }

    public void DeleteInventoryItem(GameObject inventoryItem)
    {
        currentData.inventoryItem.Remove(inventoryItem);
    }

    public void AddCompleteMainQuestIds(string mainQuestionName)
    {
        currentData.completedMainQuestIds.Add(mainQuestionName);
    }

    public void AddCompletedSubQuestIds(string subQuestionName)
    {
        currentData.completedSubQuestIds.Add(subQuestionName);
    }

    public void AddIllustrationIds(int id)
    {
        currentData.unlockedIllustrationIds.Add(id);
    }

    public void AddEndingIds(int id)
    {
        currentData.unlockedEndingIds.Add(id);
    }

    public void EarnMoney(int money)
    {
        currentData.money += money;
    }

    public void PayMoney(int money)
    {
        //수정 필요
        if (money > currentData.money)
            return;
        currentData.money -= money;
    }

    
    private void InitializePlayerData()
    {
        currentData.saveTime = DateTime.Now;
        currentData.isAutoSave = false;
        currentData.mapLocation = MapState.Null;
        currentData.playerPosition = new Vector3(0, 0, 0);
        currentData.currentSceneName = "Ch0Scene";
        currentData.currentDay = DayNightCycleManager.Instance.day;
        currentData.currentTimeofDay = DayNightCycleManager.Instance.isDayTime;
        currentData.money = 0;
        currentData.dialogueIdx = 0;
        currentData.unlockedCafeItems = new List<string>();
        currentData.inventoryItem = new List<GameObject>();
        currentData.completedMainQuestIds = new List<string>();
        currentData.completedSubQuestIds = new List<string>();
        currentData.unlockedIllustrationIds = new List<int>();
        currentData.unlockedEndingIds = new List<int>();
    }
    


}
