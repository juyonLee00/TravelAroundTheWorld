using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveDataUI : MonoBehaviour
{
    public GameObject saveDataBackground;
    public GameObject titleTxt;
    public GameObject scrollView;
    public GameObject exitBtn;

    private Vector2 titlePos;
    private Vector2 defaultPos;
    private Vector2 scrollPos;
    private Vector2 exitPos;

    private UnityEngine.Events.UnityAction exitBtnEvent;

    void Start()
    {
        SetInitData();
        CreateUIComponent(saveDataBackground, defaultPos);
        CreateUIComponent(titleTxt, titlePos);
        CreateUIComponent(scrollView, scrollPos); 
        SetScrollViewData();
        CreateUIComponent(exitBtn, exitPos);
        SetBtnEvent();
    }


    void SetInitData()
    {
        defaultPos = Vector2.zero;
        titlePos = new Vector2(-420, 395);
        scrollPos = new Vector2(0, -25);
        exitPos = new Vector2(-819, 426);
    }

    public void CreateUIComponent(GameObject obj, Vector2 pos)
    {
        GameObject placedObj = Instantiate(obj);
        placedObj.transform.SetParent(gameObject.transform, false);

        RectTransform rectTransform = placedObj.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = pos;

    }

    public void CreateUIComponentInParent(GameObject obj, Vector2 pos)
    {
        GameObject placedObj = Instantiate(obj);
        GameObject staticUICanvas = GameObject.Find("StaticUICanvas");
        placedObj.transform.SetParent(staticUICanvas.transform, false);

        RectTransform rectTransform = placedObj.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = pos;

    }

    public void CreateUIComponent(GameObject obj, Vector2 pos, bool isActivated)
    {
        GameObject placedObj = Instantiate(obj);
        placedObj.transform.SetParent(gameObject.transform, false);

        RectTransform rectTransform = placedObj.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = pos;
        placedObj.SetActive(isActivated);
    }

    public void SetScrollViewData()
    {
        List<int> saveDataList = SaveDataManager.Instance.GetAvailableSaveSlots();
        int saveDataListCount = saveDataList.Count;

        if (saveDataListCount == 0)
            return;

        else
        {
            ScrollRect scrollRect = scrollView.GetComponent<ScrollRect>();

            for(int i=0; i<saveDataListCount; i++)
            {
                TextMeshProUGUI[] textArr = scrollRect.content.GetChild(i).GetComponents<TextMeshProUGUI>();
                textArr[0].text = PlayerManager.Instance.GetIsAutoSaveToString();
                textArr[1].text = PlayerManager.Instance.GetSceneName();
                textArr[2].text = PlayerManager.Instance.GetMapLocation().ToString();
                textArr[3].text = PlayerManager.Instance.GetSaveTime().ToString();
            }
        }
    }

    void SetBtnEvent()
    {
        exitBtnEvent = CancleUIFunc;
        Button btn = exitBtn.GetComponent<Button>();
        btn.onClick.AddListener(exitBtnEvent);
        Debug.Log("HIHIHI");
    }

    public void CancleUIFunc()
    {
        Debug.Log("HI");
        SoundManager.Instance.PlaySFX("click sound");
        //UIManager.Instance.ToggleUI("SaveData");
        UIManager.Instance.DeactivatedUI("SaveData");
    }

}
