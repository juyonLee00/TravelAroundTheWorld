using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ch0DialogueBar : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameTxt;
    [SerializeField] Ch0TypeEffect talk;

    public void SetDialogue(string nameData, string dialogue)
    {
        if (nameTxt != null)
        {
            if (string.IsNullOrEmpty(nameData) || nameData == "나레이션")
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
