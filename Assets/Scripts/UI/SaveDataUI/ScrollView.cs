using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class ScrollView : MonoBehaviour
{
    public GameObject dataSlotPrefab;
    private Scrollbar verticalScrollbar;
    private ScrollRect scrollRect;

    private RectTransform content;

    private Vector3 dataSlotPos;

    private int yInterval;
    private int dataNum;

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
        content = scrollRect.content;
        verticalScrollbar = scrollRect.verticalScrollbar;

        if (scrollRect == null || content == null || verticalScrollbar == null)
        {
            Debug.LogError("ScrollRect, Content 또는 VerticalScrollbar를 찾을 수 없습니다.");
        }
    }

    void Start()
    {
        SetInitData();
        CreateScrollData();
    }

    void SetInitData()
    {
        dataSlotPos = Vector3.zero;
        yInterval = -220;
        dataNum = 10;
    }

    void CreateScrollData()
    {
        for(int i=0; i<dataNum; i++)
        {
            GameObject slot = Instantiate(dataSlotPrefab, content);
            
            slot.transform.localPosition = dataSlotPos;

            slot.name = "DataSlot" + i;
            dataSlotPos.y += yInterval;
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(content);
        RectTransform contentRect = content.GetComponent<RectTransform>();
        contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, Mathf.Abs(dataSlotPos.y));

        verticalScrollbar.gameObject.SetActive(true);
    }
}



