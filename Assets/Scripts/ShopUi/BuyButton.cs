using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Unity.Collections.AllocatorManager;

public class BuyButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite enterSprite;
    public Sprite exitSprite;

    bool click = false;

    public void OnClicked()
    {
        StartCoroutine(ClickRoutine());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!click)
        {
            transform.GetComponent<Image>().sprite = enterSprite;
        }
        
    }

    // 마우스가 슬롯을 벗어났을 때 실행되는 함수
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!click)
        {
            transform.GetComponent<Image>().sprite = exitSprite;
        }
        
        
    }

    private IEnumerator ClickRoutine()
    {
        click = true;

        // 1초 동안 대기
        yield return new WaitForSeconds(1f);

        click = false;
        transform.GetComponent<Image>().sprite = exitSprite;
    }
}
