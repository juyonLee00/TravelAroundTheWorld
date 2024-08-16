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

    public int slotIdx;
    public GameObject saveDataPopup;
    public GameObject canvas;

    private Button btn;
    private bool isSaveDataNull;

    List<int> saveDataList;

    void Start()
    {
        SetComponent();
        //AllocationCheckFunc();
        isSaveDataNull = CheckSaveDataList();
    }

    void SetComponent()
    {
        GameObject canvas = GameObject.Find("Canvas");
        titleSplitNum = gameObject.name.Length;
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

    bool CheckSaveDataList()
    {
        List<int> saveDataList = SaveDataManager.Instance.GetAvailableSaveSlots();
        if (saveDataList.Count == 0)
            return true;
        else
            return false;
    }

    void ClickDataSlotFunc()
    {
        if (isSaveDataNull)
            return;

        else
        {
            string objName = gameObject.name.Substring(titleSplitNum - 1);
            slotIdx = int.Parse(objName);

            PlayerManager.Instance.SetPlayerData(slotIdx);
            PlayerManager.Instance.SetIsLoaded();
            SceneManagerEx.Instance.SceanLoadQueue(PlayerManager.Instance.GetSceneName());
        }

    }

    /*
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
    */
}
