using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public GameObject shopPrefab; // 상점 UI 프리팹
    public GameObject selectedImageDisplay;

    // 버튼 클릭 시 호출될 메서드
    public void ShowShop()
    {
        Transform parentTransform = transform.parent;

        GameObject newObject = Instantiate(shopPrefab, parentTransform);
        newObject.SetActive(true);
    }

    // 상점 UI를 숨기는 메서드
    public void HideShop()
    {
        SoundManager.Instance.PlayMusic("click sound", loop: false);
        //Transform parentTransform = transform.parent;
        Destroy(transform.parent.gameObject);
    }

    public void HideSpeech() 
    {
        SoundManager.Instance.PlayMusic("click sound", loop: false);
        transform.parent.gameObject.SetActive(false);
        selectedImageDisplay.SetActive(false);
    }

}

