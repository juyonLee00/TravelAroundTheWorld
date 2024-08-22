using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BlinkObject : MonoBehaviour
{
    public TextMeshProUGUI pressTxt;
    public Image boxImg;
    public float blinkDuration = 1.0f;  // 깜빡이는 속도 (초 단위)
    private Color imgColor;

    private bool isBlinking = false;

    void Start()
    {
        if (pressTxt == null)
        {
            pressTxt = GetComponentInChildren<TextMeshProUGUI>();
        }

        if(boxImg == null)
        {
            boxImg = GetComponentInChildren<Image>();
        }
        imgColor = boxImg.color;

        StartCoroutine(BlinkText());
    }

    IEnumerator BlinkText()
    {
        while (true)
        {
            pressTxt.alpha = 0;
            imgColor.a = 0f;
            boxImg.color = imgColor;
            yield return new WaitForSeconds(blinkDuration);

            // 텍스트를 다시 보이게
            pressTxt.alpha = 1;
            imgColor.a = 1f;
            boxImg.color = imgColor;
            yield return new WaitForSeconds(blinkDuration);
        }
    }
}
