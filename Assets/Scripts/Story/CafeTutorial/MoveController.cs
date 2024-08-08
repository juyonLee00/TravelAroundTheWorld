using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoveController : MonoBehaviour
{
    private CafeTalkManager cafeTalkManager;
    private RectTransform rectTransform;

    public Vector2 extractPosition = new Vector2(-964.16f, -538.02f);
    public Vector2 cupPosition = new Vector2(-957.68f, -539.35f);
    public Vector2 ingredientPosition = new Vector2(-954.03f, -536.07f);
    public Vector2 makePosition = new Vector2(-953.84f, -540.12f);
    public Vector2 donePosition = new Vector2(-955.17f, -543.63f);

    void Start()
    {
        cafeTalkManager = FindObjectOfType<CafeTalkManager>();
        rectTransform = GetComponent<RectTransform>();

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

    }
}
