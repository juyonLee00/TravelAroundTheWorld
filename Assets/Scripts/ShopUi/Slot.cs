using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Sprite itemImage; // 아이템 이미지
    public string itemName;
    public string itemDescription; // 아이템 설명

    public Image itemImageDisplay;
    public TextMeshProUGUI itemNameDisplay;
    public TextMeshProUGUI itemDescriptionDisplay;

    public void OnSlotClicked()
    {
        itemImageDisplay.sprite = itemImage;
        itemNameDisplay.text = itemName;
        itemDescriptionDisplay.text = itemDescription; // 아이템 설명
;
        //shopManager.DisplayItemDetails(itemImage.sprite, itemDescription, this);
    }
}
