using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class SaveDataPopup : MonoBehaviour
{
    private Button yesBtn;
    private Button noBtn;

    private Image saveImg;
    private TextMeshProUGUI saveTypeTxt;
    private TextMeshProUGUI saveChapterTxt;
    private TextMeshProUGUI savePosTxt;
    private TextMeshProUGUI saveTimeTxt;

    void Start()
    {
        SetInitData();
        AllocationCheckFunc();
    }

    void SetInitData()
    {
        Button[] buttons = gameObject.GetComponentsInChildren<Button>(true);
        foreach(var btn in buttons)
        {
            switch(btn.gameObject.name)
            {
                case "YesBtn":
                    yesBtn = btn;
                    break;
                case "NoBtn":
                    noBtn = btn;
                    break;
            }
        }

        yesBtn.onClick.AddListener(StartWithGameData);
        noBtn.onClick.AddListener(DeactiveBtn);

        saveImg = gameObject.GetComponentsInChildren<Image>(true).FirstOrDefault(img => img.name == "SaveImg");

        TextMeshProUGUI[] textMeshProUGUIs = gameObject.GetComponentsInChildren<TextMeshProUGUI>(true);

        foreach (var comp in textMeshProUGUIs)
        {
            switch (comp.gameObject.name)
            {
                case "SaveTypeTxt":
                    saveTypeTxt = comp;
                    break;
                case "SaveChapterTxt":
                    saveChapterTxt = comp;
                    break;
                case "SavePosTxt":
                    savePosTxt = comp;
                    break;
                case "SaveTimeTxt":
                    saveTimeTxt = comp;
                    break;
            }
        }
    }


    void AllocationCheckFunc()
    {
        if (saveImg == null)
            return;
        if (saveTypeTxt == null)
            return;
        if (saveChapterTxt == null)
            return;
        if (saveChapterTxt == null)
            return;
        if (savePosTxt == null)
            return;
        if (saveTimeTxt == null)
            return;

        if (yesBtn == null)
            return;
        if (noBtn == null)
            return;
    }

    void StartWithGameData()
    {
        Debug.Log("Start");
    }

    void DeactiveBtn()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (!EventSystem.current.currentSelectedGameObject.GetComponent<SaveDataSlot>())
            return;

        else
        {
            SaveDataSlot clickSlot = EventSystem.current.currentSelectedGameObject.GetComponent<SaveDataSlot>();
            Debug.Log(clickSlot.slotIdx);
        }
        
    }
}
