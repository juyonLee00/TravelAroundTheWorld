using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    public static SaveDataManager Instance { get; private set; }

    private const string saveFolder = "SaveData";
    private const string fileExtension = ".sav";

    // 현재 활성화된 슬롯 번호를 추적
    private int activeSlotIndex = -1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // 저장 폴더가 없으면 생성
        if (!Directory.Exists(GetSaveFolderPath()))
        {
            Directory.CreateDirectory(GetSaveFolderPath());
        }
    }

    public void SaveGame(PlayerData playerData)
    {
        if (activeSlotIndex == -1)
        {
            Debug.LogError("No active save slot selected.");
            return;
        }

        string filePath = GetSaveFilePath(activeSlotIndex);

        // 저장할 데이터를 직렬화하여 바이너리 파일로 저장
        using (FileStream fs = new FileStream(filePath, FileMode.Create))
        {
            BinaryWriter writer = new BinaryWriter(fs);
            writer.Write(JsonUtility.ToJson(playerData));
        }

        Debug.Log($"Game saved to slot {activeSlotIndex}: {filePath}");
    }

    public PlayerData LoadGame(int slotIndex)
    {
        string filePath = GetSaveFilePath(slotIndex);

        if (File.Exists(filePath))
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                BinaryReader reader = new BinaryReader(fs);
                string jsonData = reader.ReadString();
                PlayerData loadedData = JsonUtility.FromJson<PlayerData>(jsonData);
                Debug.Log($"Game loaded from slot {slotIndex}: {filePath}");

                activeSlotIndex = slotIndex; // 불러온 슬롯을 활성화된 슬롯으로 설정
                return loadedData;
            }
        }
        else
        {
            Debug.LogWarning($"No save file found in slot {slotIndex}.");
            return null;
        }
    }

    public void DeleteSave(int slotIndex)
    {
        string filePath = GetSaveFilePath(slotIndex);

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log($"Save file in slot {slotIndex} deleted.");
        }
        else
        {
            Debug.LogWarning($"No save file found in slot {slotIndex}.");
        }

        // 삭제한 슬롯이 활성화된 슬롯이라면 활성화된 슬롯 번호를 초기화
        if (slotIndex == activeSlotIndex)
        {
            activeSlotIndex = -1;
        }
    }

    //가장 최근에 저장한 데이터로 수정
    public PlayerData LoadMostRecentSave()
    {
        List<int> availableSlots = GetAvailableSaveSlots();
        if (availableSlots.Count == 0)
        {
            Debug.LogWarning("No save files available.");
            return null;
        }

        string mostRecentFilePath = null;
        DateTime mostRecentSaveTime = DateTime.MinValue;

        foreach (int slotIndex in availableSlots)
        {
            string filePath = GetSaveFilePath(slotIndex);

            if (File.Exists(filePath))
            {
                // Load the save time from the file
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    BinaryReader reader = new BinaryReader(fs);
                    string jsonData = reader.ReadString();
                    PlayerData tempData = JsonUtility.FromJson<PlayerData>(jsonData);

                    if (tempData.saveTime > mostRecentSaveTime)
                    {
                        mostRecentSaveTime = tempData.saveTime;
                        mostRecentFilePath = filePath;
                    }
                }
            }
        }

        if (mostRecentFilePath != null)
        {
            // Load and return the most recent save file
            using (FileStream fs = new FileStream(mostRecentFilePath, FileMode.Open))
            {
                BinaryReader reader = new BinaryReader(fs);
                string jsonData = reader.ReadString();
                PlayerData mostRecentData = JsonUtility.FromJson<PlayerData>(jsonData);
                Debug.Log($"Most recent save loaded from: {mostRecentFilePath}");
                return mostRecentData;
            }
        }

        Debug.LogWarning("Failed to find the most recent save file.");
        return null;
    }


    public List<int> GetAvailableSaveSlots()
    {
        List<int> saveSlots = new List<int>();
        string[] files = Directory.GetFiles(GetSaveFolderPath(), $"*{fileExtension}");

        foreach (string file in files)
        {
            string fileName = Path.GetFileNameWithoutExtension(file);
            if (int.TryParse(fileName, out int slotIndex))
            {
                saveSlots.Add(slotIndex);
            }
        }

        return saveSlots;
    }

    public void SetActiveSlot(int slotIndex)
    {
        activeSlotIndex = slotIndex;
    }

    private string GetSaveFolderPath()
    {
        return Path.Combine(Application.persistentDataPath, saveFolder);
    }

    private string GetSaveFilePath(int slotIndex)
    {
        return Path.Combine(GetSaveFolderPath(), slotIndex.ToString() + fileExtension);
    }

    public bool HasSaveData()
    {
        List<int> availableSlots = GetAvailableSaveSlots();
        return availableSlots.Count > 0;
    }

    public int GetSaveDataCount()
    {
        List<int> saveSlots = GetAvailableSaveSlots();
        return saveSlots.Count;
    }
}
