using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTurorial : MonoBehaviour
{
    public ScreenFader screenFader; // 페이드인/아웃 효과 스크립트
    public TalkManager talkManager; // 토크 매니저 스크립트

    public GameObject cafe;
    public GameObject mapTutorial_1;
    public GameObject mapTutorial_2;

    private int spaceBarIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        mapTutorial_1.SetActive(false);
        mapTutorial_2.SetActive(false);
        StartCoroutine(screenFader.FadeIn(cafe));  // 맵 씬 연결 (맵 완료되면 코드 수정 필요)
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spaceBarIndex++;
            Tutorial();
        }
    }

    void Tutorial()
    {
        if (spaceBarIndex == 1)
        {
            StartCoroutine(screenFader.FadeIn(mapTutorial_1)); // 처음 스페이스바를 누르면 맵 튜토리얼1 창 활성화
        }
        else if (spaceBarIndex == 2)
        {
            mapTutorial_1.SetActive(false);
            StartCoroutine(screenFader.FadeIn(mapTutorial_2)); // 두번째 스페이스바를 누르면 맵 튜토리얼2 창 활성화
        }
        else if (spaceBarIndex == 3)
        {
            StartCoroutine(screenFader.FadeOut(mapTutorial_2)); //세번째 스페이스바를 누르면 맵 튜토리얼2 창 페이드아웃
        }
        //맵에서 특정 위치에 도달하면 토크매니저를 활성화 하도록 코드 수정 필요
        else
        {
            talkManager.currentDialogueIndex++;
            talkManager.ActivateTalk();
            this.gameObject.SetActive(false); //맵 튜토리얼 창 비활성화
        }
    }
}
