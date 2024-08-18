using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ch0TypeEffect : MonoBehaviour
{
    public GameObject endCursor;
    public int charPerSeconds;
    string targetMsg;
    [SerializeField] TextMeshProUGUI msgText;
    int idx;
    float interval;
    private bool isTyping;

    public void SetMsg(string msg)
    {
        targetMsg = msg;
        StartCoroutine(EffectStart());
    }

    IEnumerator EffectStart()
    {
        isTyping = true;
        msgText.text = "";
        idx = 0;
        interval = 1.0f / charPerSeconds;

        if (endCursor != null)
        {
            endCursor.SetActive(false);
        }

        while (idx < targetMsg.Length)
        {
            if (msgText != null)
            {
                msgText.text += targetMsg[idx];
            }
            idx++;
            yield return new WaitForSeconds(interval);
        }

        EffectEnd();
    }

    public void CompleteEffect()
    {
        if (isTyping)
        {
            // 현재 실행 중인 코루틴을 중지
            StopAllCoroutines();
            // 전체 텍스트를 한 번에 표시
            msgText.text = targetMsg;
            // 이펙트 종료 처리
            EffectEnd(); 
        }
    }

    void EffectEnd()
    {
        isTyping = false;
        if (endCursor != null)
        {
            endCursor.SetActive(true);
        }
    }

    public bool IsTyping()
    {
        return isTyping;
    }
}
