using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopStart : MonoBehaviour
{
    //public Image selectedImageDisplay;
    public GameObject buyButton;
    public GameObject selectedImageDisplay;
    public string fail;
    public string success;
    public TextMeshProUGUI purchase;

    GameObject player;
    TestPlayer test;

    int selected;
    int[] price = { 3500, 3000 };

    
    void Start()
    {
        player = GameObject.Find("TestPlayer");
        test = player.GetComponent<TestPlayer>();
        if (test != null) 
        { 
            Debug.Log("null"); 
        } else { Debug.Log("not null"); }

        // 부모 오브젝트 또는 특정 오브젝트 아래의 모든 Button 컴포넌트를 찾습니다.
        Button[] buttons = GetComponentsInChildren<Button>();

        // 각 버튼에 이벤트 리스너를 추가합니다.
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => OnButtonClick(button));
        }

        Button buyBtn = buyButton.GetComponent<Button>();
        if (buyBtn != null)
        {
            buyBtn.onClick.AddListener(OnBuyButtonClick);
        }
        else
        {
            Debug.LogError("buyButton does not have a Button component.");
        }
    }

    // 버튼이 눌렸을 때 호출되는 메소드
    void OnButtonClick(Button clickedButton)
    {
        
        buyButton.SetActive(true);
        selectedImageDisplay.SetActive(true);
        
        // 버튼의 RectTransform 컴포넌트를 가져옵니다.
        RectTransform rectTransform = clickedButton.GetComponent<RectTransform>();
        if (rectTransform.localPosition.x > -76 && rectTransform.localPosition.x < -74)
        {
            selected = 0;
            Debug.Log(selected);
        }
        else
        {
            selected = 1;
            Debug.Log(selected);
        }
        Debug.Log(rectTransform.localPosition.x);
        // 부모 오브젝트를 기준으로 한 로컬 좌표계에서의 버튼 위치
        Vector3 localPosition = rectTransform.localPosition;
        localPosition = new Vector3(
            localPosition.x - 2,
            localPosition.y + 24,
            localPosition.z
        );
        
        selectedImageDisplay.transform.localPosition = localPosition;


        //Debug.Log(rectTransform);
        //Debug.Log(localPosition);
        

    }

    void OnBuyButtonClick()
    {
        Debug.Log("Buy Button Clicked!");
        if (test.bean == 500)
        {
            Debug.Log("500");
        }
        else Debug.Log("not 500");

        if (price[selected] > test.bean)
        {
            purchase.text = fail;
        }
        else
        {
            purchase.text = success;
            test.bean -= price[selected];
            test.list[selected] = true;
        }
        
    }
}
