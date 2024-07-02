using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public string csvFileName; // Resources 폴더 내의 CSV 파일 이름

    private Dictionary<string, List<string>> dialogues = new Dictionary<string, List<string>>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        LoadDialoguesFromCSV(); // CSV 파일에서 대사 가져오기
    }

    void LoadDialoguesFromCSV()
    {
        var data = CSVReader.Read(csvFileName);
        foreach (var row in data)
        {
            if (!row.ContainsKey("npc_id") || !row.ContainsKey("dialogue"))
            {
                Debug.LogWarning("에러");
                continue;
            }

            string npcID = row["npc_id"].ToString();
            string dialogue = row["dialogue"].ToString();

            if (!dialogues.ContainsKey(npcID))
            {
                dialogues[npcID] = new List<string>();
            }
            dialogues[npcID].Add(dialogue);
        }
    }

    public void StartDialogue(string npcID)
    {
        if (dialogues.ContainsKey(npcID))
        {
            List<string> npcDialogues = dialogues[npcID];
            // 대사 ui에 어떻게 할건지 여기에 추가하기
            foreach (string dialogue in npcDialogues)
            {
                Debug.Log(dialogue); // 일단 콘솔에 대사 출력
            }
        }
        else
        {
            Debug.LogWarning("대사 없음" + npcID); // 임시 확인용 나중에 삭제
        }
    }
}
