using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoveController : MonoBehaviour
{
    private CafeTalkManager cafeTalkManager;
    private RectTransform rectTransform;
    private RectTransform descriptionRectTransform;

    public Vector2 extractPosition = new Vector2(-964.16f, -538.02f);
    public Vector2 cupPosition = new Vector2(-957.68f, -539.35f);
    public Vector2 ingredientPosition = new Vector2(-954.03f, -536.07f);
    public Vector2 makePosition = new Vector2(-953.84f, -540.12f);
    public Vector2 donePosition = new Vector2(-955.17f, -543.63f);

    void Start()
    {
        cafeTalkManager = FindObjectOfType<CafeTalkManager>();
        rectTransform = GetComponent<RectTransform>();
        descriptionRectTransform = transform.Find("Description").GetComponent<RectTransform>();

        // Ensure that Description has the same scale as its parent
        descriptionRectTransform.localScale = Vector3.one;

        // Ensure that Description has the correct z-order
        descriptionRectTransform.SetSiblingIndex(rectTransform.GetSiblingIndex() + 1);
    }

    void Update()
    {
        Vector2 newPosition = Vector2.zero;

        switch (cafeTalkManager.currentDialogueIndex)
        {
            case 40:
            case 41:
            case 42:
                newPosition = extractPosition;
                break;
            case 43:
            case 44:
            case 45:
                newPosition = cupPosition;
                break;
            case 46:
                newPosition = ingredientPosition;
                break;
            case 47:
            case 48:
            case 51:
                newPosition = makePosition;
                break;
            case 49:
                newPosition = donePosition;
                break;
            default:
                return;
        }

        rectTransform.anchoredPosition = newPosition;
        descriptionRectTransform.anchoredPosition = Vector2.zero; // 부모 위치 기준으로 Description 위치 조정

        Debug.Log("ExplainBar new position: " + newPosition);
        Debug.Log("Description parent: " + descriptionRectTransform.parent.name);
        Debug.Log("Description current position: " + descriptionRectTransform.anchoredPosition);
    }
}
