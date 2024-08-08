using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CafeTutoDialogueBar : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameTxt;
    [SerializeField] Ch0TypeEffect talk;

    // 문자열 상수 선언
    private const string narrationSpeaker = "나레이션";

    public void SetDialogue(string nameData, string dialogue)
    {
        if (nameTxt != null)
        {
            if (string.IsNullOrEmpty(nameData) || nameData == narrationSpeaker)
            {
                nameTxt.gameObject.SetActive(false);
            }
            else
            {
                nameTxt.gameObject.SetActive(true);
                nameTxt.text = nameData;
            }
        }
        talk.SetMsg(dialogue);
    }
}