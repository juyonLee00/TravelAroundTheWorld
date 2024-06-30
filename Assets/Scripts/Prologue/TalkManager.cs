using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro 네임스페이스 추가

// ProDialogue 클래스 정의 (프롤로그 대사 저장)
public class ProDialogue
{
    public int id; // 아이디
    public string speaker; // 발화자
    public string line; // 대사

    public ProDialogue(int id, string speaker, string line)
    {
        this.id = id;
        this.speaker = speaker;
        this.line = line;
    }
}

public class TalkManager : MonoBehaviour
{
    // 대사들을 저장할 리스트
    private List<ProDialogue> proDialogue;

    public GameObject narration;
    public GameObject dialogue;
    public TextMeshProUGUI NarrationText; // TextMeshPro UI 텍스트 요소
    public TextMeshProUGUI DescriptionText; // TextMeshPro UI 텍스트 요소
    public TextMeshProUGUI NameText; // TextMeshPro UI 텍스트 요소

    private int currentDialogueIndex = 0; // 현재 대사 인덱스

    void Awake()
    {
        proDialogue = new List<ProDialogue>();
        GenerateData(); // 데이터 생성 함수 호출
    }

    void GenerateData()
    {
        proDialogue.Add(new ProDialogue(1, "나레이션", "모든 것은 이 비밀스러운 초대장으로부터 시작되었다."));
        proDialogue.Add(new ProDialogue(1, "나레이션", "때는 꽃 내음이 물씬 나고 여린 초록 이파리가 차츰 나기 시작하는 새벽이었다."));
        proDialogue.Add(new ProDialogue(2, "솔", "새삼 오랜만이네.."));
        proDialogue.Add(new ProDialogue(3, "나레이션", "초대장에는 반짝거리는 듯한 왁스로 정성스럽게 실링 되어 있었다."));
        proDialogue.Add(new ProDialogue(4, "솔", "어딘가 익숙한데.."));
        proDialogue.Add(new ProDialogue(5, "나레이션", "천천히 동봉된 편지를 열어보니 순간 달콤한 향기가 퍼지며 눈앞에 글자들이 나타났다."));
        proDialogue.Add(new ProDialogue(6, "나레이션", "그제야 처음 동봉된 실링의 로고를 어디서 본 것인지 기억이 났다."));
        proDialogue.Add(new ProDialogue(6, "나레이션", "카페에서 손님이 없을 때마다 바라본 TV에서 계속해서 광고하던 루나 익스프레스의 로고였다."));
        proDialogue.Add(new ProDialogue(6, "나레이션", "돈이 있어도 갈 수 없고, 오로지 발송된 초대장을 통해서만 입장할 수 있다던 그곳."));
        proDialogue.Add(new ProDialogue(6, "나레이션", "초대장에 적힌 비밀스러운 공간에 가야지만 탑승이 가능하고,"));
        proDialogue.Add(new ProDialogue(6, "나레이션", "그 장소는 매번 바뀌어 아무도 탑승역이 어딘지 모른다던 미스터리한 곳이었다."));
        proDialogue.Add(new ProDialogue(7, "솔", "여기는...?"));
        proDialogue.Add(new ProDialogue(8, "나레이션", "가고 싶어도 갈 수 없는 사람들이 많다는 이곳."));
        proDialogue.Add(new ProDialogue(8, "나레이션", "어째서 나에게 발송이 된 것인지는 전혀 알 수가 없었다."));
        proDialogue.Add(new ProDialogue(9, "솔", "잠깐만… 당장 내일이잖아??"));
        proDialogue.Add(new ProDialogue(10, "나레이션", "달력을 보면서 내 눈을 의심했지만, 바뀌는 것은 아무것도 없었다."));
        proDialogue.Add(new ProDialogue(10, "나레이션", "처음에는 내일도 운영해야 하는 카페가 걱정이었지만,"));
        proDialogue.Add(new ProDialogue(10, "나레이션", "손님도 오지 않고 특별한 일 없이 똑같이 운영되는 카페에 지쳐있던 상황에 잘 되었다는 생각이 들었다."));
        proDialogue.Add(new ProDialogue(10, "나레이션", "그 생각이 들자, 바로 짐을 싸기 시작했다."));
        proDialogue.Add(new ProDialogue(10, "나레이션", "주저할 시간이 없었다."));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PrintProDialogue(currentDialogueIndex);
            currentDialogueIndex++;
        }
    }

    void PrintProDialogue(int index)
    {
        if (index >= proDialogue.Count) return; // 대사 리스트를 벗어나면 리턴

        ProDialogue currentDialogue = proDialogue[index];

        if (currentDialogue.speaker == "나레이션")
        {
            narration.SetActive(true);
            dialogue.SetActive(false);
            NarrationText.text = currentDialogue.line;
        }
        else
        {
            narration.SetActive(false);
            dialogue.SetActive(true);
            NameText.text = currentDialogue.speaker;
            DescriptionText.text = currentDialogue.line;
        }
    }
}
