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

    public void SetMsg(string msg)
    {
        targetMsg = msg;
        StartCoroutine(EffectStart());
    }

    IEnumerator EffectStart()
    {
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

    /*
    void EffectStart()
    {
        msgText.text = "";
        idx = 0;
        interval = 1.0f / charPerSeconds;

        if (endCursor != null)
        {
            endCursor.SetActive(false);
        }

        Invoke("Effecting", 1 / charPerSeconds);
    }

    void Effecting()
    {
        if (msgText.text == targetMsg)
        {
            EffectEnd();
            return;
        }
        msgText.text += targetMsg[idx];
        idx++;

        Invoke("Effecting", interval);
    }
    */

    void EffectEnd()
    {
        if (endCursor != null)
        {
            endCursor.SetActive(true);
        }
    }
}
