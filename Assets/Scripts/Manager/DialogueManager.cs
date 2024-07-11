using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }
    Dictionary<string, List<string[]>> talkData;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 오브젝트 유지
        }
        else
        {
            Destroy(gameObject);
        }

        talkData = new Dictionary<string, List<string[]>>();
        LoadDataFromCSV("Travel Around The World - CH1");
    }

    void LoadDataFromCSV(string fileName)
    {
        // Resources 폴더에서 CSV 파일 가져오기
        TextAsset csvFile = Resources.Load<TextAsset>(fileName);
        if (csvFile == null)
        {
            Debug.LogError($"CSV file not found: {fileName}");
            return;
        }

        StringReader sr = new StringReader(csvFile.text);
        bool isFirstLine = true;

        while (sr.Peek() != -1)
        {
            string line = sr.ReadLine();
            if (isFirstLine)
            {
                isFirstLine = false;
                continue;
            }

            string[] values = line.Split(',');

            if (values.Length < 4)
            {
                Debug.LogError($"Invalid line format: {line}");
                continue;
            }

            string npcID = values[2];

            if (string.IsNullOrEmpty(npcID) || npcID.Equals("인물"))
            {
                continue;
            }

            if (!talkData.ContainsKey(npcID))
            {
                talkData[npcID] = new List<string[]>();
            }

            talkData[npcID].Add(values);
        }

        sr.Close();
    }

    public string GetTalk(string npcID, int talkIndex)
    {
        if (!talkData.ContainsKey(npcID) || talkIndex >= talkData[npcID].Count)
            return null;

        return talkData[npcID][talkIndex][3];
    }
}
