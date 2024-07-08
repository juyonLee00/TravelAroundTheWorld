using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invitation : MonoBehaviour
{
    public TalkManager talkManager; // TalkManager 오브젝트
    private bool talkActivated = false; // TalkManager가 이미 활성화되었는지 여부

    void Update()
    {
        if (!talkActivated)
        {
            TalkManagerOn();
        }
    }

    void TalkManagerOn()
    {
        talkManager.ActivateTalk();
        talkActivated = true;
    }
}
