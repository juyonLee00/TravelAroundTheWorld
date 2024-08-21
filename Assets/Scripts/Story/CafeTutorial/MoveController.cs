using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MoveController : MonoBehaviour
{
    private CafeTalkManager cafeTalkManager;
    private RectTransform rectTransform;
    private RectTransform descriptionRectTransform;

    public GameObject explainImg;
    public TextMeshProUGUI descText;

    public Vector2 extractPosition = new Vector2(-460, 325);
    public Vector2 cupPosition = new Vector2(375, 50);
    public Vector2 ingredientPosition = new Vector2(685, 200);
    public Vector2 makePosition = new Vector2(685, 0);
    public Vector2 donePosition = new Vector2(685, -300);

    public Sprite DownL;
    public Sprite Left;
    public Sprite TopL;

    private Image imageComponent;

    void Start()
    {
        cafeTalkManager = FindObjectOfType<CafeTalkManager>();

        rectTransform = explainImg.GetComponent<RectTransform>();
        descriptionRectTransform = descText.GetComponent<RectTransform>();

        imageComponent = explainImg.GetComponent<Image>();

        DownL = Resources.Load<Sprite>("CafeImage/mal_DownL");
        Left = Resources.Load<Sprite>("CafeImage/mal_Left");
        TopL = Resources.Load<Sprite>("CafeImage/mal_TopL");     
    }

    void Update()
    {
        Vector2 newPosition = Vector2.zero;
        Vector2 descNewPosition = new Vector2(0, 25);
        
        switch (cafeTalkManager.currentDialogueIndex)
        {
            case 40:
            case 41:
            case 42:
                newPosition = extractPosition;
                imageComponent.sprite = DownL;
                break;
            case 43:
            case 44:
            case 45:
                newPosition = cupPosition;
                descNewPosition = new Vector2(25, 0);
                imageComponent.sprite = Left;
                break;
            case 46:
                newPosition = ingredientPosition;
                descNewPosition = new Vector2(0, 0);
                imageComponent.sprite = TopL;
                break;
            case 47:
            case 48:
                newPosition = makePosition;
                imageComponent.sprite = DownL;
                break;
            case 49:
                newPosition = donePosition;
                imageComponent.sprite = DownL;
                break;
            case 50:
                newPosition = new Vector2(-9999, -9999); // 화면 밖으로 이동
                break;
            case 51:
                newPosition = makePosition;
                imageComponent.sprite = DownL;
                break;
            default:
                return;
        }

        rectTransform.anchoredPosition = newPosition;
        descriptionRectTransform.anchoredPosition = descNewPosition;
    }
}