using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class SaveDataSlot : MonoBehaviour
{
    private Image saveImg;
    private TextMeshProUGUI saveChapterTxt;
    private TextMeshProUGUI savePosTxt;
    private TextMeshProUGUI saveTimeTxt;
    private TextMeshProUGUI saveCurDayTxt;
    private int titleSplitNum;

    public int slotIdx;
    public GameObject saveDataPopup;
    public GameObject canvas;

    private Button btn;

    void Start()
    {
        SetComponent();
        //AllocationCheckFunc();
        SetSlotData();
    }

    void SetComponent()
    {
        GameObject canvas = GameObject.Find("StaticUICanvas");
        titleSplitNum = gameObject.name.Length;
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(ClickDataSlotFunc);
        saveImg = gameObject.GetComponentsInChildren<Image>(true).FirstOrDefault(img => img.name == "SaveImg");

        TextMeshProUGUI[] textMeshProUGUIs = gameObject.GetComponentsInChildren<TextMeshProUGUI>(true);

        foreach(var comp in textMeshProUGUIs)
        {
            switch(comp.gameObject.name)
            {
                case "SaveCurDayTxt":
                    saveCurDayTxt = comp;
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

    void SetSlotData()
    {
        string objName = gameObject.name.Substring(titleSplitNum - 1);
        slotIdx = int.Parse(objName);

        if (!SaveDataManager.Instance.HasSaveData())
        {
            //데이터 없으면 해당 부분 투명화
            Color nonDataColor = new Color(0, 0, 0);
            nonDataColor.a = 0f;
            saveImg.color = nonDataColor;
            return;
        }
            
        else
        {
            saveChapterTxt.text = "CH " + SaveDataManager.Instance.LoadGame(slotIdx).currentSceneName;
            savePosTxt.text = SaveDataManager.Instance.LoadGame(slotIdx).mapLocation.ToString();
            saveTimeTxt.text = SaveDataManager.Instance.LoadGame(slotIdx).saveTime.ToString();
            saveCurDayTxt.text = "day " + SaveDataManager.Instance.LoadGame(slotIdx).currentDay.ToString();
        }
    }

    void ClickDataSlotFunc()
    {
        if (!SaveDataManager.Instance.HasSaveData())
        {
            return;
        }
            

        else
        {
            Debug.Log(SaveDataManager.Instance.GetSaveDataCount());
            if (slotIdx > SaveDataManager.Instance.GetSaveDataCount() && saveChapterTxt == null)
            {
                return;
            }

            else
            {
                PlayerManager.Instance.SetPlayerData(slotIdx);
                PlayerManager.Instance.SetIsLoaded();
                SceneManagerEx.Instance.SceanLoadQueue(PlayerManager.Instance.GetSceneName());
            }
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
