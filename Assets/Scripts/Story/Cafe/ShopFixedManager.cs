using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopFixedManager : MonoBehaviour
{
    public GameObject nomalShop; // 상점 UI 프리팹
    public GameObject milkShop;
    public GameObject teaSetShop;
    public GameObject selectedImageDisplay;

    private Ch1TalkManager talkManager;

    void Start()
    {
        talkManager = FindObjectOfType<Ch1TalkManager>();
    }

    // 버튼 클릭 시 호출될 메서드
    public void ShowShop()
    {
        Transform parentTransform = transform.parent;

        if (PlayerManager.Instance.IsBoughtCafeItem("milk"))
        {
            Instantiate(teaSetShop, parentTransform);
            return;
        }
        else if (PlayerManager.Instance.IsBoughtCafeItem("teaSet"))
        {
            Instantiate(milkShop, parentTransform);
            return;
        }
        else
        {
            Instantiate(nomalShop, parentTransform);
            return;
        }
    }

    // 상점 UI를 숨기는 메서드
    public void HideShop()
    {
        SoundManager.Instance.PlaySFX("click sound");
        if (talkManager != null)
        {
            talkManager.OnShopClosed();
        }
        //Transform parentTransform = transform.parent;
        Destroy(transform.parent.gameObject);
    }

    public void HideSpeech()
    {
        SoundManager.Instance.PlaySFX("click sound");
        transform.parent.gameObject.SetActive(false);
        selectedImageDisplay.SetActive(false);
    }

}

