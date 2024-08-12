using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems; // IPointerEnterHandler를 사용하기 위해 필요
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler // 인터페이스 구현
{
    public string itemDescription; // 아이템 설명
    public string itemPrice; // 아이템 가격
    public TextMeshProUGUI itemDescriptionDisplay;
    public TextMeshProUGUI itemPriceDisplay;
    public TextMeshProUGUI result;
    public Sprite enterSprite;
    public Sprite exitSprite;
    public GameObject slot;

    bool click = false;

    public void OnSlotClicked()
    {
        itemDescriptionDisplay.text = itemDescription; // 아이템 설명
        itemPriceDisplay.text = itemPrice;
        result.text = "";
        StartCoroutine(ClickRoutine());
    }

    // 마우스가 슬롯 위에 올라갔을 때 실행되는 함수
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!click)
        {
            slot.GetComponent<Image>().sprite = enterSprite;
        }
        
    }

    // 마우스가 슬롯을 벗어났을 때 실행되는 함수
    public void OnPointerExit(PointerEventData eventData)
    {
        if(!click)
        {
            slot.GetComponent<Image>().sprite = exitSprite;
        }
        
    }
    private IEnumerator ClickRoutine()
    {
        click = true;

        // 1초 동안 대기
        yield return new WaitForSeconds(1f);

        click = false;
        slot.GetComponent<Image>().sprite = exitSprite;
    }
}
