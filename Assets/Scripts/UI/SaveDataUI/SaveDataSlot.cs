using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class SaveDataSlot : MonoBehaviour
{
    private Image saveImg;
    private TextMeshProUGUI saveTypeTxt;
    private TextMeshProUGUI saveChapterTxt;
    private TextMeshProUGUI savePosTxt;
    private TextMeshProUGUI saveTimeTxt;
    private int titleSplitNum;

    private Button btn;

    void Start()
    {
        SetComponent();
        AllocationCheckFunc();
        LoadSaveData();
    }

    void SetComponent()
    {
        titleSplitNum = 7;
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(ClickDataSlotFunc);
        saveImg = gameObject.GetComponentsInChildren<Image>(true).FirstOrDefault(img => img.name == "SaveImg");

        TextMeshProUGUI[] textMeshProUGUIs = gameObject.GetComponentsInChildren<TextMeshProUGUI>(true);

        foreach(var comp in textMeshProUGUIs)
        {
            switch(comp.gameObject.name)
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

    void LoadSaveData()
    {
        //int idx = int.Parse(gameObject.name.Substring(titleSplitNum));
        /*
         * saveImg = saveDataManager.Instance.saveDataList[idx].Image;
         */
    }

    void ClickDataSlotFunc()
    {
        /*
         * SaveDataList[idx] 시점에서 게임 시작
         */
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
    }
}
