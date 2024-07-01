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
    public TextMeshProUGUI narrationText; // TextMeshPro UI 텍스트 요소

    public GameObject dialogue;
    public GameObject imageObj; // 초상화 이미지 요소
    public GameObject nameObj; // 이름 요소
    public TextMeshProUGUI nameText; // TextMeshPro UI 텍스트 요소
    public TextMeshProUGUI descriptionText; // TextMeshPro UI 텍스트 요소

    public GameObject invitation; // 초대장 오브젝트
    public TextMeshProUGUI invitationText; // TextMeshPro UI 텍스트 요소

    private int currentDialogueIndex = 0; // 현재 대사 인덱스
    private bool isActivated = false; // TalkManager가 활성화되었는지 여부

    void Awake()
    {
        proDialogue = new List<ProDialogue>();
        GenerateData(); // 데이터 생성 함수 호출
    }

    void Start()
    {
        // 처음에 TalkManager 오브젝트 비활성화
        this.gameObject.SetActive(false);
    }

    void Update()
    {
        CheckTalk();
        if (isActivated && Input.GetKeyDown(KeyCode.Space))
        {
            PrintProDialogue(currentDialogueIndex);
            currentDialogueIndex++;
        }
    }

    void GenerateData()
    {
        proDialogue.Add(new ProDialogue(1, "솔", "새삼 오랜만이네.."));
        proDialogue.Add(new ProDialogue(2, "나레이션", "초대장에는 반짝거리는 듯한 왁스로 정성스럽게 실링 되어 있었다."));
        proDialogue.Add(new ProDialogue(3, "솔", "어딘가 익숙한데.."));
        proDialogue.Add(new ProDialogue(4, "나레이션", "천천히 동봉된 편지를 열어보니 순간 달콤한 향기가 퍼지며 눈앞에 글자들이 나타났다."));
        proDialogue.Add(new ProDialogue(5, "나레이션", "그제야 처음 동봉된 실링의 로고를 어디서 본 것인지 기억이 났다."));
        proDialogue.Add(new ProDialogue(5, "나레이션", "카페에서 손님이 없을 때마다 바라본 TV에서 계속해서 광고하던 루나 익스프레스의 로고였다."));
        proDialogue.Add(new ProDialogue(5, "나레이션", "돈이 있어도 갈 수 없고, 오로지 발송된 초대장을 통해서만 입장할 수 있다던 그곳."));
        proDialogue.Add(new ProDialogue(5, "나레이션", "초대장에 적힌 비밀스러운 공간에 가야지만 탑승이 가능하고,"));
        proDialogue.Add(new ProDialogue(5, "나레이션", "그 장소는 매번 바뀌어 아무도 탑승역이 어딘지 모른다던 미스터리한 곳이었다."));
        proDialogue.Add(new ProDialogue(6, "솔", "여기는...?"));
        proDialogue.Add(new ProDialogue(7, "나레이션", "가고 싶어도 갈 수 없는 사람들이 많다는 이곳."));
        proDialogue.Add(new ProDialogue(7, "나레이션", "어째서 나에게 발송이 된 것인지는 전혀 알 수가 없었다."));
        proDialogue.Add(new ProDialogue(8, "솔", "잠깐만… 당장 내일이잖아??"));
        proDialogue.Add(new ProDialogue(9, "나레이션", "달력을 보면서 내 눈을 의심했지만, 바뀌는 것은 아무것도 없었다."));
        proDialogue.Add(new ProDialogue(9, "나레이션", "처음에는 내일도 운영해야 하는 카페가 걱정이었지만,"));
        proDialogue.Add(new ProDialogue(9, "나레이션", "손님도 오지 않고 특별한 일 없이 똑같이 운영되는 카페에 지쳐있던 상황에 잘 되었다는 생각이 들었다."));
        proDialogue.Add(new ProDialogue(9, "나레이션", "그 생각이 들자, 바로 짐을 싸기 시작했다."));
        proDialogue.Add(new ProDialogue(9, "나레이션", "주저할 시간이 없었다."));
    }

    void PrintProDialogue(int index)
    {
        if (index >= proDialogue.Count)
        {
            narration.SetActive(false);
            dialogue.SetActive(false);
            return; // 대사 리스트를 벗어나면 오브젝트 비활성화 후 리턴
        }

        ProDialogue currentDialogue = proDialogue[index];

        if (currentDialogue.speaker == "나레이션")
        {
            narration.SetActive(true);
            dialogue.SetActive(false);
            narrationText.text = currentDialogue.line;
        }
        else
        {
            narration.SetActive(false);
            dialogue.SetActive(true);
            nameText.text = currentDialogue.speaker;
            descriptionText.text = currentDialogue.line;
        }
    }

    public void ActivateTalk()
    {
        this.gameObject.SetActive(true);
        isActivated = true;
    }

    void DeactivateTalk()
    {
        this.gameObject.SetActive(false);
        isActivated = false;
    }

    void CheckTalk()
    {
        //초대장 텍스트 및 애니메이션 나오는 지점
        if (currentDialogueIndex == 4)
        {
            DeactivateTalk();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                invitationText.gameObject.SetActive(true);
            }
            ActivateTalk();
        }
        /*
        // 숲 화면 전환 전 암전되고 짐싸는 지점
        else if (currentDialogueIndex == 18)
        {
            invitation.SetActive(false);
            DeactivateTalk();
            narration.SetActive(false);
            dialogue.SetActive(false);
        }
        */
        // 마지막 대사까지 출력되었을 때
        else if (currentDialogueIndex > proDialogue.Count)
        {
            DeactivateTalk();
        }
    }
}
