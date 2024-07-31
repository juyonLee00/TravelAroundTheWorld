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

    private UIManager uIManager;

    private void Awake()
    {
        uIManager = FindObjectOfType<UIManager>();
    }

    void Start()
    {
        SetComponent();
        AllocationCheckFunc();
        LoadSaveData();
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
        //현재 렌더링된 saveDAtaPopup 오브젝트 가져오기 
        saveDataPopup = uIManager.FindChildByName(canvas, "SaveDataPopup(Clone)");
    }

    void LoadSaveData()
    {
        string objName = gameObject.name.Substring(titleSplitNum-1);
        slotIdx = int.Parse(objName);
        /*
         * saveImg = saveDataManager.Instance.saveDataList[idx].Image;
         */
    }

    void ClickDataSlotFunc()
    {
        Debug.Log("HI");
        
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
