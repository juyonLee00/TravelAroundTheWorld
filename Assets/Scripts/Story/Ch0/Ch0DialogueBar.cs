using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ch0DialogueBar : MonoBehaviour
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

    public bool IsTyping()
    {
        // 현재 타이핑 중인지 확인
        return talk.IsTyping(); 
    }

    public void CompleteTypingEffect()
    {
        // 타이핑 이펙트를 즉시 완료
        talk.CompleteEffect(); 
    }
}
