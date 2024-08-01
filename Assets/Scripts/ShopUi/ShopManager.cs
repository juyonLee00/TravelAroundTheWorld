using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public GameObject shopPrefab; // 상점 UI 프리팹

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
        //Transform parentTransform = transform.parent;
        Destroy(transform.parent.gameObject);
    }

}

