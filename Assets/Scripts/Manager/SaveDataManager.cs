using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    public static SaveDataManager Instance { get; private set; }

    private const string saveFolder = "SaveData";
    private const string fileExtension = ".sav";

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

        //저장폴더 없으면 생성
        if (!Directory.Exists(GetSaveFolderPath()))
        {
            Directory.CreateDirectory(GetSaveFolderPath());
        }
    }

    public void SaveGame(PlayerData playerData, int slotIndex)
    {
        string filePath = GetSaveFilePath(slotIndex);

        // 저장할 데이터를 직렬화하여 바이너리 파일로 저장
        using (FileStream fs = new FileStream(filePath, FileMode.Create))
        {
            BinaryWriter writer = new BinaryWriter(fs);
            writer.Write(JsonUtility.ToJson(playerData));
        }

        Debug.Log($"Game saved to slot {slotIndex}: {filePath}");
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

    private string GetSaveFolderPath()
    {
        return Path.Combine(Application.persistentDataPath, saveFolder);
    }

    private string GetSaveFilePath(int slotIndex)
    {
        return Path.Combine(GetSaveFolderPath(), slotIndex.ToString() + fileExtension);
    }
}
