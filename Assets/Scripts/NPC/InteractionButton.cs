using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionButton : MonoBehaviour
{
    private Button button; // 버튼 컴포넌트

    private void Start()
    {
        button = GetComponent<Button>(); // 버튼 컴포넌트를 가져옴
        button.onClick.AddListener(OnInteract); // 버튼 클릭 시 OnInteract 메서드 호출
        gameObject.SetActive(false); // 시작할 때 버튼 비활성화
    }

    // 버튼 클릭 시 호출되는 메서드
    public void OnInteract()
    {
        TalkManagerCH1 talkManager = FindObjectOfType<TalkManagerCH1>();
        if (talkManager != null)
        {
            // TalkManagerCH1의 ActivateTalk 메서드를 호출하여 대화를 시작
            talkManager.ActivateTalk("defaultLocation"); // 적절한 locationName 인자 전달
        }
    }
}