using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TalkManagerCH1 : MonoBehaviour
{
    private List<ProDialogue> proDialogue;

    public GameObject opening;
    public TextMeshProUGUI openingText;

    public GameObject narration;
    public TextMeshProUGUI narrationText;

    public GameObject dialogue;
    public GameObject imageObj;
    public GameObject nameObj;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;

    public GameObject letter;
    public TextMeshProUGUI letterText;

    public GameObject trainRoom;
    public GameObject jazzBar;
    public GameObject garden;

    private int currentDialogueIndex = 0;
    private bool isActivated = false;

    void Awake()
    {
        proDialogue = new List<ProDialogue>();
        LoadDialogueFromCSV();
    }

    void Start()
    {
        ActivateTalk();
    }

    void Update()
    {
        if (isActivated && currentDialogueIndex == 0)
        {
            PrintProDialogue(currentDialogueIndex);
        }
        if (isActivated && Input.GetKeyDown(KeyCode.Space))
        {
            currentDialogueIndex++;
            PrintProDialogue(currentDialogueIndex);
        }
    }

    void LoadDialogueFromCSV()
    {
        List<Dictionary<string, object>> data_Dialog = Ch0CSVReader.Read("Travel Around The World - CH1 (1)");

        foreach (var row in data_Dialog)
        {
            string dayString = row["일자"].ToString();
            int day = int.Parse(System.Text.RegularExpressions.Regex.Match(dayString, @"\d+").Value);
            string location = row["장소"].ToString();
            string speaker = row["인물"].ToString();
            string line = row["대사"].ToString();
            string screenEffect = row["화면"].ToString();
            string backgroundMusic = row["배경음악"].ToString();
            string expression = row["표정"].ToString();
            string note = row["비고"].ToString();
            string quest = row["퀘스트"].ToString();
            string questContent = row["퀘스트 내용"].ToString();

            proDialogue.Add(new ProDialogue(day, location, speaker, line, screenEffect, backgroundMusic, expression, note, quest, questContent));
        }
    }

    void PrintProDialogue(int index)
    {
        if (index >= proDialogue.Count)
        {
            narration.SetActive(false);
            dialogue.SetActive(false);
            return;
        }

        ProDialogue currentDialogue = proDialogue[index];

        if (index < 2)
        {
            narration.SetActive(false);
            dialogue.SetActive(false);
            opening.SetActive(true);
            openingText.text = currentDialogue.line;
        }

        else if (currentDialogue.speaker == "편지지")
        {
            narration.SetActive(false);
            dialogue.SetActive(false);
            opening.SetActive(false);
            if (!string.IsNullOrEmpty(letterText.text))
            {
                letterText.text += "\n";
            }
            letterText.text += currentDialogue.line;
        }
        else if (currentDialogue.speaker == "나레이션")
        {
            narration.SetActive(true);
            dialogue.SetActive(false);
            opening.SetActive(false);
            narrationText.text = currentDialogue.line;
        }
        else
        {
            narration.SetActive(false);
            dialogue.SetActive(true);
            opening.SetActive(false);
            nameText.text = currentDialogue.speaker;
            descriptionText.text = currentDialogue.line;
        }

        CheckTalk(currentDialogue.location);
    }

    public void ActivateTalk()
    {
        this.gameObject.SetActive(true);
        isActivated = true;
    }

    void DeactivateTalk()
    {
        this.gameObject.SetActive(false);
        isActivated = false;
    }

    void CheckTalk(string location)
    {
        letter.SetActive(false);
        trainRoom.SetActive(false);
        jazzBar.SetActive(false);
        garden.SetActive(false);

        switch (location)
        {
            case "객실":
                if (currentDialogueIndex >= 25 && currentDialogueIndex <= 33)
                {
                    letter.SetActive(true);
                    if (currentDialogueIndex >= 26 && currentDialogueIndex <= 29)
                    {
                        letter.gameObject.SetActive(true);
                    }
                    else
                    {
                        letter.gameObject.SetActive(false);
                    }
                }
                break;
            case "재즈바":
                jazzBar.SetActive(true);
                break;
            case "정원":
                garden.SetActive(true);
                break;
            case "카페":
                jazzBar.SetActive(true);
                break;
        }

        if (currentDialogueIndex > proDialogue.Count)
        {
            DeactivateTalk();
        }
    }
}
