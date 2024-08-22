using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BlinkObject : MonoBehaviour
{
    public TextMeshProUGUI pressTxt;
    public Image boxImg;
    public float blinkDuration = 1.0f; 
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

        isBlinking = true;
        StartCoroutine(BlinkText());
    }

    IEnumerator BlinkText()
    {
        while (isBlinking)
        {
            pressTxt.alpha = 0;
            imgColor.a = 0f;
            boxImg.color = imgColor;
            yield return new WaitForSeconds(blinkDuration);

            pressTxt.alpha = 1;
            imgColor.a = 1f;
            boxImg.color = imgColor;
            yield return new WaitForSeconds(blinkDuration);
        }
    }

    private void OnDisable()
    {
        isBlinking = false;
    }
}
