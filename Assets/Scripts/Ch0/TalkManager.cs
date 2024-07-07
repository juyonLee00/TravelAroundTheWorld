using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro 네임스페이스 추가

// ProDialogue 클래스 정의 (프롤로그 대사 저장)
public class ProDialogue
{
    public int day; // 일자
    public string location; // 장소
    public string speaker; // 인물
    public string line; // 대사

    public ProDialogue(int day, string location, string speaker, string line)
    {
        this.day = day;
        this.location = location;
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

    public GameObject invitation; // 초대장 화면
    public TextMeshProUGUI invitationText; // TextMeshPro UI 텍스트 요소

    public GameObject forest; // 숲 화면

    public GameObject trainStation; // 기차역 화면
    public GameObject train; // 기차 화면

    private int currentDialogueIndex = 0; // 현재 대사 인덱스
    private bool isActivated = false; // TalkManager가 활성화되었는지 여부

    void Awake()
    {
        proDialogue = new List<ProDialogue>();
        GenerateData(); // 데이터 생성 함수 호출
    }

    void Start()
    {
        // 처음에 오브젝트 비활성화
        this.gameObject.SetActive(false);
    }

    void Update()
    {
        //CheckTalk();
        if (isActivated && currentDialogueIndex == 0)
        {
            PrintProDialogue(currentDialogueIndex);
        }
        if (isActivated && Input.GetKeyDown(KeyCode.Space))
        {
            currentDialogueIndex++;
            PrintProDialogue(currentDialogueIndex);
        }
    }

    void GenerateData()
    {
        proDialogue.Add(new ProDialogue(0, "집", "솔", "새삼 오랜만이네.."));
        proDialogue.Add(new ProDialogue(0, "집", "나레이션", "초대장에는 반짝거리는 듯한 왁스로 정성스럽게 실링 되어 있었다."));
        proDialogue.Add(new ProDialogue(0, "집", "솔", "어딘가 익숙한데.."));
        proDialogue.Add(new ProDialogue(0, "집", "나레이션", "천천히 동봉된 편지를 열어보니 순간 달콤한 향기가 퍼지며 눈앞에 글자들이 나타났다."));
        proDialogue.Add(new ProDialogue(0, "집", "나레이션", "그제야 처음 동봉된 실링의 로고를 어디서 본 것인지 기억이 났다."));
        proDialogue.Add(new ProDialogue(0, "집", "나레이션", "카페에서 손님이 없을 때마다 바라본 TV에서 계속해서 광고하던 루나 익스프레스의 로고였다."));
        proDialogue.Add(new ProDialogue(0, "집", "나레이션", "돈이 있어도 갈 수 없고, 오로지 발송된 초대장을 통해서만 입장할 수 있다던 그곳."));
        proDialogue.Add(new ProDialogue(0, "집", "나레이션", "초대장에 적힌 비밀스러운 공간에 가야지만 탑승이 가능하고,"));
        proDialogue.Add(new ProDialogue(0, "집", "나레이션", "그 장소는 매번 바뀌어 아무도 탑승역이 어딘지 모른다던 미스터리한 곳이었다."));
        proDialogue.Add(new ProDialogue(0, "집", "솔", "여기는...?"));
        proDialogue.Add(new ProDialogue(0, "집", "나레이션", "가고 싶어도 갈 수 없는 사람들이 많다는 이곳."));
        proDialogue.Add(new ProDialogue(0, "집", "나레이션", "어째서 나에게 발송이 된 것인지는 전혀 알 수가 없었다."));
        proDialogue.Add(new ProDialogue(0, "집", "솔", "잠깐만… 당장 내일이잖아??"));
        proDialogue.Add(new ProDialogue(0, "집", "나레이션", "달력을 보면서 내 눈을 의심했지만, 바뀌는 것은 아무것도 없었다."));
        proDialogue.Add(new ProDialogue(0, "집", "나레이션", "처음에는 내일도 운영해야 하는 카페가 걱정이었지만,"));
        proDialogue.Add(new ProDialogue(0, "집", "나레이션", "손님도 오지 않고 특별한 일 없이 똑같이 운영되는 카페에 지쳐있던 상황에 잘 되었다는 생각이 들었다."));
        proDialogue.Add(new ProDialogue(0, "집", "나레이션", "그 생각이 들자, 바로 짐을 싸기 시작했다."));
        proDialogue.Add(new ProDialogue(0, "집", "나레이션", "주저할 시간이 없었다."));

        proDialogue.Add(new ProDialogue(1, "숲", "나레이션", "숲으로 가득 싸인 곳을 헤매며 들어간다."));
        proDialogue.Add(new ProDialogue(1, "숲", "솔", "잠시만... 길이 여기가 맞는 거야??"));
        proDialogue.Add(new ProDialogue(1, "숲", "나레이션", "처음에는 지도가 잘못된 줄 알았으나, 위치는 분명히 숲 속을 가리키고 있었다."));
        proDialogue.Add(new ProDialogue(1, "숲", "나레이션", "반신반의하며 숲을 헤치며 15분 정도 들어갔을까, 내 눈앞에 믿을 수 없는 광경이 나타났다."));
        proDialogue.Add(new ProDialogue(1, "기차역", "나레이션", "마치 보석을 칠해놓은 것과 같이 반짝이는 바다색의 기차역이 내 눈앞에 나타났다."));
        proDialogue.Add(new ProDialogue(1, "기차역", "나레이션", "역의 간판을 지탱하고 있는 나무는 장인의 손길로 정교하게 깎여 계속하게 쳐다볼 수밖에 없게 만들었다."));
        proDialogue.Add(new ProDialogue(1, "기차역", "솔", "이렇게 아름다운 곳이 어째서 이용되고 있지 않았던 거지?"));
        proDialogue.Add(new ProDialogue(1, "기차역", "나레이션", "아름다운 손길로 다듬어진 인위적인 깎임과, 오랜 세월 비워져 있어, 옆의 수풀과 함께 어우러져 있는 모습은 서로 반대댐으로써 더욱 아름다운 듯 보였다."));
        proDialogue.Add(new ProDialogue(1, "기차역", "나레이션", "오후 3시, 호루라기 소리와 함께 열차가 미끄러지듯 기차역을 빠져나왔다."));
        proDialogue.Add(new ProDialogue(1, "기차역", "나레이션", "그늘에서 벗어난 열차의 금빛 창틀과 문 손잡이가 햇살을 받아 반짝반짝 빛났다."));
        proDialogue.Add(new ProDialogue(1, "기차역", "나레이션", "짙은 푸른색 몸체에 흰 지붕을 이고 있는 열차는 잘 차려입고 페도라까지 쓴 세련된 신사 같았다."));
        proDialogue.Add(new ProDialogue(1, "기차역", "솔", "이곳에서는 나밖에 안 타는 건가??"));
        proDialogue.Add(new ProDialogue(1, "기차역", "나레이션", "기차가 도착했음에도, 나 이외 다른 승객들은 보이지 않았다."));
        proDialogue.Add(new ProDialogue(1, "기차역", "나레이션", "이윽고, 기차가 천천히 내 앞을 지나 멈췄다."));
        proDialogue.Add(new ProDialogue(1, "기차역", "나레이션", "바로 앞의 기차칸 문이 열리고, 바텐더복을 입은 한 여성이 내려 나를 마주하고는 반가운 웃음을 지었다."));
        proDialogue.Add(new ProDialogue(1, "기차역", "???", "아 마침 잘 왔네!"));
        proDialogue.Add(new ProDialogue(1, "기차역", "???", "당신이 오늘부터 이 카페를 담당할 바리스타라며??"));
        proDialogue.Add(new ProDialogue(1, "기차역", "바이올렛", "나는 바이올렛! 반가워~"));
        proDialogue.Add(new ProDialogue(1, "기차역", "솔", "네?? 저는 카페의 손님으로 초대가 된 줄 알았는데요???"));
        proDialogue.Add(new ProDialogue(1, "기차역", "바이올렛", "무슨 소리야 달링"));
        proDialogue.Add(new ProDialogue(1, "기차역", "바이올렛", "여기 버젓이 당신 이름으로 명찰도 만들어져 있는 걸?"));
        proDialogue.Add(new ProDialogue(1, "기차역", "나레이션", "골드에 음각으로 새겨진 나의 이름이 햇빛에 반사되어 빛이 났다."));
        proDialogue.Add(new ProDialogue(1, "기차역", "나레이션", "이름 아래에는 LUNAR EXPRESS BARISTA라는 나도 모르는 직업이 명시되어 있었다."));
        proDialogue.Add(new ProDialogue(1, "기차역", "바이올렛", "달링 이럴 시간이 없어! 지금 손님들이 아침부터 커피를 먹지 못해서 불만이 쏟아지고 있다구~"));
        proDialogue.Add(new ProDialogue(1, "기차역", "바이올렛", "간단한 건 내가 알려줄 테니 손님들 먼저 해결해 줘!!"));
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

        CheckTalk(currentDialogue.location);
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

    void CheckTalk(string location)
    {
        invitation.SetActive(false);
        forest.SetActive(false);
        trainStation.SetActive(false);
        train.SetActive(false);

        switch (location)
        {
            case "집":
                if (currentDialogueIndex >= 1 && currentDialogueIndex <= 16)
                {
                    invitation.SetActive(true);
                    if (currentDialogueIndex >= 1 && currentDialogueIndex <= 3)
                    {
                        invitationText.gameObject.SetActive(false);
                    }
                    else if (currentDialogueIndex >= 4)
                    {
                        invitationText.gameObject.SetActive(true);
                    }
                }
                break;
            case "숲":
                forest.SetActive(true);
                break;
            case "기차역":
                trainStation.SetActive(true);
                if (currentDialogueIndex >= 26)
                {
                    train.SetActive(true);
                }
                break;
        }
        if (currentDialogueIndex > proDialogue.Count)
        {
            DeactivateTalk();
        }
    }
}
