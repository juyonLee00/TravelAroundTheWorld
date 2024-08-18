using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopStart : MonoBehaviour
{
    //public Image selectedImageDisplay;
    public GameObject newSpeech;
    public GameObject selectedImageDisplay;
    public string fail;
    public string success;
    public string alreadyBuy;
    public TextMeshProUGUI purchase;
    public TextMeshProUGUI description;
    public TextMeshProUGUI pricedescription;

    PlayerManager player;

    int selected;
    int[] price = { 3500, 3000 };
    string[] item = { "milk", "teaSet" };

    
    void Start()
    {
        player = PlayerManager.Instance;

        // 부모 오브젝트 또는 특정 오브젝트 아래의 모든 Button 컴포넌트를 찾습니다.
        Button[] buttons = GetComponentsInChildren<Button>();

        // 각 버튼에 이벤트 리스너를 추가합니다.
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => OnButtonClick(button));
        }

        Button buyBtn = newSpeech.transform.GetChild(0).GetComponent<Button>();
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
        SoundManager.Instance.PlaySFX("click sound");
        newSpeech.SetActive(true);
        selectedImageDisplay.SetActive(true);
        
        // 버튼의 RectTransform 컴포넌트를 가져옵니다.
        RectTransform rectTransform = clickedButton.GetComponent<RectTransform>();

        if (rectTransform.localPosition.x > -200 && rectTransform.localPosition.x < -190)
        {
            selected = 0;
            Debug.Log(selected);
        }
        else
        {
            selected = 1;
            Debug.Log(selected);
        }
        // 부모 오브젝트를 기준으로 한 로컬 좌표계에서의 버튼 위치
        Vector3 localPosition = rectTransform.localPosition;


        localPosition = new Vector3(
            localPosition.x,
            localPosition.y + 41,
            localPosition.z
        );
        
        selectedImageDisplay.transform.localPosition = localPosition;
    }

    void OnBuyButtonClick()
    {
        SoundManager.Instance.PlaySFX("click sound");
        if (false) //PlayerManager.Instance.IsBoughtCafeItem(item[selected]))
        {
            purchase.text = alreadyBuy;
            description.text = "";
            pricedescription.text = "";
        }
        else if (price[selected] > player.GetMoney())
        {
            Debug.Log(player.GetMoney());
            purchase.text = fail;
            description.text = "";
            pricedescription.text = "";
        }
        else
        {
            purchase.text = success;
            description.text = "";
            pricedescription.text = "";
            player.PayMoney(price[selected]);
            player.AddCafeItem(item[selected]);
        }
        
    }

    public void HideSpeech()
    {
        transform.parent.gameObject.SetActive(false);
        selectedImageDisplay.SetActive(false);

    }
}
