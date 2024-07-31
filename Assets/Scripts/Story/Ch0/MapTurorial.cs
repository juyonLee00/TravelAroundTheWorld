using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTurorial : MonoBehaviour
{
    public ScreenFader screenFader; // 페이드인/아웃 효과 스크립트
    public TalkManager talkManager; // 토크 매니저 스크립트

    public GameObject mapTutorial_1;
    public GameObject mapTutorial_2;

    public GameObject player;
    public GameObject map;

    public List<GameObject> npcs; // NPC 오브젝트를 참조하기 위한 리스트
    public int activeNpcCount = 0; // 활성화된 NPC의 수

    private int spaceBarIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        mapTutorial_1.SetActive(false);
        mapTutorial_2.SetActive(false);
        StartCoroutine(screenFader.FadeIn(map));  // 맵 활성화 (맵 완료되면 코드 수정 필요)
        StartCoroutine(screenFader.FadeIn(mapTutorial_1)); // 맵 튜토리얼1 창 활성화
        player.SetActive(true); //플레이어 오브젝트 활성화
        ActivateNPC(true);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spaceBarIndex++;
            Tutorial();
        }

        int talkActivedNPC = CheckAllNpcTalkStatus();
        //모든 npc 대화 완료된 경우 로직 추가 필요
        if (activeNpcCount == talkActivedNPC)
        {
            //Debug.Log("모든 npc 대화 완료");
        }
    }

    void Tutorial()
    {
        if (spaceBarIndex == 1)
        {
            mapTutorial_1.SetActive(false);
            StartCoroutine(screenFader.FadeIn(mapTutorial_2)); // 처음 스페이스바를 누르면 맵 튜토리얼2 창 활성화
        }
        else if (spaceBarIndex == 2)
        {
            StartCoroutine(screenFader.FadeOut(mapTutorial_2)); //세번째 스페이스바를 누르면 맵 튜토리얼2 창 페이드아웃
            //this.gameObject.SetActive(false); //맵 튜토리얼 창 비활성화
        }
    }

    // 모든 NPC를 활성화 또는 비활성화
    private void ActivateNPC(bool isActive)
    {
        foreach (GameObject npc in npcs)
        {
            npc.SetActive(isActive); // NPC 오브젝트 활성화 또는 비활성화
            activeNpcCount++;
        }
    }

    // 모든 NPC의 talkActivated 상태를 확인
    private int CheckAllNpcTalkStatus()
    {
        int talkActivedCount = 0; // talkActived가 true인 NPC의 수

        foreach (GameObject npc in npcs)
        {
            if (npc.activeInHierarchy) // NPC가 활성화되어 있는 경우
            {
                var triggerArea = npc.GetComponent<Ch0TriggerArea>();
                if (triggerArea != null)
                {
                    if (triggerArea.talkActived) // talkActived가 true인 경우
                    {
                        talkActivedCount++; // talkActived가 true인 NPC의 수 증가
                    }
                }
            }
        }

        return talkActivedCount;
    }
}
